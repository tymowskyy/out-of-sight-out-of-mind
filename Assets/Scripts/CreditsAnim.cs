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

        charTimer += Time.deltaTime;

        if(charTimer >= charDelay)
        {
            tmpro.text += text[iter];
            charTimer = 0f;
            iter++;
        }

        if(iter >= text.Length)
        {
            this.enabled = false;
        }
    }

    private float charTimer;

    [SerializeField] private float charDelay;

    private TextMeshProUGUI tmpro;

    private int iter = 0;
    private const string text = "Thanks for playing!\n\nMusic and audio:\r\nM4K1\r\n\r\nCoding:\r\nGonk\r\nTymowskyy\r\nEnard\r\n\r\nArt:\r\nOrles\r\nM4K1";
}
