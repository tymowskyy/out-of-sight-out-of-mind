using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class VersionDisplay : MonoBehaviour
{
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "v" + Application.version;    
    }
}
