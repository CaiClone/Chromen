using UnityEngine;
using Yarn.Unity;

public class ClientInfo_Tutorial : ClientInfo
{
    public override void OnStart(Client client,DialogueRunner dialogueRunner)
    {
        this.client = client;
        this.dialogueRunner = dialogueRunner;
        Debug.Log("Test");
    }
}
