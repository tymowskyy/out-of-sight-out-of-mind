using UnityEngine;

public class ClickableLightSource : MonoBehaviour
{
    private void Awake()
    {
        parentCollider = transform.parent.GetComponent<Collider2D>();
        parentCollider.enabled = isActive;
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

                    break;
                }
            }
        }
    }

    private Collider2D parentCollider;

    [SerializeField] private bool isActive;
}