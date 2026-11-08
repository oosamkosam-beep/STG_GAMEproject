using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGameManager : MonoBehaviour
{
	public static CGameManager Instance;

	public GameObject Boss2_Prefab;

	public bool isPhase2Started = false;
	public bool isPhase3Started = false;
	public bool isPhaseEndStarted = false;

	[SerializeField] CLPGauge lpGauge;   
	[SerializeField] CSPGauge spGauge;   
	[SerializeField] CtxtBox txtBox;

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
	bool BossTrigger;

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
	}

	public void EventTimeline()
	{
		StartEvent?.Invoke();
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
		OnPhaseStarted?.Invoke();//映像切り替え
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
