using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrayInfo : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Ingredients;
    public float time = 2;

    public GameObject getRandIngredient()
    {
        if (Ingredients.Count == 0)
        {
            return null;
        }
        return Ingredients[Random.Range(0, Ingredients.Count)];
    }
}
