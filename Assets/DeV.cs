using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class DeV : MonoBehaviour {
    public static readonly float TANK_MOVE_SPEED = 0.03f;
    public static readonly float TANK_ROTATE_SPEED = 15f;

    public static readonly float TANK_BULLET_SPEED = 0.05f;
    public static readonly int TANK_BULLET_MAX_HITPOINT = 3;
    public static readonly int TANK_BULLET_NUM_LIMIT = 10;

    public static readonly Vector3 TANK_BULLET_INIT_POSITION_RIGHT = new Vector3(0.3f,0.08f,0.1f);
    public static readonly Vector3 TANK_BULLET_INIT_POSITION_FORWARD = new Vector3(0.1f,0.08f,0.3f);

    public static readonly float PROV_NOMOVE = 0.1f;
    public static readonly float PROV_RIGHT = 0.01f;
    public static readonly float PROV_LEFT = 0.01f;
    public static readonly float PROV_UP = 0.01f;
    public static readonly float PROV_DOWN = 0.01f;

    public static readonly float MAX_BULLET_UNHIT_TIME=0.3f;
}