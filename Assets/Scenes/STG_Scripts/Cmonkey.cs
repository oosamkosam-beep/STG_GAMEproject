using UnityEngine;
using System;
using System.Collections;

public class Cmonkey : MonoBehaviour
{
	Animator animator;
	Vector3 prevPos;

  [SerializeField] GameObject bulletPrefab_1;
  [SerializeField] GameObject bulletPrefab_2;
	[SerializeField] GameObject bulletPrefab_3;
	[SerializeField] GameObject bulletPrefab_4;
	[SerializeField] GameObject bulletPrefab_5;
	[SerializeField] SharedEnemyHPData sharedHP;
	public CLPGauge lpGauge;
	public CSPGauge spGauge;
	float randomMoveDuration_Phase1 = 1.2f;
	float randomMoveSpeed_Phase1 = 1.0f;
	float randomMoveDuration_Phase3 = 0.4f;
	float randomMoveSpeed_Phase3 = 3.0f;

	int phaseChangeHP = 1500;
	int phaseChangeSP = 0;
	[Header("LP Gauge")]

	public SharedEnemyHPData SharedHP;
  private CGameManager gameManager;
  public static event Action<Cmonkey> OnMonkeyDestroyed;

	const float BULLET_Z = 0f;
	int shotPatternCount = 0;
	bool use5Way = false;
	
	float radialSpeed = 1.0f; // 離れていく速さ（調整用）

	bool isWaitAfterRandom = false;
	float waitTimer = 0f;
	float waitDuration = 1.2f;

	float elapsedTime = 0f;
	bool randomMoveStarted = false;

	bool isRandomMoving = false;
	float randomMoveTimer = 0f;
	Vector3 randomMoveDir;   
	float randomMoveDuration;
	float randomMoveSpeed;

	bool shotInvokedInWait = false;

	// 移動先候補
	Vector3[] points =
	{
	    new Vector3(2,  8, 5),   // A
	    new Vector3(0,  8,  5),   // B
	    new Vector3(-2,  8, 5)   // C
	};
	
	void Start()
	{
		gameManager = FindAnyObjectByType<CGameManager>();	
		animator = GetComponent<Animator>();
		prevPos = transform.position;

		randomMoveDuration = randomMoveDuration_Phase1;
		randomMoveSpeed = randomMoveSpeed_Phase1;
	}

