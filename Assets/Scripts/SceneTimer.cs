﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTimer : MonoBehaviour
{   
    public string LevelToLoad;
    public Image black;
    public Animator anim;

    // Use this for initialization
    void Start () {
        StartCoroutine(Fading());
    }
    IEnumerator Fading()
    {
        yield return new WaitForSeconds(50f);
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(LevelToLoad);
        anim.SetBool("Fade", false);
    }
}
