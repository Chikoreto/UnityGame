using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowCamera : MonoBehaviour {

    [SerializeField] private Transform Player;              //注視対象

    [SerializeField] private float distance = 15.0f;        //注視対象からカメラを離す距離
    [SerializeField] public Quaternion vRotation;          //カメラの垂直回転
    [SerializeField] public Quaternion hRotation;          //カメラの水平回転
    [SerializeField] private float turnSpeed = 5.0f;




    // Use this for initialization
    void Start () {
        //回転初期化
        vRotation = Quaternion.Euler(30, 0, 0);             //垂直回転はX方向30度(見下ろし)
        hRotation = Quaternion.identity;                    //水平回転はなし
        transform.rotation = hRotation * vRotation;         //最終的なカメラ角度。垂直回転後水平回転

        //位置の初期化
        //playerの位置から距離distance手前に引いた値を設定
        transform.position = Player.position - transform.rotation * Vector3.forward * distance;

	}
	
	void LateUpdate () {

        //マウスの移動に応じて回転(水平)
        hRotation *= Quaternion.Euler(0,Input.GetAxis("Mouse X")*turnSpeed,0);



        // カメラの回転(transform.rotation)の更新
        // 方法1 : 垂直回転してから水平回転する合成回転とします
        transform.rotation = hRotation * vRotation;

        //位置の初期化
        //playerの位置から距離distance手前に引いた値を設定
        transform.position = Player.position - transform.rotation * Vector3.forward * distance;

    }
}
