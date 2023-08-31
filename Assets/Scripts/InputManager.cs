using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
            return;
        }

        instance = this;
    }

    public float GetAxisRaw(string axis)
    {
        if(isInputBlocked())
        {
            return 0f;
        }

        return Input.GetAxisRaw(axis);
    }

    public bool GetKeyDown(KeyCode key)
    {
        return Input.GetKeyDown(key) && !isInputBlocked();
    }

    public bool GetKeyUp(KeyCode key)
    {
        return Input.GetKeyUp(key) && !isInputBlocked();
    }

    public bool GetButtonDown(string button)
    {
        return Input.GetButtonDown(button) && !isInputBlocked();
    }

    private bool isInputBlocked()
    {
        return PauseMenu.instance != null && PauseMenu.instance.isPaused();
    }

    public static InputManager instance { get; private set; }
}
