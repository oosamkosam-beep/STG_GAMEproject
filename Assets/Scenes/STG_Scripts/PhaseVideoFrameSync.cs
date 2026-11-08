using System;
using UnityEngine;
using UnityEngine.Video;

public class PhaseVideoFrameSync : MonoBehaviour
{
	public GameObject phone_Prefab;
	public GameObject Suit_Prefab;
	public GameObject zenmai_Prefab;
	public GameObject puppet_Prefab;
	public GameObject Boss2_Prefab;
	[SerializeField] CLPGauge lpGauge;   
	[SerializeField] CSPGauge spGauge;   
	[SerializeField] CtxtBox txtBox;

    public VideoPlayer videoPlayer;

    bool prepared = false;
    bool phaseStarted = false;

	bool puppetSpawn1_1, puppetSpawn1_2 ,puppetSpawn1_3 ,puppetSpawn1_4 ,puppetSpawn1_5 ,puppetSpawn1_6;
	bool Phasespawn9, Phasespawn11, Phasespawn13, Phasespawn14; 
	bool Phasespawn16, Phasespawn17, Phasespawn19, Phasespawn19_5, Phasespawn21, Phasespawn22; 
	bool Phasespawn23_5, Phasespawn24, Phasespawn24_5; 
	bool puppetSpawn2_1, puppetSpawn2_2 ,puppetSpawn2_3 ,puppetSpawn2_4 ,puppetSpawn2_5 ,puppetSpawn2_6;
	bool SuitSpawn1_1, SuitSpawn1_2, SuitSpawn1_3, SuitSpawn1_4, SuitSpawn1_5; 
	bool SuitSpawn2_1, SuitSpawn2_2, SuitSpawn2_3, SuitSpawn2_4, SuitSpawn2_5, SuitSpawn2_6, SuitSpawn2_7;
	bool SuitSpawn3_1, SuitSpawn3_2, SuitSpawn3_3, SuitSpawn3_4, SuitSpawn3_5, SuitSpawn3_6, SuitSpawn3_7, SuitSpawn3_8, SuitSpawn3_9;
	bool EventTrigger;
	bool MessageTrigger;

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

/// ---------------------------------------------------
		if (!puppetSpawn1_1 && videoPlayer.frame >= 180)
		{
			Instantiate(puppet_Prefab, new Vector3(-1, 7, 5), Quaternion.identity);
			puppetSpawn1_1 = true;
		}
		if (!puppetSpawn1_2 && videoPlayer.frame >= 228)
		{
			Instantiate(puppet_Prefab, new Vector3(1, 7, 5), Quaternion.identity);
			puppetSpawn1_2 = true;
		}
		if (!puppetSpawn1_3 && videoPlayer.frame >= 276)
		{
			Instantiate(puppet_Prefab, new Vector3(-1, 7, 5), Quaternion.identity);
			puppetSpawn1_3 = true;
		}
		if (!puppetSpawn1_4 && videoPlayer.frame >= 324)
		{
			Instantiate(puppet_Prefab, new Vector3(1, 7, 5), Quaternion.identity);
			puppetSpawn1_4 = true;
		}
		if (!puppetSpawn1_5 && videoPlayer.frame >= 372)
		{
			Instantiate(puppet_Prefab, new Vector3(-1, 7, 5), Quaternion.identity);
			puppetSpawn1_5 = true;
		}
		if (!puppetSpawn1_6 && videoPlayer.frame >= 480)
		{
			Instantiate(puppet_Prefab, new Vector3(1, 7, 5), Quaternion.identity);
			puppetSpawn1_6 = true;
		}
		if (!Phasespawn9 && videoPlayer.frame >= 540)
		{
			GameObject enemyObj =
				Instantiate(phone_Prefab);
			RoadObject2D enemy =enemyObj.GetComponent<RoadObject2D>();
			enemy.SetRoadPosition(-0.1f, 5f);
			Phasespawn9 = true;
		}
		if (!Phasespawn11 && videoPlayer.frame >= 660)
		{
			GameObject enemyObj =
				Instantiate(zenmai_Prefab);
			RoadObject2D enemy =enemyObj.GetComponent<RoadObject2D>();
			enemy.SetRoadPosition(0.1f, 5f);
			Phasespawn11 = true;
		}
		if (!Phasespawn13 && videoPlayer.frame >= 780)
		{
			Vector2[] positions =
			{
				new Vector2(-0.2f, 5f),
				new Vector2(0.4f, 5.2f)
			};
			foreach (Vector2 pos in positions)
			{
				GameObject enemyObj = Instantiate(phone_Prefab);
				RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
				enemy.SetRoadPosition(pos.x, pos.y);
			}
			Phasespawn13 = true;
		}
		if (!Phasespawn14 && videoPlayer.frame >= 840)
		{
			Vector2[] positions =
			{
				new Vector2(0.2f, 5f),
				new Vector2(-0.4f, 5.2f)
			};
			foreach (Vector2 pos in positions)
			{
				GameObject enemyObj = Instantiate(zenmai_Prefab);
				RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
				enemy.SetRoadPosition(pos.x, pos.y);
			}
			Phasespawn14 = true;
		}
		if (!Phasespawn16 && videoPlayer.frame >= 960)
		{
			for (int i = 0; i < 2; i++)
				Instantiate(phone_Prefab, new Vector3(-0.3f+i*-1, 6+i*0.5f, 5), Quaternion.identity);
			Phasespawn16 = true;
		}
		if (!Phasespawn17 && videoPlayer.frame >= 1020)
		{
			GameObject enemyObj =
				Instantiate(Suit_Prefab);
			RoadObject2D enemy =enemyObj.GetComponent<RoadObject2D>();
			enemy.SetRoadPosition(-0.1f, 5f);
			Phasespawn17 = true;
		}
		if (!Phasespawn19 && videoPlayer.frame >= 1140)
		{
			Vector2[] positions =
			{
				new Vector2(0.2f, 5f),
				new Vector2(-0.4f, 5.2f)
			};
			foreach (Vector2 pos in positions)
			{
				GameObject enemyObj = Instantiate(zenmai_Prefab);
				RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
				enemy.SetRoadPosition(pos.x, pos.y);
			}
			Phasespawn19 = true;
		}
		if (!Phasespawn19_5 && videoPlayer.frame >= 1170)
		{
			Vector2[] positions =
			{
				new Vector2(-0.2f, 5f),
				new Vector2(0.4f, 5.2f)
			};
			foreach (Vector2 pos in positions)
			{
				GameObject enemyObj = Instantiate(zenmai_Prefab);
				RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
				enemy.SetRoadPosition(pos.x, pos.y);
			}
			Phasespawn19_5 = true;
		}
		if (!Phasespawn21 && videoPlayer.frame >= 1260)
		{
			Vector2[] positions =
			{
				new Vector2(0.3f, 5f),
				new Vector2(-0.5f, 5.2f)
			};
			foreach (Vector2 pos in positions)
			{
				GameObject enemyObj = Instantiate(phone_Prefab);
				RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
				enemy.SetRoadPosition(pos.x, pos.y);
			}
			Phasespawn21 = true;
		}
		if (!Phasespawn22 && videoPlayer.frame >= 1320)
		{
			GameObject enemyObj =
				Instantiate(Suit_Prefab);
			RoadObject2D enemy =
				enemyObj.GetComponent<RoadObject2D>();
			enemy.SetRoadPosition(0.1f, 5f);
			Phasespawn22 = true;
		}
		if (!Phasespawn23_5 && videoPlayer.frame >= 1410)
		{
			GameObject enemyObj = Instantiate(phone_Prefab);
			RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
			enemy.SetRoadPosition(0f, 5f);
			Phasespawn23_5 = true;
		}
		if (!Phasespawn24 && videoPlayer.frame >= 1440)
		{
			Vector2[] positions =
			{
				new Vector2(-0.3f, 5f),
				new Vector2(0.6f, 5.2f)
			};
			foreach (Vector2 pos in positions)
			{
				GameObject enemyObj = Instantiate(zenmai_Prefab);
				RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
				enemy.SetRoadPosition(pos.x, pos.y);
			}
			Phasespawn24 = true;
		}
		if (!Phasespawn24_5 && videoPlayer.frame >= 1470)
		{
			Vector2[] positions =
			{
				new Vector2(0.3f, 5f),
				new Vector2(-0.6f, 5.2f)
			};
			foreach (Vector2 pos in positions)
			{
				GameObject enemyObj = Instantiate(phone_Prefab);
				RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
				enemy.SetRoadPosition(pos.x, pos.y);
			}
			Phasespawn24_5 = true;
		}



