using UnityEngine;
using System.Collections;

public class Cpointitem : MonoBehaviour
{
    float Speed = 6.0f;
    float doropSpeed = 2.0f;
    float Angle;

    bool DoropTrigger = false;


    public void SetParam(float moveangle)
    {
        Angle = moveangle;
        StartCoroutine(DoropDelay());
    }

    IEnumerator DoropDelay()
    {
      yield return new WaitForSeconds(0.1f);
      Speed = 0f;

      yield return new WaitForSeconds(0.1f);
      DoropTrigger = true;
    }


    void Update()
    {
        if (!DoropTrigger)
        {
            transform.position +=
                CUtility.GetDirectionPI2(Angle) * Speed * Time.deltaTime;
        }
        else
        {
          float baseAngle = -90f * Mathf.Deg2Rad; // 真下

            transform.position +=
                CUtility.GetDirectionPI2(baseAngle) * doropSpeed * Time.deltaTime;
        }
    }
}
