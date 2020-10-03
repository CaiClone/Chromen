using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Client : MonoBehaviour
{
    public ClientInfo info;

    //establishes positions in recipe for different number of ingredients in {numIngredient, {x,y,ScaleX,ScaleY}}
    Dictionary<int, float[]> pinfo = new Dictionary<int, float[]>()
    {
        {2, new float[] {-0.023f,0.01f,0.38f,0.58f,0.023f,0.01f, 0.38f, 0.58f } },
        {3, new float[] {1,2,3,4,1,2,3,4 } }
    };
    public void Start()
    {
        Utils.WaitAndRun(0.1f, () => order());
    }
    void order()
    {
        var ordGo = transform.Find("Order");

        var cinfo = pinfo[info.order.Count];
        for (var i=0; i<info.order.Count;i++)
        {
            var sample = Resources.Load("Prefabs/IngredientThumb/" + info.order[i]);
            if (sample)
            {
                var go = (GameObject)Instantiate(sample, ordGo);
                go.transform.localPosition = new Vector2(cinfo[i * 4], cinfo[i * 4 + 1]);
                go.transform.localScale = new Vector2(cinfo[i * 4 + 2], cinfo[i * 4 + 3]);
            }
        }
        ordGo.gameObject.SetActive(true);
    }
}

[System.Serializable]
public class ClientInfo
{
    public List<string> order;
    public ClientInfo(List<string> order)
    {
        this.order = order;
    }
}