using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    float speed = 2;
    public readonly Vector3 direction = new Vector3(0, -1);
    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    public void Select()
    {
        gameObject.transform.Find("Selected").GetComponent<SpriteRenderer>().enabled = true;
    }

    public void Unselect()
    {
        gameObject.transform.Find("Selected").GetComponent<SpriteRenderer>().enabled = false;
    }
}
