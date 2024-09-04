using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class UD_Ingame_UnitSkillManager : MonoBehaviour
{
    public Dictionary<int, bool> TargetCellPlaceable = new Dictionary<int, bool>
    { };

    public List<int> TargetCellidx = new List<int>();

    public GameObject Bow;
    public GameObject Sword;

    public GameObject Trap;

    float skillCooldown_Cur = 0;

    public UD_Ingame_UnitCtrl UnitCtrl;
    UD_Ingame_GridManager GridManager;

    int TargetCellIdxFinal = 0;


    private void Awake()
    {
        GridManager = UD_Ingame_GridManager.inst;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TargetCellIdxFinal = Random.Range(0, TargetCellidx.Count);
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

    public void UnitSpecialSkill(int SkillCode, Vector3 TargetPos, float skillCooldown)
    {
        if (skillCooldown_Cur <= 0)
        {
            skillCooldown_Cur = skillCooldown;
            switch (SkillCode)
            {
                case 101://�� ���
                         // �˺���� ���� ó��. ���ȸ� �ٸ���.
                    break;
                case 102://�� ��ġ
                         //���� �ٶ󺸴� �������� ��ġ ������ ����. ������ ���� ������ ����. ���� ��ġ���� 2ĭ ���� ������ �ִ� ��ġ�� �� 3ĭ�� �������� �����Ͽ� ��ġ.
                         //TODO : ������ ����� ������ �Ǵ��� Ȯ���ؾ���.
                    float CurAngle = Mathf.Abs(gameObject.transform.rotation.y % 360);
                    Vector2 CurCellPos = UnitCtrl.unitPos;
                    Vector2[] TargetCells = new Vector2[]
                    {
                        Vector2.zero, Vector2.zero, Vector2.zero
                    };

                    if (CurAngle <= 45 || CurAngle > 315)//��
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            TargetCells[i].y = CurCellPos.y + 2;
                            TargetCells[i].x = CurCellPos.x - 1 + i;
                        }
                    }
                    else if (CurAngle <= 135 || CurAngle > 45)//��
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            TargetCells[i].x = CurCellPos.x + 2;
                            TargetCells[i].y = CurCellPos.y + 1 - i;
                        }
                    }
                    else if (CurAngle <= 225 || CurAngle > 135)//��
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            TargetCells[i].y = CurCellPos.y - 2;
                            TargetCells[i].x = CurCellPos.x + 1 - i;
                        }
                    }
                    else if (CurAngle <= 315 && CurAngle > 225)//��
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            TargetCells[i].x = CurCellPos.x - 2;
                            TargetCells[i].y = CurCellPos.y - 1 + i;
                        }
                    }


                    Vector3 TargetCellWorldPos = Vector3.zero;

                    for (int i = 0; i < 3; i++)
                    {
                        if (!TargetCellPlaceable.ContainsKey(i))
                        {
                            TargetCellPlaceable.Add(i, GridManager.GetTilePlaceable(TargetCells[i]));
                        }
                        if (!TargetCellidx.Contains(i))
                        {
                            TargetCellidx.Add(i);
                        }
                    }

                    int TargetCellFinal = TargetCellidx[TargetCellIdxFinal];

                    while (TargetCellidx.Count != 0)
                    {
                        if (GridManager.GetTilePlaceable(TargetCells[TargetCellIdxFinal]))
                        {
                            TargetCellWorldPos = GridManager.mapGrid.CellToWorld(new Vector3Int((int)TargetCells[TargetCellFinal].x, (int)TargetCells[TargetCellFinal].y, 1));
                            break;
                        }
                        else
                        {
                            TargetCellidx.RemoveAt(TargetCellIdxFinal);
                        }

                        if (TargetCellidx.Count == 0)
                        {
                            Debug.Log("��� ��ġ �Ұ�. ��ų ��Ÿ���� �ʱ�ȭ�մϴ�.");
                            skillCooldown_Cur = skillCooldown;
                            return;
                        }
                    }

                    if (GridManager.GetTilePlaceable(TargetCellWorldPos))
                    {
                        Vector3 CellWorldPosFinal = TargetCellWorldPos+ new Vector3(GridManager.mapGrid.cellSize.x * 0.5f,0, GridManager.mapGrid.cellSize.y * 0.5f);
                        
                            //GridManager.mapGrid.GetCellCenterWorld(new Vector3Int((int)TargetCells[TargetCellFinal].x, 1, (int)TargetCells[TargetCellFinal].y));
                        GameObject TrapObj = Instantiate(Trap);
                        TrapObj.transform.position = new Vector3(CellWorldPosFinal.x,-1,CellWorldPosFinal.z);
                        GridManager.SetTilePlaceable(TargetCellWorldPos, false);
                        TargetCellPlaceable.Clear();
                        TargetCellidx.Clear();
                    }
                    break;
            }
        }
        else 
        {
            skillCooldown_Cur -= Time.deltaTime;
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
