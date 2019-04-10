using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour {

    private PlayerController thePlayer;
    public Image black;
    public Animator anim;
    private HealthManager thePlayerHealth;
    private EffectsManager sound;

    private void Start() {
        sound = FindObjectOfType<EffectsManager>();
        thePlayerHealth = FindObjectOfType<HealthManager>();
        thePlayer = FindObjectOfType<PlayerController>();
    }
    public void Reanudar()
    {
        sound.ButtonEffect.Play();
        thePlayer.paused = false;
    }

    public void Reiniciar()
    {
        sound.ButtonEffect.Play();
        StartCoroutine(RestartDelay());
    }

    public void Salir()
    {
        sound.ButtonEffect.Play();
        StartCoroutine(ExitDelay());
    }

    public IEnumerator RestartDelay()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        anim.SetBool("Fade", false);
        Time.timeScale = 1f;
        Destroy(thePlayerHealth.gameObject);
        SceneManager.LoadScene("MainMenu");
    }

    public IEnumerator ExitDelay()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        Application.Quit();
    }

}