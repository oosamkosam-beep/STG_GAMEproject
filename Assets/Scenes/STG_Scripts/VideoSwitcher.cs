using UnityEngine;

public class VideoSwitcher : MonoBehaviour
{
    public static VideoSwitcher Instance;

    public GameObject Stage2_Way1_Video_mask;
    public GameObject Stage2_Way1_Video;
    public GameObject Stage2_Loop_Video;
    public GameObject Stage2_Way2_Video;
    public GameObject Stage2_Boss1_Video;
    public GameObject Stage2_Boss2_Video;

    void Awake()
    {
      Instance = this;
    }

    void OnEnable()
    {
        CGameManager.OnPhaseStarted += SwitchToPhaseVideo;
    }
    void OnDisable()
    {
        CGameManager.OnPhaseStarted -= SwitchToPhaseVideo;
    }

    void SwitchToPhaseVideo()
    {
        Stage2_Way1_Video_mask.SetActive(false);
        Stage2_Way1_Video.SetActive(false);
        Stage2_Way2_Video.SetActive(true);
    }
    public void SwitchLoop_Video()
    {
        Stage2_Loop_Video.SetActive(true);
    }

    public void SwitchBoss2_1Video()
    {
        Stage2_Way2_Video.SetActive(false);
        Stage2_Boss1_Video.SetActive(true);
    }
    public void SwitchBoss2_2Video()
    {
        Stage2_Boss2_Video.SetActive(true);
    }
    public void SwitchBoss2_3Video()
    {
        Stage2_Boss2_Video.SetActive(false);
    }
}
