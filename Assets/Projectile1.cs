using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile1 : MonoBehaviour
{
    public GameObject shootPoint;
    private Transform sp;
    private EffectsManager sound;
    public ParticleSystem destoyParticles;

    // Start is called before the first frame update
    void Start()
    {
        shootPoint = GameObject.Find("ShootPoint1");
        sp = shootPoint.GetComponent<Transform>();
        sound = FindObjectOfType<EffectsManager>();
    }
    
    void Update(){
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
