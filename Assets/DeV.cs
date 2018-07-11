using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class DeV : MonoBehaviour {
    public static readonly float TANK_MOVE_SPEED = 0.03f;
    public static readonly float TANK_ROTATE_SPEED = 20f;

    public static readonly float TANK_BULLET_SPEED = 0.2f;
    public static readonly int TANK_BULLET_MAX_HITPOINT = 2;
    public static readonly int TANK_BULLET_NUM_LIMIT = 5;

    public static readonly Vector3 TANK_BULLET_INIT_POSITION_RIGHT = new Vector3(0.3f,0.08f,0.1f);
    public static readonly Vector3 TANK_BULLET_INIT_POSITION_FORWARD = new Vector3(0.1f,0.08f,0.3f);

    //行動パターン
    public static readonly int[] TEST_ACTIONPATTERN_0 = { 1,1,1,1,1,1,1,1,0,0,2,2,2,2,2,2,2,2,0,0,3,3,3,3,3,3,3,3,0,0,4,4,4,4,4,4,4,4,0,0,5,5,5,5,5,5,5,5,0,0,6,6,6,6,6,6,6,6,0,0,7,7,7,7,7,7,7,7,0,0,8,8,8,8,8,8,8,8,0,0 };
    public static readonly int ACTIONPATTERN_LENGTH = 80;
}