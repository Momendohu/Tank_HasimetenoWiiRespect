using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class FieldManager : MonoBehaviour {
    //=============================================================
    public List<Vector3> goalPoint = new List<Vector3>();

    //=============================================================
    private void Init () {
        CRef();

        goalPoint.Add(new Vector3(2.6f,-0.92f,1.5f));
        goalPoint.Add(new Vector3(2.3f,-0.92f,0.3f));
        goalPoint.Add(new Vector3(2.2f,-0.92f,-1.4f));
        goalPoint.Add(new Vector3(-0.7f,-0.92f,-1.4f));
        goalPoint.Add(new Vector3(-0.7f,-0.92f,1.4f));
        goalPoint.Add(new Vector3(-2.7f,-0.92f,1.4f));
        goalPoint.Add(new Vector3(-2.7f,-0.92f,0.3f));
        goalPoint.Add(new Vector3(-2.7f,-0.92f,-1.7f));
    }

    //=============================================================
    private void CRef () {

    }

    //=============================================================
    private void Awake () {
        Init();
    }

    private void Start () {

    }

    private void Update () {

    }

    //=============================================================
    /// <summary>
    /// 特定の位置から一番近いゴールポイントを探す
    /// </summary>
    /// <param name="_vec">特定の位置</param>
    /// <returns></returns>
    public int NearPoint (Vector3 _vec) {
        int near = 0;
        float nearMag = int.MaxValue;
        for(int i = 0;i < goalPoint.Count;i++) {
            float mag = Vector3.SqrMagnitude(_vec - goalPoint[i]);
            if(nearMag > mag) {
                near = i;
            }
        }

        return near;
    }
}