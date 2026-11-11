using UnityEngine;
using UnityEngine.Video;

public class SpawnManager : MonoBehaviour
{
	[SerializeField] private VideoFrameSync videoFrameSync;
	[SerializeField] private PhaseVideoFrameSync phasevideoFrameSync;

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

    bool LoopTrigger = false;

	bool spawn3, spawn7, spawn12, spawn13;
	bool spawn19_5;
	bool spawn21_5, spawn28, spawn29, spawn34_5;
	bool spawn46, spawn59, spawn80;
	bool spawn96, spawn98;
    bool spawn_wall;

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
		if (lpGauge != null)
		{
			lpGauge.Hide();
		}
		if (spGauge != null)
		{
			spGauge.Hide();
		}
		if (txtBox != null)
		{
			txtBox.Hide();
		}
		sharedHP.lpGauge = lpGauge; 
		sharedHP.spGauge = spGauge; 
		sharedHP.Initialize();
	}

	void Update()
	{
		long gametime_frame = videoFrameSync.GameTimeframe;
		long phasetime_frame = phasevideoFrameSync.PhaseTimeframe;

		if (!spawn3 && gametime_frame >= 180)
		{
			GameObject enemyObj = Instantiate(phone_Prefab);
			RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
			enemy.SetRoadPosition(-1f, 5f);
			spawn3 = true;
		}
		if (!spawn7 && gametime_frame >= 420)
		{
			GameObject enemyObj = Instantiate(zenmai_Prefab);
			RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
			enemy.SetRoadPosition(0.3f, 5f);
			spawn7 = true;
		}
		if (!spawn12 && gametime_frame >= 720)
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
		if (!spawn13 && gametime_frame >= 780)
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
		if (!spawn19_5 && gametime_frame >= 1170)
		{
			Debug.Log("Instantiate_barcord");
			Instantiate(barcord_Prefab, new Vector3(2.6f, 6.2f, 8), Quaternion.identity);
			Instantiate(barcord_Prefab, new Vector3(3.6f, 6.2f, 8), Quaternion.identity);
			spawn19_5 = true;
		}
		if (!spawn21_5 && gametime_frame >= 1290)
		{
			GameObject enemyObj = Instantiate(phone_Prefab);
			RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
			enemy.SetRoadPosition(-0.2f, 5f);
			spawn21_5 = true;
		}

		if (!spawn28 && gametime_frame >= 1680)
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
		if (!spawn29 && gametime_frame >= 1740)
		{
			Instantiate(barcord_Prefab, new Vector3(2.6f, 6.2f, 8), Quaternion.identity);
			Instantiate(barcord_Prefab, new Vector3(3.6f, 6.2f, 8), Quaternion.identity);
			spawn29 = true;
		}
		if (!spawn34_5 && gametime_frame >= 2070)
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
		if (!spawn46 && gametime_frame >= 2460)
		{
			Instantiate(shopping_Prefab, new Vector3(0, 7, 5), Quaternion.identity);
			spawn46 = true;
		}
		if (!spawn59 && gametime_frame >= 3540)
		{
			for (int i = 0; i < 2; i++)
				Instantiate(shopping_Prefab, new Vector3(-1 + i * 2, 7, 5), Quaternion.identity);
			spawn59 = true;
		}

		if (!spawn_wall && gametime_frame >= 4836)
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

		if (!spawn96 && gametime_frame >= 5760)
		{
			GameObject enemyObj =
				Instantiate(Monkey_Prefab,
						new Vector3(0, 0, 5),Quaternion.Euler(0, 180, 0));
			SetupMonkey(enemyObj);
			RoadObject2D enemy =
				enemyObj.GetComponent<RoadObject2D>();
			enemy.SetRoadPosition(0.8f, 5f);
			spawn96 = true;
		}

		if (!spawn98 && gametime_frame >= 5880)
		{
			GameObject enemyObj =
				Instantiate(Monkey_Prefab);
			SetupMonkey(enemyObj);
			RoadObject2D enemy =
				enemyObj.GetComponent<RoadObject2D>();
			enemy.SetRoadPosition(-0.8f, 5f);
			spawn98 = true;
		}
        if (!LoopTrigger && gametime_frame >= 8589)
        {
            LoopTrigger = true;
            Debug.Log("loopflag");
            VideoSwitcher.Instance.SwitchLoop_Video();
        }
///----------------- PhaseTime ----------------------
		if (!puppetSpawn1_1 && phasetime_frame >= 180)
		{
			Instantiate(puppet_Prefab, new Vector3(-1, 7, 5), Quaternion.identity);
			puppetSpawn1_1 = true;
		}
		if (!puppetSpawn1_2 && phasetime_frame >= 228)
		{
			Instantiate(puppet_Prefab, new Vector3(1, 7, 5), Quaternion.identity);
			puppetSpawn1_2 = true;
		}
		if (!puppetSpawn1_3 && phasetime_frame >= 276)
		{
			Instantiate(puppet_Prefab, new Vector3(-1, 7, 5), Quaternion.identity);
			puppetSpawn1_3 = true;
		}
		if (!puppetSpawn1_4 && phasetime_frame >= 324)
		{
			Instantiate(puppet_Prefab, new Vector3(1, 7, 5), Quaternion.identity);
			puppetSpawn1_4 = true;
		}
		if (!puppetSpawn1_5 && phasetime_frame >= 372)
		{
			Instantiate(puppet_Prefab, new Vector3(-1, 7, 5), Quaternion.identity);
			puppetSpawn1_5 = true;
		}
		if (!puppetSpawn1_6 && phasetime_frame >= 480)
		{
			Instantiate(puppet_Prefab, new Vector3(1, 7, 5), Quaternion.identity);
			puppetSpawn1_6 = true;
		}
		if (!Phasespawn9 && phasetime_frame >= 540)
		{
			GameObject enemyObj =
				Instantiate(phone_Prefab);
			RoadObject2D enemy =enemyObj.GetComponent<RoadObject2D>();
			enemy.SetRoadPosition(-0.1f, 5f);
			Phasespawn9 = true;
		}
		if (!Phasespawn11 && phasetime_frame >= 660)
		{
			GameObject enemyObj =
				Instantiate(zenmai_Prefab);
			RoadObject2D enemy =enemyObj.GetComponent<RoadObject2D>();
			enemy.SetRoadPosition(0.1f, 5f);
			Phasespawn11 = true;
		}
		if (!Phasespawn13 && phasetime_frame >= 780)
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
		if (!Phasespawn14 && phasetime_frame >= 840)
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
		if (!Phasespawn16 && phasetime_frame >= 960)
		{
			for (int i = 0; i < 2; i++)
				Instantiate(phone_Prefab, new Vector3(-0.3f+i*-1, 6+i*0.5f, 5), Quaternion.identity);
			Phasespawn16 = true;
		}
		if (!Phasespawn17 && phasetime_frame >= 1020)
		{
			GameObject enemyObj =
				Instantiate(Suit_Prefab);
			RoadObject2D enemy =enemyObj.GetComponent<RoadObject2D>();
			enemy.SetRoadPosition(-0.1f, 5f);
			Phasespawn17 = true;
		}
		if (!Phasespawn19 && phasetime_frame >= 1140)
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
		if (!Phasespawn19_5 && phasetime_frame >= 1170)
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
		if (!Phasespawn21 && phasetime_frame >= 1260)
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
		if (!Phasespawn22 && phasetime_frame >= 1320)
		{
			GameObject enemyObj =
				Instantiate(Suit_Prefab);
			RoadObject2D enemy =
				enemyObj.GetComponent<RoadObject2D>();
			enemy.SetRoadPosition(0.1f, 5f);
			Phasespawn22 = true;
		}
		if (!Phasespawn23_5 && phasetime_frame >= 1410)
		{
			GameObject enemyObj = Instantiate(phone_Prefab);
			RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
			enemy.SetRoadPosition(0f, 5f);
			Phasespawn23_5 = true;
		}
		if (!Phasespawn24 && phasetime_frame >= 1440)
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
		if (!Phasespawn24_5 && phasetime_frame >= 1470)
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



		if (!puppetSpawn2_1 && phasetime_frame >= 1920)
		{
			Instantiate(puppet_Prefab, new Vector3(-1, 7, 5), Quaternion.identity);
			puppetSpawn2_1 = true;
		}
		if (!puppetSpawn2_2 && phasetime_frame >= 1970)
		{
			Instantiate(puppet_Prefab, new Vector3(1, 7, 5), Quaternion.identity);
			puppetSpawn2_2 = true;
		}
		if (!puppetSpawn2_3 && phasetime_frame >= 2020)
		{
			Instantiate(puppet_Prefab, new Vector3(-1, 7, 5), Quaternion.identity);
			puppetSpawn2_3 = true;
		}
		if (!puppetSpawn2_4 && phasetime_frame >= 2070)
		{
			Instantiate(puppet_Prefab, new Vector3(1, 7, 5), Quaternion.identity);
			puppetSpawn2_4 = true;
		}
		if (!puppetSpawn2_5 && phasetime_frame >= 2120)
		{
			Instantiate(puppet_Prefab, new Vector3(-1, 7, 5), Quaternion.identity);
			puppetSpawn2_5 = true;
		}
		if (!puppetSpawn2_6 && phasetime_frame >= 2170)
		{
			Instantiate(puppet_Prefab, new Vector3(1, 7, 5), Quaternion.identity);
			puppetSpawn2_6 = true;
		}



		if (!SuitSpawn1_1 && phasetime_frame >= 2520)
		{
			GameObject enemyObj = Instantiate(Suit_Prefab,
						new Vector3(0, 0, 5),Quaternion.identity);
			RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
			enemy.SetRoadPosition(0f, 5f);
			SuitSpawn1_1 = true;
		}
		if (!SuitSpawn1_2 && phasetime_frame >= 2550)
		{
			GameObject enemyObj = Instantiate(Suit_Prefab,
						new Vector3(0, 0, 5.1f),Quaternion.identity);
			RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
			enemy.SetRoadPosition(0f, 5f);
			SuitSpawn1_2 = true;
		}
		if (!SuitSpawn1_3 && phasetime_frame >= 2580)
		{
			GameObject enemyObj = Instantiate(Suit_Prefab,
						new Vector3(0, 0, 5.2f),Quaternion.identity);
			RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
			enemy.SetRoadPosition(0f, 5f);
			SuitSpawn1_3 = true;
		}
		if (!SuitSpawn1_4 && phasetime_frame >= 2610)
		{
			GameObject enemyObj = Instantiate(Suit_Prefab,
						new Vector3(0, 0, 5.3f),Quaternion.identity);
			RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
			enemy.SetRoadPosition(0f, 5f);
			SuitSpawn1_4 = true;
		}
		if (!SuitSpawn1_5 && phasetime_frame >= 2640)
		{
			GameObject enemyObj = Instantiate(Suit_Prefab,
						new Vector3(0, 0, 5.4f),Quaternion.identity);
			RoadObject2D enemy = enemyObj.GetComponent<RoadObject2D>();
			enemy.SetRoadPosition(0f, 5f);
			SuitSpawn1_5 = true;
		}


		if (!SuitSpawn2_1 && phasetime_frame >= 2760)
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
		if (!SuitSpawn2_2 && phasetime_frame >= 2790)
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
		if (!SuitSpawn2_3 && phasetime_frame >= 2820)
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
		if (!SuitSpawn2_4 && phasetime_frame >= 2850)
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
		if (!SuitSpawn2_5 && phasetime_frame >= 2880)
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
		if (!SuitSpawn2_6 && phasetime_frame >= 2910)
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
		if (!SuitSpawn2_7 && phasetime_frame >= 2940)
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

		if (!SuitSpawn3_1 && phasetime_frame >= 3060)
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
		if (!SuitSpawn3_2 && phasetime_frame >= 3090)
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
		if (!SuitSpawn3_3 && phasetime_frame >= 3120)
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
		if (!SuitSpawn3_4 && phasetime_frame >= 3150)
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
		if (!SuitSpawn3_5 && phasetime_frame >= 3180)
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
		if (!SuitSpawn3_6 && phasetime_frame >= 3210)
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
		if (!SuitSpawn3_7 && phasetime_frame >= 3240)
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
		if (!SuitSpawn3_8 && phasetime_frame >= 3270)
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
		if (!SuitSpawn3_9 && phasetime_frame >= 3300)
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

		if (!EventTrigger && phasetime_frame >= 3600)
		{
			EventTrigger = true;
			CGameManager.Instance.EventTimeline();
		}
		if (!MessageTrigger && phasetime_frame >= 3810)
		{
			MessageTrigger = true;
			txtBox.StartMessege();
		}
    }

	void SetupMonkey(GameObject enemy)
	{
		var monkey = enemy.GetComponent<Cmonkey>();
		monkey.SharedHP = sharedHP;
		monkey.lpGauge = lpGauge;
		monkey.spGauge = spGauge;
	}

	void OnEnable()
	{
		CGameManager.EventEnd += HandleBossTimeline;
	}
	void OnDisable()
	{
		CGameManager.EventEnd += HandleBossTimeline;
	}
	void HandleBossTimeline()
	{
		SetupBoss2(
			Instantiate(Boss2_Prefab, new Vector3(0, 0, 5),Quaternion.identity));
	}
	void SetupBoss2(GameObject enemy)
	{
		var Boss2 = enemy.GetComponent<CBoss2>();
		Boss2.lpGauge = lpGauge;
		Boss2.spGauge = spGauge;
		Boss2.txtBox = txtBox;
	}
}
