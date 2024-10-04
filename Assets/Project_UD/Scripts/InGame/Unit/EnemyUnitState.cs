using MonsterLove.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Idle,
    Attack,
    Move
}


public class EnemyUnitState : MonoBehaviour
{
    public StateMachine<EnemyState, StateDriverUnity> fsm;

    Ingame_UnitCtrl UnitCtrl;
    NavMeshAgent navAgent;

    Vector3 previousNavDestination;

    private void Start()
    {
        UnitCtrl = this.GetComponent<Ingame_UnitCtrl>();
        navAgent = this.GetComponent<NavMeshAgent>();

        fsm = new StateMachine<EnemyState, StateDriverUnity>(this);

        previousNavDestination = this.transform.position;
        //fsm.ChangeState(EnemyState.Move);
    }

    private void Update()
    {
        fsm.Driver.Update.Invoke();
    }

    #region Idle State
    void Idle_Enter()
    {
        Debug.Log("Enemy Idle Enter");
        
    }
    #endregion

    #region Attack State
    void Attack_Enter()
    {
        Debug.Log("Enemy Attack_Enter");
    }

    void Attack_Update()
    {
        UnitCtrl.Unit_Attack();
    }

    void Attack_Exit()
    {
        UnitCtrl.targetEnemy = null;
        UnitCtrl.isEnemyInRange = false;
    }
    #endregion

    #region Move State
    void Move_Enter()
    {
        Debug.Log("Enemy Move_Enter");
        UnitCtrl.isEnemyInRange = false;
        //navAgent.isStopped = false;

        UnitCtrl.moveTargetPos = UnitCtrl.moveTargetBasePos;
        UnitCtrl.enemy_isPathBlocked = false;
    }

    void Move_Update()
    {
        SearchPath();

        if (UnitCtrl.enemy_isBaseInRange)
        {
            fsm.ChangeState(EnemyState.Attack);
            return;
        }

        navAgent.SetDestination(UnitCtrl.moveTargetPos);

        if (UnitCtrl.enemy_isPathBlocked)
        {
            UnitCtrl.SearchEnemy();

            if (UnitCtrl.targetEnemy != null)
            {
                if (previousNavDestination.z < UnitCtrl.targetEnemy.transform.position.z)
                {
                    //Debug.Log("��ǥ ���纸�� ���� Ÿ�� ��ġ�� �� ��.");
                    UnitCtrl.enemy_isPathBlocked = false;
                    return;
                }

                float targetUnitDistance_Cur = Vector3.Distance(transform.position, UnitCtrl.targetEnemy.transform.position);
                UnitCtrl.moveTargetPos = UnitCtrl.targetEnemy.transform.position;

                if (targetUnitDistance_Cur <= UnitCtrl.unitData.attackRange)
                {
                    UnitCtrl.isEnemyInRange = true;
                    navAgent.SetDestination(UnitCtrl.transform.position);
                    return;
                }
            }
        }

        
    }

    void Move_Exit()
    {
        navAgent.SetDestination(transform.position);
        //navAgent.isStopped = true;
    }

    void SearchPath()//��ã��
    {
        StartCoroutine(DestinationValidCheck());
        IEnumerator DestinationValidCheck()
        {
            yield return new WaitUntil(() => { return !navAgent.pathPending; });

            int cornerCount = navAgent.path.corners.Length;
            //Debug.Log("��� ���� ��ǥ ���� ��ġ : " + UnitCtrl.moveTargetBasePos);

            Vector3 navDestination = new Vector3(navAgent.path.corners[cornerCount - 1].x, 0, navAgent.path.corners[cornerCount - 1].z);

            //Debug.Log("��� �� ������ ��ǥ ���� ��ġ : " + navDestination);
            //Debug.Log("�Ÿ� : " + Mathf.Abs(navDestination.x - UnitCtrl.moveTargetBasePos.x));

            if (Mathf.Abs(navDestination.x - UnitCtrl.moveTargetBasePos.x) <= UnitCtrl.unitData.attackRange)
            {
                //Debug.Log("��ǥ �������� �̵��� �� �ֽ��ϴ�.");
                UnitCtrl.enemy_isPathBlocked = false;
            }
            else
            {
                //Debug.Log("��ǥ �������� �̵��Ҽ��� ������ �߰� �������� �� �� �ֽ��ϴ�.");
                UnitCtrl.enemy_isPathBlocked = true;
            }
            previousNavDestination = navDestination;
        }
    }

    #endregion



}
