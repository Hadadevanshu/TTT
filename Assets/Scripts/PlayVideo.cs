using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayVideo : MediaPlayerScript
{

    VideoPlayer player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<VideoPlayer>();
        LoadMultipleObjectsFromRemoteServer.Instance.videoAction += PlayFunctionVideo;

    }
    private void OnDestroy()
    {
        LoadMultipleObjectsFromRemoteServer.Instance.videoAction -= PlayFunctionVideo;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void PlayFunctionVideo(VideoClip clip)
    {
        loadingBar.SetActive(false);
        player.clip = clip;
        player.Play();
    }
}
