using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class Tank : MonoBehaviour {
    //=============================================================
    private GameObject tower;
    private GameObject bodies;

    //=============================================================
    private void Init () {
        CRef();
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
        tower.transform.eulerAngles += Time.deltaTime * Vector3.up * 10;
        bodies.transform.eulerAngles += Time.deltaTime * Vector3.up * 30;

        Move();
    }

    private void Move () {
        if(Input.GetKey(KeyCode.LeftArrow)) {
            transform.position -= Vector3.right * 0.01f * DeV.TANK_MOVE_SPEED;
        }

        if(Input.GetKey(KeyCode.RightArrow)) {
            transform.position += Vector3.right * 0.01f * DeV.TANK_MOVE_SPEED;
        }

        if(Input.GetKey(KeyCode.UpArrow)) {
            transform.position += Vector3.forward * 0.01f * DeV.TANK_MOVE_SPEED;
        }

        if(Input.GetKey(KeyCode.DownArrow)) {
            transform.position -= Vector3.forward * 0.01f * DeV.TANK_MOVE_SPEED;
        }
    }
}