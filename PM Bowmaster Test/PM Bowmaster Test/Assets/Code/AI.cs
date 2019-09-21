using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class AI : MonoBehaviour, ICanBeDamaged
{
    #region Variables
    [SerializeField]
    private List<GameObject> m_WeaponList = new List<GameObject>();
    [SerializeField]
    private GameObject m_ReleasePoint;
    [SerializeField]
    private float m_MaxHealth;
    private float m_CurrentHealth;
    public static Action<bool> CheckAITurn;
    private Slider m_Slider;
    private SceneManagements m_SceneManagement;
    #endregion

    #region UnityCallbacks

    private void Start()
    {
        m_SceneManagement = GameObject.FindObjectOfType<SceneManagements>();
        m_Slider = GetComponentInChildren<Slider>();
        m_CurrentHealth = m_MaxHealth;
        m_Slider.value = m_CurrentHealth;
    }

    private void OnEnable()
    {
        PlayerController.CheckPlayerTurn += AITurn;
    }

    private void OnDisable()
    {
        PlayerController.CheckPlayerTurn -= AITurn;
    }
    #endregion

    #region ClassFunctions
    private void AITurn(bool isPlayerTurn)
    {
        StartCoroutine(WaitandFire(isPlayerTurn));
    }

    IEnumerator WaitandFire(bool isPlayerTurn)
    {
        yield return new WaitForSeconds(7);
        GameObject weaponInstance = Instantiate(m_WeaponList[UnityEngine.Random.Range(0, m_WeaponList.Count)], m_ReleasePoint.transform.position,Quaternion.identity);
        weaponInstance.GetComponent<Rigidbody2D>().AddForce(new Vector3(-0.85f,0.51f,0) * 1 * UnityEngine.Random.Range(19,22), ForceMode2D.Impulse);
        weaponInstance.GetComponent<Weapon>().WeaponAssigner = this.gameObject;
        Camera.main.gameObject.GetComponent<CameraController>().Target = weaponInstance.gameObject;
        Camera.main.gameObject.GetComponent<CameraController>().SetState(CameraController.CameraStates.AI_WEAPON);
        isPlayerTurn = true;
        if (CheckAITurn != null)
        {
            CheckAITurn(isPlayerTurn);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        m_CurrentHealth -= damageAmount;
        m_Slider.value = m_CurrentHealth;
        if (m_CurrentHealth <= 0)
        {
            m_SceneManagement.PauseGame();
            m_SceneManagement.GameOverText.text = "Player Won.  Press Esc to Restart";
        }
    }
    #endregion
}
