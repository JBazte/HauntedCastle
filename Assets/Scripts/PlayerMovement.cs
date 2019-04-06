using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Slider slider;
    Rigidbody2D rb;
    float vx = 0;
    float vy = 0;
    public int speed = 10;
    public float specialDuration = 100;
   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        slider.maxValue = specialDuration;
    }

    private void Update()
    {
        if (Input.GetButton("Jump"))
        {
            GhostMode();
        }

        slider.value = specialDuration;
    }

    private void FixedUpdate()
    {
        vx = Input.GetAxis("Horizontal");
        vy = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(vx, vy) * speed;      
   
    }


    void GhostMode()
    {
        specialDuration-= 1 *Time.deltaTime;
    }
   
}