		if (!puppetSpawn2_1 && videoPlayer.frame >= 1920)
		{
			Instantiate(puppet_Prefab, new Vector3(-1, 7, 5), Quaternion.identity);
			puppetSpawn2_1 = true;
		}
		if (!puppetSpawn2_2 && videoPlayer.frame >= 1970)
		{
			Instantiate(puppet_Prefab, new Vector3(1, 7, 5), Quaternion.identity);
			puppetSpawn2_2 = true;
		}
		if (!puppetSpawn2_3 && videoPlayer.frame >= 2020)
		{
			Instantiate(puppet_Prefab, new Vector3(-1, 7, 5), Quaternion.identity);
			puppetSpawn2_3 = true;
		}
		if (!puppetSpawn2_4 && videoPlayer.frame >= 2070)
		{
			Instantiate(puppet_Prefab, new Vector3(1, 7, 5), Quaternion.identity);
			puppetSpawn2_4 = true;
		}
		if (!puppetSpawn2_5 && videoPlayer.frame >= 2120)
		{
			Instantiate(puppet_Prefab, new Vector3(-1, 7, 5), Quaternion.identity);
			puppetSpawn2_5 = true;
		}
		if (!puppetSpawn2_6 && videoPlayer.frame >= 2170)
		{
			Instantiate(puppet_Prefab, new Vector3(1, 7, 5), Quaternion.identity);
			puppetSpawn2_6 = true;
		}



