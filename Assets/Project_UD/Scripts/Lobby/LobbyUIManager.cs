using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;


public class LobbyUIManager : MonoBehaviour
{
    public Button stageStartBtn = null;

    [Header("===== Local Situation UI ====")]
    public RectTransform localSituationPanel = null;
    public Button localSituationPanelCloseBtn = null;
    public Image battleFieldImage = null;
    public Text battleFieldScriptTxt = null;
    public Button commandSkillResetBtn = null;
    public Image[] commandSkillDeckEquipImage = null;
    public Button showCommandSkillListBtn = null;
    public Button battleStartBtn = null;

    [Header("==== CommandSkill UI ====")]
    public RectTransform commandSkillPanel = null;
    public Button commandSkillPanelCloseBtn = null;
    public Image[] commandSkillDeckListImage = null;
    public Image[] commandSkillList = null;
    public Button[] commandSkillEquipBtn = null;
    public Image commandSkillInfoImage = null;
    public Text commandSkillInfoScriptTxt = null; 
    public Button[] commandSkillClearBtn = null;
    public Button commandkSkillSaveBtn = null;

    private Sprite emptyDeckImageSprite = null;
    private Color emptyDeckImageColor;

    private bool[] isCommandSkillDeckEmpty;
    private bool[] isCommandSkillSelect;

    private int[] CommandSkillDeckIndex;

    private string[] skillIDs = {
    "lead_order101", "lead_order102", "lead_order103",
    "lead_support101", "lead_support102", "lead_support103",
    "lead_morale101", "lead_morale102", "lead_morale103"
    };



    private const int maxSkillDeckSize = 3;

    public ParticleSystem buttonParticleEffect;
    public Button particleBtn;

    private CommandSkillManager commandSkillManager;
    private List<CommandSkillManager.SkillData> commandSkillDeckList = new List<CommandSkillManager.SkillData>();

    // Start is called before the first frame update
    void Start()
    {
        commandSkillManager = CommandSkillManager.Instance;

        emptyDeckImageSprite = commandSkillDeckListImage[0].sprite;
        emptyDeckImageColor = commandSkillDeckListImage[0].color;

        isCommandSkillDeckEmpty = new bool[commandSkillDeckListImage.Length];
        CommandSkillDeckIndex = new int[commandSkillDeckListImage.Length];

        if(battleStartBtn != null)
        {
            if (battleStartBtn != null)
            {
                
                battleStartBtn.onClick.AddListener(() =>
                {
                    GlobalSoundManager.instance.PlayLobbySFX(GlobalSoundManager.lobbySfx.sfx_battleStart);
                    SceneManager.LoadSceneAsync("Stage1_Mege_LoPol 2");

                    //if (commandSkillDeckList.Count < maxSkillDeckSize)
                    //{
                    //    //StartCoroutine(FadeUI(saveErrorMessgePanel, true));
                    //    //saveErrorMessgePanel.SetActive(true);
                    //}
                    //else
                    //{
                    //    GlobalSoundManager.instance.PlayLobbySFX(GlobalSoundManager.lobbySfx.sfx_battleStart);
                    //    SceneManager.LoadSceneAsync("Stage1_Mege_LoPol 1");
                    //}
                });
            }
        }

        // UI �ǳ� On / Off
        if (stageStartBtn != null)
        {
            stageStartBtn.onClick.AddListener(() =>
            {
                GlobalSoundManager.instance.PlayLobbySFX(GlobalSoundManager.lobbySfx.sfx_click);
                ShowUI(localSituationPanel);
            });

        }

        if(particleBtn != null)
        {
            particleBtn.onClick.AddListener(() =>
            {
                GlobalSoundManager.instance.PlayLobbySFX(GlobalSoundManager.lobbySfx.sfx_click);
                PlayParticleEffect();
            });

        }

        if (localSituationPanelCloseBtn != null)
        {
            localSituationPanelCloseBtn.onClick.AddListener(() =>
            {
                GlobalSoundManager.instance.PlayLobbySFX(GlobalSoundManager.lobbySfx.sfx_click);
                HideUI(localSituationPanel);  
            });
        }

        if (commandSkillPanelCloseBtn != null)
        {
            commandSkillPanelCloseBtn.onClick.AddListener(() =>
            {
                GlobalSoundManager.instance.PlayLobbySFX(GlobalSoundManager.lobbySfx.sfx_click);
                HideUI(commandSkillPanel);  
                ShowUI(localSituationPanel);
            });
        }

        if (commandSkillResetBtn != null)
        {
            commandSkillResetBtn.onClick.AddListener(() =>
            {
                GlobalSoundManager.instance.PlayLobbySFX(GlobalSoundManager.lobbySfx.sfx_click);
                HideUI(localSituationPanel);
                ShowUI(commandSkillPanel);  

            });
        }
        // UI �ǳ� On/Off


        // �ʱ�ȭ: �� ����
        for (int i = 0; i < isCommandSkillDeckEmpty.Length; i++)
        {
            isCommandSkillDeckEmpty[i] = true;
            CommandSkillDeckIndex[i] = -1;
        }

        isCommandSkillSelect = new bool[commandSkillList.Length];

        // ��ų ���� ��ư�� ���� ������ �߰�
        for (int i = 0; i < commandSkillEquipBtn.Length; i++)
        {
            if (commandSkillEquipBtn[i] != null)
            {
                int buttonIndex = i;

                commandSkillEquipBtn[buttonIndex].onClick.AddListener(() =>
                {
                    GlobalSoundManager.instance.PlayLobbySFX(GlobalSoundManager.lobbySfx.sfx_commanderSkillEquip);
                    AddCommandSkill(buttonIndex);
                });
            }
        }

        // ��ų ���� ��ư�� ���� ������ �߰�
        for (int i = 0; i < commandSkillClearBtn.Length; i++)
        {
            if (commandSkillClearBtn[i] != null)
            {
                int buttonIndex = i;

                commandSkillClearBtn[buttonIndex].onClick.AddListener(() =>
                {
                    GlobalSoundManager.instance.PlayLobbySFX(GlobalSoundManager.lobbySfx.sfx_commanderSkillUnequip);
                    RemoveCommandSkill(buttonIndex);
                });
            }
        }

        if(showCommandSkillListBtn != null)
        {
            showCommandSkillListBtn.onClick.AddListener(() =>
            {
                GlobalSoundManager.instance.PlayLobbySFX(GlobalSoundManager.lobbySfx.sfx_click);
                ShowCommandSkillList();
            });
        }

        if (commandkSkillSaveBtn != null)
        {
            commandkSkillSaveBtn.onClick.AddListener(() =>
            {
                Dictionary<string, string> skillData = new Dictionary<string, string>();

                foreach (var skill in commandSkillDeckList)
                {
                    skillData.Add(skill.SkillID, skill.SkillName);
                }
                GlobalSoundManager.instance.PlayLobbySFX(GlobalSoundManager.lobbySfx.sfx_click);
                SaveCommandSkillList(skillData); 
            });
        }


        //if(saveErrorMessgeBtn != null)
        //{
        //    saveErrorMessgeBtn.onClick.AddListener(() =>
        //    {
        //        StartCoroutine(FadeUI(saveErrorMessgePanel, false));
        //        saveErrorMessgePanel.SetActive(false);
        //    });
        //}
    }

