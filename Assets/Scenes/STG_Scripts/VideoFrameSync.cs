using UnityEngine;
using UnityEngine.Video;

public class VideoFrameSync : MonoBehaviour
{
    public VideoPlayer videoPlayer;
	public long GameTimeframe =>
		prepared && videoPlayer != null ? videoPlayer.frame : -1;

    bool prepared = false;

    void Start()
    {
        videoPlayer.timeReference = VideoTimeReference.ExternalTime;

        videoPlayer.prepareCompleted += OnPrepared;
        videoPlayer.Prepare();

    }

    void OnPrepared(VideoPlayer vp)
    {
        prepared = true;
        // 再生状態に入れておく（Pauseしない）
        vp.Play();
    }

    void Update()
    {
        if (!prepared) return;
        if (CGameManager.Instance == null) return;
        
        // ★ ゲーム時間をそのまま動画に渡す
        double time = CGameManager.Instance.GameTime;
        if (videoPlayer.length > 0 && time >= videoPlayer.length)
        {
            videoPlayer.Pause();
            return;
        }
        else
        {
          if (CGameManager.Instance.isPaused)
          {
            if (videoPlayer.isPlaying)
              videoPlayer.Pause();
            return;
          }
          else
          {
            if (!videoPlayer.isPlaying)
              videoPlayer.Play();
          }
        }
        videoPlayer.externalReferenceTime = time;
	}
}
