using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitCurDebuff
{
    public UnitDebuff name;      // ����� �̸�
    public int stack;            // ����� ����
    public float duration;       // ����� ��ü ���� �ð�
    public float currentTime;    // ����� ���� ���� �ð�
}

public class UnitDebuffManager : MonoBehaviour
{
    UnitDebuffData[] debuffData;

    [SerializeField]
    public List<UnitCurDebuff> activeDebuffs = new List<UnitCurDebuff>(); // ���� ������ ����� ���

    private void Awake()
    {
        debuffData = InGameManager.inst.unitStatusCtrl.debuffDatas;
    }

    // �� ������ ����� ������Ʈ
    void Update()
    {
        for (int i = activeDebuffs.Count - 1; i >= 0; i--) // ����Ʈ �������� ��ȸ (���� �� ���� ����)
        {
            activeDebuffs[i].currentTime -= Time.deltaTime;

            // ����� �ð��� ���� ���
            if (activeDebuffs[i].currentTime <= 0)
            {
                RemoveDebuff(activeDebuffs[i]);
            }
        }

        //����� ���� �Լ� �����ؾ���.
    }

    // ����� �߰� �Լ�
    public void AddDebuff(UnitDebuff debuff)
    {
        if (debuffData == null)
        {
            Debug.LogError("debuffData is null. Check the source of debuffData.");
            return;
        }


        for (int i = 0; i < debuffData.Length; i++)
        {
            if (debuff == debuffData[i].name) // �̹� �ش� ������� �ִ��� Ȯ��
            {
                UnitCurDebuff existingDebuff = activeDebuffs.Find(d => d.name == debuffData[i].name);

                if (existingDebuff != null) // ������� �̹� ������ �ð� �ʱ�ȭ �� ���� ����
                {
                    existingDebuff.currentTime = debuffData[i].duration;
                    if (existingDebuff.stack < debuffData[i].stackLimit)
                    {
                        existingDebuff.stack++;
                    }
                }
                else //���� ��� ���ο� ����� �߰�
                {
                    activeDebuffs.Add(new UnitCurDebuff
                    {
                        name = debuffData[i].name,
                        stack = 1,
                        duration = debuffData[i].duration,
                        currentTime = debuffData[i].duration
                    });

                }
            }
        }
    }

    // ����� ����
    private void RemoveDebuff(UnitCurDebuff debuff)
    {
        activeDebuffs.Remove(debuff);
    }

    // Ư�� ����� ã�� �Լ�
    public bool HasDebuff(UnitDebuff debuff)
    {
        return activeDebuffs.Exists(d => d.name == debuff);
    }
}