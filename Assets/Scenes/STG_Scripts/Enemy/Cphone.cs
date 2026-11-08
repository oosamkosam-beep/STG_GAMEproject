using UnityEngine;
using System.Collections;

public class Cphone : MonoBehaviour
{
    private RoadObject2D roadObject;
    [Header("弾")]
    [SerializeField] private GameObject bulletPrefab;

    [Header("アイテム")]
    [SerializeField] private GameObject pointitem_Prefab;
    [SerializeField] private GameObject pointitem_homing_Prefab;

    [Header("撃破エフェクト")]
    [SerializeField] private GameObject Destroy_Enemy_Prefab;

    [Header("移動")]
    private float speed = 0.05f;

    private int Life = 60;
	private const float BULLET_Z = 0f;

    private void Awake()
    {
        roadObject = GetComponent<RoadObject2D>();
    }

    private void Start()
    {
        // 弾発射
        StartCoroutine(ShotBullets());
    }

    private void Update()
    {
        // ========================================
        // Road座標上で奥 → 手前へ移動
        // ========================================

		roadObject.MoveDepth(RoadSpace2D.Autospeed);


        // ========================================
        // 画面外へ出たら削除
        // ========================================

        if (CUtility.IsOut(transform.position))
        {
            Destroy(gameObject);
        }
    }

    // ============================================================
    // 弾発射
    // ============================================================
    IEnumerator ShotBullets()
    {
        yield return new WaitForSeconds(2.0f);

        Transform child = transform.Find("eye");

        if (child != null)
        {
            child.localPosition = new Vector3(0, 0, -0.1f);
        }


        for (int burst = 0; burst < 4; burst++)
        {
            for (int i = 0; i < 6; i++)
            {
                // --------------------------------------------
                // 現在の敵位置から弾を発射
                // --------------------------------------------

                Vector3 shotPos = transform.position;

                shotPos.y += 0.7f;
                shotPos.z = BULLET_Z;

                GameObject bullet =
                    Instantiate(
                        bulletPrefab,
                        shotPos,
                        Quaternion.identity
                    );


                // --------------------------------------------
                // プレイヤー方向
                // --------------------------------------------

                GameObject player =
                    GameObject.FindGameObjectWithTag("Player");

                if (player != null)
                {
                    Vector3 playerPos =
                        player.transform.position;

                    float angle =
                        Mathf.Atan2(
                            playerPos.y - transform.position.y,
                            playerPos.x - transform.position.x
                        );


                    // --------------------------------------------
                    // 弾速・方向
                    // --------------------------------------------

                    bullet
                        .GetComponent<CBullet>()
                        .SetParam(6.0f, angle);
                }


                // 10フレーム待機
                for (int f = 0; f < 10; f++)
                {
                    yield return null;
                }
            }


            // 30フレーム待機
            for (int f = 0; f < 30; f++)
            {
                yield return null;
            }
        }
    }


    // ============================================================
    // 被弾
    // ============================================================

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Shot"))
            return;


        int power = 0;


        var normalShot =
            collision.GetComponent<CShot>();

        var homingShot =
            collision.GetComponent<CShot_homing>();

        var lazerShot =
            collision.GetComponent<CShot_lazer_trigger>();


        if (normalShot != null)
            power = normalShot.ShotPower;

        if (homingShot != null)
            power = homingShot.ShotPower;

        if (lazerShot != null)
            power = lazerShot.ShotPower;


        Life -= power;


        // ========================================================
        // 撃破
        // ========================================================

        if (Life <= 0)
        {
            DestroyEnemy();
        }


        // ========================================================
        // スコア
        // ========================================================

        GameData.score += 10;
        GameData.stage2_total_score += 10;
        GameData.stage2_hitpoint_score += 10;


        Destroy(collision.gameObject);
    }


    // ============================================================
    // 敵撃破処理
    // ============================================================
    private void DestroyEnemy()
    {
        GameData.score += 100;
        GameData.stage2_total_score += 100;
        GameData.stage2_destroy_score += 100;

        // --------------------------------------------
        // 撃破エフェクト
        // --------------------------------------------
        Instantiate(
            Destroy_Enemy_Prefab,
            transform.position,
            Quaternion.identity
        );

        // --------------------------------------------
        // Point Item
        // --------------------------------------------
        float baseAngle =
            -90f * Mathf.Deg2Rad;

        float[] angleOffsets =
        {
            180f * Mathf.Deg2Rad
        };

        foreach (float offset in angleOffsets)
        {
            float angle =
                baseAngle + offset;


            GameObject pointitem =
                Instantiate(
                    pointitem_Prefab,
                    transform.position,
                    Quaternion.identity
                );


            pointitem
                .GetComponent<Cpointitem>()
                .SetParam(angle);
        }


        // --------------------------------------------
        // Homing Point Item
        // --------------------------------------------

        float[] angleOffsets_homing =
        {
            10f * Mathf.Deg2Rad,
            -120f * Mathf.Deg2Rad
        };


        foreach (float offset in angleOffsets_homing)
        {
            float angle =
                baseAngle + offset;


            GameObject pointitem_homing =
                Instantiate(
                    pointitem_homing_Prefab,
                    transform.position,
                    Quaternion.identity
                );


            pointitem_homing
                .GetComponent<Cpointitem_homing>()
                .SetParam(angle);
        }


        Destroy(gameObject);
    }
}
