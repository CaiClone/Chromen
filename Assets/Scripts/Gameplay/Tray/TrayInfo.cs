using UnityEngine;
using System.Collections.Generic;
public class TrayInfo : MonoBehaviour
{
    private List<GameObject> Ingredients = new List<GameObject>();
    public float restockTime;
    public float trayspeed;
    private System.Random rand;

    public void Start()
    {
        rand = new System.Random(GameState.Instance.lvlseed+(int)transform.position.x);
    }
    public GameObject getRandIngredient()
    {
        if (Ingredients.Count == 0)
        {
            return null;
        }
        return Ingredients[rand.Next(0, Ingredients.Count)];
    }
    public void AddIngredient(string ingredient)
    {
        GameObject go = Resources.Load<GameObject>("Prefabs/Ingredients/" + ingredient);
        //can add nulls!
        Ingredients.Add(go);
    }
    public void setParams(float restockTime, float trayspeed)
    {
        this.restockTime = restockTime;
        this.trayspeed = trayspeed;
        foreach (Transform t in transform)
        {
            t.SendMessage("RefreshSpeed");
        }
    }
}
