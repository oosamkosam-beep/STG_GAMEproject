using UnityEngine;

public class CSPGauge : MonoBehaviour
{
    [SerializeField] RectTransform bar;

    // HP満タン時と0時のX座標
    [SerializeField] float maxPosX = 100f;
    [SerializeField] float minPosX = 0f;

    public void ResetspGage()
    {
      SetRate(1f); 
    }

    public void SetRate(float rate)
    {
        rate = Mathf.Clamp01(rate);

        float x = Mathf.Lerp(minPosX, maxPosX, rate);

        Vector3 pos = bar.localPosition;
        pos.x = x;
        bar.localPosition = pos;
    }
    
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
