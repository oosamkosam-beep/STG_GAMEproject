using UnityEngine;
using System.Collections;

public class Cshopping : MonoBehaviour
{
  [SerializeField] GameObject bulletPrefab;
  [SerializeField] GameObject bulletPrefab_2;
  [SerializeField] GameObject pointitem_Prefab;
  [SerializeField] GameObject pointitem_homing_Prefab;
  [SerializeField] GameObject Destroy_Enemy_Prefab;
	int Life = 6000;

	const float BULLET_Z = 0f;
	Animator animator;

	float normalRadialSpeed = 1.0f;   // 放射移動
	float fastRadialSpeed = 1.2f;     // 最初の3秒だけ速い

	float elapsedTime = 0f;

	// ランダム移動用
	bool isRandomMoving = false;
	float randomMoveTimer = 0f;
	float randomMoveDuration = 1.2f;
	
	// 通常移動に戻る待機用
	bool isWaitAfterRandom = false;
	bool firstMoveWaitDone = false;
	float waitTimer = 0f;
	
	int randomMoveCount = 0;   // 点移動を何回やったか
	bool delayAnimPlayed = false;
	
	int specialMoveIndex = 7;      // 7回目で特殊移動

	float normalMoveDuration = 1.5f;
	float specialMoveDuration = 10.0f;

	Vector3 specialPoint = new Vector3(0.06f, 12.7f, 5f);

	Vector3 randomMoveDir;   // ← 方向だけ保存
	float randomMoveSpeed = 1.5f; // ← 好きな速さ

	float shotRotateOffsetDeg = 0f;   // 発射ごとの回転角（度）
	float shotRadius = 0.9f; // 円の半径（調整用）

	// 移動先候補
	Vector3[] points =
	{
	    new Vector3(0.5f,  3f, 5f),   // A
	    new Vector3(-1f,  1.5f,  5f),   // B
	    new Vector3(1.7f,  2f, 5f)   // C
	};

	void Start()
	{
		animator = GetComponent<Animator>();
	}

	void StartRandomMove()
	{
		randomMoveTimer = 0f;
		isRandomMoving = true;

		randomMoveCount++;

		animator.Play("shoppingRotateAnimation");

		// ★ 点移動が始まった瞬間に弾を撃つ
		StartCoroutine(Shot8wayBullets());

		// 7回目は特別
		if (randomMoveCount == specialMoveIndex)
		{
			randomMoveDir = (specialPoint - transform.position).normalized;
			randomMoveDuration = specialMoveDuration;
		}
		else
		{
			Vector3 targetPoint = points[Random.Range(0, points.Length)];
			randomMoveDir = (targetPoint - transform.position).normalized;
			randomMoveDuration = normalMoveDuration;
		}
	}

