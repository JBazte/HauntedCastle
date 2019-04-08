using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour {

    private PlayerController thePlayer;

    private void Start() {
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
        yield return new WaitForSecondsRealtime(.8f);
        System.Diagnostics.Process.Start(Application.dataPath.Replace("_Data", ".exe"));
        Application.Quit();
    }

    public IEnumerator ExitDelay()
    {
        yield return new WaitForSecondsRealtime(.8f);
        Application.Quit();
    }

}