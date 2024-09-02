using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UD_UnitDataManager;

public class UD_Ingame_UnitUpgradeManager : MonoBehaviour
{
    [System.Serializable]
    public class UnitUpgrade
    {
        public string CurrentUnitName;
        public string UpgradeOption1; 
        public string UpgradeOption2; 

        public UnitUpgrade(string currentUnit, string option1, string option2)
        {
            CurrentUnitName = currentUnit;
            UpgradeOption1 = option1;
            UpgradeOption2 = option2;
        }
    }


    private UD_Ingame_UIManager uiManager; // UI���� ��ȣ�ۿ��� ����
    private Dictionary<string, UnitUpgrade> upgradeTree = new Dictionary<string, UnitUpgrade>();


    // Start is called before the first frame update
    void Start()
    {
        uiManager = UD_Ingame_UIManager.instance;

        // �κ�
        upgradeTree.Add("�κ�", new UnitUpgrade("�κ�", "â��", "ô�ĺ�"));  // 1Ƽ��

        upgradeTree.Add("â��", new UnitUpgrade("â��", "��â����", "�보����")); // 2Ƽ��
        upgradeTree.Add("ô�ĺ�", new UnitUpgrade("ô�ĺ�", "��������", "�������"));

        upgradeTree.Add("�보����", new UnitUpgrade("�보����", "�보���", null)); // 3Ƽ��
        upgradeTree.Add("��â����", new UnitUpgrade("��â����", "��â����", null));
        upgradeTree.Add("��������", new UnitUpgrade("��������", "���ݴ���", null));
        upgradeTree.Add("�������", new UnitUpgrade("�������", "��������", null));

        // ��ɲ�
        upgradeTree.Add("��ɲ�", new UnitUpgrade("��ɲ�", "�ѷ�", "����"));

        upgradeTree.Add("�ѷ�", new UnitUpgrade("�ѷ�", "���", "�͵� ��ɲ�"));
        upgradeTree.Add("����", new UnitUpgrade("����", "������", "ȭ����"));

        upgradeTree.Add("���", new UnitUpgrade("���", "�ű�", null));
        upgradeTree.Add("�͵� ��ɲ�", new UnitUpgrade("�͵� ��ɲ�", "����� ����", null));
        upgradeTree.Add("������", new UnitUpgrade("������", "��ȣ����", null));
        upgradeTree.Add("ȭ����", new UnitUpgrade("ȭ����", "ȭ�ึ", null));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public UnitUpgrade GetUpgradeOptions(string unitName)
    {
        if (string.IsNullOrEmpty(unitName))
        {
            return null;
        }


        if (upgradeTree.ContainsKey(unitName))
        {
            return upgradeTree[unitName];
        }
        return null;
    }

    public void PerformUpgrade(UD_Ingame_UnitCtrl selectedUnit, string newUnitName)
    {
        UnitData newUnitData = UD_UnitDataManager.inst.GetUnitData(newUnitName);
        if (newUnitData != null)
        {
            selectedUnit.UnitInit(newUnitData);
            Debug.Log("���׷��̵�");

            // UI ������Ʈ
            uiManager.UpdateUnitInfoPanel(selectedUnit);
        }
        else
        {
            Debug.LogError("���� ������ ����");
        }
    }
}
