﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientInfo : MonoBehaviour
{
    public List<string> order;
    public bool flipped = false;

    public virtual void OnStart() { }
}