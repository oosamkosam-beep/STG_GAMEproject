using UnityEngine;
using UnityEngine.Video;

public class VideoFrameSync : MonoBehaviour
{
    [SerializeField] private GameObject unitA_Prefab;
    [SerializeField] private GameObject unitB_Prefab;

    public VideoPlayer videoPlayer;

    bool prepared = false;
    bool spawned = false;
    bool spawned_2 = false;
    bool LoopTrigger = false;

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

        if (!spawned_2 && videoPlayer.frame >= 1170)
        {
            spawned_2 = true;
            Debug.Log("video=1170");
        }
        if (!spawned && videoPlayer.frame >= 4836)
        {
			GameObject unitA_Obj =
				Instantiate(unitA_Prefab);

			RoadObject2D unitA =
				unitA_Obj.GetComponent<RoadObject2D>();

			unitA.SetRoadPosition(
					0.2f,  // 左右
					4.85f    // 奥
					);
            spawned = true;
            Debug.Log("video=4836");
        }
        if (!LoopTrigger && videoPlayer.frame >= 8589)
        {
            LoopTrigger = true;
            Debug.Log("loopflag");
            VideoSwitcher.Instance.SwitchLoop_Video();
        }
    }
}
