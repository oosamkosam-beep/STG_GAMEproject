using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CBoss2 : MonoBehaviour
{
    Animator animator;
    const float BULLET_Z = 0f;

    [SerializeField] GameObject strapPrefab;
    [SerializeField] GameObject curtainPrefab;
    [SerializeField] GameObject suit_eyes_Prefab;
    [SerializeField] GameObject bullet_Prefab;
    [SerializeField] GameObject bullet2_Prefab;
    [SerializeField] GameObject Convergebullet_Prefab;
    public CLPGauge lpGauge;
    public CSPGauge spGauge;
    public CtxtBox txtBox;

    int maxHP = 36000;
    int currentHP;
    int maxSP1 = 30000;
    int PhasechangeHP1 = 24000;
    int PhasechangeHP2 = 12000;
    int currentSP1;
    int maxSP2 = 40000;
    int currentSP2;
    int maxSP3 = 65000;
    int currentSP3;
    int maxSP4 = 50000;
    int currentSP4;
    
    bool Nomal_1, Nomal_2, Nomal_3; 
    bool Shield_1, Shield_2, Shield_3, Shield_4;
    bool VideoSwitch1;
    
    bool Psychedellc_Phase3, Psychedellc_Phase4;
    bool Psychedellc_Phase5, Psychedellc_Phase6;
    bool Psychedellc_Phase7;

    bool Converge_Phase2, Converge_Phase3;


    void Start()
    {
        animator = GetComponent<Animator>();

        currentHP = maxHP;
        currentSP1 = maxSP1;
        currentSP2 = maxSP2;
        currentSP3 = maxSP3;
        currentSP4 = maxSP4;
    }

    void Update()
    {
        StartCoroutine(Moving());
    }

    IEnumerator Moving()
    {
        if(!Nomal_1 && currentHP >= maxHP) 
        {
          Nomal_1 = true;
          Csounds.Instance.Play_bossBGM();
          lpGauge.Show();
          lpGauge.ResetlpGage();

          StartCoroutine(ShotNomal_1_Loop());
          yield return new WaitForSeconds(2f);
          StartCoroutine(ShoteyeNomal_1_Loop());
        }
        if(!VideoSwitch1 && currentHP <= 31000 ) 
        {
          VideoSwitch1 = true;
          VideoSwitcher.Instance.SwitchBoss2_1Video();
        }

        if(!Shield_1 && currentHP <= PhasechangeHP1) 
        {
          Shield_1 = true;
          lpGauge.Hide();
          spGauge.ResetspGage();
          spGauge.Show();

          float currentValue = animator.GetFloat("Appear");
          if (currentValue >= 0)
          {
            animator.SetFloat("Appear", 0);
          }

          CGameManager.Instance.ClearBullets();
          yield return new WaitForSeconds(1f);
          StartCoroutine(Shotstrap());
        }

        if(!Nomal_2 && currentSP1 <= 0) 
        {
          Nomal_2 = true;
          lpGauge.Show();
          spGauge.Hide();

          CGameManager.Instance.ClearBullets();
          yield return new WaitForSeconds(1f);
          StartCoroutine(ShotNomal_1_Loop());
        }

        if(!Shield_2 && currentHP <= PhasechangeHP2) 
        {
          Shield_2 = true;
          lpGauge.Hide();
          spGauge.ResetspGage();
          spGauge.Show();

          CGameManager.Instance.ClearBullets();
          yield return new WaitForSeconds(1f);
          StartCoroutine(Shotcurtain());
        }

        if(!Nomal_3 && currentSP2 <= 0) 
        {
          Nomal_3 = true;
          lpGauge.Show();
          spGauge.Hide();

          CGameManager.Instance.ClearBullets();
          yield return new WaitForSeconds(1f);
          StartCoroutine(ShotNomal_1_Loop());
        }

        if(!Shield_3 && currentHP <= 0) 
        {
          Shield_3 = true;
          lpGauge.Hide();
          spGauge.ResetspGage();
          spGauge.Show();

          CGameManager.Instance.ClearBullets();
          yield return new WaitForSeconds(1f);
          StartCoroutine(ShotPsychedelic_Phase1());
          VideoSwitcher.Instance.SwitchBoss2_2Video();

          yield return new WaitForSeconds(2.0f);
            StartCoroutine(Shot_3wayBulletALoop());
            StartCoroutine(Shot_3wayBulletBLoop());

          yield return new WaitForSeconds(4.0f);
            StartCoroutine(ShotPsychedelic_Phase2());
        }

        if(currentHP <= 0) 
        {
          if(!Psychedellc_Phase3 && currentSP3 <= 45000) 
          {
            Psychedellc_Phase3 = true;
            StartCoroutine(ShotPsychedelic_Phase3());
          }
          if(!Psychedellc_Phase4 && currentSP3 <= 23000) 
          {
            Psychedellc_Phase4 = true;
            StartCoroutine(ShotPsychedelic_Phase4());
          }
          if(!Psychedellc_Phase5 && currentSP3 <= 12000) 
          {
            Psychedellc_Phase5 = true;
            CFilterobject.Instance.SetColor(new Color(1f, 0.8f, 0.8f));
            StartCoroutine(ShotPsychedelic_Phase5());
          }
          if(!Psychedellc_Phase6 && currentSP3 <= 25000) 
          {
            Psychedellc_Phase6 = true;
            CFilterobject.Instance.SetColor(new Color(1f, 0.5f, 0.5f));
            CFilterobject.Instance.SetPostExposure(0.5f);
            StartCoroutine(ShotPsychedelic_Phase6());
          }
          if(!Psychedellc_Phase7 && currentSP3 <= 2000) 
          {
            Psychedellc_Phase7 = true;
            CFilterobject.Instance.SetColor(new Color(1f, 0f, 0f));
            CFilterobject.Instance.SetPostExposure(1.5f);
            StartCoroutine(ShotPsychedelic_Phase7());
          }
        }

        if(!Shield_4 && currentSP3 <= 0) 
        {
          Shield_4 = true;
          spGauge.ResetspGage();
          VideoSwitcher.Instance.SwitchBoss2_3Video();
          CFilterobject.Instance.LerpColor(
              new Color(1f, 1f, 1f),
              new Color(0.8f, 0.8f, 1f),
              3f
              );
          CFilterobject.Instance.SetPostExposure(0f);

          CGameManager.Instance.ClearBullets();
          yield return new WaitForSeconds(1f);
          StartCoroutine(ConvergeBullet());
        }
        if(currentSP4 <= 0) 
        {
          spGauge.Hide();
          CGameManager.Instance.ClearBullets();
          CFilterobject.Instance.SetColor(Color.white);
          txtBox.StartEndMessege();
          Destroy(gameObject);
        }
    }



    void ShoteyeNomal_1()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        Vector3 playerPos = player.transform.position;

        float baseAngle = Mathf.Atan2(
          playerPos.y - transform.position.y,
          playerPos.x - transform.position.x
          );

        float[] angleOffsets =
        {
          0f * Mathf.Deg2Rad,
          20f * Mathf.Deg2Rad,
          -20f * Mathf.Deg2Rad
        };

        foreach (float offset in angleOffsets)
        {
          Vector3 shotPos = transform.position;
          shotPos.y += 4.0f;
          shotPos.z = BULLET_Z;

          float angle = baseAngle + offset;

          GameObject bullet = Instantiate(
              bullet2_Prefab,
              shotPos,
              Quaternion.identity
              );
          bullet.GetComponent<CBullet>().SetParam(6.0f, angle);
        }
    }
    IEnumerator  ShoteyeNomal_1_Loop()
    {
      while (true)
      {
        if (!Shield_1)
        {
          for (int i = 0; i < 5; i++)
          {
            ShoteyeNomal_1();
            for (int f = 0; f < 15; f++) yield return null;
          }
        }
        yield return new WaitForSeconds(3.0f);
      }
    }

    IEnumerator  ShotNomal_1_Loop()
    {
      while (true)
      {
        if (!Shield_1)
        {
          StartCoroutine(ShotNomal_1());
        }
        yield return new WaitForSeconds(4.0f);
      }
    }
    IEnumerator  ShotNomal_1()
    {
      animator.SetFloat("Appear", 1);
      yield return new WaitForSeconds(0.5f);
        if (!Shield_1)
        {
        StartCoroutine(Nomal_handBullet_A());
        }
      yield return new WaitForSeconds(0.5f);
        if (!Shield_1)
        {
        StartCoroutine(Nomal_handBullet_B());
        }
      yield return new WaitForSeconds(2.0f);
      animator.SetFloat("Appear", 0);
      yield break;
    }
    IEnumerator Nomal_handBullet_A()
    {
      for (int burst = 0; burst < 2; burst++)
      {
        for (int i = 0; i < 5; i++)
        {
            Vector3 shotPos = transform.position;
            shotPos.x -= 3.1f;
            shotPos.y -= 0.2f;
            shotPos.z = BULLET_Z;

            GameObject bullet = Instantiate(bullet_Prefab, shotPos, Quaternion.identity);

            Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            Vector3 dir = playerPos - shotPos;
            float angle = Mathf.Atan2(dir.y, dir.x);
            bullet.GetComponent<CBullet>().SetParam(5.0f, angle);
            for (int f = 0; f < 20; f++) yield return null;
        }
        for (int f = 0; f < 120; f++) yield return null;
      }
    }
    IEnumerator Nomal_handBullet_B()
    {
      for (int burst = 0; burst < 2; burst++)
      {
        for (int i = 0; i < 5; i++)
        {
            Vector3 shotPos = transform.position;
            shotPos.x += 3.1f;
            shotPos.y -= 0.2f;
            shotPos.z = BULLET_Z;

            GameObject bullet = Instantiate(bullet_Prefab, shotPos, Quaternion.identity);

            Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            Vector3 dir = playerPos - shotPos;
            float angle = Mathf.Atan2(dir.y, dir.x);
            bullet.GetComponent<CBullet>().SetParam(5.0f, angle);
            for (int f = 0; f < 20; f++) yield return null;
        }
        for (int f = 0; f < 120; f++) yield return null;
      }
    }

    IEnumerator  Shotstrap()
    {
        Vector3 shotPos = transform.position;
        shotPos.y += 4.0f;
        shotPos.z = BULLET_Z;

        GameObject bullet = Instantiate(strapPrefab, shotPos, Quaternion.identity);
        yield break;
    }

    IEnumerator  Shotcurtain()
    {
        Vector3 shotPos = transform.position;
        shotPos.y = 1.4f;
        shotPos.z = -0.5f;

        GameObject bullet = Instantiate(curtainPrefab, shotPos, Quaternion.identity);
        yield break;
    }


    IEnumerator  ShotPsychedelic_Phase1()
    {
        for (int i = 0; i < 2; i++)
        {
          Vector3 shotPos = new Vector3(-0.6f+i*1.2f, 0.4f, 5.4f);
          GameObject bullet = Instantiate(suit_eyes_Prefab, shotPos, Quaternion.identity);
        }
        for (int i = 0; i < 2; i++)
        {
          Vector3 shotPos = new Vector3(-3+i*6, 1.2f, 5.5f);
          GameObject bullet = Instantiate(suit_eyes_Prefab, shotPos, Quaternion.identity);
        }
        yield break;
    }
    IEnumerator  ShotPsychedelic_Phase2()
    {
        for (int i = 0; i < 2; i++)
        {
          Vector3 shotPos = new Vector3(-2f+i*4f, -0.4f, 5.3f);
          GameObject bullet = Instantiate(suit_eyes_Prefab, shotPos, Quaternion.identity);
        }
        yield break;
    }
    IEnumerator  ShotPsychedelic_Phase3()
    {
        for (int i = 0; i < 2; i++)
        {
          Vector3 shotPos = new Vector3(-3.75f+i*7.5f, -2f, 5.1f);
          GameObject bullet = Instantiate(suit_eyes_Prefab, shotPos, Quaternion.identity);
        }
        yield break;
    }
    IEnumerator  ShotPsychedelic_Phase4()
    {
        for (int i = 0; i < 2; i++)
        {
          Vector3 shotPos = new Vector3(-1.8f+i*3.6f, 1.2f, 5.5f);
          GameObject bullet = Instantiate(suit_eyes_Prefab, shotPos, Quaternion.identity);
        }
        yield break;
    }
    IEnumerator  ShotPsychedelic_Phase5()
    {
        for (int i = 0; i < 2; i++)
        {
          Vector3 shotPos = new Vector3(-2.05f+i*4.1f, -1.2f, 5.2f);
          GameObject bullet = Instantiate(suit_eyes_Prefab, shotPos, Quaternion.identity);
        }
        for (int i = 0; i < 2; i++)
        {
          Vector3 shotPos = new Vector3(-2.35f+i*4.7f, -2.8f, 5f);
          GameObject bullet = Instantiate(suit_eyes_Prefab, shotPos, Quaternion.identity);
        }
        yield break;
    }
    IEnumerator  ShotPsychedelic_Phase6()
    {
        for (int i = 0; i < 2; i++)
        {
          Vector3 shotPos = new Vector3(-0.6f+i*1.2f, 1.2f, 5.5f);
          GameObject bullet = Instantiate(suit_eyes_Prefab, shotPos, Quaternion.identity);
        }
        for (int i = 0; i < 2; i++)
        {
          Vector3 shotPos = new Vector3(-3.4f+i*6.8f, -0.4f, 5.3f);
          GameObject bullet = Instantiate(suit_eyes_Prefab, shotPos, Quaternion.identity);
        }
        for (int i = 0; i < 2; i++)
        {
          Vector3 shotPos = new Vector3(-2.2f+i*4.4f, -2f, 5.1f);
          GameObject bullet = Instantiate(suit_eyes_Prefab, shotPos, Quaternion.identity);
        }
        for (int i = 0; i < 2; i++)
        {
          Vector3 shotPos = new Vector3(-3.95f+i*7.9f, -2.8f, 5f);
          GameObject bullet = Instantiate(suit_eyes_Prefab, shotPos, Quaternion.identity);
        }
        yield break;
    }
    IEnumerator  ShotPsychedelic_Phase7()
    {
        for (int i = 0; i < 2; i++)
        {
          Vector3 shotPos = new Vector3(-3.2f+i*6.4f, 0.4f, 5.4f);
          GameObject bullet = Instantiate(suit_eyes_Prefab, shotPos, Quaternion.identity);
        }
        for (int i = 0; i < 2; i++)
        {
          Vector3 shotPos = new Vector3(-1.9f+i*3.8f, 0.4f, 5.4f);
          GameObject bullet = Instantiate(suit_eyes_Prefab, shotPos, Quaternion.identity);
        }
        for (int i = 0; i < 2; i++)
        {
          Vector3 shotPos = new Vector3(-0.65f+i*1.3f, -0.4f, 5.3f);
          GameObject bullet = Instantiate(suit_eyes_Prefab, shotPos, Quaternion.identity);
        }
        for (int i = 0; i < 2; i++)
        {
          Vector3 shotPos = new Vector3(-3.55f+i*7.1f, -1.2f, 5.2f);
          GameObject bullet = Instantiate(suit_eyes_Prefab, shotPos, Quaternion.identity);
        }
        yield break;
    }


    void Shot_3wayBulletA()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        Vector3 playerPos = player.transform.position;

        float baseAngle = Mathf.Atan2(
            playerPos.y - 5.5f,
            playerPos.x - -1.5f
            );

        float[] angleOffsets =
        {
          0f * Mathf.Deg2Rad,
          30f * Mathf.Deg2Rad,
          -30f * Mathf.Deg2Rad
        };

        foreach (float offset in angleOffsets)
        {
          Vector3 shotPos = new Vector3(-1.5f, 5.5f, 0f);

          float angle = baseAngle + offset;

          GameObject bullet = Instantiate(
              bullet2_Prefab,
              shotPos,
              Quaternion.identity
              );

          bullet.GetComponent<CBullet>().SetParam(5.0f, angle);
        }
    }
    void Shot_3wayBulletB()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        Vector3 playerPos = player.transform.position;

        float baseAngle = Mathf.Atan2(
            playerPos.y - 5.5f,
            playerPos.x - 1.5f
            );

        float[] angleOffsets =
        {
          0f * Mathf.Deg2Rad,
          30f * Mathf.Deg2Rad,
          -30f * Mathf.Deg2Rad
        };

        foreach (float offset in angleOffsets)
        {
          Vector3 shotPos = new Vector3(1.5f, 5.5f, 0f);

          float angle = baseAngle + offset;

          GameObject bullet = Instantiate(
              bullet2_Prefab,
              shotPos,
              Quaternion.identity
              );

          bullet.GetComponent<CBullet>().SetParam(4.5f, angle);
        }
    }
    IEnumerator Shot_3wayBulletALoop()
    {
        while (true)
        {
          if (!Shield_4)
          {
            Shot_3wayBulletA();
          }
          yield return new WaitForSeconds(4.0f);
        }
    }
    IEnumerator Shot_3wayBulletBLoop()
    {
        while (true)
        {
          yield return new WaitForSeconds(2.0f);

          if (!Shield_4)
          {
            Shot_3wayBulletB();
          }
          yield return new WaitForSeconds(2.0f);
        }
    }


    IEnumerator ConvergeBullet()
    {
        StartCoroutine(Shot_ConvergeBullet());
        yield return new WaitForSeconds(10.0f);
          Converge_Phase2 = true;

        yield return new WaitForSeconds(10.0f);
          Converge_Phase3 = true;

        yield break;
    }
    IEnumerator Shot_ConvergeBullet()
    {
        float startSpeed;
        float burst;

        while (true)
        {
            Vector3 centerPos = new Vector3(-7f, 0f, 0f);
            float radius = 6f;

            float deg = Random.Range(75f, 295f);
            float rad = deg * Mathf.Deg2Rad;

            // 円周上のランダムな発射位置
            Vector3 shotPos = centerPos + new Vector3(
                Mathf.Cos(rad) * radius,
                Mathf.Sin(rad) * radius,
                0f
            );

            GameObject bullet = Instantiate(
                Convergebullet_Prefab,
                shotPos,
                Quaternion.identity
            );

            Vector3 convergePos = new Vector3(15f, -2.6f, 0f);

            float angle = Mathf.Atan2(
                convergePos.y - shotPos.y,
                convergePos.x - shotPos.x
            );


            if(!Converge_Phase3)
            {
              if(!Converge_Phase2)
              {
                startSpeed = 3f;
                burst = 35;
              }
              else
              {
                startSpeed = 5f;
                burst = 25;
              }
            }
            else
            {
              startSpeed = 7f;
              burst = 15;
            }

            bullet.GetComponent<CBullet>().SetParamWithAccel
            (
                startSpeed,
                20.0f,    
                angle
            );

            for (int f = 0; f < burst; f++) yield return null;
        }
    }


    void ApplyDamage(int damage)
    {
      if (currentHP > PhasechangeHP1)
      {
        Damage(damage);
      }
      else if (currentSP1 > 0) 
      {
        SP1_Damage(damage);
      }
      else if (currentHP > PhasechangeHP2)
      {
        Damage(damage);
      }
      else if (currentSP2 > 0)
      {
        SP2_Damage(damage);
      }
      else if (currentHP > 0)
      {
        Damage(damage);
      }
      else if (currentSP3 > 0)
      {
        SP3_Damage(damage);
      }
      else if (currentSP4 > 0)
      {
        SP4_Damage(damage);
      }
    }

    void Damage(int damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Max(currentHP, 0);
        if (lpGauge != null)
        {
            lpGauge.SetRate((float)currentHP / maxHP);
        }
    }
    void SP1_Damage(int damage)
    {
        currentSP1 -= damage;
        currentSP1 = Mathf.Max(currentSP1, 0);
        if (spGauge != null)
        {
            spGauge.SetRate((float)currentSP1 / maxSP1);
        }
    }
    void SP2_Damage(int damage)
    {
        currentSP2 -= damage;
        currentSP2 = Mathf.Max(currentSP2, 0);
        if (spGauge != null)
        {
            spGauge.SetRate((float)currentSP2 / maxSP2);
        }
    }
    void SP3_Damage(int damage)
    {
        currentSP3 -= damage;
        currentSP3 = Mathf.Max(currentSP3, 0);
        if (spGauge != null)
        {
            spGauge.SetRate((float)currentSP3 / maxSP3);
        }
    }
    void SP4_Damage(int damage)
    {
        currentSP4 -= damage;
        currentSP4 = Mathf.Max(currentSP4, 0);
        if (spGauge != null)
        {
            spGauge.SetRate((float)currentSP4 / maxSP4);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shot"))
        {
          int power = 0;
          var normalShot = collision.GetComponent<CShot>();
          var homingShot = collision.GetComponent<CShot_homing>();
          var lazerShot = collision.GetComponent<CShot_lazer_trigger>();
          if (normalShot != null) power = normalShot.ShotPower;
          if (homingShot != null) power = homingShot.ShotPower;
          if (lazerShot != null) power = lazerShot.ShotPower;

          if (power > 0)
          {
              ApplyDamage(power); 
          }
          Destroy(collision.gameObject);
          GameData.score += 10;
          GameData.stage2_total_score += 10;
          GameData.stage2_hitpoint_score += 10;
        }
    }
}
