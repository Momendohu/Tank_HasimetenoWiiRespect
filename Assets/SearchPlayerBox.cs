using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class SearchPlayerBox : MonoBehaviour {
    //=============================================================
    public bool IsSeePlayer;

    //=============================================================
    private void Init () {
        CRef();
        IsSeePlayer = false;
    }

    //=============================================================
    private void CRef () {

    }

    //=============================================================
    private void Awake () {
        Init();
    }

    private void OnTriggerEnter (Collider other) {
        if(other.tag.Equals("Player")) {
            IsSeePlayer = true;
        }
    }
}