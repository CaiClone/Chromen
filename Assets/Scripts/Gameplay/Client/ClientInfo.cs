using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class ClientInfo : MonoBehaviour
{
    public List<string> order;
    public bool flipped = false;
    public float timeToOrder = 1f;
    public bool talkAfterOrdering = true;

    public YarnProgram Dialogue;

    private Client client;

    public virtual void OnStart(Client client) {
        this.client = client;

        if (Dialogue != null)
        {
            DialogueRunner dialogueRunner = FindObjectOfType<DialogueRunner>();
            dialogueRunner.Add(Dialogue);
            if (talkAfterOrdering)
            {
                Utils.WaitAndRun(2.5f, () => Talk(dialogueRunner));
            }
        }
        Utils.WaitAndRun(timeToOrder, () => client.Order(order));
    }

    private void Talk(DialogueRunner dr)
    {
        dr.StartDialogue(Dialogue.name);
    }
}
