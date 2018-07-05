using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class Tank : MonoBehaviour {
    //=============================================================
    private GameObject tower;
    private GameObject bodies;

    private float Direction; //方向
    private float rotateTime;
    private bool rotateLeftCompleted;

    //=============================================================
    private void Init () {
        CRef();
        Direction = 0f;
        rotateTime = 0f;
    }

    //=============================================================
    private void CRef () {
        tower = GameObject.Find("Tank/tower");
        bodies = GameObject.Find("Tank/bodies");
    }

    //=============================================================
    private void Awake () {
        Init();
    }

    private void Start () {

    }

    private void Update () {
        //tower.transform.eulerAngles += Time.deltaTime * Vector3.up * 10;
        //bodies.transform.eulerAngles += Time.deltaTime * Vector3.up * 30;

        Move();
    }

    //_numが指定した数値(_point)の範囲内(_range)の数値かどうか
    public bool IsRange (float _num,float _point,float _range) {
        return (_point - _range <= _num && _num <= _point + _range);
    }

    private void Move () {
        bool left = Input.GetKey(KeyCode.LeftArrow);
        bool right = Input.GetKey(KeyCode.RightArrow);
        bool up = Input.GetKey(KeyCode.UpArrow);
        bool down = Input.GetKey(KeyCode.DownArrow);

        Direction = transform.eulerAngles.y;
        if(left) {
            if(IsRange(Direction,180,0.1f) == false && IsRange(Direction,-180,0.1f) == false) {
                rotateTime += Time.deltaTime * DeV.TANK_ROTATE_SPEED;
                transform.eulerAngles = Vector3.up * Mathf.LerpAngle(Direction,180,rotateTime);
                if(rotateTime >= 1) {
                    rotateTime = 0;
                    Direction = 180;
                    transform.eulerAngles = new Vector3(0,Direction,0);
                }
            } else {
                rotateTime = 0;
                transform.position -= Vector3.right * 0.01f * DeV.TANK_MOVE_SPEED;
            }

            return;
        }

        if(right) {
            if(IsRange(Direction,0,0.1f) == false) {
                rotateTime += Time.deltaTime * DeV.TANK_ROTATE_SPEED;
                transform.eulerAngles = Vector3.up * Mathf.LerpAngle(Direction,0,rotateTime);
                if(rotateTime >= 1) {
                    rotateTime = 0;
                    Direction = 0;
                    transform.eulerAngles = new Vector3(0,Direction,0);
                }
            } else {
                rotateTime = 0;
                transform.position += Vector3.right * 0.01f * DeV.TANK_MOVE_SPEED;
            }

            return;
        }

        if(up) {
            transform.position += Vector3.forward * 0.01f * DeV.TANK_MOVE_SPEED;
        }

        if(down) {
            transform.position -= Vector3.forward * 0.01f * DeV.TANK_MOVE_SPEED;
        }

        rotateTime = 0;
    }
}