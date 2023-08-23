using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreditsAnim : MonoBehaviour
{
    private void Awake()
    {
        tmpro = GetComponent<TextMeshProUGUI>();
        charTimer = 0f;
    }

    private void Update()
    {
        if (!done)
        {
            charTimer += Time.deltaTime;

            if (charTimer >= charDelay)
            {
                tmpro.text += text[iter];
                charTimer = 0f;
                iter++;
            }

            if (iter >= text.Length)
            {
                done = true;
            }
        } else
        {
            afterAnimTimeout -= Time.deltaTime;

            if(afterAnimTimeout <= 0f)
            {
                LevelManager.instance.LoadMainMenu();
            }
        }
    }

    private bool done = false;

    private float charTimer;
    private float afterAnimTimeout = 7.34f;

    [SerializeField] private float charDelay;

    private TextMeshProUGUI tmpro;

    private int iter = 0;
    private const string text = "Thanks for playing!\n\nMusic and audio:\r\nM4K1\r\n\r\nCoding:\r\nGonk\r\nTymowskyy\r\nEnard\r\n\r\nArt:\r\nOrles\r\nM4K1";
}
