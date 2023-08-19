using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ButtonFontHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Color colorDefault;
    [SerializeField] Color colorHover;

    public void Awake()
    {
        ResetColor();
        GetComponent<Button>().onClick.AddListener(ResetColor);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponentInChildren<TextMeshProUGUI>().color = colorHover;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponentInChildren<TextMeshProUGUI>().color = colorDefault;
    }

    private void ResetColor()
    {
        GetComponentInChildren<TextMeshProUGUI>().color = colorDefault;
    }
}