	void Update()
	{
	    elapsedTime += Time.deltaTime;

	    if (sharedHP.currentHP > phaseChangeHP)
	    {

	    }
	    else if (sharedHP.currentSP > phaseChangeSP) 
	    {
		    if (!gameManager.isPhase2Started)
		    {
			    gameManager.isPhase2Started = true;
			    gameManager.ClearBullets();
		    }
		    else
		    {
				    lpGauge.Hide();
				    spGauge.Show();
		    }
	    }
	    else
	    {
		    if (!gameManager.isPhase3Started)
		    {
			    gameManager.isPhase3Started = true;
			    gameManager.ClearBullets();
		    }
		    else
		    {
				    lpGauge.Show();
				    spGauge.Hide();

			    randomMoveDuration = randomMoveDuration_Phase3;
			    randomMoveSpeed = randomMoveSpeed_Phase3;
			    animator.SetFloat("S", 2);
		    }
	    }
	    if (sharedHP.currentHP <= 0) 
	    {
		    gameManager.isPhaseEndStarted = true;
		    gameManager.ClearBullets();

             lpGauge.Hide();

          OnMonkeyDestroyed?.Invoke(this);
          Destroy(gameObject);
          GameData.score += 1000;
	    }

	    // ---------- 4秒後にランダム方向を1回だけ決定 ----------
	    if (!randomMoveStarted && elapsedTime >= 4)
	    {
        randomMoveStarted = true;
        isRandomMoving = true;
        randomMoveTimer = 0f;

            lpGauge.Show();

        Vector3 targetPoint = points[UnityEngine.Random.Range(0, points.Length)];
        randomMoveDir = (targetPoint - transform.position).normalized;
	    }

	    // ---------- 移動処理 ----------
	    if (isRandomMoving)
	    {
		    // ① ABC方向へ 1.4秒移動
		    randomMoveTimer += Time.deltaTime;
		    transform.position += randomMoveDir * randomMoveSpeed * Time.deltaTime;

		    if (randomMoveTimer >= randomMoveDuration)
		    {
			    isRandomMoving = false;
			    isWaitAfterRandom = true;   
			    waitTimer = 0f;
			    
			    shotInvokedInWait = false; 
		    }
	    }
	    else if (isWaitAfterRandom)
	    {
		    waitTimer += Time.deltaTime;

		    // ★★★ 0.6秒経過したら発射 ★★★
		    if (!shotInvokedInWait && waitTimer >= 0.6f)
		    {
			    FirePatternShot();
			    shotInvokedInWait = true;
		    }

		    Vector3 basePoint = new Vector3(0.06f, 12.7f, 5f);
		    Vector3 dir = transform.position - basePoint;

		    if (dir.sqrMagnitude > 0.0001f)
		    {
			    dir = dir.normalized;
			    transform.position += dir * radialSpeed * Time.deltaTime;
		    }

		    if (waitTimer >= waitDuration)
		    {
			    Vector3 targetPoint = points[UnityEngine.Random.Range(0, points.Length)];
			    randomMoveDir = (targetPoint - transform.position).normalized;

			    randomMoveTimer = 0f;
			    isWaitAfterRandom = false;
			    isRandomMoving = true;
		    }
	    }
	    else
	    {
		    // ③ 初期 or 通常の放射移動
		    Vector3 basePoint = new Vector3(0.06f, 12.7f, 5f);
		    Vector3 dir = transform.position - basePoint;

		    if (dir.sqrMagnitude > 0.0001f)
		    {
			    dir = dir.normalized;
			    transform.position += dir * radialSpeed * Time.deltaTime;
		    }
	    }

	    if (CUtility.IsOut(transform.position))
	    {
		Destroy(gameObject);
	    }

	    // ----- 移動量からアニメーション用パラメータ算出 -----
	    Vector3 delta = transform.position - prevPos;
	    // 速度ベースにしたい場合（おすすめ）
	    Vector2 moveDir = delta / Time.deltaTime;
	    // 正規化（-1 ～ 1 に収める）
	    if (moveDir.sqrMagnitude > 0.0001f)
	    {
		    moveDir = moveDir.normalized;
	    }
	    else
	    {
		    moveDir = Vector2.zero;
	    }
	    // Animator に渡す
	    animator.SetFloat("H", moveDir.x);
	    animator.SetFloat("V", moveDir.y);
	    // 次フレーム用
	    prevPos = transform.position;
	}
	
	// ★★★ 弾幕の種類★★★
	void FirePatternShot()
	{
		if (sharedHP.currentHP > phaseChangeHP)
		{
			FirePatternShot_1();
		}
		else if(sharedHP.currentSP > phaseChangeSP) 
		{
			FirePatternShot_2();
		}
		else
		{
			FirePatternShot_3();
		}
	}

	void FirePatternShot_1()
	{
		if (use5Way)
		{
			StartCoroutine(Shot4wayBullet());
		}
		else
		{
			StartCoroutine(Shot5Bullet());
		}

		shotPatternCount++;

		// 2回撃ったら切り替え
		if (shotPatternCount >= 2)
		{
			shotPatternCount = 0;
			use5Way = !use5Way;
		}
	}
	
	void FirePatternShot_2()
	{
		StartCoroutine(Shot3wayBullet());
		StartCoroutine(Shot2wayBullet());
	}

	void FirePatternShot_3()
	{
		StartCoroutine(ShotLow6wayBullet());
		StartCoroutine(ShotLow5wayBullet());
	}

