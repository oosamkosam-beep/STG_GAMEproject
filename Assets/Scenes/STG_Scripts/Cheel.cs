using UnityEngine;
using System.Collections;

public class Cheel : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    const float BULLET_Z = 0f;
    bool endphase = false;
    bool shotpattern = false;

    Animator animator;

    void Start()
    {
       animator = GetComponent<Animator>();
    }

    void Update()
    {
        float x = transform.position.x;

        if (x < 0f)
        {
          animator.SetFloat("H",0f);
        }
        else
        {
          animator.SetFloat("H",1f);
          shotpattern = true;
        }

        if (!endphase)
        {
          endphase = true;
          StartCoroutine(DestroyAfterDelay());
        }
    }

    IEnumerator DestroyAfterDelay()
    {
      yield return new WaitForSeconds(3.0f);

      if (!shotpattern)
      {
        FirePatternA();
      }
      else 
      {
        FirePatternB();
      }

      Destroy(gameObject);
    }

    // ★★★ 弾幕の種類★★★
    void FirePatternA()
    {
        StartCoroutine(ShotA_1Bullet());
        StartCoroutine(ShotA_2Bullet());
        StartCoroutine(ShotA_3Bullet());
        StartCoroutine(ShotA_4Bullet());
        StartCoroutine(ShotA_5Bullet());
    }
    void FirePatternB()
    {
        StartCoroutine(ShotB_1Bullet());
        StartCoroutine(ShotB_2Bullet());
        StartCoroutine(ShotB_3Bullet());
        StartCoroutine(ShotB_4Bullet());
        StartCoroutine(ShotB_5Bullet());
    }

    IEnumerator ShotA_1Bullet()
    {
        Vector3 shotPos = new Vector3(2.2f, -3.7f, 0f);

        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        GameObject bullet = Instantiate(
            bulletPrefab,
            shotPos,
            Quaternion.identity
            );

        bullet.GetComponent<CBullet>().SetParamWithAccel(
            0f,        // startSpeed
            10.0f,      // maxSpeed
            angle
        );
        yield break;
    }
    IEnumerator ShotA_2Bullet()
    {
        Vector3 shotPos = new Vector3(1.7f, -2.3f, 0f);

        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        GameObject bullet = Instantiate(
            bulletPrefab,
            shotPos,
            Quaternion.identity
            );

        bullet.GetComponent<CBullet>().SetParamWithAccel(
            0f,        // startSpeed
            10.0f,      // maxSpeed
            angle
        );
        yield break;
    }
    IEnumerator ShotA_3Bullet()
    {
        Vector3 shotPos = new Vector3(1.3f, -1.0f, 0f);

        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        GameObject bullet = Instantiate(
            bulletPrefab,
            shotPos,
            Quaternion.identity
            );

        bullet.GetComponent<CBullet>().SetParamWithAccel(
            0f,        // startSpeed
            10.0f,      // maxSpeed
            angle
        );
        yield break;
    }
    IEnumerator ShotA_4Bullet()
    {
        Vector3 shotPos = new Vector3(0.9f, 0.5f, 0f);

        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        GameObject bullet = Instantiate(
            bulletPrefab,
            shotPos,
            Quaternion.identity
            );

        bullet.GetComponent<CBullet>().SetParamWithAccel(
            0f,        // startSpeed
            10.0f,      // maxSpeed
            angle
        );
        yield break;
    }
    IEnumerator ShotA_5Bullet()
    {
        Vector3 shotPos = new Vector3(0.5f, 1.9f, 0f);

        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        GameObject bullet = Instantiate(
            bulletPrefab,
            shotPos,
            Quaternion.identity
            );

        bullet.GetComponent<CBullet>().SetParamWithAccel(
            0f,        // startSpeed
            10.0f,      // maxSpeed
            angle
        );
        yield break;
    }


    IEnumerator ShotB_1Bullet()
    {
        Vector3 shotPos = new Vector3(-1.4f, -3.9f, 0f);

        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        GameObject bullet = Instantiate(
            bulletPrefab,
            shotPos,
            Quaternion.identity
            );

        bullet.GetComponent<CBullet>().SetParamWithAccel(
            0f,        // startSpeed
            10.0f,      // maxSpeed
            angle
        );
        yield break;
    }
    IEnumerator ShotB_2Bullet()
    {
        Vector3 shotPos = new Vector3(-0.9f, -2.5f, 0f);

        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        GameObject bullet = Instantiate(
            bulletPrefab,
            shotPos,
            Quaternion.identity
            );

        bullet.GetComponent<CBullet>().SetParamWithAccel(
            0f,        // startSpeed
            10.0f,      // maxSpeed
            angle
        );
        yield break;
    }
    IEnumerator ShotB_3Bullet()
    {
        Vector3 shotPos = new Vector3(-0.5f, -1.2f, 0f);

        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        GameObject bullet = Instantiate(
            bulletPrefab,
            shotPos,
            Quaternion.identity
            );

        bullet.GetComponent<CBullet>().SetParamWithAccel(
            0f,        // startSpeed
            10.0f,      // maxSpeed
            angle
        );
        yield break;
    }
    IEnumerator ShotB_4Bullet()
    {
        Vector3 shotPos = new Vector3(0.3f, 0.1f, 0f);

        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        GameObject bullet = Instantiate(
            bulletPrefab,
            shotPos,
            Quaternion.identity
            );

        bullet.GetComponent<CBullet>().SetParamWithAccel(
            0f,        // startSpeed
            10.0f,      // maxSpeed
            angle
        );
        yield break;
    }
    IEnumerator ShotB_5Bullet()
    {
        Vector3 shotPos = new Vector3(0f, 1.0f, 0f);

        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        GameObject bullet = Instantiate(
            bulletPrefab,
            shotPos,
            Quaternion.identity
            );

        bullet.GetComponent<CBullet>().SetParamWithAccel(
            0f,        // startSpeed
            10.0f,      // maxSpeed
            angle
        );
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
