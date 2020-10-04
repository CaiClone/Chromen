using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraySegment : MonoBehaviour
{
    private float speed=0;
    public readonly Vector3 direction = new Vector3(0, -1);
    void Start()
    {
        RefreshSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    public void RefreshSpeed()
    {
        TrayInfo comp = transform.parent.GetComponent<TrayInfo>();
        if (comp != null)
        {
            speed = comp.trayspeed;
        }
        else { speed = 0; }
    }
}
