using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayManager : MonoBehaviour
{
    [SerializeField]
    private List<TrayInfo> trays;
    [SerializeField]
    private bool spawning = true;

    private void OnEnable()
    {
        
        foreach (var t in GetComponentsInChildren<TrayInfo>())
        {
            StartCoroutine(sendIngredient(t));
        }
    }

    IEnumerator sendIngredient(TrayInfo t)
    {
        while (spawning)
        {
            yield return new WaitForSeconds(t.time);
            var o = t.getRandIngredient();
            if (o != null)
            {
                GameObject nobj = Instantiate(o,t.transform);
                nobj.GetComponent<Ingredient>().name = o.name;
                Destroy(nobj, 10);
            }
        }
    }
}