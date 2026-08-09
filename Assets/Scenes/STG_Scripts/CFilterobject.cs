using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CFilterobject : MonoBehaviour
{
    public static CFilterobject Instance;

    Volume volume;
    ColorAdjustments colorAdjustments;
    Coroutine colorCoroutine;  

    void Awake()
    {
       Instance = this;

        volume = GetComponent<Volume>();
        // 実行時専用に Profile を複製（超重要）
        volume.profile = Instantiate(volume.profile);
        // ColorAdjustments を取得
        if (!volume.profile.TryGet(out colorAdjustments))
        {
            Debug.LogError("ColorAdjustments が Profile に存在しません");
        }
    }

    public void SetColor(Color color)
    {
        colorAdjustments.colorFilter.value = color;
    }
    public void SetSaturation(float value)
    {
        colorAdjustments.saturation.value = Mathf.Clamp(value, -100f, 100f);
    }
    public void SetPostExposure(float value)
    {
        colorAdjustments.postExposure.value = value;
    }

    //Boss2_Shield4の演出用
    public void LerpColor(Color from, Color to, float duration)
    {
        if (colorAdjustments == null) return;

        if (colorCoroutine != null)
            StopCoroutine(colorCoroutine);

        colorCoroutine = StartCoroutine(LerpColorCoroutine(from, to, duration));
    }
    IEnumerator LerpColorCoroutine(Color from, Color to, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / duration);

            colorAdjustments.colorFilter.value = Color.Lerp(from, to, t);
            yield return null;
        }
        colorAdjustments.colorFilter.value = to;
    }
}
