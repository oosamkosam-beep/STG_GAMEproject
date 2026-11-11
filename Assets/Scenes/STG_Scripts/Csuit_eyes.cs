using UnityEngine;
using System.Collections;

public class Csuit_eyes : MonoBehaviour
{
    [SerializeField] GameObject bullet_Prefab;
    [SerializeField] GameObject bullet2_Prefab;
    [SerializeField] GameObject fugu_Prefab;
    [SerializeField] GameObject laser_Prefab;
    const float BULLET_Z = 0f;
    bool shotStarted = false;

    float radius;
    float angle;

    void Update()
    {
        if (!shotStarted)
        {
          shotStarted = true;

          float x = transform.position.x;
          float y = transform.position.y;

          if (y > 1.0f)
          {
            if (x < 0)
            {
              if (x < -2.5f)
              {
                StartCoroutine(ShotWinder3_A());
              }
              else
              {
                if (x < -1.5f)
                {
                  StartCoroutine(ShotLaser());
                }
                else
                {
                }
              }
            }
            else
            {
              if (x > 2.5f)
              {
                StartCoroutine(ShotWinder3_B());
              }
              else
              {
                if (x > 1.5f)
                {
                  StartCoroutine(ShotLaser());
                }
                else
                {
                }
              }
            }
          }
          else
          {
            if (y > 0f)
            {
              if (x < 0)
              {
                if (x < -3.0f)
                {
                }
                else
                {
                  if (x < -1.5f)
                  {
                  }
                  else
                  {
                    StartCoroutine(ShotWinder1_A());
                  }
                }
              }
              else
              {
                if (x > 3.0f)
                {
                }
                else
                {
                  if (x > 1.5f)
                  {
                  }
                  else
                  {
                    StartCoroutine(ShotWinder1_B());
                  }
                }
              }
            }
            else
            {
              if (y > -1f)
              {
                if (x < 0)
                {
                  if (x < -3.0f)
                  {
                  }
                  else
                  {
                    if (x < -1f)
                    {
                      StartCoroutine(ShotWinder2_A());
                    }
                    else
                    {
                    }
                  }
                }
                else
                {
                  if (x > 3.0f)
                  {
                  }
                  else
                  {
                    if (x > 1f)
                    {
                      StartCoroutine(ShotWinder2_B());
                    }
                    else
                    {
                    }
                  }
                }
              }
              else
              {
                if (y > -1.5f)
                {
                  if (x < 0)
                  {
                    if (x < -3.0f)
                    {
                    }
                    else
                    {
                      StartCoroutine(Shot2WinderA());
                    }
                  }
                  else
                  {
                    if (x > 3.0f)
                    {
                    }
                    else
                    {
                      StartCoroutine(Shot2WinderB());
                    }
                  }
                }
                else
                {
                  if (y > -2.5f)
                  {
                    if (x < 0)
                    {
                      if (x < -3.0f)
                      {
                        StartCoroutine(ShotBulletALoop());
                      }
                      else
                      {
                      }
                    }
                    else
                    {
                      if (x > 3.0f)
                      {
                        StartCoroutine(ShotBulletBLoop());
                      }
                      else
                      {
                      }
                    }
                  }
                  else
                  {
                    if (x < 0)
                    {
                      if (x < -3.0f)
                      {
                      }
                      else
                      {
                      }
                    }
                    else
                    {
                      if (x > 3.0f)
                      {
                      }
                      else
                      {
                      }
                    }
                  }
                }
              }
            }
          }
        }
    }


