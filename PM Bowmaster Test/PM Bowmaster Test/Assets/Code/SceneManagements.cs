using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameData;
using UnityEngine.UI;
using TMPro;

public class SceneManagements : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private TextMeshProUGUI m_GameOverText;
    #endregion

    #region Properties
    public TextMeshProUGUI GameOverText
    {
        get { return m_GameOverText;}
        set { m_GameOverText = value; }
    }
    #endregion

    #region UnityCallbacks
    private void Awake()
    {
        CharacterData.CharacterDataLoader.Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("GameScene");
        }
    }
    #endregion

    #region ClassFunctions
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    #endregion
}
