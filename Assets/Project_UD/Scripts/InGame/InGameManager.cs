using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    public static InGameManager inst;
    public GridManager gridManager;
    public UnitDebuffDataCtrl unitDebuffData;

    public CommanderSkillData[] CurCommanderSkill;

    public GameObject Base;
   

    public bool UnitSetMode = false;
    public bool AllyUnitSetMode = false;
    public bool EnemyUnitSetMode = false;

    private void Awake()
    {
        inst = this;
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
            // 타일 색상 업데이트
            GridTile[] allTiles = FindObjectsOfType<GridTile>();
            foreach (var tile in allTiles)
            {
                tile.ShowPlacementColors(true);
            }
        }
        else
        {
            // 타일 색상 업데이트
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


    // 저장된 커맨더 스킬 불러오기
    public Dictionary<string, string> LoadCommandSkillList()
    {
        Dictionary<string, string> loadedSkills = new Dictionary<string, string>();

        string skillIDList = PlayerPrefs.GetString("SkillIDList", "");

        if (!string.IsNullOrEmpty(skillIDList))
        {
            string[] skillIDs = skillIDList.Split(',');

            foreach (string skillID in skillIDs)
            {
                string skillName = PlayerPrefs.GetString(skillID, "이름 없음");
                loadedSkills.Add(skillID, skillName);
            }
        }

        if (loadedSkills.Count == 0)
        {
            Debug.LogWarning("불러온 스킬이 없습니다.");
        }

        return loadedSkills;
    }

}
