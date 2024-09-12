using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[System.Serializable]

public class EnemySpawnData
{
    public int modelType;
    public float spawnTime;
    public int HP;
    public float moveSpeed;
    public int attackPoint;
    public float attackSpeed;
    public int critChanceRate;

    public float sightRange;
    public float attackRange;

    public int generalSkill;
    public int specialSkill;
    public UnitType unitType;
    public DefenseType defenseType;
    public TargetSelectType targetSelectType;

}

//public enum EnemyType
//{
//    Warrior,
//    Archer,
//}

public class UD_Ingame_EnemySpawner : MonoBehaviour
{
    public static UD_Ingame_EnemySpawner inst;
    //UD_Ingame_ObjectPool objectPool;

    UD_Ingame_GridManager gridManager;

    int mobSpawnPosX = 0;
    int gridHeight = 0;

    int pointidx = 0;

    public Transform[] spawnPoint;
    public EnemySpawnData[] spawnData;

    bool isMobSpawnerPosSet = false;

    int level;
    float timer;

    public GameObject Test_Enemy;

    public int enemyToSpawn = 0;

    public Transform[] poolSapwnPoint;

    public int waveCount = 10;
    public int monsterPerWave = 5;
    public float spawnInterval = 2.0f;

    public GameObject Test_EnemyA;
    public GameObject Test_EnemyB;

    private int currentWave = 1;
    private int spawnedMonsterCount = 0;
    private bool isWaveInProgress = false;

    private List<GameObject> activeMonsters = new List<GameObject>();
    bool isCurrentWaveFinshed = false;


    private void Awake()
    {
       inst = this;
        
    }

    private void Start()
    {
        gridManager = UD_Ingame_GameManager.inst.gridManager;

        mobSpawnPosX = gridManager._width;
        gridHeight = gridManager._height;

        spawnPoint = new Transform[gridHeight];

        UD_Ingame_ObjectPool.Instance.Intialize(20);

        currentWave = 1;

        StartCoroutine(WaveSystem());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMobSpawnerPosSet)
        {
            for (int idx = 0; idx < gridManager.Tiles_Obj.Length; idx++)
            {
                //Debug.Log("Idx : " + idx);
                if (gridManager.Tiles_Obj[idx].GetComponent<UD_Ingame_GridTile>().GridPos.x == mobSpawnPosX - 1)
                {

                    //Debug.Log("PointIdx : " + pointidx);
                    spawnPoint[pointidx] = gridManager.Tiles_Obj[idx].transform;
                    if (pointidx >= gridHeight - 1)
                    {
                        isMobSpawnerPosSet = true;
                    }
                    else
                    {
                        pointidx++;
                    }
                    continue;
                }
            }
        }

        

        timer += Time.deltaTime;
        //level = Mathf.FloorToInt(Game_Manager.instance.gameTime / 10f);

        if (level >= spawnData.Length - 1)
        {
            level = spawnData.Length - 1;
        }

        if (timer > spawnData[level].spawnTime)
        {
            timer = 0;
            //EnemySpawn();
        }


        
    }

    //�� ��ȯ
    public GameObject EnemySpawn(int enemyType,float X, float Y)
    {
        Debug.Log(new Vector3(X, 0, Y));
        GameObject Obj = Instantiate(Test_Enemy);
        Obj.transform.position = new Vector3(X, 0, Y);
        //Obj.GetComponent<UD_Ingame_UnitCtrl>().unitPos = new Vector2(X, Y);
        Obj.GetComponent<UD_Ingame_UnitCtrl>().EnemyInit(spawnData[enemyType]);
        return Obj;
    }

    IEnumerator WaveSystem()
    {
        isWaveInProgress = true;
        spawnedMonsterCount = 0;

        Debug.Log($"Wave {currentWave} ����");

        // ���� ���� ��ƾ ����
        yield return StartCoroutine(SpawnMonstersForWave());

        // ���Ͱ� ��� ���� ������ ��ٸ�
        //yield return StartCoroutine(CheckAllMonstersDead());

        //Debug.Log("20�� ���");
        //yield return new WaitForSeconds(20f); // 20�� ���

        isWaveInProgress = false; 
    }

    // ���̺꿡 ���� ���� ���� �ڷ�ƾ
    IEnumerator SpawnMonstersForWave()
    {
        if (currentWave <= 2)
        {
            // 1, 2 ���̺꿡���� ���� A�� ����
            for (int i = 0; i < monsterPerWave; i++)
            {
                SpawnEnemy(0); // ���� A ����
                yield return new WaitForSeconds(spawnInterval);
            }
        }
        else
        {
            // 3~10 ���̺�� A, B ���Ͱ� ���� 5������ �����ǵ��� ����
            int spawnedA = 0;
            int spawnedB = 0;

            while (spawnedA < monsterPerWave || spawnedB < monsterPerWave)
            {
                // �������� A �Ǵ� B ���� ����
                int enemyType = Random.Range(0, 2);

                if (enemyType == 0 && spawnedA < monsterPerWave)
                {
                    SpawnEnemy(0); // A ���� ����
                    spawnedA++;
                }
                else if (enemyType == 1 && spawnedB < monsterPerWave)
                {
                    SpawnEnemy(1); // B ���� ����
                    spawnedB++;
                }

                // A�� B ��� ���� �Ϸ� �� ������ ����
                if (spawnedA >= monsterPerWave && spawnedB >= monsterPerWave)
                {
                    break;
                }

                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }

    // ���� ����
    void SpawnEnemy(int enemyType)
    {
        // ���� ��ġ ���� ����
        Transform spawnPos = poolSapwnPoint[Random.Range(0, poolSapwnPoint.Length)];

        GameObject enemyObj = UD_Ingame_ObjectPool.GetObject(); // ������Ʈ Ǯ���� ��������

        enemyObj.transform.position = spawnPos.position;
        enemyObj.transform.rotation = Quaternion.identity;


        if (enemyType == 0)
        {
            enemyObj.GetComponent<UD_Ingame_UnitCtrl>().EnemyInit(spawnData[enemyType]); // �� �ʱ�ȭ
        }
        else
        {
            enemyObj.GetComponent<UD_Ingame_UnitCtrl>().EnemyInit(spawnData[enemyType]); // �� �ʱ�ȭ
        }

        activeMonsters.Add(enemyObj); // ������ ���� ��Ͽ� �߰�
    }

    // ���� ���� ó��
    public void OnMonsterDead(GameObject monster)
    {
        if (activeMonsters.Contains(monster))
        {
            activeMonsters.Remove(monster);
            UD_Ingame_ObjectPool.ReturnObject(monster); // ������Ʈ�� Ǯ�� ��ȯ
        }
    }

    // ���Ͱ� ��� �׾����� Ȯ��
    IEnumerator CheckAllMonstersDead()
    {
        while (activeMonsters.Count > 0) // ���Ͱ� ���� ���� �ִ��� Ȯ��
        {
            yield return null;
        }

    }

    public void NextWave()
    {
        Debug.Log("���� ���̺�� �Ѿ�ϴ�.");

        // ���� Ȱ��ȭ�� ��� ���� ����
        foreach (var monster in activeMonsters)
        {
            Destroy(monster);
        }

        // ���� ����Ʈ �ʱ�ȭ
        activeMonsters.Clear();

        if (!isWaveInProgress && currentWave < waveCount)
        {
            currentWave++;
            StartCoroutine(WaveSystem());
        }
    }
}
