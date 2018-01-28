using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//設定したアニメフラグの名前
public enum AnimeKeyFlag
{
    keyjump,
    keyRum,
}


public class Player : MonoBehaviour {

    [SerializeField] private Vector3 velocity;  //移動方向

    [SerializeField] private float moveSpeed = 5.0f; //移動速度

    [SerializeField] private float applySpeed = 0.2f;       //回転の適用速度

    [SerializeField] private PlayerFollowCamera refCamera;  //カメラ水平回転参照

    private Animator animator;      //アニメコンポーネント


    //初期化
    void Start()
    {
        //自分に設定されているアニメコンポーネント取得
        this.animator = GetComponent<Animator>();
    }
	
	//更新
	void Update () {
        //WASDの入力でZX面を移動する方向を決定
        velocity = Vector3.zero;
        if(Input.GetKey(KeyCode.W))         //run_F
        {
            velocity.z += 1;
        }
        if (Input.GetKey(KeyCode.S))        //run_F
        {
            velocity.z -= 1;
            
        }
        if (Input.GetKey(KeyCode.D))       //run_R
        {
            velocity.x += 1;
        }
        if (Input.GetKey(KeyCode.A))        //run_L
        {
            velocity.x -= 1;
        }

            //1秒でmoveSpeed分進むように調整
            velocity = velocity.normalized * moveSpeed * Time.deltaTime;

        //いずれかの方向に移動している場合
        if (velocity.magnitude > 0)
        {

            //プレイヤー回転
            //無回転時のプレイヤー高等部をカメラの水平回転で回した移動の方向にだんだん回して近づける
            transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(refCamera.hRotation * velocity),applySpeed);

            //アニメーション設定
            this.animator.SetBool(AnimeKeyFlagStr.ObtainAnimeKeyFlag(AnimeKeyFlag.keyRum), true);

            //プレイヤーの位置更新
            //カメラの水平回転で回した移動方向を足す
            transform.position +=  refCamera.hRotation * velocity;
        }
        else
        {
            this.animator.SetBool(AnimeKeyFlagStr.ObtainAnimeKeyFlag(AnimeKeyFlag.keyRum), false);

        }

    }
}

public static class AnimeKeyFlagStr
{
    public static string ObtainAnimeKeyFlag(this AnimeKeyFlag flag)
    {
        string[] valuse = {"isJump","isRun" };
        return valuse[(int)flag];
    }
}