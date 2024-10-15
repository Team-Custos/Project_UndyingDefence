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

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner inst;
    ObjectPool objectPool;
    public List<Ingame_UnitData> enemyDatas;

    GridManager gridManager;

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

    public int currentWave = 1;
    private int spawnedMonsterCount = 0;
    public bool isWaveing = false;

    public List<GameObject> activeMonsters = new List<GameObject>();


    private void Awake()
    {
        inst = this;

        ObjectPool.Instance.Intialize(100);
    }

    private void Start()
    {
        gridManager = InGameManager.inst.gridManager;

        mobSpawnPosX = gridManager._width;
        gridHeight = gridManager._height;

        spawnPoint = new Transform[gridHeight];


        StartCoroutine(StartWaveWithDelay(1f));
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMobSpawnerPosSet)
        {
            for (int idx = 0; idx < gridManager.Tiles_Obj.Length; idx++)
            {
                //Debug.Log("Idx : " + idx);
                if (gridManager.Tiles_Obj[idx].GetComponent<GridTile>().GridPos.x == mobSpawnPosX - 1)
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



        //timer += Time.deltaTime;
        ////level = Mathf.FloorToInt(Game_Manager.instance.gameTime / 10f);

        //if (level >= spawnData.Length - 1)
        //{
        //    level = spawnData.Length - 1;
        //}

        //if (timer > spawnData[level].spawnTime)
        //{
        //    timer = 0;
        //    //EnemySpawn();
        //}



    }

    //�� ��ȯ
    public GameObject EnemySpawn(int enemyType, float X, float Y)
    {
        Debug.Log(new Vector3(X, 0, Y));
        GameObject Obj = Instantiate(Test_Enemy);
        Obj.transform.position = new Vector3(X, 0, Y);
        //Obj.GetComponent<Ingame_UnitCtrl>().unitPos = new Vector2(X, Y);
        Obj.GetComponent<Ingame_UnitCtrl>().unitData = enemyDatas[enemyType];
        return Obj;
    }

    public IEnumerator WaveSystem()
    {
        if (isWaveing)
        {
            Ingame_UIManager.instance.ShowUI(Ingame_UIManager.instance.waveStartPanel, 1.5f);

            Debug.Log($"Wave {currentWave} ����");

            // ���� ����
            yield return StartCoroutine(SpawnMonstersForWave());

            //OnMonsterDead(monster)

            // ��� ���Ͱ� �׾����� Ȯ��
            yield return StartCoroutine(CheckAllMonstersDead());

            currentWave++;


            isWaveing = false;

            isWaveing = true;

            //StartCoroutine(StartNextWaveCountdown()); // ���� ���̺� ī��Ʈ�ٿ� ����
        }
    }

    // ���̺꿡 ���� ���� ���� �ڷ�ƾ
    IEnumerator SpawnMonstersForWave()
    {
        int monstersToSpawn;

        if (currentWave <= 2)
        {
            monstersToSpawn = 5; // ���� ���̺갡 1 �Ǵ� 2�� ���, 5������ ���͸� ����
        }
        else
        {
            monstersToSpawn = 10; // ���� ���̺갡 3 �̻��� ���, 10������ ���͸� ����
        }

        for (int i = 0; i < monstersToSpawn; i++)
        {
            SpawnEnemy(1);
            yield return new WaitForSeconds(spawnInterval);  // �� ������ ����
        }

        //if (currentWave <= 2)
        //{
        //    // 1, 2 ���̺꿡���� ���� A�� ����
        //    for (int i = 0; i < monsterPerWave; i++)
        //    {
        //        SpawnEnemy(Random.Range(0, 2)); // ���� A ����
        //        yield return new WaitForSeconds(spawnInterval);
        //    }
        //}
        //else
        //{
        //    for (int i = 0; i < monsterPerWave * 2; i++)
        //    {
        //        SpawnEnemy(0);
        //        // 3~10 ���̺�� A, B ���Ͱ� ���� 5������ �����ǵ��� ����
        //        //int spawnedA = 0;
        //        //int spawnedB = 0;

        //        //while (spawnedA < monsterPerWave || spawnedB < monsterPerWave)
        //        //{
        //        //    // �������� A �Ǵ� B ���� ����
        //        //    int enemyType = Random.Range(0, 2);

        //        //    if (enemyType == 0 && spawnedA < monsterPerWave)
        //        //    {
        //        //        SpawnEnemy(0); // A ���� ����
        //        //        spawnedA++;
        //        //    }
        //        //    else if (enemyType == 1 && spawnedB < monsterPerWave)
        //        //    {
        //        //        SpawnEnemy(1); // B ���� ����
        //        //        spawnedB++;
        //        //    }

        //        yield return new WaitForSeconds(spawnInterval);
        //    }
        //}
    }

    // ���� ����
    void SpawnEnemy(int enemyType)
    {
        Transform spawnPos = poolSapwnPoint[Random.Range(0, poolSapwnPoint.Length)];
        GameObject enemyObj = ObjectPool.GetObject();

        if (enemyObj != null)
        {
            enemyObj.transform.position = spawnPos.position;
            enemyObj.transform.rotation = Quaternion.identity;
            enemyObj.GetComponent<Ingame_UnitCtrl>().unitData = enemyDatas[enemyType];
            activeMonsters.Add(enemyObj);
        }

    }

    // ���� ���� ó��
    public void OnMonsterDead(GameObject monster)
    {
        if (activeMonsters.Contains(monster))
        {
            activeMonsters.Remove(monster);
            ObjectPool.ReturnObject(monster); // ������Ʈ�� Ǯ�� ��ȯ
        }
    }

    public IEnumerator StartWaveWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 1�� ���
        StartCoroutine(WaveSystem());  // ���̺� ����
    }

    // ���Ͱ� ��� �׾����� Ȯ��
    IEnumerator CheckAllMonstersDead()
    {
        // ���Ͱ� ��� ���� ������ ���
        while (activeMonsters.Count > 0)
        {
            yield return null;
        }

        
        // 10�� ���̺� ���������� ���� �г��� ǥ��
        if (currentWave < waveCount)
        {
            Ingame_UIManager.instance.ShowUI(Ingame_UIManager.instance.waveStepSuccessPanel, 3.0f);

            yield return new WaitForSeconds(3.0f);

            Debug.Log("��� ���Ͱ� �׾����ϴ�. ���� ���̺긦 �غ��մϴ�.");
        }

        yield return new WaitForSeconds(1.0f);

        Ingame_UIManager.instance.isCountDownIng = true;

        // ������ ���̺�(10�� ���̺�)�� ������ ��
        if (currentWave == waveCount && activeMonsters.Count <= 0)
        {
            Ingame_UIManager.instance.waveResultImage.sprite = Ingame_UIManager.instance.waveWinImage;

            Ingame_UIManager.instance.waveResultPanel.SetActive(true);

            Debug.Log("���̺� ����");
            Time.timeScale = 0.0f;
        }
    }
    // ���� ���̺� ī��Ʈ�ٿ� ����


    public void NextWave()
    {
        if (isWaveing)
        {
            Debug.LogWarning("���̺갡 �̹� ���� ���Դϴ�. ���ο� ���̺� ������ �ߴ��մϴ�.");
            return;
        }

        Debug.Log("���� ���̺�� �Ѿ�ϴ�.");

        // ���� Ȱ��ȭ�� ��� ���� ����
        foreach (var monster in activeMonsters)
        {
            Destroy(monster);
        }

        // ���� ����Ʈ �ʱ�ȭ
        activeMonsters.Clear();

        if (!isWaveing && currentWave < waveCount)
        {
            StartCoroutine(WaveSystem());
        }
    }

}