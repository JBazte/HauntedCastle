using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemies : MonoBehaviour
{
    public ParticleSystem destoyParticles;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Instantiate(destoyParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Test")
        {
            Instantiate(destoyParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
