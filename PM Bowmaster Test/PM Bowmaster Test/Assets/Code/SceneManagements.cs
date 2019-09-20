using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameData;

public class SceneManagements : MonoBehaviour
{
    #region Variables
    private static SceneManagements m_Instance;
    #endregion

    #region Properties
    public static SceneManagements Instance
    {
        get { return m_Instance; }
    }
    #endregion

    #region UnityCallbacks
    private void Awake()
    {
        if (m_Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            m_Instance = this;
            DontDestroyOnLoad(gameObject);
            CharacterData.CharacterDataLoader.Init();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Play();
    }
    #endregion

    #region ClassFunctions
    public void Play()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Exit()
    {
        Application.Quit();
    }
    #endregion
}
