using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGameManager : MonoBehaviour
{
	public static CGameManager Instance;

	public bool isPhase2Started = false;
	public bool isPhase3Started = false;
	public bool isPhaseEndStarted = false;

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

	public void MonkeyDestroyed()
	{
		phaseStartTime = GameTime;
		phaseTime = 0;
		OnPhaseStarted?.Invoke();//映像切り替え
	}
	public void EventTimeline()
	{
		StartEvent?.Invoke();
	}
	public void EventEndTimeline()
	{
		EventEnd?.Invoke();
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
