using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SkipCutscene : MonoBehaviour
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

    IEnumerator Fading(string sceneName)
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(sceneName);
        anim.SetBool("Fade", false);
    }
}
