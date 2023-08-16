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
            RaycastHit2D[] allHits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            foreach(RaycastHit2D hit in allHits)
            {
                if(hit.collider == null) continue;

                if (hit.collider.gameObject == gameObject)
                {
                    toggle();
                    toggleConnected();

                    break;
                }
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