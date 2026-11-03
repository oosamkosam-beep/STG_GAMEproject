using UnityEngine;

public class CShot_homing : MonoBehaviour
{
    public float Speed = 10.0f;
    public int ShotPower = 1;

    float turnSpeed = 180f; // ★追加：回転速度（度/秒）

    float angle = 90.0f;

    void Update()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");

        if (enemy != null)
        {
            Vector3 dir = enemy.transform.position - transform.position;

            // 目標角度
            float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            // ★ここが重要：ゆっくり角度を変える
            angle = Mathf.MoveTowardsAngle(angle, targetAngle, turnSpeed * Time.deltaTime);
        }

        transform.position += CUtility.GetDirection360(angle) * Speed * Time.deltaTime;

        var rot = transform.localEulerAngles;
        rot.z = angle - 90;
        transform.localEulerAngles = rot;

        if (CUtility.BulletIsOut(transform.position))
        {
            Destroy(gameObject);
        }
        Destroy(gameObject, 2.5f);
    }
}
