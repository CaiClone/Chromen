using UnityEngine;
using System.Collections.Generic;
public class TrayInfo : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Ingredients;
    public float restockTime = 2;
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
}
