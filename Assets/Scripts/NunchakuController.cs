using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NunchakuController : MonoBehaviour
{
    Vector3 prePos; //前フレームの指の位置座標
    Vector3 nowPos; //今フレームの指の位置座標
    Vector3 preVec; //前フレームのcenter→指の方向ベクトル
    Vector3 nowVec; //今フレームのcenter→指の方向ベクトル

    [SerializeField] GameObject center; //ヌンチャクの真ん中
    Vector3 centerPos; //ヌンチャクの真ん中の位置座標
    Camera mainCamera; //MainCameraを格納しておく為の変数

    void Start()
    {
        centerPos = center.transform.position; //ヌンチャクの真ん中の座標を変数centerPosに格納
        mainCamera = Camera.main; //MainCameraを変数mainCameraに格納
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)){ //マウスのボタンを押し込んだ瞬間
            Vector3 mousePos = Input.mousePosition; //マウスの位置座標を変数mousePosに格納
            mousePos.z = -mainCamera.transform.position.z; //マウスの位置座標の奥行き補正
            prePos = mainCamera.ScreenToWorldPoint(mousePos); //mousePosをスクリーン座標→World座標に変換し、変数prePosに格納
        }

        if(Input.GetMouseButton(0)){
            Vector3 mousePos = Input.mousePosition; //マウスの位置座標を変数mousePosに格納
            mousePos.z = -mainCamera.transform.position.z; //マウスの位置座標の奥行き補正
            nowPos = mainCamera.ScreenToWorldPoint(mousePos); //mousePosをスクリーン座標→World座標に変換し、変数nowPosに格納

            preVec = (prePos - centerPos).normalized; //前フレームのcenter→指の方向ベクトル(単位ベクトル化)
            nowVec = (nowPos - centerPos).normalized; //今フレームのcenter→指の方向ベクトル(単位ベクトル化)
            // Debug.Log("preVec : " + preVec);
            // Debug.Log("nowVec : " + nowVec);

            var axis = Vector3.Cross(preVec, nowVec); //外積を計算
            float angle = Vector3.Angle(preVec, nowVec) * (axis.z < 0 ? -1 : 1); //preVecとnowVecのなす角を計算
            Debug.Log("angle : " + angle);

            //多分回転同期させる処理
            transform.eulerAngles += new Vector3(0, 0, angle); //指の移動分だけヌンチャクを回転

            prePos = nowPos; //preVecにnowVecを代入(次フレームの計算で使う為)
        }
    }
}