	void Shoot4Way()
	{
		// プレイヤー取得
		var player = GameObject.FindGameObjectWithTag("Player");
		if (player == null) return;

		Vector3 playerPos = player.transform.position;

		// プレイヤー方向を基準角（ラジアン）
		float baseAngle = Mathf.Atan2(
				playerPos.y - transform.position.y,
				playerPos.x - transform.position.x
				);

		// ★ 基準角からのオフセット（ラジアン）
		float[] angleOffsets =
		{
			15f * Mathf.Deg2Rad,
			45f * Mathf.Deg2Rad,
			-15f * Mathf.Deg2Rad,
			-45f * Mathf.Deg2Rad
		};

		foreach (float offset in angleOffsets)
		{
			Vector3 shotPos = transform.position;
			shotPos.y += -1.7f;
			shotPos.z = BULLET_Z;

			float angle = baseAngle + offset;

			GameObject bullet = Instantiate(
					bulletPrefab_2,
					shotPos,
					Quaternion.identity
					);

			// 速度 7.0f、方向 angle
			bullet.GetComponent<CBullet>().SetParam(7.0f, angle);
		}
	}
	
	void Shoot3Way()
	{
		var player = GameObject.FindGameObjectWithTag("Player");
		if (player == null) return;

		Vector3 playerPos = player.transform.position;

		float baseAngle = Mathf.Atan2(
				playerPos.y - transform.position.y,
				playerPos.x - transform.position.x
				);

		float[] angleOffsets =
		{
			0f * Mathf.Deg2Rad,
			40f * Mathf.Deg2Rad,
			-40f * Mathf.Deg2Rad
		};

		foreach (float offset in angleOffsets)
		{
			Vector3 shotPos = transform.position;
			shotPos.y += -1.7f;
			shotPos.z = BULLET_Z;

			float angle = baseAngle + offset;

			GameObject bullet = Instantiate(
					bulletPrefab_3,
					shotPos,
					Quaternion.identity
					);

			bullet.GetComponent<CBullet>().SetParam(5.5f, angle);
		}
	}

	void Shoot2Way()
	{
		var player = GameObject.FindGameObjectWithTag("Player");
		if (player == null) return;

		Vector3 playerPos = player.transform.position;

		float baseAngle = Mathf.Atan2(
				playerPos.y - transform.position.y,
				playerPos.x - transform.position.x
				);

		float[] angleOffsets =
		{
			20f * Mathf.Deg2Rad,
			-20f * Mathf.Deg2Rad
		};

		foreach (float offset in angleOffsets)
		{
			Vector3 shotPos = transform.position;
			shotPos.y += -1.7f;
			shotPos.z = BULLET_Z;

			float angle = baseAngle + offset;

			GameObject bullet = Instantiate(
					bulletPrefab_4,
					shotPos,
					Quaternion.identity
					);

			bullet.GetComponent<CBullet>().SetParam(5.0f, angle);
		}
	}

	void ShootLow4Way()
	{
		var player = GameObject.FindGameObjectWithTag("Player");
		if (player == null) return;

		Vector2 playerPos = player.transform.position;

		float baseAngle = Mathf.Atan2(
				playerPos.y - transform.position.y,
				playerPos.x - transform.position.x
				);

		float[] angleOffsets =
		{
			15f * Mathf.Deg2Rad,
			45f * Mathf.Deg2Rad,
			-15f * Mathf.Deg2Rad,
			-45f * Mathf.Deg2Rad,
		};

		foreach (float offset in angleOffsets)
		{
			Vector3 shotPos = transform.position;
			shotPos.y += -1.7f;
			shotPos.z = BULLET_Z + 0.1f;

			float angle = baseAngle + offset;

			GameObject bullet = Instantiate(
					bulletPrefab_4,
					shotPos,
					Quaternion.identity
					);

			bullet.GetComponent<CBullet>().SetParam(1.2f, angle);
		}
	}

	void ShootLow3Way()
	{
		var player = GameObject.FindGameObjectWithTag("Player");
		if (player == null) return;

		Vector2 playerPos = player.transform.position;

		float baseAngle = Mathf.Atan2(
				playerPos.y - transform.position.y,
				playerPos.x - transform.position.x
				);

		float[] angleOffsets =
		{
			0f * Mathf.Deg2Rad,
			30f * Mathf.Deg2Rad,
			-30f * Mathf.Deg2Rad,
		};

		foreach (float offset in angleOffsets)
		{
			Vector3 shotPos = transform.position;
			shotPos.y += -1.7f;
			shotPos.z = BULLET_Z;

			float angle = baseAngle + offset;

			GameObject bullet = Instantiate(
					bulletPrefab_5,
					shotPos,
					Quaternion.identity
					);

			bullet.GetComponent<CBullet>().SetParam(1.5f, angle);
		}
	}


