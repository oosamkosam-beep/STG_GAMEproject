using UnityEngine;

public class CBullet_test : MonoBehaviour
{
    float Speed = 5.0f;
    float Angle = 0.0f;

    public void SetParam(float speed, float angle)
    {
        Speed = speed;
        Angle = angle;

        // 発射時に一度だけ進行方向を向く
        float deg = Angle * Mathf.Rad2Deg - 90f;
        transform.localEulerAngles = new Vector3(0f, 0f, deg);
    }

    void Update()
    {
        // 移動のみ
        transform.position +=
            CUtility.GetDirectionPI2(Angle) * Speed * Time.deltaTime;

        if (CUtility.IsOut(transform.position))
        {
            Destroy(gameObject);
        }
    }
}
