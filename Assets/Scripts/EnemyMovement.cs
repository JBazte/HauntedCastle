using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int vx = 0;
    public float speed = 3f;
    Rigidbody2D rb;
    private PlayerController thePlayer;
    private GameObject playerGO;
    [SerializeField] int damage;
    private bool flashActive;
    public float flashLength;
    private float flashCounter;
    private SpriteRenderer enemyRenderer;
    public int Health;
    public ParticleSystem damagePrefab;
    private EffectsManager sound;
    private CameraShake cameraShake;
    private Necromancer Spawner;
    public bool followPlayer;
    private SpriteRenderer sprite;
    public float bossVelocity;
    private float distance1 = 10f;
    private float distance2 = 6f;
    private HealthManager thePlayerHealth;
    public GameObject PowerUp;
    private float minionVelocity = 3f;

    void Start()
    {
        thePlayerHealth = FindObjectOfType<HealthManager>();
        sprite = GetComponent<SpriteRenderer>();
        vx = 1;
        Spawner = FindObjectOfType<Necromancer>();
        cameraShake = FindObjectOfType<CameraShake>();
        enemyRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        playerGO = FindObjectOfType<PlayerController>().gameObject;
        thePlayer = FindObjectOfType<PlayerController>();
        sound = FindObjectOfType<EffectsManager>();
    }

    private void Update()
    {
        if (!followPlayer && vx < 0)
        {
            sprite.flipX = true;
        }
        else if (!followPlayer && vx > 0)
        {
            sprite.flipX = false;
        }
        if (Health <= 0 && this.gameObject.name == "NecromancerMinion3")
        {
            sound.EnemyDying2.Play();
            ItemDrop();
        }
        else if (Health <= 0 && this.gameObject.name == "OgreMinion" || Health <= 0 && this.gameObject.name == "NecromancerMinion2(Clone)")
        {
            sound.EnemyDying1.Play();
            ItemDrop();
        }
        else if (Health <= 0 && this.gameObject.name == "NecromancerBoss")
        {
            sound.EnemyDying.Play();
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
            sprite.flipX = true;

        }
        else if (followPlayer && transform.position.x < playerGO.transform.position.x)
        {
            sprite.flipX = false;
        }

    }

    private void OnDestroy()
    {
        if (this.gameObject.tag == "Clone")
        {
            Spawner.Timer = 0;
        }
    }

    private void FixedUpdate()
    {
        if (this.gameObject.name == "NecromancerBoss")
        {
            if (Vector3.Distance(transform.position, playerGO.transform.position) > distance1)
            {
                rb.velocity = new Vector2(vx, 0) * speed;
                followPlayer = false;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, playerGO.transform.position, bossVelocity * Time.deltaTime);
                followPlayer = true;
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, playerGO.transform.position) > distance2)
            {
                rb.velocity = new Vector2(vx, 0) * speed;
                followPlayer = false;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, playerGO.transform.position, minionVelocity * Time.deltaTime);
                followPlayer = true;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!followPlayer)
        {
            if (collision.gameObject.tag == "Test" || collision.gameObject.tag == "Enemy")
            {
                vx = -vx;
            }
        }
        if (collision.gameObject.tag == "Ghost")
        {
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ghost")
        {
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
        else if (other.gameObject.tag == "Player")
        {
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            this.gameObject.GetComponent<EnemyMovement>().minionVelocity = .1f;
            this.gameObject.GetComponent<EnemyMovement>().bossVelocity = .1f;
            if (thePlayerHealth.timerToHit < 1)
            {
                thePlayer.DealDamage(damage);
                sound.HurtPlayer.Play();
                StartCoroutine(cameraShake.Shake(.3f, .2f));
                thePlayerHealth.timerToHit = 1.8f;
            }
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ghost")
        {
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            ChangeVelocity();
        }
        else if (other.gameObject.tag == "Player")
        {
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            ChangeVelocity();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet" && this.gameObject.name == "NecromancerBoss")
        {
            Health -= 10;
            flashActive = true;
            flashCounter = flashLength;
            ParticleSystem clone = Instantiate(damagePrefab, transform.position, Quaternion.identity);
            clone.transform.localScale = new Vector3(2, 2, 2);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Bullet")
        {
            Health -= 10;
            flashActive = true;
            flashCounter = flashLength;
            Instantiate(damagePrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Ghost")
        {
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ghost")
        {
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
        else if (other.gameObject.tag == "Player")
        {
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            this.gameObject.GetComponent<EnemyMovement>().minionVelocity = .1f;
            this.gameObject.GetComponent<EnemyMovement>().bossVelocity = .1f;
            if (thePlayerHealth.timerToHit < 1)
            {
                thePlayer.DealDamage(damage);
                sound.HurtPlayer.Play();
                StartCoroutine(cameraShake.Shake(.3f, .2f));
                thePlayerHealth.timerToHit = 1.8f;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.tag == "Ghost")
        {
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            ChangeVelocity();
        }
        else if (other.gameObject.tag == "Player")
        {
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            ChangeVelocity();
        }
    }

    private void ItemDrop()
    {
        int rand = (int)Random.Range(1f, 100f);

        if (thePlayerHealth.playerHealth == 1 && rand <= 60f)
        {
            Instantiate(PowerUp, this.transform.position, this.transform.rotation);
        }
        else if (thePlayerHealth.playerHealth == 3 && rand <= 40f || thePlayerHealth.playerHealth == 2 && rand <= 40f)
        {
            Instantiate(PowerUp, this.transform.position, this.transform.rotation);
        }
        else if (thePlayerHealth.playerHealth == 4 && rand <= 20f)
        {
            Instantiate(PowerUp, this.transform.position, this.transform.rotation);
        }
        Destroy(gameObject);
    }

    public void ChangeVelocity()
    {
        this.gameObject.GetComponent<EnemyMovement>().bossVelocity = .5f;
        this.gameObject.GetComponent<EnemyMovement>().minionVelocity = 3f;
    }
}
