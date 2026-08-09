using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class VideoOnEnable : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    void OnEnable()
    {
        // 有効化された瞬間に先頭から再生
        videoPlayer.Stop();
        videoPlayer.Play();
    }

    void OnDisable()
    {
        // 非表示時は止める
        videoPlayer.Stop();
    }
}
