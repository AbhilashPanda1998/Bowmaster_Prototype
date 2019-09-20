using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;
using System;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private InitializeDrag m_InitializeDrag;
    [SerializeField]
    private float m_PowerMultiplier;
    private GameObject m_Weapon;
    private float m_DragPower;
    private float m_Angle;
    private bool m_isPlayerTurn =true;
    public Transform ReleasePointTransform;
    public Transform ProjectileSpawnTransform;
    public Transform AimerTransform;
    private GameObject m_WeaponInstance = null;
    private float m_PlayerHp;
    private string m_SelectedCharacterName;
    private CharacterData m_CharacterData;
    public static Action<bool> CheckPlayerTurn;
    #endregion

    #region Properties
    public GameObject WeaponPrefab
    {
        get { return m_Weapon; }
        set { m_Weapon = value; }
    }

    public string CharacterName
    {
        get { return m_SelectedCharacterName; }
        set { m_SelectedCharacterName = value; }
    }
    #endregion

    #region Unity Callbacks
    private void Start()
    {
        m_CharacterData = CharacterData.CharacterDataLoader.GetData(m_SelectedCharacterName);
        m_PlayerHp = m_CharacterData._hp;
        m_Weapon = m_CharacterData._weapon;
    }
    private void OnEnable()
    {
        InitializeDrag.ReleaseDragEvent += OnWeaponRelease;
        AI.CheckAITurn += ResetPlayerTurn;
    }

    private void OnDisable()
    {
        InitializeDrag.ReleaseDragEvent -= OnWeaponRelease;
        AI.CheckAITurn -= ResetPlayerTurn;
    }

    private void Update()
    {
        m_DragPower = Vector2.Distance(m_InitializeDrag.transform.position, ReleasePointTransform.transform.position);
        m_Angle = Vector2.Angle(m_InitializeDrag.transform.position, ReleasePointTransform.transform.position) + 90;
        var pullDirection = ReleasePointTransform.position - (m_InitializeDrag.transform.position - ReleasePointTransform.position).normalized;
        AimerTransform.position = pullDirection;
    }
    #endregion

    #region ClassFunctions
    private void OnWeaponRelease()
    {
        m_isPlayerTurn = false;
        m_InitializeDrag.gameObject.SetActive(false);
        m_WeaponInstance = Instantiate(m_Weapon, ProjectileSpawnTransform.position, m_Weapon.transform.rotation) as GameObject;
        m_WeaponInstance.GetComponent<Weapon>().WeaponAssigner = this.gameObject;
        Vector2 direction = (AimerTransform.position - ReleasePointTransform.transform.position).normalized;
        m_WeaponInstance.GetComponent<Rigidbody2D>().AddForce(direction * m_DragPower * m_PowerMultiplier, ForceMode2D.Impulse);
        if(CheckPlayerTurn!=null)
        {
            CheckPlayerTurn(m_isPlayerTurn);
        }

    }

    private void ResetPlayerTurn(bool isPlayerTurn)
    {
        m_isPlayerTurn = isPlayerTurn;
        m_InitializeDrag.gameObject.SetActive(true);
    }
    #endregion
}
