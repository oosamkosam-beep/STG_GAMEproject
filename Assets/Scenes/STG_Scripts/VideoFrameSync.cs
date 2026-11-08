using UnityEngine;
using UnityEngine.Video;

public class VideoFrameSync : MonoBehaviour
{
	public GameObject phone_Prefab;
	public GameObject shopping_Prefab;
	public GameObject barcord_Prefab;
	public GameObject Monkey_Prefab;
	public GameObject Suit_Prefab;
	public GameObject zenmai_Prefab;
	public GameObject puppet_Prefab;
	public GameObject unitA_Prefab;
	public GameObject unitB_Prefab;
	public GameObject Boss2_Prefab;
	[SerializeField] CLPGauge lpGauge;   
	[SerializeField] CSPGauge spGauge;   
	[SerializeField] CtxtBox txtBox;
	[SerializeField] SharedEnemyHPData sharedHP;

    public VideoPlayer videoPlayer;

    bool prepared = false;
    bool LoopTrigger = false;

	bool spawn3, spawn7, spawn12, spawn13;
	bool spawn19_5;
	bool spawn21_5, spawn28, spawn29, spawn34_5;
	bool spawn46, spawn59, spawn80;
	bool spawn96, spawn98;
	bool puppetSpawn1_1, puppetSpawn1_2 ,puppetSpawn1_3 ,puppetSpawn1_4 ,puppetSpawn1_5 ,puppetSpawn1_6;
    bool spawn_wall;

    void Start()
    {
        videoPlayer.timeReference = VideoTimeReference.ExternalTime;

        videoPlayer.prepareCompleted += OnPrepared;
        videoPlayer.Prepare();

		sharedHP.lpGauge = lpGauge; 
		sharedHP.spGauge = spGauge; 
		sharedHP.Initialize();
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

/// ---------------------------------------------------
		if (!spawn3 && videoPlayer.frame >= 180)
		{
			GameObject enemyObj = Instantiate(phone_Prefab);
			RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
			enemy.SetRoadPosition(-1f, 5f);
			spawn3 = true;
		}
		if (!spawn7 && videoPlayer.frame >= 420)
		{
			GameObject enemyObj = Instantiate(zenmai_Prefab);
			RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
			enemy.SetRoadPosition(0.3f, 5f);
			spawn7 = true;
		}
		if (!spawn12 && videoPlayer.frame >= 720)
		{
			Vector2[] positions =
			{
				new Vector2(0f, 5f),
				new Vector2(0.2f, 5.1f),
				new Vector2(0.4f, 5.2f)
			};
			foreach (Vector2 pos in positions)
			{
				GameObject enemyObj = Instantiate(phone_Prefab);
				RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();

				enemy.SetRoadPosition(pos.x, pos.y);
			}
			spawn12 = true;
		}
		if (!spawn13 && videoPlayer.frame >= 780)
		{
			Vector2[] positions =
			{
				new Vector2(0f, 5f),
				new Vector2(-0.2f, 5.1f),
				new Vector2(-0.4f, 5.2f)
			};
			foreach (Vector2 pos in positions)
			{
				GameObject enemyObj = Instantiate(phone_Prefab);
				RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();

				enemy.SetRoadPosition(pos.x, pos.y);
			}
			spawn13 = true;
		}
		if (!spawn19_5 && videoPlayer.frame >= 1170)
		{
			Debug.Log("Instantiate_barcord");
			Instantiate(barcord_Prefab, new Vector3(2.6f, 6.2f, 8), Quaternion.identity);
			Instantiate(barcord_Prefab, new Vector3(3.6f, 6.2f, 8), Quaternion.identity);
			spawn19_5 = true;
		}
		if (!spawn21_5 && videoPlayer.frame >= 1290)
		{
			GameObject enemyObj = Instantiate(phone_Prefab);
			RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
			enemy.SetRoadPosition(-0.2f, 5f);
			spawn21_5 = true;
		}

		if (!spawn28 && videoPlayer.frame >= 1680)
		{
			Vector2[] positions =
			{
				new Vector2(0f, 5f),
				new Vector2(0.2f, 5.1f),
				new Vector2(0.4f, 5.2f)
			};
			foreach (Vector2 pos in positions)
			{
				GameObject enemyObj = Instantiate(phone_Prefab);
				RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
				enemy.SetRoadPosition(pos.x, pos.y);
			}
			spawn28 = true;
		}
		if (!spawn29 && videoPlayer.frame >= 1740)
		{
			Instantiate(barcord_Prefab, new Vector3(2.6f, 6.2f, 8), Quaternion.identity);
			Instantiate(barcord_Prefab, new Vector3(3.6f, 6.2f, 8), Quaternion.identity);
			spawn29 = true;
		}
		if (!spawn34_5 && videoPlayer.frame >= 2070)
		{
			Vector2[] positions =
			{
				new Vector2(-0.2f, 5f),
				new Vector2(0.4f, 5.1f)
			};
			foreach (Vector2 pos in positions)
			{
				GameObject enemyObj = Instantiate(zenmai_Prefab);
				RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
				enemy.SetRoadPosition(pos.x, pos.y);
			}
			spawn34_5 = true;
		}
		if (!spawn46 && videoPlayer.frame >= 2460)
		{
			Instantiate(shopping_Prefab, new Vector3(0, 7, 5), Quaternion.identity);
			spawn46 = true;
		}
		if (!spawn59 && videoPlayer.frame >= 3540)
		{
			for (int i = 0; i < 2; i++)
				Instantiate(shopping_Prefab, new Vector3(-1 + i * 2, 7, 5), Quaternion.identity);
			spawn59 = true;
		}

		if (!spawn_wall && videoPlayer.frame >= 4836)
		{
			GameObject unitA_Obj = Instantiate(unitA_Prefab);
			RoadObject2D unitA1 = unitA_Obj.GetComponent<RoadObject2D>();
			unitA1.SetRoadPosition(0.2f,4.85f);

			GameObject unitB_Obj = Instantiate(unitB_Prefab);
			RoadObject2D unitB = unitB_Obj.GetComponent<RoadObject2D>();
			unitB.SetRoadPosition(-0.2f,4.85f);

			spawn_wall = true;
			Debug.Log("video=4836");
		}

		if (!spawn96 && videoPlayer.frame >= 5760)
		{
			GameObject enemyObj =
				Instantiate(Monkey_Prefab,
						new Vector3(0, 0, 5),Quaternion.Euler(0, 180, 0));
			SetupMonkey(enemyObj);
			RoadObject2D enemy =
				enemyObj.GetComponent<RoadObject2D>();
			enemy.SetRoadPosition(1f, 5f);
			spawn96 = true;
		}

		if (!spawn98 && videoPlayer.frame >= 5880)
		{
			GameObject enemyObj =
				Instantiate(Monkey_Prefab);
			SetupMonkey(enemyObj);
			RoadObject2D enemy =
				enemyObj.GetComponent<RoadObject2D>();
			enemy.SetRoadPosition(-1f, 5f);
			spawn98 = true;
		}
        if (!LoopTrigger && videoPlayer.frame >= 8589)
        {
            LoopTrigger = true;
            Debug.Log("loopflag");
            VideoSwitcher.Instance.SwitchLoop_Video();
        }
    }

	void SetupMonkey(GameObject enemy)
	{
		var monkey = enemy.GetComponent<Cmonkey>();
		monkey.SharedHP = sharedHP;
		monkey.lpGauge = lpGauge;
		monkey.spGauge = spGauge;
	}
}
