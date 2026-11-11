using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player_1;
    public Transform player_2;

    public Transform player;

    [Range(0, 1)] 
    public float moveRatio = 0.3f; // 0なら動かない、1ならプレイヤーと等速
    
    private float playerStartX;
    private float cameraStartX;

    void Awake()
    {
      if(!GameData.Player)
      {
        player = player_1;
      }
      else
      {
        player = player_2;
      }

    }
    void Start()
    {
        playerStartX = player.position.x;
        cameraStartX = transform.position.x;
    }

    void LateUpdate()
    {
        float playerDeltaX = player.position.x - playerStartX;
        
        float newX = cameraStartX + (playerDeltaX * moveRatio);
        
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
