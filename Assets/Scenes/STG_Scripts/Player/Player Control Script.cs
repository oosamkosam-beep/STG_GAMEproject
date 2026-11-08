using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlyaerControlScript : MonoBehaviour
{
    Animator _Animator;
    Rigidbody2D rb;
    Vector2 move;
    float shotTimer = 0f;

    [SerializeField] GameObject Bomb_Prefab;
    [SerializeField] GameObject Shield_break_Prefab;
    [SerializeField] GameObject Redperticle;
    [SerializeField] GameObject Darkframe;
    [SerializeField] GameObject Mask_Video;

    public GameObject[] ShotObjs; 
    public CHItBox hitbox;
    
    bool keyLocked = false; 
    bool Invincible = false;
    Vector3 autoMoveTarget = new Vector3(0, -2.5f, 0);
    float autoMoveSpeed = 1.5f;

    float speed;
    float Sqrt2;
    Dictionary<string, int> InputArray = new Dictionary<string, int>();  

    void Awake()
    {
      if (GameData.Player) 
      {
        gameObject.SetActive(false);
      }
    }

    void Start()
    {
       _Animator = GetComponent<Animator>();
       rb = GetComponent<Rigidbody2D>();

       Sqrt2 = 1.0f / Mathf.Sqrt(2.0f);
       InputArray["Fire1"] = 0;
       InputArray["Fire2"] = 0;
       InputArray["Fire3"] = 0;

    }
    
    void CalcInput()
    { 	    
       string[] str = {"Fire1", "Fire2","Fire3"};
       for(int i = 0; i < str.Length; ++i)  
       {	      
         if (Input.GetButton(str[i]))     
         {
           ++InputArray[str[i]];
         }
         else
         {	   
           InputArray[str[i]] = 0;
         }
       }   
    }   

    void Update()
    {
        CalcInput();

        speed = 0 < InputArray["Fire3"] ? 2.5f : 7.0f;
        float x =Input.GetAxisRaw("Horizontal");
        float y =Input.GetAxisRaw("Vertical");
        move = new Vector2(-x, -y) * speed;
        // 斜め補正
        if (move.x != 0 && move.y != 0)
        {
          move *= Sqrt2;
        }
        if (keyLocked)
        {
          move = new Vector2(0, 0);
        }

        shotTimer += Time.deltaTime;
        if (!keyLocked)
        {
          if (0 < InputArray["Fire3"])//低速移動中なら 
          {
            if (0 < InputArray["Fire1"] && shotTimer >= 0.04f) 
            {
                LowerSpeedShot();
                shotTimer = 0f; 
            }   
          }
          else
          {
            if (0 < InputArray["Fire1"] && shotTimer >= 0.2f) 
            {
                HiSpeedShot();
                shotTimer = 0f; 
            }   
          }

          if (Input.GetButtonDown("Fire2"))
          {
            if (GameData.Bomb_stock > 0)
            {
              keyLocked = true;
              GameData.Bomb_stock -= 1;
              StartBomb();
              return;
            }
          }

          bool slow = 0 < InputArray["Fire3"];
          if (slow)
            hitbox.FadeIn();   // フェードイン
          else
            hitbox.FadeOut();   // フェードアウト
        }
        _Animator.SetFloat("H", x); 
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + move * Time.fixedDeltaTime);
    }


    int[] CShot0Num = { 2, 4 }; 
    Vector3[] HiSpeedShotOffsetPos = 
    {
           new Vector3(-0.15f,0.8f,5f),
           new Vector3(0.15f,0.8f,5f),
           new Vector3(-0.45f,0.4f,5f),
           new Vector3(0.45f,0.4f,5f),
    };
    
    void HiSpeedShot() 
    {
			    Vector3 shotPos = transform.position + new Vector3(0.45f,0.4f,5f);
          Instantiate(ShotObjs[0], shotPos, Quaternion.identity);
    }
    Vector3[] LowerSpeedShotOffsetPos = 
    { 	    
           new Vector3(-0.05f,0.8f,5f),
           new Vector3(0.05f,0.8f,5f),
           new Vector3(-0.25f,0.4f,5f),
           new Vector3(0.25f,0.4f,5f),
    };

    void LowerSpeedShot () 
    {
        for (int i = 0; i < CShot0Num[1]; i++)   	    
        {
          Instantiate(ShotObjs[1], transform.position + LowerSpeedShotOffsetPos[i], Quaternion.identity);
        }		 
    }

    void StartBomb()
    {
      _Animator.SetBool("Bomb",true); 
      hitbox.Hide();
      StartCoroutine(Bomb());
    }
    IEnumerator Bomb()
    {
        Redperticle.GetComponent<SpriteRenderer>().color = new Color(0f, 0, 0, 0.8f);
        Darkframe.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        Mask_Video.transform.localScale = new Vector3(0, 0, 0);
        CGameManager.Instance.PauseGame();
      yield return new WaitForSecondsRealtime(0.7f);
        Redperticle.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        Darkframe.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        Mask_Video.transform.localScale = new Vector3(1, 1, 1);
        Instantiate(Bomb_Prefab, transform.position, Quaternion.identity);
        CGameManager.Instance.ResumeGame();
      yield return new WaitForSeconds(0.3f);
        _Animator.SetBool("Bomb",false); 
        hitbox.Show();
        keyLocked = false;
    }


    void OnEnable()
    {
      CGameManager.StartEvent += AutoMove;
      CGameManager.EventEnd += AllowMove;
    }
    void OnDisable()
    {
      CGameManager.StartEvent -= AutoMove;
      CGameManager.EventEnd -= AllowMove;
    }
    void AutoMove()
    {
      keyLocked = true;
      AutoMoveUpdate();
    }
    void AutoMoveUpdate()
    {
      transform.position = Vector3.MoveTowards(
          transform.position,
          autoMoveTarget,
          autoMoveSpeed * Time.deltaTime
          );
    }
    void AllowMove()
    {
      keyLocked = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      if (!Invincible)
      {
          if (!keyLocked)
          {
            if(collision.tag == "Bullet")
            {
                Destroy(collision.gameObject);

                if (GameData.Shield_stock > 0)
                {
                  keyLocked = true;
                  StartCoroutine(Shild_break());
                }
                else
                {
                  keyLocked = true;
                  StartDeath();
                }
            }
          }
      }
      if(collision.tag == "PointItem")
      {
        GameData.score += 100;
        GameData.stage2_total_score += 100;
        GameData.stage2_pointitem_score += 100;

       Csounds.Instance.Get_pointitem();
        Destroy(collision.gameObject);
      }  	   
    }
    IEnumerator Shild_break()
    {
        CGameManager.Instance.PauseGame();
        Instantiate(Shield_break_Prefab, transform.position, Quaternion.identity);
      yield return new WaitForSecondsRealtime(0.7f);
        CGameManager.Instance.ResumeGame();
        Invincible = true;
        keyLocked = false;
      yield return new WaitForSecondsRealtime(2.0f);
        Invincible = false;
    }
    void StartDeath()
    {
      _Animator.SetBool("Death",true); 
      hitbox.Hide();

      Redperticle.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0, 0, 1f);
      Darkframe.GetComponent<Image>().color = new Color(1, 1, 1, 0);
      Mask_Video.transform.localScale = new Vector3(0, 0, 0);
      CGameManager.Instance.PauseGame();
    }
}    
