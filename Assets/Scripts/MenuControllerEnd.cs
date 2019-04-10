using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControllerEnd : MonoBehaviour {

    public Image black;
    public Animator anim;
    private GameObject button;
    private EffectsManager sound;

    void Start() {
        sound = FindObjectOfType<EffectsManager>();
        button = GameObject.Find("Reiniciar");
        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(button);
    }

    public void NewScene(string sceneName)
    {
        sound.ButtonEffect.Play();
        StartCoroutine(Fading(sceneName));
    }

    public void RestartScene()
    {
        sound.ButtonEffect.Play();
        StartCoroutine(RestartDelay());
    }

    public void ExitScene()
    {
        sound.ButtonEffect.Play();
        StartCoroutine(Delay());
    }

    IEnumerator Fading(string sceneName)
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        anim.SetBool("Fade", false);
        SceneManager.LoadScene(sceneName);
    }

    public IEnumerator RestartDelay()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        anim.SetBool("Fade", false);
        SceneManager.LoadScene("MainMenu");
    }

    public IEnumerator Delay()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        Application.Quit();
    }
}