		if (!SuitSpawn1_1 && videoPlayer.frame >= 2520)
		{
			GameObject enemyObj = Instantiate(Suit_Prefab,
						new Vector3(0, 0, 5),Quaternion.identity);
			RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
			enemy.SetRoadPosition(0f, 5f);
			SuitSpawn1_1 = true;
		}
		if (!SuitSpawn1_2 && videoPlayer.frame >= 2550)
		{
			GameObject enemyObj = Instantiate(Suit_Prefab,
						new Vector3(0, 0, 5.1f),Quaternion.identity);
			RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
			enemy.SetRoadPosition(0f, 5f);
			SuitSpawn1_2 = true;
		}
		if (!SuitSpawn1_3 && videoPlayer.frame >= 2580)
		{
			GameObject enemyObj = Instantiate(Suit_Prefab,
						new Vector3(0, 0, 5.2f),Quaternion.identity);
			RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
			enemy.SetRoadPosition(0f, 5f);
			SuitSpawn1_3 = true;
		}
		if (!SuitSpawn1_4 && videoPlayer.frame >= 2610)
		{
			GameObject enemyObj = Instantiate(Suit_Prefab,
						new Vector3(0, 0, 5.3f),Quaternion.identity);
			RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
			enemy.SetRoadPosition(0f, 5f);
			SuitSpawn1_4 = true;
		}
		if (!SuitSpawn1_5 && videoPlayer.frame >= 2640)
		{
			GameObject enemyObj = Instantiate(Suit_Prefab,
						new Vector3(0, 0, 5.4f),Quaternion.identity);
			RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
			enemy.SetRoadPosition(0f, 5f);
			SuitSpawn1_5 = true;
		}


