using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientInfo_nopumpkin: ClientInfo
{
    public override void OnStart()
    {
        dialogueRunner.AddCommandHandler("order", askOrder);
    }
    private void askOrder(string[] parameters)
    {
        client.Order(curr_order);
    }
    public override void RemoveCommands()
    {
        base.RemoveCommands();
        dialogueRunner.RemoveCommandHandler("order");
    }
    protected override bool annoyed()
    {
        dialogueRunner.StartDialogue("OnFail");
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
