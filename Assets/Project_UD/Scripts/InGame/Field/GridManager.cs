using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Tilemaps;
using System;


public class GridManager : MonoBehaviour
{
    public static GridManager inst;

    public int _width, _height;

    [SerializeField] private GridTile _tilePrefab;

    public Dictionary<Vector2, bool> _tiles = new Dictionary<Vector2, bool>
    { };

    public GameObject TILEPARENT;

    public GridTile[] Tiles_Obj;
    int Tiles_idx;

    public Grid mapGrid;
    public Tilemap groundTilemap;

    public float tile_Offset;

    private void Awake()
    {
        inst = this;
        Tiles_Obj = FindObjectsOfType<GridTile>();
        for (Tiles_idx = 0; Tiles_idx < Tiles_Obj.Length; Tiles_idx++)
        {
            Tiles_Obj[Tiles_idx].GridPos = new Vector2(mapGrid.WorldToCell(Tiles_Obj[Tiles_idx].transform.position).x, mapGrid.WorldToCell(Tiles_Obj[Tiles_idx].transform.position).y);
            _tiles.Add(new Vector2(Tiles_Obj[Tiles_idx].GridPos.x, Tiles_Obj[Tiles_idx].GridPos.y), true);
        }
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public Vector2 GetTilePos(Vector3 pos)
    {
        Vector2 TilePos = new Vector2(groundTilemap.WorldToCell(pos).x, groundTilemap.WorldToCell(pos).y);
        return TilePos;
    }

    public void SetTilePlaceable(Vector3 pos, bool SetManualMode, bool PlaceableToSetManual)
    {
        Vector3Int CurCellPos = groundTilemap.WorldToCell(pos);
        Vector3 CurCellWorldPos = new Vector3 (groundTilemap.GetCellCenterWorld(CurCellPos).x,0,groundTilemap.GetCellCenterWorld(CurCellPos).z);

        Vector3 CurUnitWorldPos = new Vector3(pos.x, 0, pos.z);

        //Debug.Log("���� ��ġ ���� ���� : " + _tiles[CurCellPos]);
        //Debug.Log("���� ��ġ�� Ÿ���� �׸��� ��ǥ : " + CurCellPos);
        //Debug.Log("���� ��ġ�� Ÿ���� ���� ��ǥ : " + CurCellWorldPos);

        // ���ְ� Ÿ���� �߽� ���� �Ÿ� ��� (x�� z�� ��)
        float distanceX = Mathf.Abs(CurCellWorldPos.x - CurUnitWorldPos.x);
        float distanceZ = Mathf.Abs(CurCellWorldPos.z - CurUnitWorldPos.z);

        //Debug.Log(new Vector2(distanceX,distanceZ));

        if (SetManualMode == true)//���� �����Ұ��
        {
            _tiles[new Vector2(CurCellPos.x, CurCellPos.y)] = PlaceableToSetManual;
            return;
        }
        else
        {
            // Ÿ�� �߽ɰ� ���� ��ġ�� x �� z �Ÿ� ���̸� �̿��Ͽ� ��ġ ���� ���θ� �Ǵ�
            if (distanceX > 0.95f || distanceZ > 0.95f)
            {
                // Ÿ�ϰ� ������ ���� ��ġ�� �ִٰ� �Ǵ� - ��ġ ����
                _tiles[new Vector2(CurCellPos.x, CurCellPos.y)] = true;
            }
            else
            {
                // Ÿ�ϰ� ������ ����� ������ ���� ���� - ��ġ �Ұ�
                _tiles[new Vector2(CurCellPos.x, CurCellPos.y)] = false;
            }
        }

        
    }

    public bool GetTilePlaceable(Vector3 WorldPosToFind)
    {
        Vector3Int CellPos = groundTilemap.WorldToCell(WorldPosToFind);
        if (_tiles.ContainsKey(new Vector2(CellPos.x, CellPos.y)))
        {
            return _tiles[new Vector2(CellPos.x, CellPos.y)];
        }
        else
        {
            return false;
        }
    }


    public bool IsTilesAllOff()
    {
        if (Tiles_Obj[Tiles_idx].GetComponent<GridTile>().Selected)
        {
            return false;
        }
        else { return true; }
    }

}
