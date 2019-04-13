using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EffectsManager : MonoBehaviour {
    public AudioSource HurtEnemy;
    public AudioSource HurtPlayer;
    public AudioSource HelathUp;
    public AudioSource SpecialAbility;
    public AudioSource ButtonEffect;
    public AudioSource GhostDying;
    public AudioSource EnemyDying;
    public AudioSource EnemyDying1;
    public AudioSource EnemyDying2;
    private Scene theScene;

    void Start() {
        theScene = SceneManager.GetActiveScene();
        if(theScene.name == "Lose"){
            GhostDying.Play();
        }
    }

}