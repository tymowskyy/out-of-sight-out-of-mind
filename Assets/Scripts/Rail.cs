using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail : MonoBehaviour
{
    public Transform railStart;
    public Transform railEnd;
    public GameObject head;
    private bool isDragging = false;
    private Vector3 mouseOffset;

    private void Start()
    {
        head.transform.position = PointToSegmentProjecion(head.transform.position, railStart.position, railEnd.position);
    }

    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (InputManager.instance.GetButtonDown("Fire1"))
        {
            if(head.GetComponent<Collider2D>().OverlapPoint(mousePos))
            {
                isDragging = true;
                mouseOffset = mousePos - head.transform.position;
            }
        }

        if (InputManager.instance.GetButtonUp("Fire1"))
        {
            isDragging = false;
        }
        
        if (isDragging)
        {
            head.transform.position = PointToSegmentProjecion(mousePos - mouseOffset, railStart.position, railEnd.position);
        }
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
