using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientInfo_wrongClient : ClientInfo
{
    [SerializeField]
    List<string> fakeOrder;
    public override void OnStart()
    {
        dialogueRunner.AddCommandHandler("order", askOrder);
    }
    private void askOrder(string[] parameters)
    {
        client.Order(fakeOrder);
    }
    public override void RemoveCommands()
    {
        base.RemoveCommands();
        dialogueRunner.RemoveCommandHandler("order");
    }
    protected override bool annoyed()
    {
        dialogueRunner.StartDialogue("OnFail");
        AudioController.Instance.Play("wrongorder");
        client.gameObject.tag = "Untagged";
        return true;
    }

    protected override bool satisifed()
    {
        dialogueRunner.StartDialogue("OnSuccess");
        client.gameObject.tag = "Untagged";
        return true;
    }
}
