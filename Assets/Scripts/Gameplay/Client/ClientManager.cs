using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Yarn.Unity;
using System.Linq;

public class ClientManager : MonoBehaviour
{
    List<Transform> clientPositions = new List<Transform>();
    List<DialogueRunner> dialogueRunners = new List<DialogueRunner>();
    GameObject baseClient;
    Queue<ClientInfo> queue = new Queue<ClientInfo>();
    Client[] counterClients;


    [SerializeField]
    private float clientRespawnTime=3;
    void Start()
    {
        baseClient = Resources.Load<GameObject>("Prefabs/Client");
        foreach(Transform pos in transform)
        {
            clientPositions.Add(pos);
        }
        counterClients = new Client[clientPositions.Count];

        foreach (var dr in FindObjectsOfType<DialogueRunner>().OrderBy(x => x.gameObject.name))
        {
            dialogueRunners.Add(dr);
        }

        loadFromQueue();
    }

    private void loadFromQueue()
    { 
        foreach (GameObject go in Resources.LoadAll("Prefabs/ClientInfos/"+GameState.Instance.lvl))
        {
            var comp = go.GetComponent<ClientInfo>();
            if (comp != null)
            {
                queue.Enqueue(comp);
            }
        }
    }

    private void Update()
    {
        checkEmptySeats();
    }

    void checkEmptySeats()
    {
        for(var seat =0;seat<counterClients.Length;seat++)
        {
            if (counterClients[seat] == null)
            {
                //that mf left
                AddClient(seat);
            }
        }
    }

    void AddClient(ClientInfo cinfo, int seat)
    {
        var go = Instantiate(baseClient, clientPositions[seat]);
        var cli = go.GetComponent<Client>();
        cli.info = cinfo;
        cli.dialogueRunner = dialogueRunners[seat];
        counterClients[seat] = cli;
    }

    //Adds the new client from the queue
    void AddClient(int seat)
    {
        if (queue.Count > 0)
        {
            AddClient(queue.Dequeue(), seat);
        }
    }
}
