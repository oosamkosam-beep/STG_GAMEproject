using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class CtxtBox : MonoBehaviour
{
    Animator animator;

    public Cportrail Portrail;
    public Caban_stage2 aban_stage2;
    
    public GameObject portrail;
    public GameObject Stage2_Event_Video;
    public GameObject frameA;
    public GameObject frameB;
    public GameObject BG;
    public TMP_Text tmpText;

    bool inputLocked = true;   bool flagvideo = false;   

    [TextArea]
    public List<string> messages = new List<string>();
    int currentIndex = 0;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void StartMessege()
    {
        Show();
        animator.SetBool("S", true);
        StartCoroutine(StartMessage());
    }
    IEnumerator StartMessage()
    {
        yield return new WaitForSeconds(1.0f);
          inputLocked = true;
          Portrail.trueAnimation();
          currentIndex = 0;
          tmpText.text = messages[currentIndex];

        yield return new WaitForSeconds(1.0f);
          inputLocked = false;
    }
    public void StartEndMessege()
    {
        Show();
        animator.SetBool("S", true);
        StartCoroutine(StartEndMessage());
    }
    IEnumerator StartEndMessage()
    {
        yield return new WaitForSeconds(1.0f);
          Portrail.trueAnimation();
          NextMessage();

        yield return new WaitForSeconds(1.0f);
          inputLocked = false;
    }

    void Update()
    {
        if (inputLocked) return;

        if (Input.GetButtonDown("Fire1"))
        {
            if (!flagvideo && currentIndex == 3)
            {
                StartCoroutine(PlayVideo());
                return;
            }
            if (flagvideo && currentIndex == 3)
            {
                portrail.gameObject.SetActive(true);
                Portrail.trueAnimation();
                frameA.gameObject.SetActive(true);
                frameB.gameObject.SetActive(true);
                BG.gameObject.SetActive(true);
                tmpText.gameObject.SetActive(true);
                NextMessage();
                return;
            }
            if (currentIndex == 7)
            {
                inputLocked = true;
                StartCoroutine(EndMessage());
                Stage2_Event_Video.SetActive(false);
                CGameManager.Instance.HandleBossTimeline();
                NextMessage();
                return;
            }
            if (currentIndex == 10)
            {
                inputLocked = true;
                aban_stage2.Start_aban();
                return;
            }
            NextMessage();
        }
    }

    IEnumerator PlayVideo()
    {
        inputLocked = true;
        Portrail.falseAnimation();
        portrail.gameObject.SetActive(false);
        frameA.gameObject.SetActive(false);
        frameB.gameObject.SetActive(false);
        BG.gameObject.SetActive(false);
        tmpText.gameObject.SetActive(false);
        Stage2_Event_Video.SetActive(true);

        yield return new WaitForSeconds(2.5f);
        inputLocked = false;
        flagvideo = true;
    }

    void NextMessage()
    {
        currentIndex++;
        tmpText.text = messages[currentIndex];
    }

    IEnumerator EndMessage()
    {
          Portrail.falseAnimation();
          animator.SetBool("S", false);
          animator.SetBool("H", true);
        yield return new WaitForSeconds(1.0f);
          animator.SetBool("H", false);
          Hide();
    }

    public void Show()
    {
        transform.Find("txtBox_frameA").gameObject.SetActive(true);
        transform.Find("txtBox_frameB").gameObject.SetActive(true);
        transform.Find("txtBox_BG").gameObject.SetActive(true);
    }

    public void Hide()
    {
        transform.Find("txtBox_frameA").gameObject.SetActive(false);
        transform.Find("txtBox_frameB").gameObject.SetActive(false);
        transform.Find("txtBox_BG").gameObject.SetActive(false);
    }
}
