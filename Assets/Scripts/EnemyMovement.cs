using System.Collections;
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

    private bool flashActive;
    public float flashLength;
    private float flashCounter;
    public float smoothTime = 1f;
    private Vector3 smoothVelocity = Vector3.zero;
    private SpriteRenderer enemyRenderer;
    [SerializeField] private int Health;
    public ParticleSystem damagePrefab;
    private EffectsManager sound;
    private float timerToHit;
    private CameraShake cameraShake;

    void Start()
    {
        cameraShake = FindObjectOfType<CameraShake>();
        enemyRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        vx = 1;
        playerGO = FindObjectOfType <PlayerController>().gameObject;
        player = FindObjectOfType<PlayerController>();
        sound = FindObjectOfType<EffectsManager>();
    }

    private void Update()
    {
        if (Health <= 0)
        {
           Destroy(gameObject);
        }
        if (flashActive)
        {
            if (flashCounter > flashLength * .66f)
            {
                enemyRenderer.color = Color.red;
            }
            else if (flashCounter > flashLength * .33f)
            {
                enemyRenderer.color = Color.white;
                enemyRenderer.color = new Color(enemyRenderer.color.r, enemyRenderer.color.g, enemyRenderer.color.b, 1f);
            }
            else if (flashCounter > 0f)
            {
                enemyRenderer.color = Color.red;
                enemyRenderer.color = new Color(enemyRenderer.color.r, enemyRenderer.color.g, enemyRenderer.color.b, 0f);
            }
            else
            {
                enemyRenderer.color = Color.white;
                enemyRenderer.color = new Color(enemyRenderer.color.r, enemyRenderer.color.g, enemyRenderer.color.b, 1f);
                flashActive = false;
            }
            flashCounter -= Time.deltaTime;

            
        }
        if (transform.position.x > playerGO.transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

    }

    private void FixedUpdate()
    {
        
        if (Vector3.Distance(transform.position, playerGO.transform.position) > 5f ) {
            rb.velocity = new Vector2(vx, 0) * speed;
        } else
        {
            transform.position = Vector3.SmoothDamp(transform.position, playerGO.transform.position, ref smoothVelocity, smoothTime);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Test" && !hasChangedVel) { 
        vx = -vx;
        
            transform.localScale = new Vector3(-transform.localScale.x,1,1);
        }

        
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player" ) {
            timerToHit -= Time.deltaTime;
            if(timerToHit < 1){
            player.DealDamage(damage);
            sound.HurtPlayer.Play();
            StartCoroutine(cameraShake.Shake(.3f, .2f));       
            timerToHit = 1.8f;
        }
        }

    }

    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player"){
            timerToHit = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Health -= 10;
            flashActive = true;
            flashCounter = flashLength;
            Destroy(other.gameObject);


            
            Instantiate(damagePrefab, transform.position, Quaternion.identity);

        }
    }
}
