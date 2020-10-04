using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class ClientInfo : MonoBehaviour
{
    public List<string> curr_order;
    public bool flipped = false;
    public float timeToOrder = 1f;
    public bool talkAfterOrdering = true;
    public Sprite sprite;
    public float timeToArrive = 4f;
    public float timeToLeave = 2f;

    public YarnProgram Dialogue;

    protected Client client;
    protected DialogueRunner dialogueRunner;

    public virtual void OnStart()
    {
        if(curr_order!= null && curr_order.Count > 0)
        {
            Utils.WaitAndRun(timeToArrive+timeToOrder, () => client.Order(curr_order));
        }
        Utils.WaitAndRun(timeToArrive+ timeToOrder+timeToLeave, () => annoyed());
    }

    public void AddDefaultCommands(Client client, DialogueRunner dialogueRunner)
    {
        this.client = client;
        this.dialogueRunner = dialogueRunner;
        if (dialogueRunner != null)
        {
            dialogueRunner.Stop();
            dialogueRunner.Clear();
            dialogueRunner.AddCommandHandler("flashcolor",flashColor);
            dialogueRunner.AddCommandHandler("loadstory",loadstory);
            dialogueRunner.AddCommandHandler("leave",leave);
            dialogueRunner.AddCommandHandler("startline", startline);
            dialogueRunner.AddCommandHandler("setcolor", setcolor);
            dialogueRunner.AddCommandHandler("setsprite", setsprite);
            dialogueRunner.AddCommandHandler("blackscreen", blackscreen);
            dialogueRunner.AddCommandHandler("playsound", playsound);
            if (Dialogue != null)
            {
                dialogueRunner.Add(Dialogue);
            }
        }
        client.StartCoroutine(co_fadeIn(2f));
    }
    protected void Talk()
    {
        if (client.gameObject == null)
        {
            return;
        }
        dialogueRunner.StartDialogue(Dialogue.name);
    }

    protected void SetOrder(List<string> order)
    {
        curr_order = order;
    }

    private void startline(string[] parameters)
    {
        FindObjectOfType<TrayManager>().enabled = true;
    }
    private void flashColor(string[] parameters)
    {
        if (parameters.Length > 1)
            flashColor(parameters[0], float.Parse(parameters[1]));
        else
            flashColor(parameters[0]);
    }
    private void loadstory(string[] parameters)
    {
        GameState.Instance.lvl = parameters[0];
        SceneManager.LoadScene("Story");
    }
    private void leave(string[] parameters)
    {
        if (parameters==null || parameters.Length == 0)
            leave();
        else
            leave(float.Parse(parameters[0]));

    }
    private void blackscreen(string[] parameters)
    {
        var ppvolume = FindObjectOfType<PostProcessVolume>();
        Vignette vig;
        ppvolume.profile.TryGetSettings(out vig);
        if (parameters[0] == "on")
        {
            client.StartCoroutine(fadeInScreen(vig,1f));
        }
        else if(parameters[0]=="off")
        {
            client.StartCoroutine(fadeOutScreen(vig));
        }
    }

    private IEnumerator fadeInScreen(Vignette vig, float speed = 0.5f)
    {
        for (float i = vig.intensity.value; i <= 1f; i += Time.deltaTime * speed)
        {
            vig.intensity.value = i;
            yield return null;
        }
    }
    private IEnumerator fadeOutScreen(Vignette vig, float speed = 0.5f)
    {
        for (float i = 1; i > 0.127; i -= Time.deltaTime * speed)
        {
            vig.intensity.value = i;
            yield return null;
        }
    }
    private void setcolor(string[] parameters)
    {
        ///ahhhhh should reutilize the one I already had, but no time
        Color color;
        switch (parameters[0]) {
            case "me":
                color = new Color(1f, 0.84f, 0.66f);
                break;
            default:
                color = Color.white;
                break;
        }
        foreach(var txt in FindObjectsOfType<TextMeshProUGUI>())
        {
            txt.color = color;
        }
    }
    private void setsprite(string[] parameters)
    {
        sprite = Resources.Load<Sprite>("Sprites/"+parameters[0]);
        var sr = client.gameObject.GetComponent<SpriteRenderer>();
        sr.sprite = sprite;
        sr.enabled = true;
    }
    private void playsound(string[] parameters)
    {
        switch (parameters[0])
        {
            case "THUNK":
                //TODO
                break;
        }
    }
    private void leave(float speed = 0.5f)
    {
        client.StartCoroutine(co_fadeOut(speed));
    }
    private void flashColor(string colorName,float speed = 4f)
    {
        Color color;
        switch (colorName.ToLower())
        {
            case "red":
                color = new Color(1f, 0.2f, 0.2f);
                break;
            case "green":
                color = new Color(0.2f, 1f, 0.2f);
                break;
            case "purple":
                color = new Color(0.8f,0.2f, 0.8f);
                break;
            default:
                color = new Color(1f, 1f, 1f);
                break;
        }
        client.StartCoroutine(co_flash(color, speed));
    }
    //Return if the order should be cancelled
    public bool Served(List<Ingredient> servedIngredients)
    {
        bool correct = servedIngredients.Select((x) => x.ingName).OrderBy(x => x).SequenceEqual(curr_order.OrderBy(x => x));
        if (correct)
        {
            return satisifed();
        }
        else
        {
            return annoyed();
        }
    }

    protected virtual bool satisifed()
    {
        flashColor("Green");
        client.gameObject.tag = "Untagged";
        Utils.WaitAndRun(2f, () => leave());
        return true;
    }
    protected virtual bool annoyed()
    {
        flashColor("Red");
        client.gameObject.tag = "Untagged";
        Utils.WaitAndRun(2f, () => leave(1.5f));
        return true;
    }

    private IEnumerator co_flash(Color color,float speed)
    {
        var sr = client.GetComponent<SpriteRenderer>();
        for (float i = 0; i <= 1; i += Time.deltaTime * speed)
        {
            sr.color = Color.Lerp(Color.white, color, i);
            yield return null;
        }
        for (float i = 0; i <= 1; i += Time.deltaTime * speed)
        {
            sr.color = Color.Lerp(color, Color.white, i);
            yield return null;
        }
    }
    private IEnumerator co_fadeOut(float speed)
    {
        var sr = client.GetComponent<SpriteRenderer>();
        for (float i = 1; i >= 0; i -= Time.deltaTime * speed)
        {
            sr.color = new Color(1, 1, 1, i);
            yield return null;
        }
        Destroy(client.gameObject);
    }
    private IEnumerator co_fadeIn(float speed)
    {
        yield return new WaitForSeconds(timeToArrive);
        var sr = client.GetComponent<SpriteRenderer>();
        sr.enabled = true;
        for (float i = 0; i < 1; i += Time.deltaTime * speed)
        {
            sr.color = new Color(i, i, i, i);
            yield return null;
        }

        if (talkAfterOrdering && Dialogue!=null)
        {
            Utils.WaitAndRun(2.5f, () => Talk());
        }
    }
    ///Should automatize this
    public virtual void RemoveCommands()
    {
        dialogueRunner.RemoveCommandHandler("flashcolor");
        dialogueRunner.RemoveCommandHandler("loadstory");
        dialogueRunner.RemoveCommandHandler("leave");
        dialogueRunner.RemoveCommandHandler("startline");
        dialogueRunner.RemoveCommandHandler("setcolor");
        dialogueRunner.RemoveCommandHandler("blackscreen");
        dialogueRunner.RemoveCommandHandler("setsprite");
        dialogueRunner.RemoveCommandHandler("playsound");
    }
}
