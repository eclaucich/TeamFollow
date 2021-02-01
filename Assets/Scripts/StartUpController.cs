using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class StartUpController : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer = null;

    [SerializeField] private List<VideoClip> videos = null;

    [SerializeField] private GameObject overlay = null;

    private void Start()
    {
        overlay.SetActive(true);
        videoPlayer.loopPointReached += EndReached;
        videoPlayer.prepareCompleted += VideoPrepared;
        videoPlayer.Prepare();
    }

    public void SetVideo(int i)
    {
        videoPlayer.clip = videos[i];
        PlayVideo();
    }

    public void PlayVideo()
    {
        gameObject.SetActive(true);
        if (!videoPlayer.isPrepared)
            videoPlayer.Prepare();
        if (videoPlayer.isPrepared && !videoPlayer.isPlaying)
            videoPlayer.Play();
    }

    void VideoPrepared(VideoPlayer vp)
    {
        Debug.Log("VIDEO PREPARADO");
        PlayVideo();
    }

    void EndReached(VideoPlayer vp)
    {
        videoPlayer.Prepare();
        overlay.SetActive(false);
        gameObject.SetActive(false);
    }
}
