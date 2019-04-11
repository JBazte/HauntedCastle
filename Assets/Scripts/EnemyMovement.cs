using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int vx = 0;
    public float speed = 3f;
    Rigidbody2D rb;
    public bool hasChangedVel = false;
    private PlayerController thePlayer;
    private GameObject playerGO;
    [SerializeField] int damage;
    private bool flashActive;
    public float flashLength;
    private float flashCounter;
    public float smoothTime = 1f;
    private Vector3 smoothVelocity = Vector3.zero;
    private SpriteRenderer enemyRenderer;
    public int Health;
    public ParticleSystem damagePrefab;
    private EffectsManager sound;
    private float timerToHit;
    private CameraShake cameraShake;
    private Necromancer Spawner;
    public bool followPlayer;
    private SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        vx = 1;
        Spawner = FindObjectOfType<Necromancer>();
        cameraShake = FindObjectOfType<CameraShake>();
        enemyRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        playerGO = FindObjectOfType <PlayerController>().gameObject;
        thePlayer = FindObjectOfType<PlayerController>();
        sound = FindObjectOfType<EffectsManager>();
    }

    private void Update()
    {
        if(!followPlayer && vx < 0){
            sprite.flipX = true;
        }
        else if(!followPlayer && vx > 0)
        {
            sprite.flipX = false;
        }
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
        if (followPlayer && transform.position.x > playerGO.transform.position.x)
        {
            //rb.velocity = new Vector2(vx, 0) * speed;
            //transform.localScale = new Vector3(-1, 1, 1);
            sprite.flipX = true;

        }
        else if(followPlayer && transform.position.x < playerGO.transform.position.x)
        {
            //rb.velocity = new Vector2(vx, 0) * speed;
            //transform.localScale = new Vector3(1, 1, 1);
            sprite.flipX = false;
        }

    }

    private void OnDestroy() {
        if(this.gameObject.tag == "Clone" || this.gameObject.tag == "Clone"){
            Spawner.Timer = 0;
        }
    }

    private void FixedUpdate()
    {
        if(this.gameObject.name == "NecromancerBoss"){
            if (Vector3.Distance(transform.position, playerGO.transform.position) > 10f ) {
                rb.velocity = new Vector2(vx, 0) * speed;
                followPlayer = false;
            }
            else
            {
                float step = 1.8f * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, playerGO.transform.position, step);
                followPlayer = true;
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, playerGO.transform.position) > 5f ) {
                rb.velocity = new Vector2(vx, 0) * speed;
                followPlayer = false;
            }
            else
            {
                float step = 3f * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, playerGO.transform.position, step);
                followPlayer = true;
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!followPlayer){
            if (collision.gameObject.tag == "Test" && !hasChangedVel) { 
                vx = -vx;
                //transform.localScale = new Vector3(-transform.localScale.x,1,1);
            }
        }
        if(collision.gameObject.tag == "Ghost"){
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }        
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ghost"){
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
        else if(other.gameObject.tag == "Player" ) {
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            timerToHit -= Time.deltaTime;
            if(timerToHit < 1){
            thePlayer.DealDamage(damage);
            sound.HurtPlayer.Play();
            StartCoroutine(cameraShake.Shake(.3f, .2f));       
            timerToHit = 1.8f;
        }
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        
        if(other.gameObject.tag == "Ghost"){
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        } else if(other.gameObject.tag == "Player"){
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
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
            Instantiate(damagePrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
        
        if(other.gameObject.tag == "Ghost"){
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Ghost"){
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        } else if(other.gameObject.tag == "Player" ) {
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            timerToHit -= Time.deltaTime;
            if(timerToHit < 1){
            thePlayer.DealDamage(damage);
            sound.HurtPlayer.Play();
            StartCoroutine(cameraShake.Shake(.3f, .2f));       
            timerToHit = 1.8f;
            }
}
}
    private void OnTriggerExit2D(Collider2D other) {

        if(other.gameObject.tag == "Ghost"){
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        } else if(other.gameObject.tag == "Player"){
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            timerToHit = 0;
        }
    }
}
