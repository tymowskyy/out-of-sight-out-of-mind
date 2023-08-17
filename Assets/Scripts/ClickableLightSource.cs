using UnityEngine;
using UnityEngine.Rendering.Universal;

//SEHR WICHTIG: this should be attached a CHILD of the light source, not the actual light source object itself
//and also the object it is attached to should have a collider
public class ClickableLightSource : MonoBehaviour
{
    private void Awake()
    {
        parentCollider = transform.parent.GetComponent<Collider2D>();
        parentLightSource = transform.parent.GetComponent<Light2D>();

        parentCollider.enabled = isActive;
        parentLightSource.enabled = isActive;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (GetComponent<Collider2D>().OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition))) {
                toggle();
                toggleConnected();
            }
        }
    }

    public void toggle()
    {
        isActive = !isActive;

        parentCollider.enabled = isActive;
        parentLightSource.enabled = isActive;
    }

    private void toggleConnected()
    {
        foreach(ClickableLightSource connectedSource in connectedLightSources)
        {
            connectedSource.toggle();
        }
    }

    private Collider2D parentCollider;
    private Light2D parentLightSource;

    [SerializeField] private bool isActive;
    [SerializeField] private ClickableLightSource[] connectedLightSources;
}