    IEnumerator ShotWinder1_A()
    {
        float baseAngle = -110f;   // 真下（度）
        float offsetDeg = -15.0f;    // 開始角度
        float dir = 0.6f;           // 進行方向（+1 or -1）

        while (true)
        {
            Vector3 shotPos = transform.position;
            shotPos.y += 0.5f;
            shotPos.z = BULLET_Z +0.05f;

            float angleRad = (baseAngle + offsetDeg) * Mathf.Deg2Rad;

            GameObject bullet = Instantiate(
                fugu_Prefab,
                shotPos,
                Quaternion.identity
            );

            bullet.GetComponent<CBullet>().SetParam(5.5f, angleRad);

            // 次の角度を更新
            offsetDeg += dir;

            // 反転
            if (offsetDeg >= 15.0f)
            {
                offsetDeg = 15.0f;
                dir = -0.6f;
            }
            else if (offsetDeg <= -15.0f)
            {
                offsetDeg = -15.0f;
                dir = 0.6f;
            }
            for (int f = 0; f < 10; f++) yield return null;
        }
    }
    IEnumerator ShotWinder1_B()
    {
        float baseAngle = -70f;   // 真下（度）
        float offsetDeg = 15.0f;    // 開始角度
        float dir = 0.6f;           // 進行方向（+1 or -1）

        while (true)
        {
            Vector3 shotPos = transform.position;
            shotPos.y += 0.5f;
            shotPos.z = BULLET_Z +0.05f;

            float angleRad = (baseAngle + offsetDeg) * Mathf.Deg2Rad;

            GameObject bullet = Instantiate(
                fugu_Prefab,
                shotPos,
                Quaternion.identity
            );

            bullet.GetComponent<CBullet>().SetParam(5.5f, angleRad);

            offsetDeg += dir;

            if (offsetDeg >= 15.0f)
            {
                offsetDeg = 15.0f;
                dir = -0.6f;
            }
            else if (offsetDeg <= -15.0f)
            {
                offsetDeg = -15.0f;
                dir = 0.6f;
            }
            for (int f = 0; f < 10; f++) yield return null;
        }
    }

    IEnumerator ShotWinder2_A()
    {
        float baseAngle = -25f;   // 真下（度）
        float offsetDeg = 7.5f;    // 開始角度
        float dir = -0.3f;           // 進行方向（+1 or -1）

        while (true)
        {
            Vector3 shotPos = transform.position;
            shotPos.x += 0.2f;
            shotPos.y += 0.8f;
            shotPos.z = BULLET_Z +0.05f;

            float angleRad = (baseAngle + offsetDeg) * Mathf.Deg2Rad;

            GameObject bullet = Instantiate(
                fugu_Prefab,
                shotPos,
                Quaternion.identity
            );

            bullet.GetComponent<CBullet>().SetParam(5.5f, angleRad);

            offsetDeg += dir;

            if (offsetDeg >= 7.5f)
            {
                offsetDeg = 7.5f;
                dir = -0.3f;
            }
            else if (offsetDeg <= -7.5f)
            {
                offsetDeg = -7.5f;
                dir = 0.3f;
            }
            for (int f = 0; f < 10; f++) yield return null;
        }
    }
    IEnumerator ShotWinder2_B()
    {
        float baseAngle = -155f;   
        float offsetDeg = -7.5f;   
        float dir = 0.3f;         

        while (true)
        {
            Vector3 shotPos = transform.position;
            shotPos.x -= 0.2f;
            shotPos.y += 0.8f;
            shotPos.z = BULLET_Z +0.05f;

            float angleRad = (baseAngle + offsetDeg) * Mathf.Deg2Rad;

            GameObject bullet = Instantiate(
                fugu_Prefab,
                shotPos,
                Quaternion.identity
            );
            bullet.GetComponent<CBullet>().SetParam(5.5f, angleRad);

            offsetDeg += dir;

            if (offsetDeg >= 7.5f)
            {
                offsetDeg = 7.5f;
                dir = -0.3f;
            }
            else if (offsetDeg <= -7.5f)
            {
                offsetDeg = -7.5f;
                dir = 0.3f;
            }
            for (int f = 0; f < 10; f++) yield return null;
        }
    }

    IEnumerator ShotWinder3_A()
    {
        float baseAngle = -80f;   // 真下（度）
        float offsetDeg = 15.0f;    // 開始角度
        float dir = -0.6f;           // 進行方向（+1 or -1）

        while (true)
        {
            Vector3 shotPos = transform.position;
            shotPos.y += 0.5f;
            shotPos.z = BULLET_Z +0.05f;

            float angleRad = (baseAngle + offsetDeg) * Mathf.Deg2Rad;

            GameObject bullet = Instantiate(
                fugu_Prefab,
                shotPos,
                Quaternion.identity
            );

            bullet.GetComponent<CBullet>().SetParam(5.5f, angleRad);

            offsetDeg += dir;

            if (offsetDeg >= 15.0f)
            {
                offsetDeg = 15.0f;
                dir = -0.6f;
            }
            else if (offsetDeg <= -15.0f)
            {
                offsetDeg = -15.0f;
                dir = 0.6f;
            }
            for (int f = 0; f < 10; f++) yield return null;
        }
    }
    IEnumerator ShotWinder3_B()
    {
        float baseAngle = -100f;   // 真下（度）
        float offsetDeg = -15.0f;    // 開始角度
        float dir = 0.6f;           // 進行方向（+1 or -1）

        while (true)
        {
            Vector3 shotPos = transform.position;
            shotPos.y += 0.5f;
            shotPos.z = BULLET_Z +0.05f;

            float angleRad = (baseAngle + offsetDeg) * Mathf.Deg2Rad;

            GameObject bullet = Instantiate(
                fugu_Prefab,
                shotPos,
                Quaternion.identity
            );

            bullet.GetComponent<CBullet>().SetParam(5.5f, angleRad);

            offsetDeg += dir;

            if (offsetDeg >= 15.0f)
            {
                offsetDeg = 15.0f;
                dir = -0.6f;
            }
            else if (offsetDeg <= -15.0f)
            {
                offsetDeg = -15.0f;
                dir = 0.6f;
            }
            for (int f = 0; f < 10; f++) yield return null;
        }
    }


