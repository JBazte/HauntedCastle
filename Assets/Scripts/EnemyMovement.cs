﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int vx = 0;
    
    
    public float speed = 3f;
    Rigidbody2D rb;
    public bool hasChangedVel = false;
    private PlayerController player;
    private GameObject playerGO;
    [SerializeField] int damage;


    public float smoothTime = 1f;
    private Vector3 smoothVelocity = Vector3.zero;

    [SerializeField] private int Health;


    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        vx = 1;
        playerGO = FindObjectOfType <PlayerController>().gameObject;
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (Health <= 0)
        {
           Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        
        if (Vector3.Distance(transform.position, playerGO.transform.position) > 10 ) {
            rb.velocity = new Vector2(vx, 0) * speed;
        } else
        {
            transform.position = Vector3.SmoothDamp(transform.position, playerGO.transform.position, ref smoothVelocity, smoothTime);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player" ) {
            player.DealDamage(damage);
        }

        if (collision.gameObject.tag == "Test" && !hasChangedVel) { 
        vx = -vx;
        
            transform.localScale = new Vector3(-transform.localScale.x,1,1);
        }

        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Health -= 10;
            Destroy(other.gameObject);
        }
    }
}
