using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {
    public int playerHealth;
    public float timerToHit;

    // Start is called before the first frame update
    void Start () {
        DontDestroyOnLoad (transform.gameObject);
        playerHealth = 5;
    }

    void FixedUpdate () {
        timerToHit -= Time.deltaTime;
    }
}