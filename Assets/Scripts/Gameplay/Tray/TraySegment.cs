using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraySegment : MonoBehaviour
{
    private float speed;
    public readonly Vector3 direction = new Vector3(0, -1);
    void Start()
    {
        TrayInfo comp = transform.parent.GetComponent<TrayInfo>();
        if (comp != null)
        {
            speed = comp.trayspeed;
        }
        else { speed = 0; }

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
