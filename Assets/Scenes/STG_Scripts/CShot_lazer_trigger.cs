using UnityEngine;

public class CShot_lazer_trigger : MonoBehaviour
{
    public int ShotPower = 20;

    void Start()
    {
        Destroy(gameObject, 0.2f);
    }

    void Update()
    {
        if (CUtility.BulletIsOut(transform.position))
        {
            Destroy(gameObject);
        }
    }
}
