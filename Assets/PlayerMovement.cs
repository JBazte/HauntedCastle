using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    float vx = 0;
    public float jumpForce = 0;
    public int speed = 10;
    public float specialDuration = 100;
    [SerializeField] bool isGrounded;
   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            GhostMode();
        }
    }

    private void FixedUpdate()
    {
        vx = Input.GetAxis("Horizontal");


        rb.velocity = new Vector2(vx, 0) * speed;


        if (Input.GetAxis("Vertical") > 0 && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce);
        }

        
    }

    void GhostMode()
    {


        specialDuration -= 1 * Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isGrounded = false;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isGrounded = true;
        }
    }
}
