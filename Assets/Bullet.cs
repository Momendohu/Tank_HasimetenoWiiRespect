using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class Bullet : MonoBehaviour {
    //=============================================================
    public GameObject Formar; //発射元

    public Vector3 Speed;
    public int HitPoint; //体力

    private Vector3 screenPos;

    private float unHitTime;

    //=============================================================
    private void Init () {
        CRef();

        HitPoint = DeV.TANK_BULLET_MAX_HITPOINT;
    }

    //=============================================================
    private void CRef () {

    }

    //=============================================================
    private void Awake () {
        Init();
        unHitTime = DeV.MAX_BULLET_UNHIT_TIME;
    }

    private void Start () {

    }

    private void Update () {
        transform.position += Speed;
        unHitTime -= Time.deltaTime;

        //カメラの外に出たら消す
        screenPos = GameObject.Find("Main Camera").GetComponent<Camera>().WorldToViewportPoint(this.transform.position);
        if(screenPos.x <= 0 || screenPos.x >= 1 || screenPos.y <= 0 || screenPos.y >= 1) {
            Formar.GetComponent<Tank>().BulletNum--;
            Destroy(this.gameObject);
        }

        //体力が0以下になったら消す
        if(HitPoint <= 0) {
            Formar.GetComponent<Tank>().BulletNum--;
            Destroy(this.gameObject);
        }

        //発射元が倒されたら消す
        if(Formar.GetComponent<Tank>().RemoveFlag) {
            Formar.GetComponent<Tank>().BulletNum--;
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter (Collider other) {
        if(unHitTime <= 0) {
            if(other.tag.Equals("Player")) {
                other.GetComponent<Tank>().RemoveFlag = true;
            }

            if(other.tag.Equals("Enemy")) {
                other.GetComponent<Tank>().RemoveFlag = true;
            }
        }

        if(other.tag.Equals("WallUD")) {
            HitPoint--;
            Speed = new Vector3(Speed.x,Speed.y,Speed.z * (-1));

        }

        if(other.tag.Equals("WallRL")) {
            HitPoint--;
            Speed = new Vector3(Speed.x * (-1),Speed.y,Speed.z);
        }
    }
}