using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class VersionDisplay : MonoBehaviour
{
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "v" + Application.version;    
    }
}
