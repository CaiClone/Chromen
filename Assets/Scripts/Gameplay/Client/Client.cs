using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using Yarn.Unity;

public class Client : MonoBehaviour
{
    public ClientInfo info;
    public DialogueRunner dialogueRunner;

    //establishes positions in recipe for different number of ingredients in {numIngredient, {x,y,ScaleX,ScaleY}}
    Dictionary<int, float[]> pinfo = new Dictionary<int, float[]>()
    {
        {1, new float[] {0f,0.1f,6f,6f} },
        {2, new float[] {-0.48f,0.1f,5f,5f,0.46f,0.1f, 5f, 5f } },
        {3, new float[] {1,2,3,4,1,2,3,4 } }
    };

    public void Start()
    {
        if (info.flipped)
        {
            var sprite = GetComponent<SpriteRenderer>();
            sprite.flipX = true;
        }
        info.OnStart(this,dialogueRunner);
    }
    public void Order(List<string> order)
    {
        var ordGo = transform.Find("Order");

        if (!pinfo.ContainsKey(order.Count))
        {
            Debug.LogError("No specified thumbnail distribution for a order of size "+order.Count.ToString());
            return;
        }
        var cinfo = pinfo[order.Count];
        for (var i=0; i<order.Count;i++)
        {
            var sample = Resources.Load("Prefabs/IngredientThumb/" + order[i]);
            if (sample)
            {
                var go = (GameObject)Instantiate(sample, ordGo);
                go.transform.localPosition = new Vector2(cinfo[i * 4], cinfo[i * 4 + 1]);
                go.transform.localScale = new Vector2(cinfo[i * 4 + 2], cinfo[i * 4 + 3]);
            }
        }
        ordGo.gameObject.SetActive(true);
    }

    public void Serve(List<Ingredient> selectedIngredients)
    {
        bool toCancel = info.Served(selectedIngredients);
        if (toCancel)
        {
            cancelOrder();
        }
    }
    private void cancelOrder()
    {
        var ordGo = transform.Find("Order");
        foreach(Transform o in ordGo.transform)
        {
            Destroy(o.gameObject);
        }
        ordGo.gameObject.SetActive(false);
    }
}

