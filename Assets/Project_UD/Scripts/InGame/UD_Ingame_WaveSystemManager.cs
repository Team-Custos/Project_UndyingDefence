using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class UD_Ingame_WaveSystemManager : MonoBehaviour
{
    [Header("====WaveData====")]
    public int waveMax = 10;
    public UnitType[][] unitType;

    public int waveCur = 1;
    
    public float waveStartDelay = 0;
    float waveStartDelayCur = 0;

    public GameObject[] remainEnemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //���� �Ѹ� ������� ���� ���̺�� ����.
        if (remainEnemy.Length != 0)
        {
            Debug.Log("Wave Complete!");

            if (waveCur >= waveMax)
            {
                Debug.Log("Stage Complete!");
                //TODO : �������� �Ϸ� ������ �ۼ�.
            }
            else
            {
                if (waveStartDelayCur > 0)
                {
                    waveStartDelayCur -= Time.deltaTime;
                }
                else
                {
                    waveCur++;
                    waveStartDelayCur = waveStartDelay;
                }
            }
        }



    }
}
