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

    private void Start() {
        thePlayerHealth = FindObjectOfType<HealthManager>();
        thePlayer = FindObjectOfType<PlayerController>();
    }
    public void Reanudar()
    {
        thePlayer.paused = false;
    }

    public void Reiniciar()
    {
        StartCoroutine(RestartDelay());
    }

    public void Salir()
    {
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
        yield return new WaitForSecondsRealtime(.8f);
        Application.Quit();
    }

}