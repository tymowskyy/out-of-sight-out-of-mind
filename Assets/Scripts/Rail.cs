using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail : MonoBehaviour
{
    public Transform railStart;
    public Transform railEnd;
    private bool isDragging = false;

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetButtonDown("Fire1"))
        {
            if(IsColliding(mousePos))
            {
                isDragging = true;
            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            isDragging = false;
        }
        
        if (isDragging)
        {
            transform.position = PointToSegmentProjecion(mousePos, railStart.position, railEnd.position);
        }
    }

    private bool IsColliding(Vector3 position)
    {
        RaycastHit2D[] allHits = Physics2D.RaycastAll(position, Vector2.zero);

        foreach(RaycastHit2D hit in allHits)
        {
            if(hit.collider == null) continue;

            if (hit.collider.gameObject == gameObject)
            {
                return true;
            }
        }
        return false;
    }

    private Vector3 PointToSegmentProjecion(Vector3 point, Vector3 start, Vector3 end)
    {
        Vector3 line = end - start;
        float lineLength = line.magnitude;
        Vector3 lineDirection = line / lineLength;
        float t = Vector3.Dot(lineDirection, point - start);
        if (t <= 0)
        {
            return start;
        }
        if (t >= lineLength)
        {
            return end;
        }
        Vector3 projection = start + t * lineDirection;
        return projection;
    }
}
