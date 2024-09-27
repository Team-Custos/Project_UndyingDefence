using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


[System.Serializable]
public class UnitCurDebuff
{
    public UnitDebuff name;
    public int stack;
    public float Time;
    public float Cur_Time;
}

public enum UnitDebuff
{
    Bleed,
    Dizzy,
    Stun,
    Tied,
    Burn,
    Inferno
}

public class UnitDebuffManager : MonoBehaviour
{
    public Dictionary<int, UnitDebuff> GeneralSkillCodeToDebuff = new Dictionary<int, UnitDebuff>()
    {
        {101 , UnitDebuff.Bleed},
        {102 , UnitDebuff.Bleed},
    };

    public Dictionary<int, UnitDebuff> SpecialSkillCodeToDebuff = new Dictionary<int, UnitDebuff>()
    {
        {101 , UnitDebuff.Dizzy},
        {102 , UnitDebuff.Tied},
    };

    public UnitCurDebuff[] Debuffs2Manage; // �迭�� ����

    void Update()
    {
        for (int idx = 0; idx < Debuffs2Manage.Length; idx++)
        {
            UpdateDebuff(Debuffs2Manage[idx], idx);
        }
    }

    // ����� �߰� �Լ�
    public void AddUnitDebuff(UnitDebuff debuffToAdd)
    {
        UnitDebuffData debuffData = new UnitDebuffData();
        UnitCurDebuff existingDebuff = FindDebuff(debuffToAdd);

        if (existingDebuff != null)
        {
            // �̹� �ִ� ������� ��� �ð� �ʱ�ȭ �� ���� ����
            existingDebuff.Cur_Time = debuffData.Time;
            if (existingDebuff.stack < debuffData.stackLimit)
            {
                existingDebuff.stack++;
            }
        }
        else
        {
            // ���ο� ����� �߰�
            if (Debuffs2Manage.Length == 0)
            {
                Debuffs2Manage[0] = new UnitCurDebuff
                {
                    name = debuffToAdd,
                    Cur_Time = debuffData.Time,
                    stack = 1
                };
            }
            else
            {
                
            }

            

            for (int idx = 0; idx < Debuffs2Manage.Length; idx++)
            {
                if (Debuffs2Manage[idx] == null) // �� �ڸ��� �߰�
                {
                    Debuffs2Manage[idx] = new UnitCurDebuff
                    {
                        name = debuffToAdd,
                        Cur_Time = debuffData.Time,
                        stack = 1
                    };
                    UnitCurDebuff[] resizedArray = new UnitCurDebuff[Debuffs2Manage.Length + 1];
                    Array.Copy(Debuffs2Manage, resizedArray, Debuffs2Manage.Length);
                    Debuffs2Manage = resizedArray;
                    break;
                }
            }
        }
    }

    // ����� ������Ʈ �Լ�
    void UpdateDebuff(UnitCurDebuff debuff, int idx)
    {
        if (debuff.Cur_Time <= 0)
        {
            // �ð��� �ٵ� ����� ����
            Debuffs2Manage[idx] = null; // �迭���� �ش� ��� ����
            Debuffs2Manage = Debuffs2Manage.Where(x => x != null).ToArray(); // null ����
        }
        else
        {
            debuff.Cur_Time -= Time.deltaTime; // �ð� ����
        }
    }

    // Ư�� ����� ã�� �Լ�
    UnitCurDebuff FindDebuff(UnitDebuff debuffToFind)
    {
        return Debuffs2Manage.FirstOrDefault(d => d != null && d.name == debuffToFind);
    }
}


/*
[System.Serializable]
public class UnitCurDebuff
{
    public UnitDebuff name;
    public int stack;
    public float Time;
    public float Cur_Time;
}

public enum UnitDebuff
{
    Bleed,
    Dizzy,
    Stun,
    Tied,
    Burn,
    Inferno
}

//-> ȿ������ ���� �����ͷ� �и�. -> ���� ������(�迭�� ��ҵ��� üũ.)�� ���� ȿ�� ����.

//���� �����ؾ� �� ȿ����
//- ����
//- ��� -> ����
//- �ӹ� 


public class UD_Ingame_UnitDebuffManager : MonoBehaviour
{
    public List<UnitDebuff> UnitCurDebuff = new List<UnitDebuff>();

    public UnitCurDebuff[] Debuffs2Manage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int idx = 0; idx < UnitCurDebuff.Count; idx++)
        {
            UnitDebuffUpdate(UnitCurDebuff[idx]);
        }
    }
    
    //����� �߰�
    public void AddUnitDebuff(UnitDebuff debuff2Add)
    {
        UnitDebuffData debuffData = new UnitDebuffData();

        if (UnitCurDebuff.Contains(debuff2Add))//�̹� �ִ� ������ϰ�� ��Ÿ�� �ʱ�ȭ. ���� �Ҽ� �ִٸ� ���� �߰�. 
        {
            for (int idx = 0; idx < Debuffs2Manage.Length; idx++)
            {
                if (Debuffs2Manage[idx].name == debuff2Add)
                {
                    Debuffs2Manage[idx].Cur_Time = debuffData.Time;
                    if (Debuffs2Manage[idx].stack < debuffData.stackLimit)
                    {
                        Debuffs2Manage[idx].stack++;
                    }
                    else
                    {
                       //�ٸ� �����̻����� ��ȭ�ϰų� �׳� ����.
                    }
                    
                }
            }
        }
        else
        {
            UnitCurDebuff.Add(debuff2Add);
            for (int idx = 0; idx < Debuffs2Manage.Length; idx++)
            {
                if (Debuffs2Manage[idx].name == debuff2Add)
                {
                    Debuffs2Manage[idx].Cur_Time = debuffData.Time;
                    Debuffs2Manage[idx].stack = 1;
                }
            }
        }
    }

    //����� ������Ʈ (���� �ð� üũ)
    void UnitDebuffUpdate(UnitDebuff debuff)
    {
        if (UnitCurDebuff.Contains(debuff))
        {
            for (int idx = 0; idx < Debuffs2Manage.Length; idx++)
            {
                if (Debuffs2Manage[idx].name == debuff)
                {
                    if (Debuffs2Manage[idx].Cur_Time <= 0)
                    {
                        //����� ����.
                        UnitCurDebuff.Remove(debuff);
                        Debuffs2Manage = Debuffs2Manage
                        .Where((item, index) => index != idx)
                        .ToArray();
                        break;
                    }
                    else
                    {
                        Debuffs2Manage[idx].Cur_Time -= Time.deltaTime;
                    }
                }
            }
        }
    }
}
*/
