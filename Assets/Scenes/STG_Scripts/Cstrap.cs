using UnityEngine;
using System.Collections;

public class Cstrap : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    const float BULLET_Z = 0f;
    bool shotStarted = false;

    float circumference = 48.9f;
    float maxSpeed = 4.0f;
    float accelTime = 9.0f;
    float startDelay = 1.5f;

    float radius;
    float angle;

    Vector3 centerPos = new Vector3(0f, 11.6f, 0f);

    void Start()
    {
        radius = circumference / (2f * Mathf.PI);
        StartCoroutine(MoveCircle());
    }

    IEnumerator MoveCircle()
    {
        yield return new WaitForSeconds(startDelay);

        if (transform.position.y > 11.5f)
        {
          if (transform.position.x > -7.5f)
          {
            if (transform.position.x > -7.0f)
            {
              if (transform.position.x > -6.0f)
              {
                if (transform.position.x > -5.0f)
                {
                  if (transform.position.x > -3.0f)
                  {
                    if (transform.position.x > -1.5f)
                    {
                      if (transform.position.x > 0f)
                      {
                        if (transform.position.x > 2.0f)
                        {
                          if (transform.position.x > 4.0f)
                          {
                            if (transform.position.x > 6.0f)
                            {
                              if (transform.position.x > 7.0f)
                              {
                                if (transform.position.x > 7.3f)
                                {
                                    angle = 0f * Mathf.Deg2Rad;       
                                }
                                else
                                {
                                    angle = 15f * Mathf.Deg2Rad;       
                                }
                              }
                              else
                              {
                                  angle = 30f * Mathf.Deg2Rad;       
                              }
                            }
                            else
                            {
                                angle = 45f * Mathf.Deg2Rad;       
                            }
                          }
                          else
                          {
                              angle = 60f * Mathf.Deg2Rad;       
                          }
                        }
                        else
                        {
                            angle = 75f * Mathf.Deg2Rad;       
                        }
                      }
                      else
                      {
                          angle = 90f * Mathf.Deg2Rad;       
                      }
                    }
                    else
                    {
                        angle = 105f * Mathf.Deg2Rad;       
                    }
                  }
                  else
                  {
                      angle = 120f * Mathf.Deg2Rad;       
                  }
                }
                else
                {
                    angle = 135f * Mathf.Deg2Rad;       
                }
              }
              else
              {
                  angle = 150f * Mathf.Deg2Rad;       
              }
            }
            else
            {
                angle = 165f * Mathf.Deg2Rad;       
            }
          }
          else
          {
              angle = 180f * Mathf.Deg2Rad;       
          }
        }
        else
        {
          if (transform.position.x > -7.5f)
          {
            if (transform.position.x > -7.0f)
            {
              if (transform.position.x > -6.0f)
              {
                if (transform.position.x > -5.0f)
                {
                  if (transform.position.x > -3.0f)
                  {
                    if (transform.position.x > -1.5f)
                    {
                      if (transform.position.x > 0f)
                      {
                        if (transform.position.x > 2.0f)
                        {
                          if (transform.position.x > 4.0f)
                          {
                            if (transform.position.x > 6.0f)
                            {
                              if (transform.position.x > 7.0f)
                              {
                                if (transform.position.x > 7.3f)
                                {
                                    angle = 360f * Mathf.Deg2Rad;       
                                }
                                else
                                {
                                    angle = 345f * Mathf.Deg2Rad;       
                                }
                              }
                              else
                              {
                                  angle = 330f * Mathf.Deg2Rad;       
                              }
                            }
                            else
                            {
                                angle = 315f * Mathf.Deg2Rad;       
                            }
                          }
                          else
                          {
                              angle = 300f * Mathf.Deg2Rad;       
                          }
                        }
                        else
                        {
                            angle = 285f * Mathf.Deg2Rad;       
                        }
                      }
                      else
                      {
                          angle = 270f * Mathf.Deg2Rad;       
                      }
                    }
                    else
                    {
                        angle = 255f * Mathf.Deg2Rad;       
                    }
                  }
                  else
                  {
                      angle = 240f * Mathf.Deg2Rad;       
                  }
                }
                else
                {
                    angle = 225f * Mathf.Deg2Rad;       
                }
              }
              else
              {
                  angle = 210f * Mathf.Deg2Rad;       
              }
            }
            else
            {
                angle = 195f * Mathf.Deg2Rad;       
            }
          }
          else
          {
              angle = 180f * Mathf.Deg2Rad;       
          }
        }

        if (!shotStarted)
        {
          shotStarted = true;
          StartCoroutine(ShotAfterDelay());
        }

        float accelTimer = 0f;
        float currentSpeed = 0f;

        while (true)
        {
            accelTimer += Time.deltaTime;
            currentSpeed = Mathf.Lerp(0f, maxSpeed, accelTimer / accelTime);

            angle += (currentSpeed / radius) * Time.deltaTime;

            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            transform.position = centerPos + new Vector3(x, y, 0f);

            // ★ 速度に応じてZ回転
            float t = Mathf.Clamp01(currentSpeed / maxSpeed);
            float zRot = Mathf.Lerp(0f, -12f, t);
            transform.rotation = Quaternion.Euler(0f, 0f, zRot);

            yield return null;
        }
    }

    IEnumerator ShotAfterDelay()
    {
      yield return new WaitForSeconds(1.0f);
      StartCoroutine(Shot3wayBullet());
    }

    void Shoot3Way()
    {
      float baseAngle = -90f * Mathf.Deg2Rad; // 真下

      float[] angleOffsets =
      {
        10f * Mathf.Deg2Rad,
        -10f * Mathf.Deg2Rad
      };

      foreach (float offset in angleOffsets)
      {
        Vector3 shotPos = transform.position;
        shotPos.y -= 1.5f;
        shotPos.z = BULLET_Z;

        float angle = baseAngle + offset;

        GameObject bullet = Instantiate(
            bulletPrefab,
            shotPos,
            Quaternion.identity
            );

        bullet.GetComponent<CBullet>().SetParam(5.0f, angle);
      }
    }

    IEnumerator Shot3wayBullet()
    {
      while (true)
      {
        Shoot3Way();

        for (int f = 0; f < 90; f++) yield return null;
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
        Destroy(gameObject);
    }
}
