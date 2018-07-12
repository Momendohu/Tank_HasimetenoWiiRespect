using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class Tank : MonoBehaviour {
    //=============================================================
    public bool IsUseAI = false;
    public int BulletNum;

    private GameObject tower;
    private GameObject bodies;
    private FieldManager fieldManager;

    private RaycastHit hit; //レイ

    //入力保持用
    private bool left;
    private bool right;
    private bool up;
    private bool down;
    private bool space;

    private float Direction; //方向
    private float rotateTime; //回転処理中の時間
    private bool movable; //動けるかどうか

    private int AIgoal; //AI使用時のゴール

    //=============================================================
    private void Init () {
        CRef();
        Direction = 0f;
        rotateTime = 0f;
        movable = true;

        AIgoal = 1;
    }

    //=============================================================
    private void CRef () {
        tower = GameObject.Find("Tank/tower");
        bodies = GameObject.Find("Tank/bodies");
        fieldManager = GameObject.Find("FieldManager").GetComponent<FieldManager>();
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

    //=====================================================================================================================================
    /// <summary>
    /// とある数値が指定した数値の範囲内かどうか
    /// </summary>
    /// <param name="_num">対象の数値</param>
    /// <param name="_point">目標となる数値</param>
    /// <param name="_range">範囲</param>
    /// <returns>とある数値が指定した数値の範囲内かどうか</returns>
    public bool IsRange (float _num,float _point,float _range) {
        return (_point - _range <= _num && _num <= _point + _range);
    }

    //=====================================================================================================================================
    /// <summary>
    /// 回転処理と移動処理
    /// </summary>
    /// <param name="_trigger">移動条件</param>
    /// <param name="_directionAngle">向く方向</param>
    /// <param name="_directionSpd">向いた時の加算するベクトル</param>
    /// <returns>処理をしたかどうか</returns>
    private bool RotateDirection (bool _trigger,float _directionAngle,Vector3 _directionSpd) {
        if(_trigger) {
            //打ち消しあう方向ならリターンする
            if(_directionAngle == -1) {
                return true;
            }

            if(IsRange(Direction,_directionAngle,0.1f) == false && IsRange(Direction,-_directionAngle,0.1f) == false) {
                rotateTime += Time.deltaTime * DeV.TANK_ROTATE_SPEED;
                transform.eulerAngles = Vector3.up * Mathf.LerpAngle(Direction,_directionAngle,rotateTime);
                if(rotateTime >= 1) {
                    rotateTime = 0;
                    Direction = _directionAngle;
                    transform.eulerAngles = new Vector3(0,Direction,0);
                }
            } else {
                rotateTime = 0;
                transform.position += _directionSpd * DeV.TANK_MOVE_SPEED;
            }
            return true;
        }

        return false;
    }

    //=====================================================================================================================================
    /// <summary>
    /// 行動変数の初期化
    /// </summary>
    private void ActionInit () {
        left = false;
        right = false;
        up = false;
        down = false;
        space = false;
    }

    //=====================================================================================================================================
    /// <summary>
    /// 移動処理を統括した関数
    /// </summary>
    private void Move () {
        ActionInit();

        if(IsUseAI) {
            bool ray1 = Physics.Raycast(transform.position,new Vector3(Mathf.Cos(Mathf.Deg2Rad * Direction),0,-Mathf.Sin(Mathf.Deg2Rad * Direction)),out hit,Mathf.Infinity);
            bool ray2 = Physics.Raycast(transform.position,new Vector3(Mathf.Cos(Mathf.Deg2Rad * (Direction + 30)),0,-Mathf.Sin(Mathf.Deg2Rad * (Direction + 30))),out hit,Mathf.Infinity);
            bool ray3 = Physics.Raycast(transform.position,new Vector3(Mathf.Cos(Mathf.Deg2Rad * (Direction - 30)),0,-Mathf.Sin(Mathf.Deg2Rad * (Direction - 30))),out hit,Mathf.Infinity);
            if(ray1 || ray2 || ray3) {
                if(hit.collider.tag.Equals("Player")) {
                    space = true;
                }
            }

            Vector3 vec = fieldManager.goalPoint[AIgoal] - this.transform.position;
            if(vec.sqrMagnitude > 0.1f) {
                if(Mathf.Abs(vec.x) >= 0.05f) {
                    if(vec.x > 0) {
                        right = true;
                    } else if(vec.x < 0) {
                        left = true;
                    }
                }

                if(Mathf.Abs(vec.z) >= 0.05f) {
                    if(vec.z > 0) {
                        up = true;
                    } else if(vec.z < 0) {
                        down = true;
                    }
                }
            } else {
                ChangeAIGoal(Random.Range(0,fieldManager.goalPoint.Count));
            }
        } else {

            left = Input.GetKey(KeyCode.LeftArrow);
            right = Input.GetKey(KeyCode.RightArrow);
            up = Input.GetKey(KeyCode.UpArrow);
            down = Input.GetKey(KeyCode.DownArrow);
            space = Input.GetKeyDown(KeyCode.Space);
        }

        Direction = transform.eulerAngles.y; //方向の更新

        //動作可能状態なら
        if(movable) {
            if(space) {
                StartCoroutine(CreateBullet());
            }

            if(RotateDirection(left && right && up && down,-1,Vector3.zero)) return;

            if(RotateDirection(left && right && up,270,Vector3.forward)) return;
            if(RotateDirection(left && right && down,90,Vector3.back)) return;
            if(RotateDirection(left && up && down,180,Vector3.left)) return;
            if(RotateDirection(right && up && down,0,Vector3.right)) return;

            if(RotateDirection(left && up,225,Vector3.left + Vector3.forward)) return;
            if(RotateDirection(left && right,-1,Vector3.zero)) return;
            if(RotateDirection(left && down,135,Vector3.left + Vector3.back)) return;
            if(RotateDirection(right && up,315,Vector3.right + Vector3.forward)) return;
            if(RotateDirection(right && down,45,Vector3.right + Vector3.back)) return;
            if(RotateDirection(up && down,-1,Vector3.zero)) return;

            if(RotateDirection(left,180,Vector3.left)) return;
            if(RotateDirection(right,0,Vector3.right)) return;
            if(RotateDirection(up,270,Vector3.forward)) return;
            if(RotateDirection(down,90,Vector3.back)) return;
        }

        rotateTime = 0;
    }

    //=====================================================================================================================================
    /// <summary>
    /// 弾を作成する
    /// </summary>
    /// <returns></returns>
    private IEnumerator CreateBullet () {
        if(BulletNum <= DeV.TANK_BULLET_NUM_LIMIT) {
            BulletNum++;
            GameObject obj = Instantiate(Resources.Load("Prefabs/Bullet")) as GameObject;
            obj.transform.position = this.transform.position +
                new Vector3(
                    DeV.TANK_BULLET_INIT_POSITION_RIGHT.x * Mathf.Cos(Mathf.Deg2Rad * Direction) + DeV.TANK_BULLET_INIT_POSITION_FORWARD.x * Mathf.Sin(Mathf.Deg2Rad * Direction),
                    DeV.TANK_BULLET_INIT_POSITION_RIGHT.y,
                    DeV.TANK_BULLET_INIT_POSITION_RIGHT.z * Mathf.Cos(Mathf.Deg2Rad * Direction) + DeV.TANK_BULLET_INIT_POSITION_FORWARD.z * Mathf.Sin(Mathf.Deg2Rad * Direction) * (-1)
                );

            obj.GetComponent<Bullet>().Speed +=
                new Vector3(DeV.TANK_BULLET_SPEED * Mathf.Cos(Mathf.Deg2Rad * Direction),0,DeV.TANK_BULLET_SPEED * Mathf.Sin(Mathf.Deg2Rad * Direction) * (-1));

            obj.GetComponent<Bullet>().Formar = this.gameObject;

        }
        yield break;
    }

    //=====================================================================================================================================
    /// <summary>
    /// AIGoalを変更する
    /// </summary>
    private void ChangeAIGoal (int _goal) {
        AIgoal = _goal;
    }

    private void OnCollisionEnter (Collision collision) {
        //Debug.Log("HIT:" + collision.gameObject.tag);
        if(IsUseAI) {
            if(collision.gameObject.gameObject.tag.Equals("WallRL") || collision.gameObject.gameObject.tag.Equals("WallUD")) {
                ChangeAIGoal(Random.Range(0,fieldManager.goalPoint.Count));
            }
        }
    }
}