using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    public Image black;
    public Animator anim;
    private EffectsManager sound;

    private void Start() {
        sound = FindObjectOfType<EffectsManager>();
    }
    public void NewScene(string sceneName)
    {
        sound.ButtonEffect.Play();
        StartCoroutine(Fading(sceneName));
    }
    
    public void ExitScene()
    {
        sound.ButtonEffect.Play();
        StartCoroutine(ExitDelay());
    }

    IEnumerator Fading(string sceneName)
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        anim.SetBool("Fade", false);
        SceneManager.LoadScene(sceneName);
    }

    public IEnumerator ExitDelay()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        Application.Quit();
    }
}