using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MediaPlayerScript
{


    AudioSource src;
    protected override void PlayFunctionAudio(AudioClip clip)
    {
        loadingBar.SetActive(false);
        src.clip = clip;
        src.Play();

    }
    // Start is called before the first frame update
    void Start()
    {
        src = GetComponent<AudioSource>();
        LoadMultipleObjectsFromRemoteServer.Instance.audioAction += PlayFunctionAudio;
    }
    private void OnDestroy()
    {
        LoadMultipleObjectsFromRemoteServer.Instance.audioAction -= PlayFunctionAudio;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
