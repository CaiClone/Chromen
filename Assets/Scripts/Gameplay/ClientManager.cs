using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClientManager : MonoBehaviour
{
    List<Transform> clientPositions = new List<Transform>();
    GameObject baseClient;
    void Start()
    {
        baseClient = Resources.Load<GameObject>("Prefabs/Client");
        foreach(Transform pos in transform)
        {
            clientPositions.Add(pos);
        }

        foreach(var pos in clientPositions)
        {
            Instantiate(baseClient, pos);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
