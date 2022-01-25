using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Make this a singleton, to have continuous music across multiple levels.
public class MusicPlayer : MonoBehaviour
{
    private void Awake()
    {
        SetUpSingleton();
    }
    private void SetUpSingleton()
    {
        if(FindObjectsOfType<MusicPlayer>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    
    public void Mute()
    {
        GetComponent<AudioSource>().mute = true;
    }
   
}
