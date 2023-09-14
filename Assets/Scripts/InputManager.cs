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

        GameObject exitDoorGameObject = GameObject.FindGameObjectWithTag("ExitDoor");
        
        if(exitDoorGameObject != null)
        {
            exitDoor = exitDoorGameObject.GetComponent<ExitDoor>();
        }
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

    public bool GetButtonUp(string button)
    {
        return Input.GetButtonUp(button) && !isInputBlocked();
    }

    public bool isInputBlocked()
    {
        return (PauseMenu.instance != null && PauseMenu.instance.isPaused()) || (exitDoor != null && exitDoor.isCloseAnimationPlaying());
    }

    public static InputManager instance { get; private set; }

    private ExitDoor exitDoor;
}
