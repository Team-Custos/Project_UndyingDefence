using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UD_Ingame_UnitSkillManager : MonoBehaviour
{

    public GameObject Bow;
    public GameObject Sword;

    public UD_Ingame_UnitCtrl UnitCtrl;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnitGeneralSkill(int SkillCode, Vector3 TargetPos)
    {
        switch (SkillCode)
        {
            case 101://�� ����
                //�ٰŸ� ���� �������� ��� �۾�.

                break;
            case 102://Ȱ ���
                if (Bow != null)
                {
                    Bow.transform.LookAt(TargetPos);
                    Bow.GetComponent<UD_Ingame_BowCtrl>().ArrowShoot(UnitCtrl.weaponCooldown, UnitCtrl.attackPoint, true);
                }
                break;  
        }
    }

    public void UnitSpecialSkill(int SkillCode, Vector3 TargetPos)
    {
        switch (SkillCode)
        {
            case 101://�� ���
                // �˺���� ���� ó��. ���ȸ� �ٸ���.
                break;
            case 102://�� ��ġ
                

                break;
        }
    }

    public void EnemyGeneralSkill(int SkillCode, Vector3 TargetPos)
    {
        switch (SkillCode)
        {
            case 101://�� ����
                break;
            case 102://Ȱ ���
                if (Bow != null)
                {
                    Bow.transform.LookAt(TargetPos);
                    Bow.GetComponent<UD_Ingame_BowCtrl>().ArrowShoot(UnitCtrl.weaponCooldown, UnitCtrl.attackPoint, true);
                }
                break;
        }
    }
}
