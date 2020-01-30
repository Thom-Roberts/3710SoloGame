﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    public float cameraDampenSpeed = 0.05f;
    private Vector3 offset;


    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - playerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate() {
        transform.position = Vector3.Lerp(transform.position, playerTransform.position + offset, cameraDampenSpeed);
    }
}