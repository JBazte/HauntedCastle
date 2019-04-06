using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Slider slider;
    private Rigidbody2D rb;
    private float vx = 0;
    private float vy = 0;
    public int moveSpeed = 10;
    public float specialDuration = 100;
    private Animator anim;
    private Vector2 moveXY;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        slider.maxValue = specialDuration;
    }

    private void Update()
    {
        moveXY = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        if (moveXY != Vector2.zero)
        {
            rb.velocity = new Vector2(moveXY.x * moveSpeed, moveXY.y * moveSpeed);
            anim.SetBool("Moving", true);
        }
        else
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("Moving", false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }

        if (Input.GetButton("Jump"))
        {
            GhostMode();
        }
        
        slider.value = specialDuration;
    }

    void GhostMode()
    {
        specialDuration-= 1 *Time.deltaTime;
    }
   
}
