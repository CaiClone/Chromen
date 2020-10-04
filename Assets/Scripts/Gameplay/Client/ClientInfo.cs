using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Yarn.Unity;

public class ClientInfo : MonoBehaviour
{
    public List<string> curr_order;
    public bool flipped = false;
    public float timeToOrder = 1f;
    public bool talkAfterOrdering = true;

    public YarnProgram Dialogue;

    protected Client client;
    protected DialogueRunner dialogueRunner;

    public virtual void OnStart()
    {
        if (talkAfterOrdering)
        {
            Utils.WaitAndRun(2.5f, () => Talk());
        }
        Utils.WaitAndRun(timeToOrder, () => client.Order(curr_order));
    }

    public void AddDefaultCommands(Client client, DialogueRunner dialogueRunner)
    {
        this.client = client;
        this.dialogueRunner = dialogueRunner;
        if (dialogueRunner != null)
        {
            dialogueRunner.AddCommandHandler(
                "flashcolor",
                flashColor
            );
            dialogueRunner.Add(Dialogue);
        }
    }
    protected void Talk()
    {
        dialogueRunner.StartDialogue(Dialogue.name);
    }

    protected void SetOrder(List<string> order)
    {
        curr_order = order;
    }
    
    private void flashColor(string[] parameters)
    {
        if (parameters.Length > 1)
            flashColor(parameters[0], float.Parse(parameters[1]));
        else
            flashColor(parameters[0]);
    }
    private void flashColor(string colorName,float speed = 3f)
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
        for(float i = 0; i <= 1; i += Time.deltaTime * speed)
        {
            client.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, color, i);
            yield return null;
        }
        for (float i = 0; i <= 1; i += Time.deltaTime * speed)
        {
            client.GetComponent<SpriteRenderer>().color = Color.Lerp(color, Color.white, i);
            yield return null;
        }
    }
}
