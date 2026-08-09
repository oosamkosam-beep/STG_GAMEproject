using System.Collections;
using UnityEngine;

public class CDestroy_Enemy : MonoBehaviour
{
  void Start()
  {
      Destroy(gameObject, 1.5f);
      Csounds.Instance.Play_Destroy_Enemy();
  }
}
