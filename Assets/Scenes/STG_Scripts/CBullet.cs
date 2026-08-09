using UnityEngine;

public class CBullet : MonoBehaviour
{
    float Speed;
    float maxSpeed;
    float Angle;

    float accel = 1.0f; 
    bool useAccel = false;
    [SerializeField] GameObject Destroy_Bullet_Prefab;

    public void SetParam(float speed, float angle)
    {
        Speed = speed;
        Angle = angle;

        // 発射時に一度だけ進行方向を向く
        float deg = Angle * Mathf.Rad2Deg - 90f;
        transform.localEulerAngles = new Vector3(0f, 0f, deg);
    }

    //加速を使うときはこっち
    public void SetParamWithAccel(float startSpeed, float targetSpeed, float angle)
    {
        Speed = startSpeed;
        maxSpeed = targetSpeed;
        Angle = angle;
        useAccel = true;

        float deg = Angle * Mathf.Rad2Deg - 90f;
        transform.localEulerAngles = new Vector3(0f, 0f, deg);
    }

    void Update()
    {
        transform.position +=
            CUtility.GetDirectionPI2(Angle) * Speed * Time.deltaTime;

        // 加速処理
        if (useAccel)
        {
            Speed = Mathf.MoveTowards(
                Speed,
                maxSpeed,
                accel * Time.deltaTime
            );
        }
        Vector3 dir = new Vector3(
            Mathf.Cos(Angle),
            Mathf.Sin(Angle),
            0f
        );


        if (CUtility.BulletIsOut(transform.position))
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        CGameManager.ClearBulletsflag += ClearBullets;
    }
    void OnDisable()
    {
        CGameManager.ClearBulletsflag -= ClearBullets;
    }
    public void ClearBullets()
    {
        Instantiate(Destroy_Bullet_Prefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      if(collision.tag == "Bomb")
      {
        Instantiate(Destroy_Bullet_Prefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
      }
    }
}
