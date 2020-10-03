using UnityEngine;
using Yarn.Unity;

public class ClientInfo_Tutorial : ClientInfo
{
    public override void OnStart(Client client,DialogueRunner dialogueRunner)
    {
        base.OnStart(client, dialogueRunner);
        Debug.Log("Test");
    }
}
