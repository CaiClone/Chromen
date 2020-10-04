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
        loadIngredients();

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
    private void loadIngredients()
    {
        //I'm sorry, I'm truly sorry
        switch (GameState.Instance.lvl)
        {
            case "level1":
                trays[0].AddIngredient("IngredientA");
                trays[0].AddIngredient("IngredientB");
                trays[0].setParams(1.3f, 1.2f);
                trays[1].AddIngredient("IngredientC");
                trays[1].setParams(2f, 1f);
                trays[2].AddIngredient("IngredientD");
                trays[2].AddIngredient("None");
                trays[2].setParams(1f, 1f);
                trays[3].setParams(1.3f, 0f);
                break;
            case "level2":
                trays[0].AddIngredient("IngredientA");
                trays[0].AddIngredient("IngredientB");
                trays[0].setParams(1.3f, 1.2f);
                trays[1].AddIngredient("IngredientE");
                trays[1].AddIngredient("IngredientB");
                trays[2].AddIngredient("IngredientA");
                trays[1].setParams(2f, 1f);
                trays[2].AddIngredient("IngredientF");
                trays[2].AddIngredient("IngredientC");
                trays[2].setParams(1f, 1f);
                trays[3].AddIngredient("IngredientD");
                trays[3].AddIngredient("None");
                trays[3].setParams(1.3f, 3f);
                break;
        }
    }
}