		if (!SuitSpawn2_1 && videoPlayer.frame >= 2760)
		{
			Vector2[] positions =
			{
				new Vector2(0.2f, 5f),
				new Vector2(-0.2f, 5f)
			};
			foreach (Vector2 pos in positions)
			{
				GameObject enemyObj = Instantiate(Suit_Prefab,
						new Vector3(0, 0, 5),Quaternion.identity);
				RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
				enemy.SetRoadPosition(pos.x, pos.y);
			}
			SuitSpawn2_1 = true;
		}
		if (!SuitSpawn2_2 && videoPlayer.frame >= 2790)
		{
			Vector2[] positions =
			{
				new Vector2(0.2f, 5f),
				new Vector2(-0.2f, 5f)
			};
			foreach (Vector2 pos in positions)
			{
				GameObject enemyObj = Instantiate(Suit_Prefab,
						new Vector3(0, 0, 5.1f),Quaternion.identity);
				RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
				enemy.SetRoadPosition(pos.x, pos.y);
			}
			SuitSpawn2_2 = true;
		}
		if (!SuitSpawn2_3 && videoPlayer.frame >= 2820)
		{
			Vector2[] positions =
			{
				new Vector2(0.2f, 5f),
				new Vector2(-0.2f, 5f)
			};
			foreach (Vector2 pos in positions)
			{
				GameObject enemyObj = Instantiate(Suit_Prefab,
						new Vector3(0, 0, 5.2f),Quaternion.identity);
				RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
				enemy.SetRoadPosition(pos.x, pos.y);
			}
			SuitSpawn2_3 = true;
		}
		if (!SuitSpawn2_4 && videoPlayer.frame >= 2850)
		{
			Vector2[] positions =
			{
				new Vector2(0.2f, 5f),
				new Vector2(-0.2f, 5f)
			};
			foreach (Vector2 pos in positions)
			{
				GameObject enemyObj = Instantiate(Suit_Prefab,
						new Vector3(0, 0, 5.3f),Quaternion.identity);
				RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
				enemy.SetRoadPosition(pos.x, pos.y);
			}
			SuitSpawn2_4 = true;
		}
		if (!SuitSpawn2_5 && videoPlayer.frame >= 2880)
		{
			Vector2[] positions =
			{
				new Vector2(0.2f, 5f),
				new Vector2(-0.2f, 5f)
			};
			foreach (Vector2 pos in positions)
			{
				GameObject enemyObj = Instantiate(Suit_Prefab,
						new Vector3(0, 0, 5.4f),Quaternion.identity);
				RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
				enemy.SetRoadPosition(pos.x, pos.y);
			}
			SuitSpawn2_5 = true;
		}
		if (!SuitSpawn2_6 && videoPlayer.frame >= 2910)
		{
			Vector2[] positions =
			{
				new Vector2(0.2f, 5f),
				new Vector2(-0.2f, 5f)
			};
			foreach (Vector2 pos in positions)
			{
				GameObject enemyObj = Instantiate(Suit_Prefab,
						new Vector3(0, 0, 5.5f),Quaternion.identity);
				RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
				enemy.SetRoadPosition(pos.x, pos.y);
			}
			SuitSpawn2_6 = true;
		}
		if (!SuitSpawn2_7 && videoPlayer.frame >= 2940)
		{
			Vector2[] positions =
			{
				new Vector2(0.2f, 5f),
				new Vector2(-0.2f, 5f)
			};
			foreach (Vector2 pos in positions)
			{
				GameObject enemyObj = Instantiate(Suit_Prefab,
						new Vector3(0, 0, 5.6f),Quaternion.identity);
				RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
				enemy.SetRoadPosition(pos.x, pos.y);
			}
			SuitSpawn2_7 = true;
		}

