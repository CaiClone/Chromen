using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClientManager : MonoBehaviour
{
    List<Transform> clientPositions = new List<Transform>();
    GameObject baseClient;
    List<ClientInfo> queue = new List<ClientInfo>();

    void Start()
    {
        baseClient = Resources.Load<GameObject>("Prefabs/Client");
        foreach(Transform pos in transform)
        {
            clientPositions.Add(pos);
        }


        //TEMP
        var cinfo = new ClientInfo(new List<string> { "IngredientA", "IngredientB" });
        queue.Add(cinfo);

        AddClient(cinfo, clientPositions[0]);
    }

    void AddClient(ClientInfo cinfo, Transform pos)
    {
        var go = Instantiate(baseClient, pos);
        go.GetComponent<Client>().info = cinfo;
    }
}
