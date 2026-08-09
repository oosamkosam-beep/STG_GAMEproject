using UnityEngine;

public class CShot_lazer : MonoBehaviour
{
    [SerializeField] GameObject Destroy_Shot_Prefab;
    [SerializeField] GameObject Shot_lazer_trigger;

    private bool hasHit = false;
    private Vector3 bestHitPos;
    private float minY = float.MaxValue;

    void Start()
    {
        Destroy(gameObject, 0.2f);
    }

    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;

        Vector3 hitPos = collision.ClosestPoint(transform.position);

        // 一番yが小さいものだけ保存
        if (hitPos.y < minY)
        {
            minY = hitPos.y;
            bestHitPos = hitPos;
            hasHit = true;
        }
    }

    private void LateUpdate()
    {
        // そのフレームの最後に1回だけ生成
        if (hasHit)
        {
            GameObject cloneEffect = Instantiate(Destroy_Shot_Prefab);
            GameObject clone_trigger = Instantiate(Shot_lazer_trigger);
            cloneEffect.transform.position = bestHitPos;
            clone_trigger.transform.position = bestHitPos;

            hasHit = false;
            minY = float.MaxValue;
            Destroy(gameObject);
        }
    }
}
