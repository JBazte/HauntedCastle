using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int vx = 0;
    int vy = 0;
    public int speed = 20;
    Rigidbody2D rb;
    public bool hasChangedVel = false;
    private PlayerController player;
    [SerializeField] int damage;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        vx = 1;
        player = FindObjectOfType<PlayerController>();
    }

    

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(vx, vy)* speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.name == "Player" ) {
            player.DealDamage(damage);
        }
        if (collision.gameObject.tag == "Test" && !hasChangedVel) { 
        vx = -vx;
        vy = -vy;
            transform.localScale = new Vector3(-transform.localScale.x,1,1);
        }
    }
}
