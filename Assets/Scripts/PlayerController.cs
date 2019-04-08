using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Slider sliderBar;
    private Rigidbody2D rb;
    private int moveSpeed = 5;
    private float specialDuration = 5;
    private Animator anim;
    private Vector2 moveXY;
    private SpriteRenderer sprite;
    public ParticleSystem trail;
    private CameraShake cameraShake;
    public int health;

    // Start is called before the first frame update
    void Start()
    {
        sliderBar = FindObjectOfType<Slider>();
        cameraShake = FindObjectOfType<CameraShake>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sliderBar.maxValue = specialDuration;
    }

    private void Update()
    {

        sliderBar.value += Time.deltaTime * .5f;
        moveXY = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        if (moveXY != Vector2.zero)
        {
            rb.velocity = new Vector2(moveXY.x * moveSpeed, moveXY.y * moveSpeed);
            anim.SetBool("Moving", true);
            trail.gameObject.SetActive(true);
        }
        else
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("Moving", false);
            trail.gameObject.SetActive(false);
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

        sliderBar.value = specialDuration;
            
    }

    public void GhostMode()
    {
        specialDuration-= 1 *Time.deltaTime;
    }

    public void DealDamage(int damage)
    {
        health -= damage;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == ("Enemy"))
        {
            ContactPoint2D contact = other.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;
            //Instantiate(damagePrefab, pos, rot);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Item"){
            health += 1;
            Destroy(other.gameObject);
        }
    }
}
