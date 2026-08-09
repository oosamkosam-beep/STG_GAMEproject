using UnityEngine;

public class CShot : MonoBehaviour
{
      public float Speed = 20.0f;
      public int ShotPower = 1 ;
      // Start is called before the first frame update 
      void Start()
      {
 
      }

      void Update()
      {
        float angle = 90.0f;
              transform.position += CUtility.GetDirection360(angle) * Speed * Time.deltaTime;

        // 弾が進行方向を向くようにする
        var angles = transform.localEulerAngles;
              angles.z = angle - 90;
              transform.localEulerAngles = angles;

        if (CUtility.BulletIsOut(transform.position))
        {
            Destroy(gameObject);
        }
      } 
}
