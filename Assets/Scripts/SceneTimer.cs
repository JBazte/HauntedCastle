using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTimer : MonoBehaviour
{   
    public string LevelToLoad;
    public Image black;
    public Animator anim;
    public float timeToLoad;
    private HealthManager thePlayerHealth;
    // Use this for initialization
    void Start () {
        thePlayerHealth = FindObjectOfType<HealthManager>();
        StartCoroutine(Fading());
        if(LevelToLoad == "Win"){
            Destroy(thePlayerHealth.gameObject);
        }
    }

    IEnumerator Fading()
    {
        yield return new WaitForSeconds(timeToLoad);
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(LevelToLoad);
        anim.SetBool("Fade", false);
    }
}
