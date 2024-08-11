using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.Events;

public class LoadMultipleObjectsFromRemoteServer : MonoBehaviour
{
    public static LoadMultipleObjectsFromRemoteServer Instance { get; private set; }
    public List<string> audioName;
    public List<string> videoName;
    public Slider slider;
    AsyncOperationHandle<AudioClip> audiohandel;
    AsyncOperationHandle<VideoClip> videohandel;
    public AudioClip audioclip;
    public VideoClip videoClip;
    [SerializeField] AudioSource audioSource;

    public UnityAction<AudioClip> audioAction;
    public UnityAction<VideoClip> videoAction;

    bool audioDownloading;
    bool videoDownloading;

    int audioIndex, videoIndex;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        audioIndex = 0;
        videoIndex = 0;
        GetComponent<GameController>().audioEvent += LoadAudioAsset;
        GetComponent<GameController>().videoEvent += LoadVideoAsset;
    }

    private void OnDestroy()
    {
        GetComponent<GameController>().audioEvent -= LoadAudioAsset;
        GetComponent<GameController>().videoEvent -= LoadVideoAsset;
    }
    private void Update()
    {
        if(audioDownloading) 
        {
            slider.value= audiohandel.PercentComplete;
        }
        else if(videoDownloading) 
        {
            slider.value= videohandel.PercentComplete;
        }
    }

    //private void LoadAssetsByName(List<string> names)
    //{
    //    foreach (var name in names)
    //    {
    //        handel = Addressables.LoadAssetAsync<AudioClip>(name);
    //        handel.Completed += OnAssetLoaded;
    //    }

    //}

    [ContextMenu("LoadAudioAsset")]
    private void LoadAudioAsset()
    {
        slider.gameObject.SetActive(true);
        audiohandel = Addressables.LoadAssetAsync<AudioClip>(audioName[audioIndex]);
        audiohandel.Completed += OnAudioAssetLoaded;
        audioIndex++;
        if(audioIndex >= audioName.Count) 
        {
            audioIndex = 0;
        }
        videoDownloading = false;
        audioDownloading = true;
    }

    private void LoadVideoAsset()
    {
        slider.gameObject.SetActive(true);
        videohandel = Addressables.LoadAssetAsync<VideoClip>(videoName[videoIndex]);
        videohandel.Completed += OnVideoAssetLoaded;
        videoIndex++;
        if (videoIndex >= videoName.Count) 
        {
            videoIndex = 0;
        }
        videoDownloading = true;
        audioDownloading = false;
    }

    private void OnAudioAssetLoaded(AsyncOperationHandle<AudioClip> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
        //    Debug.Log("Loaded from remote server: " + obj.DebugName);
            audioclip = obj.Result;
            audioAction?.Invoke(audioclip);
        }
        else
        {
  //          Debug.LogError("Failed to load asset from remote server: " + obj.DebugName);
        }
    }private void OnVideoAssetLoaded(AsyncOperationHandle<VideoClip> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
//            Debug.Log("Loaded from remote server: " + obj.DebugName);
            videoClip = obj.Result;
            videoAction?.Invoke(videoClip);
        }
        else
        {
            Debug.LogError("Failed to load asset from remote server: " + obj.DebugName);
        }
    }

    // you can use this commented code for video load and play

    //public AssetReference videoReference;  // AssetReference for the MP4 video

    //private VideoPlayer videoPlayer;

    //void Start()
    //{
    //    videoPlayer = gameObject.AddComponent<VideoPlayer>();
    //    videoPlayer.playOnAwake = false;
    //    videoPlayer.renderMode = VideoRenderMode.CameraNearPlane; // or other render modes like RenderTexture, MaterialOverride, etc.

    //    LoadAndPlayVideo();
    //}

    //private void LoadAndPlayVideo()
    //{
    //    videoReference.LoadAssetAsync<VideoClip>().Completed += OnVideoLoaded;
    //}

    //private void OnVideoLoaded(AsyncOperationHandle<VideoClip> obj)
    //{
    //    if (obj.Status == AsyncOperationStatus.Succeeded)
    //    {
    //        videoPlayer.clip = obj.Result;
    //        videoPlayer.Play();
    //    }
    //    else
    //    {
    //        Debug.LogError("Failed to load video from Addressable Asset.");
    //    }
    //}
}
