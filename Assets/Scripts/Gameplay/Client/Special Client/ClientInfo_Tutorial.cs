using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class ClientInfo_Tutorial : ClientInfo
{
    //Can't display an array of lists on unity editor
    //at least not directly
    public List<string> order1;
    public List<string> order2;

    private int state;
    //0 - talking
    //1 - waiting order 1
    //2 - waiting order 2
    public override void OnStart()
    {
        dialogueRunner.AddCommandHandler(
            "order",
            askOrder
        );
        Utils.WaitAndRun(2.5f, () => Talk());
        state = 0;
    }

    private void askOrder(string[] parameters)
    {
        switch (parameters[0]){
            case "1":
                SetOrder(order1);
                client.Order(curr_order);
                state++;
                break;
            case "2":
                SetOrder(order2);
                client.Order(curr_order);
                state++;
                break;
        }
    }
    protected override void satisifed()
    {
        switch (state)
        {
            case 0:
                dialogueRunner.StartDialogue("NotYet");
                break;
            case 1:
                dialogueRunner.StartDialogue("OnSuccess");
                break;

        }
    }
    protected override void annoyed()
    {
        switch (state)
        {
            case 0:
                dialogueRunner.StartDialogue("NotYet");
                break;
            case 1:
                dialogueRunner.StartDialogue("OnFail");
                break;
        }
    }
}
