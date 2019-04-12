using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealthBar : MonoBehaviour
{
    public int numHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    private PlayerController thePlayer;
    public Image black;
    public Animator anim;
    private HealthManager thePlayerHealth;
    //private EffectsManager sound;

    // Start is called before the first frame update
    void Start()
    {
        //sound = FindObjectOfType<EffectsManager>();
        thePlayerHealth = FindObjectOfType<HealthManager>();
        thePlayer = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (thePlayerHealth.playerHealth < 1){
            StartCoroutine(Fading());
            //sound.GhostDying.Play();
        }

        if(thePlayerHealth.playerHealth > numHearts)
        {
            thePlayerHealth.playerHealth = numHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < thePlayerHealth.playerHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
    
    IEnumerator Fading()
    {
        Time.timeScale = 0f;
        anim.SetBool("Fade", true);
        //yield return new WaitForSeconds(sound.GhostDying.clip.length);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene("Lose");
        anim.SetBool("Fade", false);
        thePlayer.gameObject.SetActive(false);
        Time.timeScale = 1f;
        Destroy(thePlayerHealth.gameObject);
    }
}
