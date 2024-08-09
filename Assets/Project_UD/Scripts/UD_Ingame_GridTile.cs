using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UD_Ingame_GridTile : MonoBehaviour
{
    UD_Ingame_GameManager GAMEMANAGER;

    public bool Selected = false;
    public Vector2 GridPos = Vector2.zero;

    public Color32 colorDefault = Color.white;
    public Color32 colorHighlit = Color.white;
    public Color32 colorSelected = Color.white;

    MeshRenderer MeshR;

    public GameObject currentPlacedUnit;

    bool mouseHover = false;
    public bool isPlaceable = true;

    public Color32 colorOccupied = Color.red; // ������ �ִ� Ÿ�� ����
    public Color32 colorAvailable = Color.green; // ������ ���� Ÿ�� ����
    public bool showPlacementColors = false;

    void Start()
    {
        GAMEMANAGER = UD_Ingame_GameManager.inst;
        MeshR = GetComponent<MeshRenderer>();
        Selected = false;

        GridPos = new Vector2((int)(transform.position.x / 2), (int)(transform.position.z / 2));

        this.gameObject.name = this.name + " " + GridPos.x + " " + GridPos.y;
    }

    void Update()
    {
        //if (Selected)
        //{
        //    MeshR.material.color = colorSelected;
        //}
        //else if (!mouseHover)
        //{
        //    MeshR.material.color = colorDefault;
        //}

        if (showPlacementColors)
        {
            if (currentPlacedUnit != null)
            {
                MeshR.material.color = colorOccupied;
            }
            else if (!mouseHover)
            {
                MeshR.material.color = colorAvailable;
            }
        }
        else
        {
            if (Selected)
            {
                MeshR.material.color = colorSelected;
            }
            else if (!mouseHover)
            {
                MeshR.material.color = colorDefault;
            }
        }
    }

    private void OnMouseOver()
    {
        //mouseHover = true;
        //MeshR.material.color = colorHighlit;

        mouseHover = true;
        if (!showPlacementColors)
        {
            MeshR.material.color = colorHighlit;
        }
    }

    private void OnMouseExit()
    {
        //    mouseHover = false;
        //    GetComponent<MeshRenderer>().material.color = colorDefault;

        mouseHover = false;
        if (!showPlacementColors)
        {
            MeshR.material.color = colorDefault;
        }
    }

    private void OnMouseDown()
    {
        if (currentPlacedUnit != null)
        {
            return;
        }

        if (GAMEMANAGER.AllyUnitSetMode && isPlaceable)
        {
            isPlaceable = false;
        }
        else
        {
            Selected = !Selected;
        }

        //Debug.Log(gameObject.name + " Selected");
    }

    public void SetPlaceable(bool placeable)
    {
        isPlaceable = placeable;
    }

    public bool IsPlaceable()
    {
        return isPlaceable;
    }

    public void ShowPlacementColors(bool show)
    {
        showPlacementColors = show;
    }
}
