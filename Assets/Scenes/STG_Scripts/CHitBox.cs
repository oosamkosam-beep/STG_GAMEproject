using UnityEngine;
using System.Collections;

public class CHItBox : MonoBehaviour
{
    SpriteRenderer sr;
    SpriteRenderer circle_sr;
    SpriteRenderer outreticle_1_sr;
    SpriteRenderer outreticle_2_sr;
    SpriteRenderer outreticle_3_sr;
    SpriteRenderer outreticle_4_sr;
    SpriteRenderer inreticle_1_sr;
    SpriteRenderer inreticle_2_sr;
    SpriteRenderer inreticle_3_sr;
    SpriteRenderer triangle_1_sr;
    SpriteRenderer triangle_2_sr;
    SpriteRenderer triangle_3_sr;
    Transform outreticle_1;
    Transform outreticle_2;
    Transform outreticle_3;
    Transform outreticle_4;
    Transform inreticle_1;
    Transform inreticle_2;
    Transform inreticle_3;
    float fadeTime = 0.3f;
    float fastfadeTime = 0.05f;
    bool Input = false;

    
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        circle_sr = transform.Find("HitBox_circle").GetComponent<SpriteRenderer>();
        outreticle_1_sr = transform.Find("hitbox_outreticle/hitbox_outreticle_1").GetComponent<SpriteRenderer>();
        outreticle_2_sr = transform.Find("hitbox_outreticle/hitbox_outreticle_2").GetComponent<SpriteRenderer>();
        outreticle_3_sr = transform.Find("hitbox_outreticle/hitbox_outreticle_3").GetComponent<SpriteRenderer>();
        outreticle_4_sr = transform.Find("hitbox_outreticle/hitbox_outreticle_4").GetComponent<SpriteRenderer>();
        inreticle_1_sr = transform.Find("hitbox_inreticle/hitbox_inreticle_1").GetComponent<SpriteRenderer>();
        inreticle_2_sr = transform.Find("hitbox_inreticle/hitbox_inreticle_2").GetComponent<SpriteRenderer>();
        inreticle_3_sr = transform.Find("hitbox_inreticle/hitbox_inreticle_3").GetComponent<SpriteRenderer>();
        triangle_1_sr = transform.Find("hitbox_inreticle/hitbox_triangle_1").GetComponent<SpriteRenderer>();
        triangle_2_sr = transform.Find("hitbox_inreticle/hitbox_triangle_2").GetComponent<SpriteRenderer>();
        triangle_3_sr = transform.Find("hitbox_inreticle/hitbox_triangle_3").GetComponent<SpriteRenderer>();

        outreticle_1 =transform.Find("hitbox_outreticle/hitbox_outreticle_1"); 
        outreticle_2 =transform.Find("hitbox_outreticle/hitbox_outreticle_2"); 
        outreticle_3 =transform.Find("hitbox_outreticle/hitbox_outreticle_3"); 
        outreticle_4 =transform.Find("hitbox_outreticle/hitbox_outreticle_4"); 
        inreticle_1 =transform.Find("hitbox_inreticle/hitbox_inreticle_1"); 
        inreticle_2 =transform.Find("hitbox_inreticle/hitbox_inreticle_2"); 
        inreticle_3 =transform.Find("hitbox_inreticle/hitbox_inreticle_3"); 
        