    // ��ų ���� ��� �� UI ������Ʈ
    private void AddCommandSkill(int buttonIndex)
    {
        if (commandSkillDeckList.Count >= maxSkillDeckSize)
        {
            Debug.Log("��ų ���� �ʰ�");
            return;
        }

        if (buttonIndex >= 0 && buttonIndex < skillIDs.Length)
        {
            string skillID = skillIDs[buttonIndex];

            if (commandSkillDeckList.Exists(skill => skill.SkillID == skillID))
            {
                Debug.Log("��ų �ߺ�");
                return;
            }

            if (commandSkillManager.skillDataDictionary.TryGetValue(skillID, out CommandSkillManager.SkillData skill))
            {
                commandSkillDeckList.Add(skill);
                Debug.Log($"Command Skill {skill.SkillName} �߰���");

                for (int i = 0; i < commandSkillDeckListImage.Length; i++)
                {
                    if (isCommandSkillDeckEmpty[i])
                    {
                        commandSkillDeckListImage[i].sprite = commandSkillList[buttonIndex].sprite;
                        commandSkillDeckListImage[i].color = commandSkillList[buttonIndex].color;
                        isCommandSkillDeckEmpty[i] = false;
                        isCommandSkillSelect[buttonIndex] = true;
                        CommandSkillDeckIndex[i] = buttonIndex;
                        commandSkillDeckEquipImage[i].sprite = commandSkillDeckListImage[i].sprite;
                        commandSkillDeckEquipImage[i].color = commandSkillDeckListImage[i].color;

                        break;
                    }
                }
            }
            else
            {
                Debug.LogWarning($"{skillID} ��ų ����");
            }
        }
        else
        {
            Debug.LogWarning("�߸��� �ε���");
        }
    }


    // ��ų ���� ��� �� UI ������Ʈ
    private void RemoveCommandSkill(int deckIndex)
    {
        // �ε��� ���� ����
        if (deckIndex < 0 || deckIndex >= commandSkillDeckListImage.Length)
        {
            Debug.LogWarning("�� �ε����� �߸��Ǿ����ϴ�.");
            return;
        }

        int skillIndex = CommandSkillDeckIndex[deckIndex];

        // �ش� ���� ��� �ִ��� Ȯ�� (skillIndex == -1�� �ƴϰ�, �� �ε����� �ùٸ��� Ȯ��)
        if (skillIndex != -1 && deckIndex >= 0 && deckIndex < commandSkillDeckList.Count)
        {
            Debug.Log($"{commandSkillDeckList[deckIndex].SkillName} ����");
            commandSkillDeckList.RemoveAt(deckIndex);

            // UI ������Ʈ
            commandSkillDeckListImage[deckIndex].sprite = emptyDeckImageSprite;
            commandSkillDeckListImage[deckIndex].color = emptyDeckImageColor;
            isCommandSkillDeckEmpty[deckIndex] = true;
            isCommandSkillSelect[skillIndex] = false;
            CommandSkillDeckIndex[deckIndex] = -1;
            commandSkillDeckEquipImage[deckIndex].sprite = emptyDeckImageSprite;
            commandSkillDeckEquipImage[deckIndex].color = emptyDeckImageColor;
        }
        else
        {
            Debug.LogWarning("�߸��� ��ư �ε��� �Ǵ� ���� ��� �ֽ��ϴ�.");
        }
    }


