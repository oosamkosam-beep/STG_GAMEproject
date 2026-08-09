using UnityEngine;

public class RoadSpace2D : MonoBehaviour
{
    public static RoadSpace2D Instance { get; private set; }
	public static float Autospeed = 0.25f;

    [Header("消失点")]
    [SerializeField]
    private Vector2 vanishingPoint = new Vector2(0.6783f, 8.757f);

    [Header("道路の手前側")]
    [SerializeField]
    private Vector2 bottomLeft = new Vector2(-3.48f, -9f);

    [SerializeField]
    private Vector2 bottomRight = new Vector2(5.293f, -9f);


    [Header("実際の奥行き")]
    [Tooltip("一番奥の奥行き")]
    private float farDistance = 20f;

    [Tooltip("一番手前の奥行き")]
    private float nearDistance = 2.2f;


    private void Awake()
    {
        Instance = this;
    }


    /// <summary>
    /// Road座標を画面上のWorld座標へ変換する。
    ///
    /// x:
    /// -1 = 左
    ///  0 = 中央
    /// +1 = 右
    ///
    /// distance:
    /// farDistance  = 最奥
    /// nearDistance = 最手前
    /// </summary>
    public Vector3 RoadToWorld(float x, float distance)
    {
        // ----------------------------------------
        // 奥行きを範囲内に収める
        // ----------------------------------------

        distance = Mathf.Clamp(
            distance,
            nearDistance,
            farDistance
        );


        // ----------------------------------------
        // 手前側の道路上のX位置
        // ----------------------------------------

        float x01 = (x + 1f) * 0.5f;

        Vector2 nearPosition =
            Vector2.Lerp(
                bottomLeft,
                bottomRight,
                x01
            );


        // ----------------------------------------
        // 透視投影
        // ----------------------------------------
        //
        // 画面上の位置は
        //
        //      1
        //    ─────
        //      Z
        //
        // に比例する。
        //
        // そのため、奥行きZが小さくなるほど
        // 画面上の移動量が大きくなる。
        //

        float inverseFar =
            1f / farDistance;

        float inverseNear =
            1f / nearDistance;

        float inverseDistance =
            1f / distance;


        // 0 = 消失点
        // 1 = 手前
        float t =
            (inverseDistance - inverseFar) /
            (inverseNear - inverseFar);


        t = Mathf.Clamp01(t);


        // ----------------------------------------
        // 消失点 → 手前側
        // ----------------------------------------

        Vector2 position =
            Vector2.Lerp(
                vanishingPoint,
                nearPosition,
                t
            );


        return new Vector3(
            position.x,
            position.y,
            0f
        );
    }


    /// <summary>
    /// 最奥の距離
    /// </summary>
    public float FarDistance
    {
        get => farDistance;
    }


    /// <summary>
    /// 最手前の距離
    /// </summary>
    public float NearDistance
    {
        get => nearDistance;
    }
}
