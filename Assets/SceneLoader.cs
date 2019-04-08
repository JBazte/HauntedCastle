using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator anim;
    public string LevelToLoad;
    public Image black;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            StartCoroutine(Fading());
        }

        if(other.gameObject.tag == "Ghost"){
            StartCoroutine(Fading());
        }
    }
    
    IEnumerator Fading()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(LevelToLoad);
        anim.SetBool("Fade", false);
    }
}