	IEnumerator Shot4wayBullet()
	{
		for (int burst = 0; burst < 1; burst++)
		{
			for (int i = 0; i < 3; i++)
			{
				Shoot4Way();

				for (int f = 0; f < 40; f++) yield return null;//バースト間隔
			}

			for (int f = 0; f < 35; f++) yield return null;
		}
	}
	
	IEnumerator Shot3wayBullet()
	{
		for (int burst = 0; burst < 1; burst++)
		{
			for (int i = 0; i < 1; i++)
			{
				Shoot3Way();
				for (int f = 0; f < 15; f++) yield return null;//バースト間隔
			}

			for (int f = 0; f < 35; f++) yield return null;
		}
	}

	IEnumerator Shot2wayBullet()
	{
		for (int burst = 0; burst < 1; burst++)
		{
			for (int i = 0; i < 1; i++)
			{
				Shoot2Way();

				for (int f = 0; f < 15; f++) yield return null;//バースト間隔
			}

			for (int f = 0; f < 35; f++) yield return null;
		}
	}

	IEnumerator ShotLow6wayBullet()
	{
		for (int burst = 0; burst < 1; burst++)
		{
			for (int i = 0; i < 1; i++)
			{
				ShootLow4Way();

				for (int f = 0; f < 80; f++) yield return null;//バースト間隔
			}

			for (int f = 0; f < 35; f++) yield return null;
		}
	}
	
	IEnumerator ShotLow5wayBullet()
	{
		for (int burst = 0; burst < 1; burst++)
		{
			for (int i = 0; i < 2; i++)
			{
				ShootLow3Way();

				for (int f = 0; f < 120; f++) yield return null;//バースト間隔
			}

			for (int f = 0; f < 90; f++) yield return null;
		}
	}
	
	void ApplyDamage(int damage)
	{
		if (sharedHP.currentHP > phaseChangeHP)
		{
			sharedHP.TakeDamage(damage);
		}
		else if (sharedHP.currentSP > phaseChangeSP) 
		{
			sharedHP.TakeDamage_2(damage);

		}
		else
		{
			sharedHP.TakeDamage(damage);
		}
	}

	IEnumerator Shot5Bullet()
	{
		for (int burst = 0; burst < 1; burst++)
		{
			for (int i = 0; i < 5; i++)
			{

				Vector3 shotPos = transform.position;
				shotPos.y +=  -1.7f;
				shotPos.z = BULLET_Z;

				GameObject bullet = Instantiate(bulletPrefab_1, shotPos, Quaternion.identity);

				Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
				float angle = Mathf.Atan2(
						playerPos.y - transform.position.y,
						playerPos.x - transform.position.x
						);

				//速度、方向
				bullet.GetComponent<CBullet>().SetParam(10.0f, angle);

				for (int f = 0; f < 15; f++) yield return null;

			}

			for (int f = 0; f < 15; f++) yield return null;
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
	    if (collision.CompareTag("Shot"))
	    {
         int power = 0;

         var shot = collision.GetComponent<CShot>();
         var homing = collision.GetComponent<CShot_homing>();
         var lazerShot = collision.GetComponent<CShot_lazer_trigger>();
         if (shot != null) power = shot.ShotPower;
         if (homing != null) power = homing.ShotPower;
         if (lazerShot != null) power = lazerShot.ShotPower;

         if (power > 0 && SharedHP != null)
         {
            ApplyDamage(power); // ★共有HPを減らす
         }
         GameData.score += 10;
         GameData.stage2_total_score += 10;
         GameData.stage2_hitpoint_score += 10;
         Destroy(collision.gameObject);
	    }
	}
}