    void ShotBullet()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        Vector3 playerPos = player.transform.position;

        float baseAngle = Mathf.Atan2(
          playerPos.y - transform.position.y -0.9f,
          playerPos.x - transform.position.x
          );

        float[] angleOffsets =
        {
          10f * Mathf.Deg2Rad,
          -10f * Mathf.Deg2Rad
        };

        foreach (float offset in angleOffsets)
        {
          Vector3 shotPos = transform.position;
          shotPos.y += 0.9f;
          shotPos.z = BULLET_Z;

          float angle = baseAngle + offset;

          GameObject bullet = Instantiate(
              bullet_Prefab,
              shotPos,
              Quaternion.identity
              );

          bullet.GetComponent<CBullet>().SetParam(2.5f, angle);
        }
    }
    IEnumerator ShotBulletALoop()
    {
        while (true)
        {
          ShotBullet();
          yield return new WaitForSeconds(4.0f);
        }
    }
    IEnumerator ShotBulletBLoop()
    {
        while (true)
        {
          yield return new WaitForSeconds(2.0f);
          ShotBullet();
          yield return new WaitForSeconds(2.0f);
        }
    }


    IEnumerator Shot2WinderA()
    {
        float baseAngle = -35f;   // 真下（度）
        float offsetDeg = -60f;    // 開始角度
        float dir = 30f;           // 進行方向（+1 or -1）

        while (true)
        {
            Vector3 shotPos = transform.position;
            shotPos.y += 0.9f;
            shotPos.z = BULLET_Z;

            float angleRad = (baseAngle + offsetDeg) * Mathf.Deg2Rad;

            GameObject bullet = Instantiate(
                bullet2_Prefab,
                shotPos,
                Quaternion.identity
            );

            bullet.GetComponent<CBullet>().SetParam(2.0f, angleRad);

            // 次の角度を更新
            offsetDeg += dir;

            // 反転
            if (offsetDeg >= 60.0f)
            {
                offsetDeg = 60.0f;
                dir = -30f;
            }
            else if (offsetDeg <= -60.0f)
            {
                offsetDeg = -60.0f;
                dir = 30f;
            }
            for (int f = 0; f < 50; f++) yield return null;
        }
    }
    IEnumerator Shot2WinderB()
    {
        float baseAngle = -145f;   // 真下（度）
        float offsetDeg = 0f;    // 開始角度
        float dir = -30f;           // 進行方向（+1 or -1）

        while (true)
        {
            Vector3 shotPos = transform.position;
            shotPos.y += 0.9f;
            shotPos.z = BULLET_Z;

            float angleRad = (baseAngle + offsetDeg) * Mathf.Deg2Rad;

            GameObject bullet = Instantiate(
                bullet2_Prefab,
                shotPos,
                Quaternion.identity
            );

            bullet.GetComponent<CBullet>().SetParam(2.0f, angleRad);

            offsetDeg += dir;

            if (offsetDeg >= 60.0f)
            {
                offsetDeg = 60.0f;
                dir = -30f;
            }
            else if (offsetDeg <= -60.0f)
            {
                offsetDeg = -60.0f;
                dir = 30f;
            }
            for (int f = 0; f < 50; f++) yield return null;
        }
    }

    IEnumerator ShotLaser()
    {
        Vector3 shotPos = transform.position;
        shotPos.y += 0.9f;
        shotPos.z = BULLET_Z +0.1f;

        GameObject laser = Instantiate(laser_Prefab, shotPos, Quaternion.identity);

        yield break;
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
        Destroy(gameObject);
    }
}
