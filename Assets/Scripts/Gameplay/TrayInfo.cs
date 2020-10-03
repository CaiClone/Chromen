using UnityEngine;
using System.Collections.Generic;
public class TrayInfo : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Ingredients;
    public float time = 2;
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
