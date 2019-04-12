using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile5 : MonoBehaviour
{
    public GameObject shootPoint;
    private Transform sp;
    private EffectsManager sound;
    public ParticleSystem destoyParticles;
    private GameObject boss;

    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.Find("NecromancerBoss");
        shootPoint = GameObject.Find("ShootPoint5");
        sp = shootPoint.GetComponent<Transform>();
        sound = FindObjectOfType<EffectsManager>();
    }

    void Update(){
        if(boss.GetComponent<EnemyMovement>().Health < 1){
            Instantiate(destoyParticles, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        float step = 8f * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, sp.position, step);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            sound.HurtPlayer.Play();
            Instantiate(destoyParticles, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        if(other.gameObject.tag == "Test"){
            Instantiate(destoyParticles, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