	void Update()
	{
	    elapsedTime += Time.deltaTime;

	    // ---------- 3秒後にランダム方向を1回だけ決定 ----------
	    if (elapsedTime >= 2.5f && !firstMoveWaitDone)
	    {
		    isWaitAfterRandom = true;
		    waitTimer = 0f;
		    delayAnimPlayed = false;

		    firstMoveWaitDone = true;

		    animator.Play("shoppingDelayAnimation");
	    }

	    if (randomMoveTimer >= randomMoveDuration)
	    {
		    randomMoveTimer = 0f;
		    isRandomMoving = false;
		    isWaitAfterRandom = true;
		    waitTimer = 0f;
		    delayAnimPlayed = false;
		    
		    animator.Play("shoppingIdleAnimation");
	    }

        // ---------- 移動処理 ----------
	if (isRandomMoving)
	{
	    // ① ABC方向へ 1.5秒移動
	    randomMoveTimer += Time.deltaTime;
	    transform.position += randomMoveDir * randomMoveSpeed * Time.deltaTime;

	    if (isWaitAfterRandom)
	    {
		    waitTimer += Time.deltaTime;

		    float waitDuration = 0.5f;

		    // ★ 3回目・6回目の後だけ1秒待機
		    if (randomMoveCount == 2 || randomMoveCount == 4 || randomMoveCount == 6)
		    {
			    waitDuration = 1.0f;

			    // ★ 残り0.5秒になったら DelayAnimation 再生
			    if (!delayAnimPlayed && waitTimer >= 0.5f)
			    {
				    animator.Play("shoppingDelayAnimation");
				    delayAnimPlayed = true;
			    }
		    }

		    if (waitTimer >= waitDuration)
		    {
			    isWaitAfterRandom = false;
			    StartRandomMove();
		    }
	    }
	}

	else if (isWaitAfterRandom)
	{
		waitTimer += Time.deltaTime;

		bool isLongWait = (randomMoveCount == 2 || randomMoveCount == 4 || randomMoveCount == 6);
		float currentWait = isLongWait ? 1.0f : 0.5f;

		// ★ 待機に入った瞬間は必ず Idle
		if (waitTimer <= Time.deltaTime)
		{
			animator.Play("shoppingIdleAnimation");
			delayAnimPlayed = false;
		}

		// ★ 通常待機（0.5秒）はすぐ Delay
		if (!isLongWait && !delayAnimPlayed)
		{
			animator.Play("shoppingDelayAnimation");
			delayAnimPlayed = true;
		}

		// ★ 長待機（1秒）は後半0.5秒だけ Delay
		if (isLongWait && !delayAnimPlayed && waitTimer >= 0.5f)
		{
			animator.Play("shoppingDelayAnimation");
			delayAnimPlayed = true;
		}

		// 放射移動
		Vector3 basePoint = new Vector3(0.06f, 12.7f, 5f);
		Vector3 dir = transform.position - basePoint;

		if (dir.sqrMagnitude > 0.0001f)
		{
			dir = dir.normalized;
			float speed = (elapsedTime < 2.5f) ? fastRadialSpeed : normalRadialSpeed;
			transform.position += dir * speed * Time.deltaTime;
		}

		if (waitTimer >= currentWait)
		{
			waitTimer = 0f;
			isWaitAfterRandom = false;
			delayAnimPlayed = false;
			StartRandomMove();
		}
	}
	else
	{
		if (elapsedTime < 2.5f)
		{
			animator.Play("shoppingWalkAnimation");
		}

		Vector3 basePoint = new Vector3(0.06f, 12.7f, 5f);
		Vector3 dir = transform.position - basePoint;

		if (dir.sqrMagnitude > 0.0001f)
		{
			dir = dir.normalized;

			float currentRadialSpeed =
				(elapsedTime < 2.5f) ? fastRadialSpeed : normalRadialSpeed;

			transform.position += dir * currentRadialSpeed * Time.deltaTime;
		}
	}
	    if (CUtility.IsOut(transform.position))
	    {
          Destroy(gameObject);
	    }
	}

    // ★★★ 弾幕の種類★★★
    void Shoot4Way()
    {
	    var player = GameObject.FindGameObjectWithTag("Player");
	    if (player == null) return;

	    Vector3 playerPos = player.transform.position;

	    float baseAngle = Mathf.Atan2(
			    playerPos.y - transform.position.y,
			    playerPos.x - transform.position.x
			    );

	    // ★ 発射ごとの回転オフセット（ラジアン）
	    float rotateRad = shotRotateOffsetDeg * Mathf.Deg2Rad;

	    float[] angleOffsets =
	    {
		    0f * Mathf.Deg2Rad,
		    180f * Mathf.Deg2Rad,
	    };
	    foreach (float offset in angleOffsets)
	    {
		    float angle = baseAngle + rotateRad + offset;

		    Vector3 shotPos = transform.position;
		    shotPos.x += Mathf.Cos(angle) * shotRadius;
		    shotPos.y += Mathf.Sin(angle) * shotRadius;
		    shotPos.z = BULLET_Z;

		    GameObject bullet = Instantiate(
				    bulletPrefab,
				    shotPos,
				    Quaternion.identity
				    );

		    bullet.GetComponent<CBullet>().SetParam(3.0f, angle);
	    }
	    // ★ 次の発射に向けて +回転
	    shotRotateOffsetDeg += 25f;
    }

