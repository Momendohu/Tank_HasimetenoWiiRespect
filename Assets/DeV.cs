using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class DeV : MonoBehaviour {
    public static readonly float TANK_MOVE_SPEED = 0.04f;
    public static readonly float TANK_ROTATE_SPEED = 3f;

    public static readonly float TANK_BULLET_SPEED = 0.1f;
    public static readonly int TANK_BULLET_MAX_HITPOINT = 10;
    public static readonly int TANK_BULLET_NUM_LIMIT = 10;

    public static readonly Vector3 TANK_BULLET_INIT_POSITION_RIGHT = new Vector3(0.3f,0.08f,0.1f);
    public static readonly Vector3 TANK_BULLET_INIT_POSITION_FORWARD = new Vector3(0.1f,0.08f,0.3f);
}