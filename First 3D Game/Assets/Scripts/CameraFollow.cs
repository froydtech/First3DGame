﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;
    
    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;           
        }

        Vector3 newPos = target.position + offset;
        //newPos.y = offset.y;
        transform.position = newPos;

    }
}
