using UnityEngine;
using TMPro;
using System.Collections;

public class Caban_scoreboad : MonoBehaviour
{
    Animator animator;
    public TMP_Text pointitem_score_txt;
    public TMP_Text hitpoint_score_txt;
    public TMP_Text destroy_score_txt;
    public TMP_Text total_score_txt;

    private Coroutine count_pointitem_Coroutine;
    private Coroutine count_hitpoint_Coroutine;
    private Coroutine count_destroy_Coroutine;
    private Coroutine count_total_Coroutine;

    private int target_pointitem_score;
    private int current_pointitem_score;
    private int target_hitpoint_score;
    private int current_hitpoint_score;
    private int target_destroy_score;
    private int current_destroy_score;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // ★ 状態管理
    enum ScoreState
    {
        None,
        pointitem,
        hitPoint,
        destroy,
        total
    }
    private ScoreState currentState = ScoreState.None; 


    public void Show_score_text()
    {
        target_pointitem_score = GameData.stage2_pointitem_score;
        current_pointitem_score = 0;
        currentState = ScoreState.pointitem;
        count_pointitem_Coroutine = StartCoroutine(CountUp_pointitem_score());
    }
    void Show_hitpoint_score_text()
    {
        target_hitpoint_score = GameData.stage2_hitpoint_score;
        current_hitpoint_score = 0;
        currentState = ScoreState.hitPoint;
        count_hitpoint_Coroutine = StartCoroutine(CountUp_hitpoint_score());
    }
    void Show_destroy_score_text()
    {
        target_destroy_score = GameData.stage2_destroy_score;
        current_destroy_score = 0;
        currentState = ScoreState.destroy;
        count_destroy_Coroutine = StartCoroutine(CountUp_destroy_score());
    }
    void Show_total_score_text()
    {
        currentState = ScoreState.total;
        count_total_Coroutine = StartCoroutine(CountUp_total_score());
    }


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            SkipToFinal();
        }
    }


    IEnumerator CountUp_pointitem_score()
    {
        int addPerStep = 100;
        float interval = 0.02f;
        while (current_pointitem_score < target_pointitem_score)
        {
            current_pointitem_score += addPerStep;

            if (current_pointitem_score > target_pointitem_score)
                current_pointitem_score = target_pointitem_score;
            Update_pointitem_score_Text(current_pointitem_score);
            yield return new WaitForSeconds(interval);
        }
        Show_hitpoint_score_text();
    }
    IEnumerator CountUp_hitpoint_score()
    {
        int addPerStep = 100;
        float interval = 0.02f;
        while (current_hitpoint_score < target_hitpoint_score)
        {
            current_hitpoint_score += addPerStep;

            if (current_hitpoint_score > target_hitpoint_score)
                current_hitpoint_score = target_hitpoint_score;
            Update_hitpoint_score_Text(current_hitpoint_score);
            yield return new WaitForSeconds(interval);
        }
        Show_destroy_score_text();
    }
    IEnumerator CountUp_destroy_score()
    {
        int addPerStep = 100;
        float interval = 0.02f;
        while (current_destroy_score < target_destroy_score)
        {
            current_destroy_score += addPerStep;

            if (current_destroy_score > target_destroy_score)
                current_destroy_score = target_destroy_score;
            Update_destroy_score_Text(current_destroy_score);
            yield return new WaitForSeconds(interval);
        }
        Show_total_score_text();
    }
    IEnumerator CountUp_total_score()
    {
        animator.SetBool("S", true);
        yield return new WaitForSeconds(0.15f);
        Update_total_score_Text(GameData.stage2_total_score);
    }

    void SkipToFinal()
    {
        switch (currentState)
        {
            case ScoreState.pointitem:
                if (count_pointitem_Coroutine != null)
                {
                    StopCoroutine(count_pointitem_Coroutine);
                    count_pointitem_Coroutine = null;
                }
                current_pointitem_score = target_pointitem_score;
                Update_pointitem_score_Text(current_pointitem_score);
                Show_hitpoint_score_text();
                break;

            case ScoreState.hitPoint:
                if (count_hitpoint_Coroutine != null)
                {
                    StopCoroutine(count_hitpoint_Coroutine);
                    count_hitpoint_Coroutine = null;
                }
                current_hitpoint_score = target_hitpoint_score;
                Update_hitpoint_score_Text(current_hitpoint_score);
                Show_destroy_score_text();
                break;

            case ScoreState.destroy:
                if (count_destroy_Coroutine != null)
                {
                    StopCoroutine(count_destroy_Coroutine);
                    count_destroy_Coroutine = null;
                }
                current_destroy_score = target_destroy_score;
                Update_destroy_score_Text(current_destroy_score);
                Show_total_score_text();
                break;
        }
    }

    void Update_pointitem_score_Text(int score)
    {
        string scoreStr = score.ToString();
        string spriteText = "";
        foreach (char c in scoreStr)
        {
            spriteText += $"<sprite={c}>";
        }
        pointitem_score_txt.text = spriteText;
    }
    void Update_hitpoint_score_Text(int score)
    {
        string scoreStr = score.ToString();
        string spriteText = "";
        foreach (char c in scoreStr)
        {
            spriteText += $"<sprite={c}>";
        }
        hitpoint_score_txt.text = spriteText;
    }
    void Update_destroy_score_Text(int score)
    {
        string scoreStr = score.ToString();
        string spriteText = "";
        foreach (char c in scoreStr)
        {
            spriteText += $"<sprite={c}>";
        }
        destroy_score_txt.text = spriteText;
    }
    void Update_total_score_Text(int score)
    {
        string scoreStr = score.ToString();
        string spriteText = "";
        foreach (char c in scoreStr)
        {
            spriteText += $"<sprite={c}>";
        }
        total_score_txt.text = spriteText;
    }
}