		if (!SuitSpawn3_1 && videoPlayer.frame >= 3060)
		{
			Vector2[] positions =
			{
				new Vector2(0.3f, 5f),
				new Vector2(-0f, 5f),
				new Vector2(-0.3f, 5f)
			};
			foreach (Vector2 pos in positions)
			{
				GameObject enemyObj = Instantiate(Suit_Prefab,
						new Vector3(0, 0, 5),Quaternion.identity);
				RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
				enemy.SetRoadPosition(pos.x, pos.y);
			}
			SuitSpawn3_1 = true;
		}
		if (!SuitSpawn3_2 && videoPlayer.frame >= 3090)
		{
			Vector2[] positions =
			{
				new Vector2(0.3f, 5f),
				new Vector2(-0f, 5f),
				new Vector2(-0.3f, 5f)
			};
			foreach (Vector2 pos in positions)
			{
				GameObject enemyObj = Instantiate(Suit_Prefab,
						new Vector3(0, 0, 5.1f),Quaternion.identity);
				RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
				enemy.SetRoadPosition(pos.x, pos.y);
			}
			SuitSpawn3_2 = true;
		}
		if (!SuitSpawn3_3 && videoPlayer.frame >= 3120)
		{
			Vector2[] positions =
			{
				new Vector2(0.3f, 5f),
				new Vector2(-0f, 5f),
				new Vector2(-0.3f, 5f)
			};
			foreach (Vector2 pos in positions)
			{
				GameObject enemyObj = Instantiate(Suit_Prefab,
						new Vector3(0, 0, 5.2f),Quaternion.identity);
				RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
				enemy.SetRoadPosition(pos.x, pos.y);
			}
			SuitSpawn3_3 = true;
		}
		if (!SuitSpawn3_4 && videoPlayer.frame >= 3150)
		{
			Vector2[] positions =
			{
				new Vector2(0.3f, 5f),
				new Vector2(-0f, 5f),
				new Vector2(-0.3f, 5f)
			};
			foreach (Vector2 pos in positions)
			{
				GameObject enemyObj = Instantiate(Suit_Prefab,
						new Vector3(0, 0, 5.3f),Quaternion.identity);
				RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
				enemy.SetRoadPosition(pos.x, pos.y);
			}
			SuitSpawn3_4 = true;
		}
		if (!SuitSpawn3_5 && videoPlayer.frame >= 3180)
		{
			Vector2[] positions =
			{
				new Vector2(0.3f, 5f),
				new Vector2(-0f, 5f),
				new Vector2(-0.3f, 5f)
			};
			foreach (Vector2 pos in positions)
			{
				GameObject enemyObj = Instantiate(Suit_Prefab,
						new Vector3(0, 0, 5.4f),Quaternion.identity);
				RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
				enemy.SetRoadPosition(pos.x, pos.y);
			}
			SuitSpawn3_5 = true;
		}
		if (!SuitSpawn3_6 && videoPlayer.frame >= 3210)
		{
			Vector2[] positions =
			{
				new Vector2(0.3f, 5f),
				new Vector2(-0f, 5f),
				new Vector2(-0.3f, 5f)
			};
			foreach (Vector2 pos in positions)
			{
				GameObject enemyObj = Instantiate(Suit_Prefab,
						new Vector3(0, 0, 5.5f),Quaternion.identity);
				RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
				enemy.SetRoadPosition(pos.x, pos.y);
			}
			SuitSpawn3_6 = true;
		}
		if (!SuitSpawn3_7 && videoPlayer.frame >= 3240)
		{
			Vector2[] positions =
			{
				new Vector2(0.3f, 5f),
				new Vector2(-0f, 5f),
				new Vector2(-0.3f, 5f)
			};
			foreach (Vector2 pos in positions)
			{
				GameObject enemyObj = Instantiate(Suit_Prefab,
						new Vector3(0, 0, 5.6f),Quaternion.identity);
				RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
				enemy.SetRoadPosition(pos.x, pos.y);
			}
			SuitSpawn3_7 = true;
		}
		if (!SuitSpawn3_8 && videoPlayer.frame >= 3270)
		{
			Vector2[] positions =
			{
				new Vector2(0.3f, 5f),
				new Vector2(-0f, 5f),
				new Vector2(-0.3f, 5f)
			};
			foreach (Vector2 pos in positions)
			{
				GameObject enemyObj = Instantiate(Suit_Prefab,
						new Vector3(0, 0, 5.7f),Quaternion.identity);
				RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
				enemy.SetRoadPosition(pos.x, pos.y);
			}
			SuitSpawn3_8 = true;
		}
		if (!SuitSpawn3_9 && videoPlayer.frame >= 3300)
		{
			Vector2[] positions =
			{
				new Vector2(0.3f, 5f),
				new Vector2(-0f, 5f),
				new Vector2(-0.3f, 5f)
			};
			foreach (Vector2 pos in positions)
			{
				GameObject enemyObj = Instantiate(Suit_Prefab,
						new Vector3(0, 0, 5.8f),Quaternion.identity);
				RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
				enemy.SetRoadPosition(pos.x, pos.y);
			}
			SuitSpawn3_9 = true;
		}

		if (!EventTrigger && videoPlayer.frame >= 3600)
		{
			EventTrigger = true;
			CGameManager.Instance.EventTimeline();
		}
		if (!MessageTrigger && videoPlayer.frame >= 3810)
		{
			MessageTrigger = true;
			txtBox.StartMessege();
		}
	}
}
