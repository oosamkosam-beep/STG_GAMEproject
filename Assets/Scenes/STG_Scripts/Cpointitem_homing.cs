using UnityEngine;
using System.Collections;

public class Cpointitem_homing : MonoBehaviour
{
    private Transform player;
    private GameObject obj;

    float Speed = 8.0f;
    float Angle;
    float Delay_Time;

    float rotateSpeed = 360f;
    float shrinkSpeed = 12f;
    float minRadius = 0.1f;

    private float angle;
    private float radius;

    bool homingTrigger = false;

    void Start()
    {
        obj = GameObject.FindGameObjectWithTag("Player");
    }

    public void SetParam(float moveangle)
    {
        Angle = moveangle;
        StartCoroutine(HomingDelay());
    }

    IEnumerator HomingDelay()
    {
      yield return new WaitForSeconds(0.2f);
      Speed = 0f;
      float distance = Vector3.Distance(transform.position, obj.transform.position);
      Delay_Time = distance * 0.05f;

      yield return new WaitForSeconds(Delay_Time);
      HomingStart();
    }

    void HomingStart()
    {
        homingTrigger = true;

        if (Random.value < 0.5f)
        {
          rotateSpeed *= -1f;
        }
        if (obj != null)
        {
            player = obj.transform;

            radius = Vector3.Distance(transform.position, player.position);

            Vector3 dir = transform.position - player.position;
            angle = Mathf.Atan2(dir.y, dir.x);

            Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
            transform.position = player.position + offset;
        }
    }

    void Update()
    {
        if (!homingTrigger)
        {
            // 通常移動
            transform.position +=
                CUtility.GetDirectionPI2(Angle) * Speed * Time.deltaTime;
        }
        else
        {
            if (player == null) return;

            // 回転
            angle += rotateSpeed * Mathf.Deg2Rad * Time.deltaTime;

            // 半径縮小
            radius -= shrinkSpeed * Time.deltaTime;

            if (radius <= minRadius)
            {
                transform.position = player.position;
                return;
            }
            // 円運動
            Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
            transform.position = player.position + offset;
        }
    }
}
