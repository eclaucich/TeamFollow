using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using GoogleMobileAds.Api;
using GoogleMobileAds.Placement;

public class StartUpController : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer = null;

    [SerializeField] private List<VideoClip> videos = null;

    [SerializeField] private GameObject overlay = null;

    [SerializeField] private BannerAdGameObject bannerGO;

    private void Start()
    {
        //overlay.SetActive(true);
        //bannerGO.gameObject.SetActive(false);
        videoPlayer.loopPointReached += EndReached;
        videoPlayer.prepareCompleted += VideoPrepared;
        //SetVideo(0);
        //videoPlayer.Prepare();
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
        //PlayVideo();
    }

    void EndReached(VideoPlayer vp)
    {
        AppController.instance.appStarted = true;
        bannerGO.gameObject.SetActive(true);
        videoPlayer.Prepare();
        overlay.SetActive(false);
        gameObject.SetActive(false);
    }
}
