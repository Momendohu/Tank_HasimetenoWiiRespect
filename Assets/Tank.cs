using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class Tank : MonoBehaviour {
    //=============================================================
    private GameObject tower;

	//=============================================================
	private void Init(){
		CRef();
	}

	//=============================================================
	private void CRef(){
        tower = GameObject.Find("Tank/tower");
	}

	//=============================================================
	private void Awake () {
		Init();
	}

	private void Start () {
		
	}
	
	private void Update () {
        tower.transform.eulerAngles+=Time.deltaTime*Vector3.up*10;
	}
}