using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour {

    public string LevelToLoad;
    public Image black;
    public Animator anim;
    private bool time;

    // Use this for initialization
    void Start () {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    // Update is called once per frame
    void Update () {
        if (time)
        {
            Time.timeScale = 0f;
        }
	}

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player")
        {
            time = true;
            StartCoroutine(Fading());
        }
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        GameObject[] objects = scene.GetRootGameObjects();
    }
    public IEnumerator Fading()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(LevelToLoad);
        anim.SetBool("Fade", false);
    }
}