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

    public virtual void OnStart(Client client, DialogueRunner dialogueRunner) {
        this.client = client;
        this.dialogueRunner = dialogueRunner;

        if (Dialogue != null)
        {
            dialogueRunner.Add(Dialogue);
            if (talkAfterOrdering)
            {
                Utils.WaitAndRun(2.5f, () => Talk());
            }
        }
        Utils.WaitAndRun(timeToOrder, () => client.Order(curr_order));
    }

    protected void Talk()
    {
        dialogueRunner.StartDialogue(Dialogue.name);
    }

    protected void SetOrder(List<string> order)
    {
        curr_order = order;
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
        GetComponent<SpriteRenderer>().color = new Color(0.2f, 1, 0.2f);
        Destroy(gameObject, 1.5f);
    }
    protected virtual void annoyed()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 0.2f, 0.2f);
        Destroy(gameObject, 1.5f);
    }
}
