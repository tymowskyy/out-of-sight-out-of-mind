using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    private void Start()
    {
        if(GameObject.FindGameObjectsWithTag("Music").Length > 1)
        {
            if(instance != this)
                Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    public void UpdateVolume(System.Single volume)
    {
        GetComponent<AudioSource>().volume = Mathf.Pow(volume, 4);
    }
}
