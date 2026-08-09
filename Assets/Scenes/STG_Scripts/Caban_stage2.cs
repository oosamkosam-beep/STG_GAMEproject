using UnityEngine;
using System.Collections;

public class Caban_stage2 : MonoBehaviour
{
    Animator animator;
    public Caban_scoreboad aban_scoreboad;

    void Awake()
    {
      animator = GetComponent<Animator>();
    }

    public void Start_aban()
    {
      StartCoroutine(aban());
    }
    IEnumerator aban()
    {
      Csounds.Instance.Play_abanBGM();
        yield return new WaitForSeconds(0.5f);
      animator.SetBool("S", true);
        yield return new WaitForSeconds(3.5f);
      Show_aban_scoreboad();
    }

    void Show_aban_scoreboad()
    {
      aban_scoreboad.Show_score_text();
    }
}
