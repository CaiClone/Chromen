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
    private DialogueRunner dialogueRunner;

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
        Utils.WaitAndRun(timeToOrder, () => client.Order(order));
    }

    private void Talk()
    {
        dialogueRunner.StartDialogue(Dialogue.name);
    }
}
