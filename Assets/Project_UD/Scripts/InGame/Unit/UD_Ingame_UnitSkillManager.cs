using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class UD_Ingame_UnitSkillManager : MonoBehaviour
{
    public Dictionary<int, bool> TargetCellPlaceable = new Dictionary<int, bool>
    { };

    public List<int> TargetCellIdx = new List<int>();

    public GameObject Bow;
    public GameObject Sword;

    public GameObject Trap;

    float weaponCooldown_Cur = 0;
    float skillCooldown_Cur = 0;

    public UD_Ingame_UnitCtrl UnitCtrl;
    UD_Ingame_GridManager GridManager;

    public GameObject SetObject = null;

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
        //TargetCellIdxFinal = Random.Range(0, TargetCellidx.Count);
    }

    public void UnitGeneralSkill(int SkillCode, UD_Ingame_UnitCtrl TargetEnemy, float weaponCooldown, bool isEnemyAttack)
    {
        Vector3 TargetPos = TargetEnemy.transform.position;
        if (weaponCooldown_Cur <= 0)
        {
            weaponCooldown_Cur = weaponCooldown;
            switch (SkillCode)
            {
                case 101://�� ����
                         //�ٰŸ� ���� �������� ��� �۾�. -> Ÿ���� ������� ���۾���.
                    TargetEnemy.ReceivePhysicalDamage(UnitCtrl.attackPoint, UnitCtrl.critChanceRate, AttackType.Slash);
                    break;
                case 102://Ȱ ���
                    if (Bow != null && Bow.GetComponent<UD_Ingame_BowCtrl>() != null)
                    {
                        Bow.transform.LookAt(TargetPos);
                        Bow.GetComponent<UD_Ingame_BowCtrl>().ArrowShoot(isEnemyAttack);
                        TargetEnemy.ReceivePhysicalDamage(UnitCtrl.attackPoint, UnitCtrl.critChanceRate, AttackType.Pierce);
                    }
                    break;
            }
        }
        else
        {
            weaponCooldown_Cur -= Time.deltaTime;
        }
    }

    int SkillDamageInit()
    {


        return 0;
    }


    public void UnitSpecialSkill(int SkillCode,float skillCooldown)
    {
        int SkillDamage = SkillDamageInit();



        UD_Ingame_UnitCtrl TargetEnemy = null;
        if (UnitCtrl.targetEnemy != null)
        {
            TargetEnemy = UnitCtrl.targetEnemy.GetComponent<UD_Ingame_UnitCtrl>();
        }

        if (skillCooldown_Cur <= 0)
        {
            skillCooldown_Cur = skillCooldown;
            switch (SkillCode)
            {
                case 101://�� ���
                         //������ �տ� �ִ� �� ���� ���� ���� ��� 5 �������� ���� ������ ���Ѵ�. ġ��Ÿ�� 5% ����. ġ��Ÿ �ߵ� �� ��� ȿ��
                         // �˺���� ���� ó��. ���ȸ� �ٸ���.
                    if (TargetEnemy == null)
                    {
                        return;
                    }

                    TargetEnemy.ReceivePhysicalDamage(SkillDamage, UnitCtrl.critChanceRate + 5, AttackType.Pierce);

                    break;
                case 102://�� ��ġ
                        //���ִ� ĭ�� �������� �ٶ󺸴� ���� 2ĭ �տ� �浣�� ��ġ�Ͽ� �������� �� ���� ������ 5 �������� ���� ������ ���Ѵ�. ġ��Ÿ���� 5% ����. ġ��Ÿ �ߵ� �� �ӹ� ȿ��.
                        //�̹� ��ġ�� �浣�� �ߵ��Ǳ� ���� �ش� ��ų�� �ٽ� ����Ѵٸ�, ������ ��ġ�� �浣�� �μ����� �ش� ��ų�� �ߵ��Ǿ� ���� �浣�� ��ġ�Ѵ�.
                        //-> �� ��ġ �����ؼ� ��ȹ���̶� �̾߱⸦ �ؾ��� �ʿ䰡 �־��.

                    if (SetObject != null)
                    {
                        GridManager.SetTilePlaceable(SetObject.transform.position, true, true);
                        Destroy(SetObject);
                        SetObject = null;
                    }

                    float CurAngle = gameObject.transform.eulerAngles.y;
                    Debug.Log("���� ����: " + CurAngle);
                    Vector2 CurCellPos = UnitCtrl.unitPos;
                    Vector2[] TargetCells = new Vector2[3];

                    // ������ ���� x, y �������� ����
                    Vector2[] directionOffsets;
                    if (CurAngle <= 45 || CurAngle > 315)       // ����
                    {
                        directionOffsets = new Vector2[] { new Vector2(-1, 2), new Vector2(0, 2), new Vector2(1, 2) };
                    }
                    else if (CurAngle <= 135 && CurAngle > 45)   // ����
                    {
                        directionOffsets = new Vector2[] { new Vector2(2, 1), new Vector2(2, 0), new Vector2(2, -1) };
                    }
                    else if (CurAngle <= 225 && CurAngle > 135)  // ����
                    {
                        directionOffsets = new Vector2[] { new Vector2(1, -2), new Vector2(0, -2), new Vector2(-1, -2) };
                    }
                    else if (CurAngle > 225 && CurAngle <= 315) // ����
                    {
                        directionOffsets = new Vector2[] { new Vector2(-2, -1), new Vector2(-2, 0), new Vector2(-2, 1) };
                    }
                    else
                    {
                        Debug.LogError("�� ��ġ ���� ���� ��� ����.");
                        directionOffsets = new Vector2[] { new Vector2(-1, 2), new Vector2(0, 2), new Vector2(1, 2) };
                    }

                    // TargetCells�� ������ �����Ͽ� ��ǥ ���
                    for (int i = 0; i < 3; i++)
                    {
                        TargetCells[i] = CurCellPos + directionOffsets[i];
                    }


                    Vector3 TargetCellWorldPos = Vector3.zero;

                    for (int i = 0; i < 3; i++)
                    {
                        if (!TargetCellPlaceable.ContainsKey(i))
                        {
                            TargetCellPlaceable.Add(i, GridManager.GetTilePlaceable(TargetCells[i]));
                        }
                        if (!TargetCellIdx.Contains(i))
                        {
                            TargetCellIdx.Add(i);
                        }
                    }

                    if (TargetCellIdx.Count > 0)
                    {
                        TargetCellIdxFinal = Random.Range(0, TargetCellIdx.Count - 1);
                        Debug.Log("���� : " + TargetCellIdxFinal);
                        int TargetCellFinal = TargetCellIdx[TargetCellIdxFinal];
                        Vector3 TargetCellFinalWorldPos = GridManager.mapGrid.CellToWorld(new Vector3Int((int)TargetCells[TargetCellFinal].x, (int)TargetCells[TargetCellFinal].y, 1));

                        while (TargetCellIdx.Count != 0)
                        {
                            TargetCellFinalWorldPos = GridManager.mapGrid.CellToWorld(new Vector3Int((int)TargetCells[TargetCellFinal].x, (int)TargetCells[TargetCellFinal].y, 1));

                            Debug.Log(TargetCellFinal + "�� ���� ��ġ�� �õ��մϴ�.");
                            if (GridManager.GetTilePlaceable(TargetCellFinalWorldPos))
                            {
                                Debug.Log("�ش� ���� ��ġ�� ���� �մϴ�.");
                                TargetCellWorldPos = TargetCellFinalWorldPos;
                                break;
                            }
                            else
                            {
                                Debug.Log("�ش� ���� ��ġ�� �Ұ��� �մϴ�.");
                                TargetCellIdx.RemoveAt(TargetCellIdxFinal);
                                if (TargetCellIdx.Count == 0)
                                {
                                    Debug.Log("��� ��ġ �Ұ�. ��ų ��Ÿ���� �ʱ�ȭ�մϴ�.");
                                    skillCooldown_Cur = 0;
                                    return;
                                }
                                else
                                {
                                    TargetCellIdxFinal = Random.Range(0, TargetCellIdx.Count - 1);
                                    Debug.Log("���ο� ���� : " + TargetCellIdxFinal);
                                    TargetCellFinal = TargetCellIdx[TargetCellIdxFinal];
                                    continue;
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("��� ��ġ �Ұ�. ��ų ��Ÿ���� �ʱ�ȭ�մϴ�.");
                        skillCooldown_Cur = 0;
                        return;
                    }


                    

                    if (GridManager.GetTilePlaceable(TargetCellWorldPos))
                    {
                        Vector3 CellWorldPosFinal = TargetCellWorldPos+ new Vector3(GridManager.mapGrid.cellSize.x * 0.5f,0, GridManager.mapGrid.cellSize.y * 0.5f);
                        
                            //GridManager.mapGrid.GetCellCenterWorld(new Vector3Int((int)TargetCells[TargetCellFinal].x, 1, (int)TargetCells[TargetCellFinal].y));
                        SetObject = Instantiate(Trap);
                        SetObject.transform.position = new Vector3(CellWorldPosFinal.x,-1,CellWorldPosFinal.z);
                        GridManager.SetTilePlaceable(TargetCellWorldPos, true, false);
                        TargetCellPlaceable.Clear();
                        TargetCellIdx.Clear();
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
                    Bow.GetComponent<UD_Ingame_BowCtrl>().ArrowShoot(true);
                }
                break;
        }
    }
}
