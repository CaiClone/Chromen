using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public float speed = 2;
    public readonly Vector3 direction = new Vector3(0, -1);
    public string ingName;
    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    public void Select()
    {
        var cmp = gameObject.transform.Find("Selected").GetComponent<SpriteRenderer>();
        if (cmp)
            cmp.enabled = true;
    }

    public void Unselect()
    {
        var cmp = gameObject.transform.Find("Selected").GetComponent<SpriteRenderer>();
        if (cmp)
            cmp.enabled = false;
    }
}
