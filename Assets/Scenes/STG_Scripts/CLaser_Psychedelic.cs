using UnityEngine;
using System.Collections;

public class CLaser_Psychedelic : MonoBehaviour
{
    float zAngle = 0f;  
    [SerializeField] GameObject Destroy_Bullet_Prefab;

    void Start()
    {
        StartCoroutine(StartDelay());
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(2.0f);
        float x = transform.position.x;
        if (x < 0)
        {
          StartCoroutine(RotationA());
        }
        else
        {
          StartCoroutine(RotationB());
        }
    }

    IEnumerator RotationA()
    {
        float dir = 1f;

        while (true)
        {
            zAngle += dir * 0.5f;

            if (zAngle >= 10f)
            {
                zAngle = 10f;
                dir = -1f;
            }
            else if (zAngle <= -10f)
            {
                zAngle = -10f;
                dir = 1f;
            }

            transform.rotation = Quaternion.Euler(0f, 0f, zAngle);

            for (int f = 0; f < 10; f++) yield return null;
        }
    }
    
    IEnumerator RotationB()
    {
        float dir = -1f;

        while (true)
        {
            zAngle += dir * 0.5f;

            if (zAngle >= 10f)
            {
                zAngle = 10f;
                dir = -1f;
            }
            else if (zAngle <= -10f)
            {
                zAngle = -10f;
                dir = 1f;
            }

            transform.rotation = Quaternion.Euler(0f, 0f, zAngle);

            for (int f = 0; f < 10; f++) yield return null;
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
}
