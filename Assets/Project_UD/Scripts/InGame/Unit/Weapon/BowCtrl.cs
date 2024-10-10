using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowCtrl : MonoBehaviour
{
    public GameObject Arrow;
    public Transform ShootPos;

    float ShootCooldown_Cur = 0;

    public void ArrowShoot(bool isEnemyAttack)
    {
        GameObject arrow_Obj = Instantiate(Arrow);
        arrow_Obj.transform.SetPositionAndRotation(this.ShootPos.position, this.transform.rotation);
        AttackCtrl arrowCtrl = arrow_Obj.GetComponent<AttackCtrl>();

        //���⼭ NullReferenceException ���� �߻�. ����?????
        //arrowCtrl.isEnemyAttack = isEnemyAttack;
    }
}
