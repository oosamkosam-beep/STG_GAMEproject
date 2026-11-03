using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Shared HP Data")]
public class SharedEnemyHPData : ScriptableObject
{
    [SerializeField] GameObject lpGaugeRoot; // LPgaugeの親（Canvas配下など）
    [SerializeField] GameObject spGaugeRoot; 

    public int maxHP = 2400;
    public int maxSP = 1000;
    public int currentHP;
    public int currentSP;

    public CLPGauge lpGauge;
    public CSPGauge spGauge;


    public void Initialize()
    {
        currentHP = maxHP;
        if (lpGauge != null)
        {
            lpGauge.ResetlpGage();
        }
	
        currentSP = maxSP;
        if (spGauge != null)
        {
            spGauge.ResetspGage();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Max(currentHP, 0);

        if (lpGauge != null)
        {
            lpGauge.SetRate((float)currentHP / maxHP);
        }
    }

    public void TakeDamage_2(int damage)
    {
        currentSP -= damage;
        currentSP = Mathf.Max(currentSP, 0);

        if (spGauge != null)
        {
            spGauge.SetRate((float)currentSP / maxSP);
        }

    }
}
