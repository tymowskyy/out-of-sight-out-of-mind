using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreditsAnim : MonoBehaviour
{
    private void Awake()
    {
        tmpro = GetComponent<TextMeshProUGUI>();
        textSound = GetComponent<AudioSource>();

        charTimer = 0f;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Escape))
        {
            if (!done)
            {
                //Skip the animation
                done = true;

                tmpro.text = creditsText;
            }

            else
            {
                //Skip the wait after the animation ends
                afterAnimTimeout = 0f;
            }
        }

        if (!done)
        {
            charTimer += Time.deltaTime;

            if (charTimer >= charDelay)
            {
                tmpro.text += creditsText[nextCharToDisplay];

                if (char.IsLetterOrDigit(creditsText[nextCharToDisplay]))
                    textSound.Play();

                charTimer = 0f;
                nextCharToDisplay++;
            }

            if (nextCharToDisplay >= creditsText.Length)
            {
                done = true;
            }
        } 
        
        else
        {
            afterAnimTimeout -= Time.deltaTime;

            if(afterAnimTimeout <= 0f)
            {
                LevelManager.instance.LoadMainMenu();
            }
        }
    }

    private TextMeshProUGUI tmpro;
    private AudioSource textSound;

    [SerializeField] private float charDelay;
    [SerializeField] private float afterAnimTimeout;

    private bool done = false;

    private int nextCharToDisplay = 0;
    private float charTimer;

    private const string creditsText = "Thanks for playing!\n\nMusic and audio:\r\nM4K1\r\n\r\nCoding:\r\nGonk\r\nTymowskyy\r\nEnard\r\n\r\nArt:\r\nOrles\r\nM4K1";
}
