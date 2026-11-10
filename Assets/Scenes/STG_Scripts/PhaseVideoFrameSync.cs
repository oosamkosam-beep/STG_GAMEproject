using System;
using UnityEngine;
using UnityEngine.Video;

public class PhaseVideoFrameSync : MonoBehaviour
{
    public VideoPlayer videoPlayer;
	public long PhaseTimeframe =>
		prepared && videoPlayer != null ? videoPlayer.frame : -1;

    bool prepared = false;
    bool phaseStarted = false;

    void Start()
    {
        videoPlayer.timeReference = VideoTimeReference.ExternalTime;

        videoPlayer.prepareCompleted += OnPrepared;
        videoPlayer.Prepare();
    }

    void OnPrepared(VideoPlayer vp)
    {
        prepared = true;
        // いったん停止状態で待機
        vp.Play();
        vp.Pause();
    }

    void Update()
    {
        if (!prepared) return;
        if (CGameManager.Instance == null) return;

        // フェーズがまだ始まっていない
        if (!CGameManager.Instance.IsPhaseActive)
        {
            // 念のため先頭で止めておく
            videoPlayer.externalReferenceTime = 0;
            return;
        }
        // 再生開始（ここがフェーズ0秒）
        if (!phaseStarted)
        {
            phaseStarted = true;
            videoPlayer.Play();
        }
        // ★ フェーズ経過時間を動画に渡す
        double time = CGameManager.Instance.PhaseTime;
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
