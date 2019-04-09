using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Slider sliderBar;
    private Rigidbody2D rb;
    private int moveSpeed = 5;
    public float specialDuration = 5;
    private Animator anim;
    private Vector2 moveXY;
    private SpriteRenderer sprite;
    public ParticleSystem trail;
    private CameraShake cameraShake;
    private EffectsManager sound;
    private SpriteRenderer playerRenderer;
    private HealthManager thePlayerHealth;
    private bool isReady;
    private GameObject colorBar;
    private Image spriteColorBar;
    public GameObject HUD;
    private bool isActive;
    public bool paused;
    private bool effectActive;

    // Start is called before the first frame update
    void Start()
    {
        colorBar = GameObject.FindGameObjectWithTag("Fill");
        spriteColorBar = colorBar.GetComponent<Image>();
        thePlayerHealth = FindObjectOfType<HealthManager>();
        playerRenderer = GetComponent<SpriteRenderer>();
        sound = FindObjectOfType<EffectsManager>();
        sliderBar = FindObjectOfType<Slider>();
        cameraShake = FindObjectOfType<CameraShake>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sliderBar.maxValue = specialDuration;
        isReady = true;
        isActive = false;
        paused = false;
        effectActive = false;
    }

    private void Update()
    {
        specialDuration += Time.deltaTime * .5f;
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

        if(specialDuration > 5f){
            specialDuration = 5f;
        }

        if(specialDuration == 5f && isReady == false){
            isReady = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }

        if(!paused){
            HUD.gameObject.SetActive(false);
            isActive = false;
            Time.timeScale = 1f;
        }

        if (Input.GetKeyDown(KeyCode.P) && isActive == false)
        {
            HUD.gameObject.SetActive(true);
            isActive = true;
            paused = true;
            Time.timeScale = 0f;
        }
        else if (Input.GetKeyDown(KeyCode.P) && isActive == true)
        {
            HUD.gameObject.SetActive(false);
            isActive = false;
            paused = false;
            Time.timeScale = 1f;
        }

        if(isReady == false){
            spriteColorBar.color = Color.red;
        }
        else
        {
            spriteColorBar.color = Color.green;
        }

        if(Input.GetButtonDown("Jump") && isReady && effectActive == false)
        {
            StartCoroutine(waitForSound());
        }

        if (Input.GetButton("Jump") && isReady)
        {
            GhostMode();
        }
        else if (Input.GetButtonUp("Jump"))
        {
            playerRenderer.color = new Color(255f, 255f, 255f, 255f);
            this.gameObject.tag = "Player";
        }

        sliderBar.value = specialDuration;
        
    }

    public void GhostMode()
    {
        if(specialDuration > 0f){
            specialDuration -= Time.deltaTime * 4f;
            playerRenderer.color = new Color(0f, 0f, 0f, 255f);
            this.gameObject.tag = "Ghost";
        }
        else
        {
            isReady = false;
            playerRenderer.color = new Color(255f, 255f, 255f, 255f);
        }
    }

    public void DealDamage(int damage)
    {
        thePlayerHealth.playerHelath -= damage;
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

    IEnumerator waitForSound()
    {
        sound.SpecialAbility.Play();
        while (sound.SpecialAbility.isPlaying)
        {
            effectActive = true;
            yield return null;
        }
        effectActive = false;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Item"){
            thePlayerHealth.playerHelath += 1;
            sound.HelathUp.Play();
            Destroy(other.gameObject);
        }
    }
}