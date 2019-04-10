using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public ParticleSystem destoyParticles;
    private EffectsManager sound;
    private CameraShake cameraShake;
    private HealthManager thePlayer;

    private void Start() {
        thePlayer = FindObjectOfType<HealthManager>();
        cameraShake = FindObjectOfType<CameraShake>();
        sound = FindObjectOfType<EffectsManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(cameraShake.Shake(.3f, .2f));       
            thePlayer.playerHealth -= 1;
            sound.HurtPlayer.Play();
            Instantiate(destoyParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