        SetAlpha(0f); // 初期は透明
        SetAlpha_fast(0f); // 初期は透明
    }

    public void FadeIn()
    {
      if(!Input)
      {
        Input = true;
        StopAllCoroutines();
        StartCoroutine(Close_Reticle());
      }
    }
    public void FadeOut()
    {
      if(Input)
      {
        Input = false;
        StopAllCoroutines();
        StartCoroutine(Open_Reticle());
      }
    }
    IEnumerator Close_Reticle()
    {
        StartCoroutine(CloseoutReticle_1());
        StartCoroutine(CloseoutReticle_2());
        StartCoroutine(CloseoutReticle_3());
        StartCoroutine(CloseoutReticle_4());
        StartCoroutine(FadeTo(1f));
      yield return new WaitForSeconds(0.2f);
        StartCoroutine(CloseinReticle_1());
        StartCoroutine(CloseinReticle_2());
        StartCoroutine(CloseinReticle_3());
      yield return new WaitForSeconds(0.3f);
        StartCoroutine(FadeTo_fast(1f));
    }
    IEnumerator Open_Reticle()
    {
        StartCoroutine(OpeninReticle_1());
        StartCoroutine(OpeninReticle_2());
        StartCoroutine(OpeninReticle_3());
        StartCoroutine(FadeTo(0f));
        StartCoroutine(FadeTo_fast(0f));
      yield return new WaitForSeconds(0.2f);
        StartCoroutine(OpenoutReticle_1());
        StartCoroutine(OpenoutReticle_2());
        StartCoroutine(OpenoutReticle_3());
        StartCoroutine(OpenoutReticle_4());
    }


    IEnumerator FadeTo(float targetAlpha)
    {
        float start = sr.color.a;
        float t = 0;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(start, targetAlpha, t / fadeTime);
            SetAlpha(a);
            yield return null;
        }
        SetAlpha(targetAlpha);
    }
    void SetAlpha(float a)
    {
        var c = sr.color;
        c.a = a;

        sr.color = c;
        outreticle_1_sr.color = c;
        outreticle_2_sr.color = c;
        outreticle_3_sr.color = c;
        outreticle_4_sr.color = c;
        inreticle_1_sr.color = c;
        inreticle_2_sr.color = c;
        inreticle_3_sr.color = c;
    }
    IEnumerator FadeTo_fast(float targetAlpha)
    {
        float start = circle_sr.color.a;
        float t = 0;
        while (t < fastfadeTime)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(start, targetAlpha, t / fastfadeTime);
            SetAlpha_fast(a);
            yield return null;
        }
        SetAlpha_fast(targetAlpha);
    }

    void SetAlpha_fast(float a)
    {
        var circle_c = circle_sr.color;
        circle_c.a = a;

        circle_sr.color = circle_c;
        triangle_1_sr.color = circle_c;
        triangle_2_sr.color = circle_c;
        triangle_3_sr.color = circle_c;
    }

    IEnumerator CloseoutReticle_1()
    {
        Vector3 target = new Vector3(0, 3.7f, 0);
        while (Vector3.Distance(outreticle_1.localPosition, target) > 0.01f)
        {
            outreticle_1.localPosition = Vector3.MoveTowards(
                outreticle_1.localPosition, target, 6.0f * Time.deltaTime
            );
            yield return null;
        }
        outreticle_1.localPosition = target;
    }
    IEnumerator CloseoutReticle_2()
    {
        Vector3 target = new Vector3(3.7f, 0, 0);
        while (Vector3.Distance(outreticle_2.localPosition, target) > 0.01f)
        {
            outreticle_2.localPosition = Vector3.MoveTowards(
                outreticle_2.localPosition, target, 6.0f * Time.deltaTime
            );
            yield return null;
        }
        outreticle_2.localPosition = target;
    }
    IEnumerator CloseoutReticle_3()
    {
        Vector3 target = new Vector3(0, -3.7f, 0);
        while (Vector3.Distance(outreticle_3.localPosition, target) > 0.01f)
        {
            outreticle_3.localPosition = Vector3.MoveTowards(
                outreticle_3.localPosition, target, 6.0f * Time.deltaTime
            );
            yield return null;
        }
        outreticle_3.localPosition = target;
    }
    IEnumerator CloseoutReticle_4()
    {
        Vector3 target = new Vector3(-3.7f, 0, 0);
        while (Vector3.Distance(outreticle_4.localPosition, target) > 0.01f)
        {
            outreticle_4.localPosition = Vector3.MoveTowards(
                outreticle_4.localPosition, target, 6.0f * Time.deltaTime
            );
            yield return null;
        }
        outreticle_4.localPosition = target;
    }
    IEnumerator CloseinReticle_1()
    {
        Vector3 target = new Vector3(1.4f, 0.63f, 0);
        while (Vector3.Distance(inreticle_1.localPosition, target) > 0.01f)
        {
            inreticle_1.localPosition = Vector3.MoveTowards(
                inreticle_1.localPosition, target, 6.0f * Time.deltaTime
            );
            yield return null;
        }
        inreticle_1.localPosition = target;
    }
    IEnumerator CloseinReticle_2()
    {
        Vector3 target = new Vector3(-1.4f, 0.63f, 0);
        while (Vector3.Distance(inreticle_2.localPosition, target) > 0.01f)
        {
            inreticle_2.localPosition = Vector3.MoveTowards(
                inreticle_2.localPosition, target, 6.0f * Time.deltaTime
            );
            yield return null;
        }
        inreticle_2.localPosition = target;
    }
    IEnumerator CloseinReticle_3()
    {
        Vector3 target = new Vector3(0, -1.7f, 0);
        while (Vector3.Distance(inreticle_3.localPosition, target) > 0.01f)
        {
            inreticle_3.localPosition = Vector3.MoveTowards(
                inreticle_3.localPosition, target, 6.0f * Time.deltaTime
            );
            yield return null;
        }
        inreticle_3.localPosition = target;
    }

    IEnumerator OpenoutReticle_1()
    {
        Vector3 target = new Vector3(0, 5.0f, 0);
        while (Vector3.Distance(outreticle_1.localPosition, target) > 0.01f)
        {
            outreticle_1.localPosition = Vector3.MoveTowards(
                outreticle_1.localPosition, target, 6.0f * Time.deltaTime
            );
            yield return null;
        }
        outreticle_1.localPosition = target;
    }
    IEnumerator OpenoutReticle_2()
    {
        Vector3 target = new Vector3(5.0f, 0, 0);
        while (Vector3.Distance(outreticle_2.localPosition, target) > 0.01f)
        {
            outreticle_2.localPosition = Vector3.MoveTowards(
                outreticle_2.localPosition, target, 6.0f * Time.deltaTime
            );
            yield return null;
        }
        outreticle_2.localPosition = target;
    }
    IEnumerator OpenoutReticle_3()
    {
        Vector3 target = new Vector3(0, -5.0f, 0);
        while (Vector3.Distance(outreticle_3.localPosition, target) > 0.01f)
        {
            outreticle_3.localPosition = Vector3.MoveTowards(
                outreticle_3.localPosition, target, 6.0f * Time.deltaTime
            );
            yield return null;
        }
        outreticle_3.localPosition = target;
    }
    IEnumerator OpenoutReticle_4()
    {
        Vector3 target = new Vector3(-5.0f, 0, 0);
        while (Vector3.Distance(outreticle_4.localPosition, target) > 0.01f)
        {
            outreticle_4.localPosition = Vector3.MoveTowards(
                outreticle_4.localPosition, target, 6.0f * Time.deltaTime
            );
            yield return null;
        }
        outreticle_4.localPosition = target;
    }
    IEnumerator OpeninReticle_1()
    {
        Vector3 target = new Vector3(2.3f, 1.3f, 0);
        while (Vector3.Distance(inreticle_1.localPosition, target) > 0.01f)
        {
            inreticle_1.localPosition = Vector3.MoveTowards(
                inreticle_1.localPosition, target, 6.0f * Time.deltaTime
            );
            yield return null;
        }
        inreticle_1.localPosition = target;
    }
    IEnumerator OpeninReticle_2()
    {
        Vector3 target = new Vector3(-2.3f, 1.3f, 0);
        while (Vector3.Distance(inreticle_2.localPosition, target) > 0.01f)
        {
            inreticle_2.localPosition = Vector3.MoveTowards(
                inreticle_2.localPosition, target, 6.0f * Time.deltaTime
            );
            yield return null;
        }
        inreticle_2.localPosition = target;
    }
    IEnumerator OpeninReticle_3()
    {
        Vector3 target = new Vector3(0, -3f, 0);
        while (Vector3.Distance(inreticle_3.localPosition, target) > 0.01f)
        {
            inreticle_3.localPosition = Vector3.MoveTowards(
                inreticle_3.localPosition, target, 6.0f * Time.deltaTime
            );
            yield return null;
        }
        inreticle_3.localPosition = target;
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
