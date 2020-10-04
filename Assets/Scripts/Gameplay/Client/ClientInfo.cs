using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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

    public YarnProgram Dialogue;

    protected Client client;
    protected DialogueRunner dialogueRunner;

    public virtual void OnStart()
    {
        if(curr_order!= null && curr_order.Count > 0)
        {
            Utils.WaitAndRun(timeToOrder, () => client.Order(curr_order));
        }
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
            satisifed();
            return true;
        }
        else
        {
            annoyed();
            return false;
        }
    }

    protected virtual void satisifed()
    {
        flashColor("Green");
        Destroy(gameObject, 1.5f);
    }
    protected virtual void annoyed()
    {
        flashColor("Red");
        Destroy(gameObject, 1.5f);
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
            sr.color = new Color(1, 1, 1, i);
            yield return null;
        }

        if (talkAfterOrdering)
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
    }
}
