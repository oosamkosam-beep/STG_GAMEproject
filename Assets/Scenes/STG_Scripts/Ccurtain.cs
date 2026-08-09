using UnityEngine;
using System.Collections;

public class Ccurtain : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject heel_Prefab;
    const float BULLET_Z = 0f;
    bool shotStarted = false;

    float radius;
    float angle;

    void Update()
    {
        if (!shotStarted)
        {
          shotStarted = true;
          StartCoroutine(ShotAfterDelay());
        }
    }

    IEnumerator ShotAfterDelay()
    {
      yield return new WaitForSeconds(3.5f);
      StartCoroutine(Shot10wayBullet());

      yield return new WaitForSeconds(3.0f);
      StartCoroutine(ShotheelALoop());

      yield return new WaitForSeconds(3.5f);
      StartCoroutine(ShotheelBLoop());
    }

    IEnumerator ShotheelALoop()
    {
        while (true)
        {
          StartCoroutine(ShotheelA());
          yield return new WaitForSeconds(7.0f);
        }
    }
    IEnumerator ShotheelBLoop()
    {
        while (true)
        {
          StartCoroutine(ShotheelB());
          yield return new WaitForSeconds(7.0f);
        }
    }

    void Shoot10Way()
    {
      float baseAngle = -90f * Mathf.Deg2Rad; // 真下

      float[] angleOffsets =
      {
        8f * Mathf.Deg2Rad,
        24f * Mathf.Deg2Rad,
        40f * Mathf.Deg2Rad,
        56f * Mathf.Deg2Rad,
        72f * Mathf.Deg2Rad,
        -8f * Mathf.Deg2Rad,
        -24f * Mathf.Deg2Rad,
        -40f * Mathf.Deg2Rad,
        -56f * Mathf.Deg2Rad,
        -72f * Mathf.Deg2Rad,
      };

      foreach (float offset in angleOffsets)
      {
        Vector3 shotPos = new Vector3(0, 5, 0); 

        float angle = baseAngle + offset;

        GameObject bullet = Instantiate(
            bulletPrefab,
            shotPos,
            Quaternion.identity
            );

        bullet.GetComponent<CBullet>().SetParam(5.5f, angle);
      }
    }

    IEnumerator Shot10wayBullet()
    {
      while (true)
      {
        Shoot10Way();

        for (int f = 0; f < 90; f++) yield return null;
      }
    }

    IEnumerator ShotheelA()
    {
        Vector3 shotPos = new Vector3(-3.0f, 5.0f, 0f);
        GameObject bullet = Instantiate(heel_Prefab, shotPos, Quaternion.Euler(0, 0, 90));
        yield break;
    }
    IEnumerator ShotheelB()
    {
        Vector3 shotPos = new Vector3(3.0f, 5.0f, 0f);
        GameObject bullet = Instantiate(heel_Prefab, shotPos, Quaternion.Euler(0, 0, 90));
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
