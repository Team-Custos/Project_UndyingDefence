using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ingame_SceneManager : MonoBehaviour
{
    public void _nextScene(int sceneIdx)
    {
        if (sceneIdx >= 0)
        {
            SceneManager.LoadSceneAsync(sceneIdx);
        }
    }

    public void RestartCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();  // ���� Ȱ��ȭ�� �� ��������
        SceneManager.LoadSceneAsync(currentScene.buildIndex);  // �񵿱������� ���� �� �ٽ� �ε�
    }

    // �κ� ������ �̵�
    public void GoToLobby()
    {
        SceneManager.LoadSceneAsync("LobbyScene_LoPo2");  // �񵿱������� �κ� �� �ε�
    }

    // Start is called before the first frame update
    void Start()
    {
        if(Ingame_UIManager.instance.waveRestartBtn != null)
        {
            Time.timeScale = 1.0f;
            Ingame_UIManager.instance.waveRestartBtn.onClick.AddListener(RestartCurrentScene);
        }

        if(Ingame_UIManager.instance.lobbybtn != null)
        {
            Time.timeScale = 1.0f;
            Ingame_UIManager.instance.lobbybtn.onClick.AddListener(GoToLobby);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