    private void ShowCommandSkillList()
    {
        if (commandSkillDeckList.Count > 0)
        {
            Debug.Log("���� ������ Command Skills:");
            for (int i = 0; i < commandSkillDeckList.Count; i++)
            {
                var skill = commandSkillDeckList[i];
                Debug.Log($"Skill {i + 1}: {skill.SkillName}");
            }
        }
        else
        {
            Debug.Log("Command Skill ����");
        }
    }

    // Ŀ�Ǵ� ��ų ����
    public void SaveCommandSkillList(Dictionary<string, string> skillData)
    {
        UserDataModel.instance.skillIDs.Clear();
        List<string> skillIDs = new List<string>();

        foreach(var skill in skillData)
        {
            skillIDs.Add(skill.Key);
        }


        //foreach (var entry in skillData)
        //{
        //    // ��ų ID�� �̸��� ���� PlayerPrefs�� ����
        //    PlayerPrefs.SetString(entry.Key, entry.Value);
        //    skillIDs.Add(entry.Key);
        //    UserDataModel.instance.skillIDs.Add(entry.Key);
        //}

        //// ��� ��ų ID ����Ʈ�� ���� (���߿� �ҷ��� �� ���)
        //PlayerPrefs.SetString("SkillIDList", string.Join(",", skillIDs));

        //PlayerPrefs.Save(); // ����

        Debug.Log("Ŀ�Ǵ� ��ų ����");
    }



    // �г��� ũ�⸦ �����ϴ� �ڷ�ƾ
    public IEnumerator AnimateUI(RectTransform ui, bool isActive, float duration = 0.3f)
    {
        // �г��� �߽��� ȭ�� �߾����� ���� (�ǹ��� ��Ŀ�� �߾�����)
        ui.pivot = new Vector2(0.5f, 0.5f);
        ui.anchorMin = new Vector2(0.5f, 0.5f);
        ui.anchorMax = new Vector2(0.5f, 0.5f);

        // ���� ũ��� �� ũ�� ����
        Vector3 startScale = ui.localScale;
        Vector3 endScale;

        // isActive�� ���� ũ�� ����
        if (isActive)
        {
            endScale = Vector3.one;  // (1, 1, 1) ũ��� Ȱ��ȭ
        }
        else
        {
            endScale = Vector3.zero; // (0, 0, 0) ũ��� ��Ȱ��ȭ
        }

        // �ִϸ��̼� Ÿ�̸�
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            // ũ�⸦ ������ ��ȭ��Ŵ
            ui.localScale = Vector3.Lerp(startScale, endScale, time / duration);
            yield return null;
        }

        // ���� ũ�� ����
        ui.localScale = endScale;
        // ������ �� �г��� Ȱ��/��Ȱ�� ����
        ui.gameObject.SetActive(isActive);
    }

    // �г��� Ȱ��ȭ�ϴ� �Լ�
    public void ShowUI(RectTransform ui)
    {
        ui.gameObject.SetActive(true); // ���� Ȱ��ȭ
        StartCoroutine(AnimateUI(ui, true, 0.3f)); // Ŀ���鼭 ��Ÿ���� ����
    }

    // �г��� ��Ȱ��ȭ�ϴ� �Լ�
    public void HideUI(RectTransform ui)
    {
        StartCoroutine(AnimateUI(ui, false, 0.3f)); // �۾����鼭 ������� ����
    }

    private void PlayParticleEffect()
    {
        if (buttonParticleEffect != null)
        {
            buttonParticleEffect.Play();  // ��ƼŬ ȿ�� ����
        }
    }


    // ���̵� ��/�ƿ��� ���� �ڷ�ƾ �Լ�
    public IEnumerator FadeUI(GameObject uiElement, bool isFadeIn, float duration = 0.5f)
    {
        CanvasGroup canvasGroup = uiElement.GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            canvasGroup = uiElement.AddComponent<CanvasGroup>();
        }

        float startAlpha;
        float endAlpha;

        if (isFadeIn)
        {
            startAlpha = 0; 
            endAlpha = 1;   
        }
        else
        {
            startAlpha = 1; 
            endAlpha = 0;   
        }

        float time = 0f;

        // �ִϸ��̼� ���� �ð��� ���� alpha ���� ��ȭ
        while (time < duration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, time / duration);
            yield return null;
        }

        canvasGroup.alpha = endAlpha;

        if (!isFadeIn)
        {
            canvasGroup.blocksRaycasts = true;
            uiElement.SetActive(false);
        }
        else
        {
            canvasGroup.blocksRaycasts = false;
            uiElement.SetActive(true); 
        }
    }

}
