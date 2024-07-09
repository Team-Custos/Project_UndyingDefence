using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface IState
{
    public void EnterState();

    public void UpdateState();
    public void ExitState();

}

//TODO : ���� ���� ���� �߰�

public class UD_Ingame_UnitCtrl : MonoBehaviour
{

    MeshRenderer MeshRenderer;


    public UnitType UnitType;



    public Color32 colorAlly = Color.blue;
    public Color32 colorEnemy = Color.red;


    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        MeshRenderer.material.color = colorAlly;
        //switch (UnitType)
        //{
        //    case UnitType.Enemy:
        //        MeshRenderer.material.color = colorEnemy;
        //        break;
        //    case UnitType.Ally:
        //        MeshRenderer.material.color = colorAlly;
        //        break;

        //}


    }

    void Init(UnitSpawnData data)
    {
        
    }
}
