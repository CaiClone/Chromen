
using UnityEngine;

public class IngredientEater : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Ingredient") || col.gameObject.CompareTag("Segment"))
        {
            Destroy(col.gameObject);
        }
    }
}
