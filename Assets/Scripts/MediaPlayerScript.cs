using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class  MediaPlayerScript : MonoBehaviour
{
    public GameObject loadingBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected virtual void PlayFunctionAudio(AudioClip clip)
    {
        // Implement this function in child classes 
    }
    protected virtual void PlayFunctionVideo(VideoClip clip)
    {
        // Implement this function in child classes 
    }

}
