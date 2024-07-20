using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;
using static UD_UnitDataManager;

public class UD_UnitDataManager : MonoBehaviour
{
    public enum UnitClass
    {
        Militiaman,
        Spearman,
        DolGyukByeong,
        Archer,
        Crossbowman,
        Hunter,
        Taoist,
        Shamanist,
        FengShui
    }


    public class UnitData
    {
        public string Name;
        public string Type;
        public int Level;
        public int HP;
        public string Material;

        public UnitData(string name, string type, int level, int hp, string material)
        {
            Name = name;
            Type = type;
            Level = level;
            HP = hp;
            Material = material;
        }

        
    }

    private List<UnitData> unitDatas = new List<UnitData>();

    private Dictionary<UnitClass, UnitData> unitDataDictionary = new Dictionary<UnitClass, UnitData>();

    public void SetUnitData(List<UnitData> unitData)
    {
        unitDatas = unitData;
        ShowUnitData();
    }



    //public void SetUnitData(UnitClass unitClass, UnitData unitData)
    //{
    //    unitDataDictionary[unitClass] = unitData;
    //    Debug.Log($"Set data for {unitClass}: Name={unitData.Name}, Level={unitData.Level}, HP={unitData.HP}, Material={unitData.Material}");
    //}

    public UnitData GetUnitData(UnitClass unitClass)
    {
        if (unitDataDictionary.TryGetValue(unitClass, out UnitData unitData))
        {
            return unitData;
        }
        else
        {
            return null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    
    void ShowUnitData()
    {
        if (unitDatas.Count == 0)
        {
            return;
        }

        foreach (var unit in unitDatas)
        {
            Debug.Log($"Name: {unit.Name}, Type: {unit.Type}, Level: {unit.Level}, HP: {unit.HP}, Material: {unit.Material}");
        }
    }

    public UnitClass ParseUnitClass(string unitClass)
    {
        switch (unitClass)
        {
            case "�κ�":
                return UnitClass.Militiaman;
            case "â��":
                return UnitClass.Spearman;
            case "���ݺ�":
                return UnitClass.DolGyukByeong;
            case "�ü�":
                return UnitClass.Archer;
            case "�뺴":
                return UnitClass.Crossbowman;
            case "��ɲ�":
                return UnitClass.Hunter;
            case "����":
                return UnitClass.Taoist;
            case "�ּ�����":
                return UnitClass.Shamanist;
            case "ǳ������":
                return UnitClass.FengShui;
            default:
                return UnitClass.Militiaman;
        }
    }

}