    void Shoot4Way_2()
    {
	    var player = GameObject.FindGameObjectWithTag("Player");
	    if (player == null) return;

	    Vector3 playerPos = player.transform.position;

	    float baseAngle = Mathf.Atan2(
			    playerPos.y - transform.position.y,
			    playerPos.x - transform.position.x
			    );

	    // ★ 発射ごとの回転オフセット（ラジアン）
	    float rotateRad = shotRotateOffsetDeg * Mathf.Deg2Rad;

	    float[] angleOffsets =
	    {
		    90f * Mathf.Deg2Rad,
		    270f * Mathf.Deg2Rad,
	    };
	    foreach (float offset in angleOffsets)
	    {
		    float angle = baseAngle + rotateRad + offset;

		    Vector3 shotPos = transform.position;
		    shotPos.x += Mathf.Cos(angle) * shotRadius;
		    shotPos.y += Mathf.Sin(angle) * shotRadius;
		    shotPos.z = BULLET_Z;

		    GameObject bullet = Instantiate(
				    bulletPrefab_2,
				    shotPos,
				    Quaternion.identity
				    );

		    bullet.GetComponent<CBullet>().SetParam(3.0f, angle);
	    }
	    shotRotateOffsetDeg += 25f;
    }

    // ★★★ 2連射 × 1セット ★★★
    IEnumerator Shot8wayBullets()
    {
        for (int burst = 0; burst < 1; burst++)
        {
            for (int i = 0; i < 16; i++)
            {
                Shoot4Way();
                Shoot4Way_2();

                for (int f = 0; f < 18; f++) yield return null;//バースト間隔
            }

            for (int f = 0; f < 35; f++) yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Shot")
        {
            int power = 0;
            var normalShot = collision.GetComponent<CShot>();
            var homingShot = collision.GetComponent<CShot_homing>();
            var lazerShot = collision.GetComponent<CShot_lazer_trigger>();

            if (normalShot != null) power = normalShot.ShotPower;
            if (homingShot != null) power = homingShot.ShotPower;
            if (lazerShot != null) power = lazerShot.ShotPower;

            Life -= power;

            if (Life <= 0) 
            {
              Destroy(gameObject);
              GameData.score += 100;
              GameData.stage2_total_score += 100;
              GameData.stage2_destroy_score += 100;
              Instantiate(Destroy_Enemy_Prefab, transform.position, Quaternion.identity);

              float baseAngle = -90f * Mathf.Deg2Rad; // 真下

              float[] angleOffsets =
              {
                180f * Mathf.Deg2Rad
              };
              foreach (float offset in angleOffsets)
              {
                float angle = baseAngle + offset;
                GameObject pointitem = Instantiate(pointitem_Prefab, transform.position, Quaternion.identity);
                pointitem.GetComponent<Cpointitem>().SetParam(angle);
              }

              float[] angleOffsets_homing =
              {
                10f * Mathf.Deg2Rad,
                -120f * Mathf.Deg2Rad
              };
              foreach (float offset in angleOffsets_homing)
              {
                float angle = baseAngle + offset;
                GameObject pointitem_homing = Instantiate(pointitem_homing_Prefab, transform.position, Quaternion.identity);
                pointitem_homing.GetComponent<Cpointitem_homing>().SetParam(angle);
              }
            }
            Destroy(collision.gameObject);
            GameData.score += 10;
            GameData.stage2_total_score += 10;
            GameData.stage2_hitpoint_score += 10;
        }
    }
}
