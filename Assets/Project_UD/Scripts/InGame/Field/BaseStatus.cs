using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStatus : MonoBehaviour
{
    public int BaseHPMax = 0;
    public int BaseHPCur = 0;


    // Start is called before the first frame update
    void Start()
    {
        BaseHPCur = BaseHPMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (BaseHPCur <= 0)
        {
            //�й�ó��
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject OBJ = collision.gameObject;
        //�ǰ� ����
        if (OBJ.CompareTag(CONSTANT.TAG_ATTACK))
        {
            AttackCtrl Attack = OBJ.GetComponent<AttackCtrl>();

            if (Attack.isEnemyAttack)
            {
                //Debug.Log(this.gameObject.name + " attack hit!");
                this.BaseHPCur -= Attack.Atk;
                if (Attack.MethodType == AttackMethod.Arrow)
                {
                    Destroy(OBJ);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject OBJ = other.gameObject;
        //�ǰ� ����
        if (OBJ.CompareTag(CONSTANT.TAG_ATTACK))
        {
            AttackCtrl Attack = OBJ.GetComponent<AttackCtrl>();

            if (Attack.isEnemyAttack)
            {
                //Debug.Log(this.gameObject.name + " attack hit!");
                this.BaseHPCur -= Attack.Atk;
                if (Attack.MethodType == AttackMethod.Arrow)
                {
                    Destroy(OBJ);
                }
            }
        }
    }
}
