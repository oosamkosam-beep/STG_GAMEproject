using UnityEngine;

//便利な関数を管理する静的クラス  
public static class CUtility
{
   //移動可能な範囲
   static Vector2 MoveLimitTopLeft =new Vector2(-3.7f,4.4f); 	  
   static Vector2 MoveLimitButtomRight =new Vector2(3.7f,-4.4f); 	  
    
   static Vector2 EnemyLimitTopLeft = new Vector2(-10.0f,7.5f); 	  
   static Vector2 EnemyLimitButtomRight = new Vector2(10.0f,-7.5f); 	  

   static Vector2 BulletLimitTopLeft = new Vector2(-13.0f,8.0f); 	  
   static Vector2 BulletLimitButtomRight = new Vector2(13.0f,-6.0f); 	  
   //指定された移動可能な範囲に収めた値を返す
   public static Vector3 ClampPosition(Vector3 position) 
   {
       return new Vector3
       ( 
           Mathf.Clamp(position.x, MoveLimitTopLeft.x, MoveLimitButtomRight.x),
           Mathf.Clamp(position.y, MoveLimitButtomRight.y, MoveLimitTopLeft.y),
           1
       ); 
   }
   public static bool IsOut(Vector3 position)
   {
           return (position. x < EnemyLimitTopLeft.x ||
                   EnemyLimitTopLeft.y < position.y || 
                   EnemyLimitButtomRight.x < position.x || 
                   position.y < EnemyLimitButtomRight.y);
   }

   public static bool BulletIsOut(Vector3 position)
   {
           return (position. x < BulletLimitTopLeft.x ||
                   BulletLimitTopLeft.y < position.y || 
                   BulletLimitButtomRight.x < position.x || 
                   position.y < BulletLimitButtomRight.y);
   }
   //指定された角度 ( 0 ~ 360 ) をベクトルに変換して返す
   public static Vector3 GetDirection360(float angle360) 
   { 
       float angle = angle360 * Mathf.Deg2Rad;	   
       return new Vector3
       ( 
           Mathf.Cos(angle),
           Mathf.Sin(angle),
           0
       ); 
   }
   //引数に角度( 0 ~ 1.0f )を渡すとベクトルに変換して返す
   public static Vector3 GetDirection1(float angle1) 
   {
       float angle = angle1 / (3.14f * 2);
       return new Vector3
       ( 
           Mathf.Cos(angle),
           Mathf.Sin(angle),
           0
       ); 
   }
   //引数に角度( 0 ~ PI2 )を渡すとベクトルに変換して返す
   public static Vector3 GetDirectionPI2(float angle_pi2) 
   {
       return new Vector3
       ( 
           Mathf.Cos(angle_pi2),
           Mathf.Sin(angle_pi2),
           0
       ); 
   }
}
