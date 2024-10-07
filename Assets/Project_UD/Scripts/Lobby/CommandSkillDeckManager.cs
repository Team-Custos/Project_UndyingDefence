using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CommandSkillDeckManager : MonoBehaviour
{
    public Image[] CommandSkillDeckList = null;
    public Image[] unitList = null;

    public Button[] unitDeckButton = null;
    public Button[] unitListButton = null;

    private Sprite emptyDeckImageSprite = null;
    private Color emptyDeckImageColor;

    private bool[] isUnitDeckEmpty;
    private bool[] isUnitSelect;

    private int[] unitDeckIndex;

    public Button StageStartBtn = null;


    private List<CommandSkillManager.SkillData> commandskillList = new List<CommandSkillManager.SkillData>();
    private CommandSkillManager commandskillDataManager;



    // Start is called before the first frame update
    void Start()
    {
        commandskillDataManager = FindObjectOfType<CommandSkillManager>();

        emptyDeckImageSprite = CommandSkillDeckList[0].sprite;
        emptyDeckImageColor = CommandSkillDeckList[0].color;

        isUnitDeckEmpty = new bool[CommandSkillDeckList.Length];

        unitDeckIndex = new int[CommandSkillDeckList.Length];

        for (int i = 0; i < isUnitDeckEmpty.Length; i++)
        {
            isUnitDeckEmpty[i] = true;
            unitDeckIndex[i] = -1;
        }

        isUnitSelect = new bool[unitList.Length];

        for (int i = 0; i < isUnitSelect.Length; i++)
        {
            isUnitSelect[i] = false;
        }

        for (int i = 0; i < unitListButton.Length; i++)
        {
            
            if (unitListButton[i] != null)
            {
                int unitIndex = i;
                unitListButton[i].onClick.AddListener(() => _addUnitToDeck(unitIndex));
            }
        }

        for (int i = 0; i < unitDeckButton.Length; i++)
        {

            if (unitDeckButton[i] != null)
            {
                int deckIndex = i;
                unitDeckButton[i].onClick.AddListener(() => _clearUnitDeck(deckIndex));
            }
        }

        if (StageStartBtn != null)
        {
            StageStartBtn.onClick.AddListener(() =>
            {
                SceneManager.LoadSceneAsync("Stage 1");
            });
        }

        for (int i = 0; i < unitDeckButton.Length; i++)
        {
            int index = i;
            unitDeckButton[index].onClick.AddListener(() => AddCommandSkillToList(index));
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void _addUnitToDeck(int unitIndex)
    {
        if (isUnitSelect[unitIndex])
        {
            Debug.Log("���Ǵ� ��ų �ߺ�");
            return;
        }


        for (int i = 0; i < isUnitDeckEmpty.Length; i++)
        {
            if (isUnitDeckEmpty[i])
            {
                CommandSkillDeckList[i].sprite = unitList[unitIndex].sprite;
                CommandSkillDeckList[i].color = unitList[unitIndex].color;
                isUnitDeckEmpty[i] = false;
                isUnitSelect[unitIndex] = true;
                unitDeckIndex[i] = unitIndex;
                break;
            }
        }
    }

    void _clearUnitDeck(int deckIndex)
    {
        int unitIndex = unitDeckIndex[deckIndex];
        if (unitIndex != -1)
        {
            CommandSkillDeckList[deckIndex].sprite = emptyDeckImageSprite;
            CommandSkillDeckList[deckIndex].color = emptyDeckImageColor;
            isUnitDeckEmpty[deckIndex] = true;
            isUnitSelect[unitIndex] = false;
            unitDeckIndex[deckIndex] = -1;
        }
    }

    private void AddCommandSkillToList(int index)
    {
        string stringIndex = index.ToString();

        // CommandSkillManager ��ųʸ����� Ư�� CommandSkill ��������
        if (commandskillDataManager.skillDataDictionary.TryGetValue(stringIndex, out CommandSkillManager.SkillData skill))
        {
            commandskillList.Add(skill); // ����Ʈ�� �߰�
            Debug.Log($"CommandSkill {index} �߰���");
        }
        else
        {
            Debug.LogWarning("�ش� �ε����� ���� CommandSkill�� ã�� �� �����ϴ�.");
        }
    }

    public void ClearUnitDeck(int deckIndex)
    {
        if (deckIndex >= 0 && deckIndex < commandskillList.Count)
        {
            commandskillList.RemoveAt(deckIndex); // ����Ʈ���� �ش� �ε��� ����
            Debug.Log($"CommandSkill {deckIndex} ������");
        }
        else
        {
            Debug.LogWarning("��ȿ���� ���� �ε����Դϴ�.");
        }
    }
}
