using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public enum CameraStates
    {
        IDLE,
        PLAYER_WEAPON,
        PLAYER,
        AI,
        AI_WEAPON
    }

    #region Variables
    private GameObject m_Weapon;
    private GameObject m_AI;
    private GameObject m_Player;
    private CameraStates m_States;
    private GameObject m_Target;
    #endregion

    #region Properties
    public GameObject Target
    {
        get { return m_Target;}
        set { m_Target = value; }
    }
    #endregion
    #region Unity Callbacks
    private void OnEnable()
    {
        PlayerController.CheckPlayerTurn += AssignPlayerWeapon;
        m_AI = GameObject.FindObjectOfType<AI>().gameObject;
    }

    private void OnDisable()
    {
        PlayerController.CheckPlayerTurn -= AssignPlayerWeapon;
    }

    void FixedUpdate()
    {
        switch (m_States)
        {
            case CameraStates.IDLE:
                m_Target = gameObject;
                break;
            case CameraStates.PLAYER:
                m_Target = m_Player;
                break;
            case CameraStates.PLAYER_WEAPON:
                m_Target = m_Weapon;
                break;
            case CameraStates.AI:
                m_Target = m_AI;
                break;
            case CameraStates.AI_WEAPON:
                break;
            default:
                break;
        }
        float interpolation = 5 * Time.deltaTime;
        Vector3 position = this.transform.position;
        position.y = Mathf.Lerp(this.transform.position.y, m_Target.transform.position.y, interpolation);
        position.x = Mathf.Lerp(this.transform.position.x, m_Target.transform.position.x, interpolation);
        this.transform.position = position;
    }
    #endregion

    #region ClassFunctions
    private void AssignPlayerWeapon(bool m_PlayerTurn)
    {
        m_Player = GameObject.FindObjectOfType<PlayerController>().gameObject;
        m_Weapon = GameObject.FindObjectOfType<Weapon>().gameObject;
        SetState(CameraStates.PLAYER_WEAPON);
    }

    public void SetState(CameraStates state)
    {
        m_States = state;
    }
    #endregion
}
