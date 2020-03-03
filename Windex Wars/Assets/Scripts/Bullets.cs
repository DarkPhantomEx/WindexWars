﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public float speed = 8f;
    public float lifeDuration = 2f;
    private float lifeTimer;

    
    void Start()
    {
        lifeTimer = lifeDuration;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += transform.forward * speed * Time.deltaTime;

        lifeTimer -= Time.deltaTime;
        if (lifeTimer<=0f)
        {
            Destroy(this.gameObject);
        }
    }
}
