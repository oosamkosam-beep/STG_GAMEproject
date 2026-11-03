using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGameManager : MonoBehaviour
{
	public static CGameManager Instance;

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

	public bool isPhase2Started = false;
	public bool isPhase3Started = false;
	public bool isPhaseEndStarted = false;

	[SerializeField] CLPGauge lpGauge;   
	[SerializeField] CSPGauge spGauge;   
	[SerializeField] CtxtBox txtBox;
	[SerializeField] SharedEnemyHPData sharedHP;

	// ===== 時間管理 =====
	double gameTime = 0;
	double phaseTime = 0;
	double phaseStartTime = -1;
	public double GameTime => gameTime;
	public double PhaseTime => phaseTime;
	public bool IsPhaseActive => phaseStartTime >= 0;
	public bool isPaused = false;

	public static event Action OnPhaseStarted;
	public static event Action StartEvent;
	public static event Action EventEnd;
	public static event Action ClearBulletsflag;
	bool EventTrigger;
	bool MessageTrigger;
	bool BossTrigger;

	// ===== 出現フラグ =====
	bool spawn3, spawn7, spawn12, spawn13;
	bool spawn19_5;
	bool spawn21_5, spawn28, spawn29, spawn34_5;
	bool spawn46, spawn59, spawn80;
	bool spawn96, spawn98;
	bool puppetSpawn1_1, puppetSpawn1_2 ,puppetSpawn1_3 ,puppetSpawn1_4 ,puppetSpawn1_5 ,puppetSpawn1_6;
	bool Phasespawn9, Phasespawn11, Phasespawn13, Phasespawn14; 
	bool Phasespawn16, Phasespawn17, Phasespawn19, Phasespawn19_5, Phasespawn21, Phasespawn22; 
	bool Phasespawn23_5, Phasespawn24, Phasespawn24_5; 
	bool puppetSpawn2_1, puppetSpawn2_2 ,puppetSpawn2_3 ,puppetSpawn2_4 ,puppetSpawn2_5 ,puppetSpawn2_6;
	bool SuitSpawn1_1, SuitSpawn1_2, SuitSpawn1_3, SuitSpawn1_4, SuitSpawn1_5; 
	bool SuitSpawn2_1, SuitSpawn2_2, SuitSpawn2_3, SuitSpawn2_4, SuitSpawn2_5, SuitSpawn2_6, SuitSpawn2_7;
	bool SuitSpawn3_1, SuitSpawn3_2, SuitSpawn3_3, SuitSpawn3_4, SuitSpawn3_5, SuitSpawn3_6, SuitSpawn3_7, SuitSpawn3_8, SuitSpawn3_9;


	void Awake()
	{
		Instance = this;
	}
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
		float dt = Time.deltaTime;
		gameTime += dt;
		if (phaseStartTime >= 0)
		{
			phaseTime += dt;
		}

		Time.timeScale = isPaused ? 0f : 1f;
		if (Input.GetButtonDown("Cancel"))
		{
			isPaused = !isPaused;
		}

		HandleGlobalTimeline();
		if (phaseStartTime >= 0)
		{
			HandlePhaseTimeline();
		}
	}


	void HandleGlobalTimeline()
	{
		if (!spawn3 && GameTime >= 3.0)
		{
			GameObject enemyObj =
				Instantiate(Suit_Prefab);

			RoadObject2D enemy =
				enemyObj.GetComponent<RoadObject2D>();

			enemy.SetRoadPosition(
					-5f,  // 左
					5.2f    // 奥
					);
			spawn3 = true;
		}

///		if (!spawn3 && GameTime >= 3.0)
///		{
///			Instantiate(phone_Prefab, new Vector3(-1, 7, 5), Quaternion.identity);
///			spawn3 = true;
///		}
///
///		if (!spawn7 && GameTime >= 7.0)
///		{
///			Instantiate(zenmai_Prefab, new Vector3(1, 7, 5), Quaternion.identity);
///			spawn7 = true;
///		}
///
///		if (!spawn12 && GameTime >= 12.0)
///		{
///			for (int i = 0; i < 3; i++)
///				Instantiate(phone_Prefab, new Vector3(-0.3f + i * -0.4f, 6 + i * 0.5f, 5), Quaternion.identity);
///			spawn12 = true;
///		}
///
///		if (!spawn13 && GameTime >= 13.0)
///		{
///			for (int i = 0; i < 3; i++)
///				Instantiate(phone_Prefab, new Vector3(0.3f + i * 0.4f, 6 + i * 0.5f, 5), Quaternion.identity);
///			spawn13 = true;
///		}
///
///		if (!spawn19_5 && GameTime >= 19.5)
///		{
///			Debug.Log("Instantiate_barcord");
///			Instantiate(barcord_Prefab, new Vector3(2.6f, 6.2f, 8), Quaternion.identity);
///			Instantiate(barcord_Prefab, new Vector3(3.6f, 6.2f, 8), Quaternion.identity);
///			spawn19_5 = true;
///		}
///
///		if (!spawn21_5 && GameTime >= 21.5)
///		{
///			Instantiate(phone_Prefab, new Vector3(-2, 7, 5), Quaternion.identity);
///			spawn21_5 = true;
///		}
///
///		if (!spawn29 && GameTime >= 29)
///		{
///			Instantiate(barcord_Prefab, new Vector3(2.6f, 6.2f, 8), Quaternion.identity);
///			Instantiate(barcord_Prefab, new Vector3(3.6f, 6.2f, 8), Quaternion.identity);
///			spawn29 = true;
///		}
///
///		if (!spawn28 && GameTime >= 28)
///		{
///			for (int i = 0; i < 3; i++)
///			{
///				Instantiate(phone_Prefab, new Vector3(-0.5f + i * -0.5f, 6 + i * 0.5f, 5), Quaternion.identity);
///				Instantiate(phone_Prefab, new Vector3(0.5f + i * 0.5f, 6 + i * 0.5f, 5), Quaternion.identity);
///			}
///			spawn28 = true;
///		}
///
///		if (!spawn34_5 && GameTime >= 34.5)
///		{
///			for (int i = 0; i < 2; i++)
///				Instantiate(zenmai_Prefab, new Vector3(-1.5f+i*3.0f, 7, 5), Quaternion.identity);
///			spawn34_5 = true;
///		}
///
///		if (!spawn46 && GameTime >= 41)
///		{
///			Instantiate(shopping_Prefab, new Vector3(0, 7, 5), Quaternion.identity);
///			spawn46 = true;
///		}
///
///		if (!spawn59 && GameTime >= 59)
///		{
///			for (int i = 0; i < 2; i++)
///				Instantiate(shopping_Prefab, new Vector3(-1 + i * 2, 7, 5), Quaternion.identity);
///			spawn59 = true;
///		}
///
///		if (!spawn80 && GameTime >= 80.6)
///		{
///			Debug.Log("80.6");
///			Instantiate(unitA_Prefab, new Vector3(-5, 7, 5), Quaternion.identity);
///			Instantiate(unitB_Prefab, new Vector3(5, 7, 5), Quaternion.identity);
///			spawn80 = true;
///		}
///
///		if (!spawn96 && GameTime >= 96)
///		{
///			SetupMonkey(
///					Instantiate(Monkey_Prefab, new Vector3(1, 6.5f, 5), Quaternion.Euler(0, 180, 0)));
///			spawn96 = true;
///		}
///
///		if (!spawn98 && GameTime >= 98)
///		{
///			SetupMonkey(
///					Instantiate(Monkey_Prefab, new Vector3(-1, 6.5f, 5), Quaternion.identity));
///			spawn98 = true;
///		}
	}

	void HandlePhaseTimeline()
	{
		if (!puppetSpawn1_1 && PhaseTime >= 3.0)
		{
			Instantiate(puppet_Prefab, new Vector3(-1, 7, 5), Quaternion.identity);
			puppetSpawn1_1 = true;
		}
		if (!puppetSpawn1_2 && PhaseTime >= 3.8)
		{
			Instantiate(puppet_Prefab, new Vector3(1, 7, 5), Quaternion.identity);
			puppetSpawn1_2 = true;
		}
		if (!puppetSpawn1_3 && PhaseTime >= 4.6)
		{
			Instantiate(puppet_Prefab, new Vector3(-1, 7, 5), Quaternion.identity);
			puppetSpawn1_3 = true;
		}
		if (!puppetSpawn1_4 && PhaseTime >= 5.4)
		{
			Instantiate(puppet_Prefab, new Vector3(1, 7, 5), Quaternion.identity);
			puppetSpawn1_4 = true;
		}
		if (!puppetSpawn1_5 && PhaseTime >= 6.2)
		{
			Instantiate(puppet_Prefab, new Vector3(-1, 7, 5), Quaternion.identity);
			puppetSpawn1_5 = true;
		}
		if (!puppetSpawn1_6 && PhaseTime >= 8.0)
		{
			Instantiate(puppet_Prefab, new Vector3(1, 7, 5), Quaternion.identity);
			puppetSpawn1_6 = true;
		}



		if (!Phasespawn9 && PhaseTime >= 9.0)
		{
			Instantiate(phone_Prefab, new Vector3(-1, 7, 5), Quaternion.identity);
			Phasespawn9 = true;
		}
		if (!Phasespawn11 && PhaseTime >= 11.0)
		{
			Instantiate(zenmai_Prefab, new Vector3(1, 7, 5), Quaternion.identity);
			Phasespawn11 = true;
		}
		if (!Phasespawn13 && PhaseTime >= 13.0)
		{
			for (int i = 0; i < 2; i++)
				Instantiate(phone_Prefab, new Vector3(-0.4f+i*1.6f, 6+i*0.4f, 5), Quaternion.identity);
			Phasespawn13 = true;
		}
		if (!Phasespawn14 && PhaseTime >= 14.0)
		{
			for (int i = 0; i < 2; i++)
				Instantiate(zenmai_Prefab, new Vector3(0.4f+i*-1.6f, 6+i*0.4f, 5), Quaternion.identity);
			Phasespawn14 = true;
		}
		if (!Phasespawn16 && PhaseTime >= 16.0)
		{
			for (int i = 0; i < 2; i++)
				Instantiate(phone_Prefab, new Vector3(-0.3f+i*-1, 6+i*0.5f, 5), Quaternion.identity);
			Phasespawn16 = true;
		}
		if (!Phasespawn17 && PhaseTime >= 17.0)
		{
			Instantiate(Suit_Prefab, new Vector3(0.8f, 7, 5), Quaternion.identity);
			Phasespawn17 = true;
		}
		if (!Phasespawn19 && PhaseTime >= 19.0)
		{
			for (int i = 0; i < 2; i++)
				Instantiate(zenmai_Prefab, new Vector3(-0.5f+i*-0.8f, 6+i*0.5f, 5), Quaternion.identity);
			Phasespawn19 = true;
		}
		if (!Phasespawn19_5 && PhaseTime >= 19.5)
		{
			for (int i = 0; i < 2; i++)
				Instantiate(zenmai_Prefab, new Vector3(0.5f+i*0.8f, 6+i*0.5f, 5), Quaternion.identity);
			Phasespawn19_5 = true;
		}
		if (!Phasespawn21 && PhaseTime >= 21.0)
		{
			for (int i = 0; i < 2; i++)
				Instantiate(phone_Prefab, new Vector3(0.3f+i*1, 6+i*0.5f, 5), Quaternion.identity);
			Phasespawn21 = true;
		}
		if (!Phasespawn22 && PhaseTime >= 22.0)
		{
			Instantiate(Suit_Prefab, new Vector3(-0.8f, 7, 5), Quaternion.identity);
			Phasespawn22 = true;
		}
		if (!Phasespawn23_5 && PhaseTime >= 23.5)
		{
			Instantiate(phone_Prefab, new Vector3(0, 7, 5), Quaternion.identity);
			Phasespawn23_5 = true;
		}
		if (!Phasespawn24 && PhaseTime >= 24.0)
		{
			for (int i = 0; i < 2; i++)
				Instantiate(zenmai_Prefab, new Vector3(-0.8f+i*1.6f, 7, 5), Quaternion.identity);
			Phasespawn24 = true;
		}
		if (!Phasespawn24_5 && PhaseTime >= 24.5)
		{
			for (int i = 0; i < 2; i++)
				Instantiate(phone_Prefab, new Vector3(-1.6f+i*3.2f, 7, 5), Quaternion.identity);
			Phasespawn24_5 = true;
		}



		if (!puppetSpawn2_1 && PhaseTime >= 32.0)
		{
			Instantiate(puppet_Prefab, new Vector3(-1, 7, 5), Quaternion.identity);
			puppetSpawn2_1 = true;
		}
		if (!puppetSpawn2_2 && PhaseTime >= 32.8)
		{
			Instantiate(puppet_Prefab, new Vector3(1, 7, 5), Quaternion.identity);
			puppetSpawn2_2 = true;
		}
		if (!puppetSpawn2_3 && PhaseTime >= 33.6)
		{
			Instantiate(puppet_Prefab, new Vector3(-1, 7, 5), Quaternion.identity);
			puppetSpawn2_3 = true;
		}
		if (!puppetSpawn2_4 && PhaseTime >= 34.4)
		{
			Instantiate(puppet_Prefab, new Vector3(1, 7, 5), Quaternion.identity);
			puppetSpawn2_4 = true;
		}
		if (!puppetSpawn2_5 && PhaseTime >= 35.2)
		{
			Instantiate(puppet_Prefab, new Vector3(-1, 7, 5), Quaternion.identity);
			puppetSpawn2_5 = true;
		}
		if (!puppetSpawn2_6 && PhaseTime >= 36.0)
		{
			Instantiate(puppet_Prefab, new Vector3(1, 7, 5), Quaternion.identity);
			puppetSpawn2_6 = true;
		}



		if (!SuitSpawn1_1 && PhaseTime >= 42.0)
		{
			Instantiate(Suit_Prefab, new Vector3(0, 7, 5),Quaternion.identity);
			SuitSpawn1_1 = true;
		}
		if (!SuitSpawn1_2 && PhaseTime >= 42.3)
		{
			Instantiate(Suit_Prefab, new Vector3(0, 7, 5.1f),Quaternion.identity);
			SuitSpawn1_2 = true;
		}
		if (!SuitSpawn1_3 && PhaseTime >= 42.6)
		{
			Instantiate(Suit_Prefab, new Vector3(0, 7, 5.2f),Quaternion.identity);
			SuitSpawn1_3 = true;
		}
		if (!SuitSpawn1_4 && PhaseTime >= 42.9)
		{
			Instantiate(Suit_Prefab, new Vector3(0, 7, 5.3f),Quaternion.identity);
			SuitSpawn1_4 = true;
		}
		if (!SuitSpawn1_5 && PhaseTime >= 43.2)
		{
			Instantiate(Suit_Prefab, new Vector3(0, 7, 5.4f),Quaternion.identity);
			SuitSpawn1_5 = true;
		}


		if (!SuitSpawn2_1 && PhaseTime >= 46.0)
		{
			for (int i = 0; i < 2; i++)
				Instantiate(Suit_Prefab, new Vector3(-1+5*2, 7, 5),Quaternion.identity);
			SuitSpawn2_1 = true;
		}
		if (!SuitSpawn2_2 && PhaseTime >= 46.3)
		{
			for (int i = 0; i < 2; i++)
				Instantiate(Suit_Prefab, new Vector3(-1+i*2, 7, 5.1f),Quaternion.identity);
			SuitSpawn2_2 = true;
		}
		if (!SuitSpawn2_3 && PhaseTime >= 46.6)
		{
			for (int i = 0; i < 2; i++)
				Instantiate(Suit_Prefab, new Vector3(-1+i*2, 7, 5.2f),Quaternion.identity);
			SuitSpawn2_3 = true;
		}
		if (!SuitSpawn2_4 && PhaseTime >= 46.9)
		{
			for (int i = 0; i < 2; i++)
				Instantiate(Suit_Prefab, new Vector3(-1+i*2, 7, 5.3f),Quaternion.identity);
			SuitSpawn2_4 = true;
		}
		if (!SuitSpawn2_5 && PhaseTime >= 47.2)
		{
			for (int i = 0; i < 2; i++)
				Instantiate(Suit_Prefab, new Vector3(-1+i*2, 7, 5.4f),Quaternion.identity);
			SuitSpawn2_5 = true;
		}
		if (!SuitSpawn2_6 && PhaseTime >= 47.5)
		{
			for (int i = 0; i < 2; i++)
				Instantiate(Suit_Prefab, new Vector3(-1+i*2, 7, 5.4f),Quaternion.identity);
			SuitSpawn2_6 = true;
		}
		if (!SuitSpawn2_7 && PhaseTime >= 47.7)
		{
			for (int i = 0; i < 2; i++)
				Instantiate(Suit_Prefab, new Vector3(-1+i*2, 7, 5.4f),Quaternion.identity);
			SuitSpawn2_7 = true;
		}

		if (!SuitSpawn3_1 && PhaseTime >= 50.0)
		{
			for (int i = 0; i < 3; i++)
				Instantiate(Suit_Prefab, new Vector3(-1.5f+i*1.5f, 7, 5),Quaternion.identity);
			SuitSpawn3_1 = true;
		}
		if (!SuitSpawn3_2 && PhaseTime >= 50.3)
		{
			for (int i = 0; i < 3; i++)
				Instantiate(Suit_Prefab, new Vector3(-1.5f+i*1.5f, 7, 5.1f),Quaternion.identity);
			SuitSpawn3_2 = true;
		}
		if (!SuitSpawn3_3 && PhaseTime >= 50.6)
		{
			for (int i = 0; i < 3; i++)
				Instantiate(Suit_Prefab, new Vector3(-1.5f+i*1.5f, 7, 5.2f),Quaternion.identity);
			SuitSpawn3_3 = true;
		}
		if (!SuitSpawn3_4 && PhaseTime >= 50.9)
		{
			for (int i = 0; i < 3; i++)
				Instantiate(Suit_Prefab, new Vector3(-1.5f+i*1.5f, 7, 5.3f),Quaternion.identity);
			SuitSpawn3_4 = true;
		}
		if (!SuitSpawn3_5 && PhaseTime >= 51.2)
		{
			for (int i = 0; i < 3; i++)
				Instantiate(Suit_Prefab, new Vector3(-1.5f+i*1.5f, 7, 5.4f),Quaternion.identity);
			SuitSpawn3_5 = true;
		}
		if (!SuitSpawn3_6 && PhaseTime >= 51.5)
		{
			for (int i = 0; i < 3; i++)
				Instantiate(Suit_Prefab, new Vector3(-1.5f+i*1.5f, 7, 5.4f),Quaternion.identity);
			SuitSpawn3_6 = true;
		}
		if (!SuitSpawn3_7 && PhaseTime >= 51.8)
		{
			for (int i = 0; i < 3; i++)
				Instantiate(Suit_Prefab, new Vector3(-1.5f+i*1.5f, 7, 5.4f),Quaternion.identity);
			SuitSpawn3_7 = true;
		}
		if (!SuitSpawn3_8 && PhaseTime >= 52.1)
		{
			for (int i = 0; i < 3; i++)
				Instantiate(Suit_Prefab, new Vector3(-1.5f+i*1.5f, 7, 5.4f),Quaternion.identity);
			SuitSpawn3_8 = true;
		}
		if (!SuitSpawn3_9 && PhaseTime >= 52.4)
		{
			for (int i = 0; i < 3; i++)
				Instantiate(Suit_Prefab, new Vector3(-1.5f+i*1.5f, 7, 5.4f),Quaternion.identity);
			SuitSpawn3_9 = true;
		}

		if (IsPhaseActive && !EventTrigger && PhaseTime >= 60.0)
		{
			EventTrigger = true;
			StartEvent?.Invoke();
		}
		if (IsPhaseActive && !MessageTrigger && PhaseTime >= 63.5)
		{
			MessageTrigger = true;
			txtBox.StartMessege();
		}
	}

	public void HandleBossTimeline()
	{
		if (!BossTrigger)
		{
			SetupBoss2(
					Instantiate(Boss2_Prefab, new Vector3(0, 0, 0),Quaternion.identity));
			EventEnd?.Invoke();
		}
	}

	void OnEnable()
	{
		Cmonkey.OnMonkeyDestroyed += HandleMonkeyDestroyed;
	}

	void OnDisable()
	{
		Cmonkey.OnMonkeyDestroyed -= HandleMonkeyDestroyed;
	}

	void HandleMonkeyDestroyed(Cmonkey monkey)
	{
		phaseStartTime = GameTime;
		phaseTime = 0;
		ResetPhaseSpawnFlags();
		OnPhaseStarted?.Invoke();//映像切り替え
	}

	void ResetPhaseSpawnFlags()
	{
		puppetSpawn1_1 = false;puppetSpawn1_2 = false;puppetSpawn1_3 = false;puppetSpawn1_4 = false;puppetSpawn1_5 = false;puppetSpawn1_6 = false;

		Phasespawn9 = false;Phasespawn11 = false;Phasespawn13 = false;Phasespawn14 = false; 
		Phasespawn16 = false;Phasespawn17 = false;Phasespawn19 = false;Phasespawn19_5 = false;Phasespawn21 = false;Phasespawn22 = false; 
		Phasespawn23_5 = false;Phasespawn24 = false;Phasespawn24_5 = false; 

		puppetSpawn2_1 = false;puppetSpawn2_2 = false;puppetSpawn2_3 = false;puppetSpawn2_4 = false;puppetSpawn2_5 = false;puppetSpawn2_6 = false;

		SuitSpawn1_1 = false;SuitSpawn1_2 = false;SuitSpawn1_3 = false;SuitSpawn1_4 = false;SuitSpawn1_5 = false;
		SuitSpawn2_1 = false;SuitSpawn2_2 = false;SuitSpawn2_3 = false;SuitSpawn2_4 = false;SuitSpawn2_5 = false;SuitSpawn2_6 = false;SuitSpawn2_7 = false;
		SuitSpawn3_1 = false;SuitSpawn3_2 = false;SuitSpawn3_3 = false;SuitSpawn3_4 = false;SuitSpawn3_5 = false;SuitSpawn3_6 = false;SuitSpawn3_7 = false;SuitSpawn3_8 = false;SuitSpawn3_9 = false;
	}

	void SetupMonkey(GameObject enemy)
	{
		var monkey = enemy.GetComponent<Cmonkey>();
		monkey.SharedHP = sharedHP;
		monkey.lpGauge = lpGauge;
		monkey.spGauge = spGauge;
	}
	void SetupBoss2(GameObject enemy)
	{
		var Boss2 = enemy.GetComponent<CBoss2>();
		Boss2.lpGauge = lpGauge;
		Boss2.spGauge = spGauge;
		Boss2.txtBox = txtBox;
	}

	public void PauseGame()
	{
		isPaused = true;
	}
	public void ResumeGame()
	{
		isPaused = false;
	}

	public void ClearBullets()
	{
		StartCoroutine(ClearBulletsForSeconds(0.8f));
	}

	IEnumerator ClearBulletsForSeconds(float duration)
	{
		float timer = 0f;

		while (timer < duration)
		{
			ClearBulletsflag?.Invoke();
			timer += Time.deltaTime;
			yield return null;
		}
	}
}
