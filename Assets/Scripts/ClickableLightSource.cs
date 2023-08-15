using UnityEngine;
using UnityEngine.Rendering.Universal;

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
                    isActive = !isActive;

                    parentCollider.enabled = isActive;
                    parentLightSource.enabled = isActive;

                    break;
                }
            }
        }
    }

    private Collider2D parentCollider;
    private Light2D parentLightSource;

    [SerializeField] private bool isActive;
}