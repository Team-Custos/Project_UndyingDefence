using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UD_UnitDataManager;

public class UD_Ingame_UnitUpgradeManager : MonoBehaviour
{
    private UD_Ingame_UIManager uiManager; // UI���� ��ȣ�ۿ��� ����


    // Start is called before the first frame update
    void Start()
    {
        uiManager = UD_Ingame_UIManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<string> GetUpgradeOptions(string unitID)
    {
        List<string> upgradeOptions = new List<string>();

        // ���׷��̵� ���� : ID �ڿ� 1 �Ǵ� 2�� �߰�
        string option1 = unitID + "1";
        string option2 = unitID + "2";

        if(UD_UnitDataManager.inst.DoesUnitExist(option1))
        {
            upgradeOptions.Add(option1);
        }
        
        if(UD_UnitDataManager.inst.DoesUnitExist(option2))
        {
            upgradeOptions.Add(option2);
        }

        if(upgradeOptions.Count == 0)
        {
            Debug.Log("���׷��̵� ����");
        }

        return upgradeOptions;
    }

    public void PerformUpgrade(UD_Ingame_UnitCtrl selectedUnit, string newUnitID)
    {
        UnitData newUnitData = UD_UnitDataManager.inst.GetUnitData(newUnitID);

        if(newUnitData != null)
        {
            selectedUnit.UnitInit(newUnitData);
            Debug.Log(selectedUnit.unitName + "���׷��̵� �Ϸ�");

            uiManager.UpdateUnitInfoPanel(selectedUnit);
        }
        else
        {
            Debug.Log("���� ������ ����");
        }
    }


}
