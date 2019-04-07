using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public int numHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    private PlayerController thePlayer;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

        if(thePlayer.health > numHearts)
        {
            thePlayer.health = numHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < thePlayer.health)
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
}
