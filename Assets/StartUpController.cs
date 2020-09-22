using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class StartUpController : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer = null;

    private void Start()
    {
        videoPlayer.loopPointReached += EndReached;
        videoPlayer.Prepare();
    }

    private void Update()
    {
        if (videoPlayer.isPrepared && !videoPlayer.isPlaying)
            videoPlayer.Play();
    }

    void EndReached(VideoPlayer vp)
    {
        gameObject.SetActive(false);
    }
}
