using UnityEngine;

public class ClientInfo_Tutorial : ClientInfo
{
    public override void OnStart(Client client)
    {
        base.OnStart(client);
        Debug.Log("Test");
    }
}
