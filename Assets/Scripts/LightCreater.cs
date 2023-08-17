using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LightCreater : MonoBehaviour
{
    public Shader shader;

    void Awake()
    {
        SpriteRenderer rend = GetComponent<SpriteRenderer> ();

        rend.material = new Material(shader);
    }
}
