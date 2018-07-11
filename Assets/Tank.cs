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

    //入力保持用
    private bool left;
    private bool right;
    private bool up;
    private bool down;
    private bool space;

    private int actionPoint; //行動ポイント

    private float Direction; //方向
    private float rotateTime; //回転処理中の時間
    private bool movable; //動けるかどうか

    //=============================================================
    private void Init () {
        CRef();
        Direction = 0f;
        rotateTime = 0f;
        movable = true;
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
            switch(DeV.TEST_ACTIONPATTERN_0[actionPoint]) {
                case 0:
                break;

                case 1:
                left = true;
                break;

                case 2:
                right = true;
                break;

                case 3:
                up = true;
                break;

                case 4:
                down = true;
                break;

                case 5:
                left = true;
                up = true;
                break;

                case 6:
                left = true;
                down = true;
                break;

                case 7:
                right = true;
                up = true;
                break;

                case 8:
                right = true;
                down = true;
                break;

                case 9:
                space = true;
                break;

                default:
                break;
            }

            actionPoint++;
            if(actionPoint >= DeV.ACTIONPATTERN_LENGTH) {
                actionPoint = 0;
            }

            /*
            if(Random.Range(0,3) == 2) {
                left = true;
            }

            if(Random.Range(0,3) == 2) {
                right = true;
            }

            if(Random.Range(0,3) == 2) {
                up = true;
            }

            if(Random.Range(0,3) == 2) {
                down = true;
            }

            if(Random.Range(0,3) == 2) {
                space = true;
            }
            */

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
}