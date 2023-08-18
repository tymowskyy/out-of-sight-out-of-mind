using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriangleRecalculate : MonoBehaviour
{
    [SerializeField] private UnityEngine.Rendering.Universal.Light2D light;
    public void Recalculate(){
        float s = gameObject.transform.localScale.x*transform.localScale.x/(gameObject.transform.localScale.y*gameObject.transform.localScale.y);
        float x = 1/Mathf.Sqrt(s+4);
        float y = -2*x+0.5f;
        float x2 = 1/Mathf.Sqrt(s+16);
        float y2 = -4*x2+0.5f;
        float x3 = 1/Mathf.Sqrt(s+64);
        float y3 = -8*x3+0.5f;
        Vector2[] colliderPoints = new [] { new Vector2(0f,0.5f), 
                                            new Vector2(-x,y), 
                                            new Vector2(-x2,y2), 
                                            new Vector2(-x3,y3), 
                                            new Vector2(0f,-0.5f),
                                            new Vector2(x3,y3), 
                                            new Vector2(x2,y2),
                                            new Vector2(x,y),  
                                        };
        GetComponent<PolygonCollider2D>().SetPath(0,colliderPoints);

        Vector3[] lightPoints = new [] { new Vector3(0f,0.5f,0), 
                                            new Vector3(-x,y,0), 
                                            new Vector3(-x2,y2,0), 
                                            new Vector3(-x3,y3,0), 
                                            new Vector3(0f,-0.5f,0),
                                            new Vector3(x3,y3,0), 
                                            new Vector3(x2,y2,0),
                                            new Vector3(x,y,0),  
                                        };
        light.SetShapePath(lightPoints);
        Debug.Log(s);
    }
}
