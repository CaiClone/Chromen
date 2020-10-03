using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayManager : MonoBehaviour
{
    [SerializeField]
    private List<TrayInfo> trays;
    [SerializeField]
    private bool spawning = true;

    private GameObject traySegment;

    private void OnEnable()
    {
        traySegment = Resources.Load<GameObject>("Prefabs/TraySegment");

        foreach (var t in GetComponentsInChildren<TrayInfo>())
        {
            StartCoroutine(sendIngredient(t));
        }
        foreach (var t in GetComponentsInChildren<TrayInfo>())
        {
            StartCoroutine(SpawnSegment(t));
        }
    }

    IEnumerator sendIngredient(TrayInfo t)
    {
        while (spawning)
        {
            yield return new WaitForSeconds(t.restockTime);
            var o = t.getRandIngredient();
            if (o != null)
            {
                GameObject nobj = Instantiate(o,t.transform);
                var ing = nobj.GetComponent<Ingredient>();
                ing.ingName = o.name;
                ing.speed = t.trayspeed;
                Destroy(nobj, 30);
            }
        }
    }
    IEnumerator SpawnSegment(TrayInfo t)
    {
        while (t.trayspeed >= 0)
        {
            yield return new WaitForSeconds(0.6f/t.trayspeed);
            GameObject nobj = Instantiate(traySegment, t.transform);
            Destroy(nobj, 30);
        }
    }
}