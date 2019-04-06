using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemies : MonoBehaviour
{
    public ParticleSystem destoyParticles;

    void OnDestroy()
    {
        Instantiate(destoyParticles, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Test")
        {
            Destroy(gameObject);
        }
    }
}
