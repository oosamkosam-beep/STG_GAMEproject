using UnityEngine;
using System.Collections;

public class Bomb_1 : MonoBehaviour
{
    void Start()
    {
      StartCoroutine(EndPhase());
    }

    IEnumerator EndPhase()
    {
      yield return new WaitForSeconds(1.6f);
        Destroy(gameObject);
    }
}
