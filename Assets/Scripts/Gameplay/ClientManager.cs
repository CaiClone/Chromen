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
        cinfo = new ClientInfo(new List<string> { "IngredientC", "IngredientD" });
        queue.Add(cinfo);

        AddClient(queue[0], clientPositions[0]);
        AddClient(queue[1], clientPositions[1]);
    }

    void AddClient(ClientInfo cinfo, Transform pos)
    {
        var go = Instantiate(baseClient, pos);
        go.GetComponent<Client>().info = cinfo;
    }
}
