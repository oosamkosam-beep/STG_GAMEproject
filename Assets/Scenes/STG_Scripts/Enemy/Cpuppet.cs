using UnityEngine;
using System.Collections;

public class Cpuppet : MonoBehaviour
{
  [SerializeField] GameObject bulletPrefab;
  [SerializeField] GameObject hitodama_Prefab;
  [SerializeField] GameObject Destroy_Enemy_Prefab;
  int Life = 20;
  const float BULLET_Z = 0f;
  Animator animator;

  bool Invincible = true;

  void Awake()
  {
    animator = GetComponent<Animator>();
  }

  void Start()
  {
    StartCoroutine(Shot5Bullets());
  }

  void Update()
  {
    float x = transform.position.x;
    if (x < 0f)
    {
      animator.SetFloat("H",0f);
    }
    if (x > 0f)
    {
      animator.SetFloat("H",1f);
    }

    if (CUtility.IsOut(transform.position))
    {
      Destroy(gameObject);
    }
  }

  IEnumerator Shot5Bullets()
  {
    yield return new WaitForSeconds(0.5f);
      Invincible = false;
    yield return new WaitForSeconds(2.0f);

    for (int burst = 0; burst < 1; burst++)
    {
      for (int i = 0; i < 5; i++)
      {
        Vector3 shotPos = transform.position;
        shotPos.y += 1.0f;
        shotPos.z = BULLET_Z;

        GameObject bullet = Instantiate(bulletPrefab, shotPos, Quaternion.identity);

        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        float angle = Mathf.Atan2(
            playerPos.y - transform.position.y,
            playerPos.x - transform.position.x
            );

        //速度、方向
        bullet.GetComponent<CBullet>().SetParam(5.0f, angle);

        for (int f = 0; f < 15; f++) yield return null;
      }

      for (int f = 0; f < 20; f++) yield return null;
    }
  }
  private void OnTriggerEnter2D(Collider2D collision)
  {
      if (collision.gameObject.tag == "Shot")
      {
          int power = 0;

          if (!Invincible)
          {
            var normalShot = collision.GetComponent<CShot>();
            var homingShot = collision.GetComponent<CShot_homing>();
            var lazerShot = collision.GetComponent<CShot_lazer_trigger>();
            if (normalShot != null) power = normalShot.ShotPower;
            if (homingShot != null) power = homingShot.ShotPower;
            if (lazerShot != null) power = lazerShot.ShotPower;

            Life -= power;
          }

          if (Life <= 0) 
          {
            Destroy(gameObject);
            GameData.score += 100;
            GameData.stage2_total_score += 100;
            GameData.stage2_destroy_score += 100;
            Instantiate(Destroy_Enemy_Prefab, transform.position, Quaternion.identity);
            Instantiate(hitodama_Prefab, transform.position, Quaternion.identity);
          }
          Destroy(collision.gameObject);
          GameData.score += 10;
          GameData.stage2_total_score += 10;
          GameData.stage2_hitpoint_score += 10;
      }
  }
}
