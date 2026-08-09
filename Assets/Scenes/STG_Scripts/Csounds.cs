using UnityEngine;

public class Csounds : MonoBehaviour
{
    public static Csounds Instance;

    public AudioSource SE;
    public AudioSource bossBGM;
    public AudioSource abanBGM;

    public AudioClip SE_Destroy_Enemy;
    public AudioClip SE_Get_pointitem;

    void Awake()
    {
       Instance = this;
    }

    public void Play_Destroy_Enemy()
    {
      if(SE !=null)
      {
        SE.PlayOneShot(SE_Destroy_Enemy);
      }
    }
    public void Get_pointitem()
    {
      if(SE !=null)
      {
        SE.clip = SE_Get_pointitem;
        SE.Play();
      }
    }

    public void Play_bossBGM()
    {
        bossBGM.Play();
    }

    public void Play_abanBGM()
    {
        abanBGM.Play();
    }
}
