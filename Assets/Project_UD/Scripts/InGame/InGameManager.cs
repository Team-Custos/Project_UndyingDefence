using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    public static InGameManager inst;
    public GridManager gridManager;
    public UnitDebuffDataCtrl unitStatusCtrl;

    public GameObject Base;
   

    public bool UnitSetMode = false;
    public bool AllyUnitSetMode = false;
    public bool EnemyUnitSetMode = false;

    private void Awake()
    {
        inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            UnitSetMode = !UnitSetMode;
            EnemyUnitSetMode = !EnemyUnitSetMode;
        }

        if (UnitSetMode && AllyUnitSetMode)
        {
            // Ÿ�� ���� ������Ʈ
            GridTile[] allTiles = FindObjectsOfType<GridTile>();
            foreach (var tile in allTiles)
            {
                tile.ShowPlacementColors(true);
            }
        }
        else
        {
            // Ÿ�� ���� ������Ʈ
            GridTile[] allTiles = FindObjectsOfType<GridTile>();
            foreach (var tile in allTiles)
            {
                tile.ShowPlacementColors(false);
            }
        }


    }

    public void AllUnitSelectOff()
    {
        Ingame_UnitCtrl[] allUnit = FindObjectsOfType<Ingame_UnitCtrl>();
        foreach (var unit in allUnit)
        {
            unit.isSelected = false;
        }
    }

    public void AllTileSelectOff()
    {
        GridTile[] allTiles = FindObjectsOfType<GridTile>();
        foreach (var tile in allTiles)
        {
            tile.Selected = false;
        }
    }


    // ����� Ŀ�Ǵ� ��ų �ҷ�����
    public Dictionary<string, string> LoadCommandSkillList()
    {
        Dictionary<string, string> loadedSkills = new Dictionary<string, string>();

        string skillIDList = PlayerPrefs.GetString("SkillIDList", "");

        if (!string.IsNullOrEmpty(skillIDList))
        {
            string[] skillIDs = skillIDList.Split(',');

            foreach (string skillID in skillIDs)
            {
                string skillName = PlayerPrefs.GetString(skillID, "�̸� ����");
                loadedSkills.Add(skillID, skillName);
            }
        }

        if (loadedSkills.Count == 0)
        {
            Debug.LogWarning("�ҷ��� ��ų�� �����ϴ�.");
        }

        return loadedSkills;
    }

}
