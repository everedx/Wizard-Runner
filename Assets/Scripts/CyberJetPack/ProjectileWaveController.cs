﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWaveController : MonoBehaviour
{
    [SerializeField] float MoveSpeed = 10.0f;

    public float frequency = 20.0f;  // Speed of sine movement
    public float magnitude = 0.5f;   // Size of sine movement
    private Vector3 axis;

    private Vector3 pos;
    private Vector3 leftVector;
    private float sinFactor;
    private float newRotation;
    Vector3 dir;
    void Awake()
    {
        pos = transform.position;
        axis = transform.up;  // May or may not be the axis you want
        leftVector = -transform.right;
    }

    void Update()
    { 
        pos += leftVector * Time.deltaTime * MoveSpeed;
        sinFactor = Mathf.Sin(Time.time * frequency);
        transform.position = pos + axis * sinFactor * magnitude;
        //transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + 10f);
       
    }


}
