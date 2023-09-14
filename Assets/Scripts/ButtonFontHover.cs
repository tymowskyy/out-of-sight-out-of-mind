using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Image))]
public class ButtonFontHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color colorDefault;
    public Color colorHover;
    public Sprite spriteDefault;
    public Sprite spriteHover;

    public void Awake()
    {
        ResetColor();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponentInChildren<TextMeshProUGUI>().color = colorHover;
        GetComponent<Image>().sprite  = spriteHover;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ResetColor();
    }

    void OnDisable()
    {
        ResetColor();
    }

    private void ResetColor()
    {
        GetComponentInChildren<TextMeshProUGUI>().color = colorDefault;
        GetComponent<Image>().sprite = spriteDefault;
    }
}
