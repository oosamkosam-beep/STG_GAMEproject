using UnityEngine;

public class RoadObject2D : MonoBehaviour
{
    [Header("道路上の位置")]
    [SerializeField]
    private float roadX = 0f;


    [Header("現在の奥行き")]
    [SerializeField]
    private float distance = 20f;


    public float RoadX
    {
        get => roadX;

        set
        {
            roadX = Mathf.Clamp(value, -1f, 1f);
            UpdatePosition();
        }
    }


    public float Distance
    {
        get => distance;

        set
        {
            distance = value;
            UpdatePosition();
        }
    }


    private void Start()
    {
        // 初期位置を反映
        UpdatePosition();
    }


    /// <summary>
    /// 奥行きから画面座標を計算
    /// </summary>
    public void UpdatePosition()
    {
        if (RoadSpace2D.Instance == null)
            return;


        Vector3 position =
            RoadSpace2D.Instance.RoadToWorld(
                roadX,
                distance
            );


        // Zは現在のオブジェクトの値を維持
        transform.position =
            new Vector3(
                position.x,
                position.y,
                transform.position.z
            );
    }


    /// <summary>
    /// Road上の位置を設定
    /// </summary>
    public void SetRoadPosition(
        float x,
        float newDistance)
    {
        roadX = Mathf.Clamp(
            x,
            -1f,
            1f
        );

        distance = newDistance;

        UpdatePosition();
    }


    /// <summary>
    /// 奥 → 手前へ一定速度で移動
    /// </summary>
    public void MoveDepth(float speed)
    {
        // 奥行き距離を減らす
        distance -= speed * Time.deltaTime;


        // 手前まで来た
        if (distance <= RoadSpace2D.Instance.NearDistance)
        {
            distance =
                RoadSpace2D.Instance.NearDistance;
        }


        UpdatePosition();
    }


    /// <summary>
    /// 左右移動
    /// </summary>
    public void MoveRoadX(float speed)
    {
        roadX += speed * Time.deltaTime;

        roadX = Mathf.Clamp(
            roadX,
            -1f,
            1f
        );

        UpdatePosition();
    }


    /// <summary>
    /// 手前まで来たか
    /// </summary>
    public bool IsNear()
    {
        return distance <=
               RoadSpace2D.Instance.NearDistance;
    }
}
