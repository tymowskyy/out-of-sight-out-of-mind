using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableLightSource : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        lightSourceEnabled = !lightSourceEnabled;

        Collider2D lightCollider = transform.parent.GetComponent<Collider2D>();

        lightCollider.enabled = lightSourceEnabled;
    }

    [SerializeField] private bool lightSourceEnabled;
}
