
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayVideoClass : MonoBehaviour
{
    public VideoPlayer player;
    public VideoClip clip;
    
    // Start is called before the first frame update
    void Start()
    {
        player.clip = clip;
        player.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
