using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;
using System;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour,ICanBeDamaged
{
    #region Variables
    [SerializeField]
    private InitializeDrag m_InitializeDrag;
    [SerializeField]
    private float m_PowerMultiplier;
    [SerializeField]
    private TextMeshProUGUI m_PowerValue;
    [SerializeField]
    private TextMeshProUGUI m_AngleValue;
    private GameObject m_Weapon;
    private Slider m_Slider;
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
    private SceneManagements m_SceneManagement;
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
        m_SceneManagement = GameObject.FindObjectOfType<SceneManagements>();
        m_CharacterData = CharacterData.CharacterDataLoader.GetData(m_SelectedCharacterName);
        m_Slider = GetComponentInChildren<Slider>();
        m_PlayerHp = m_CharacterData._hp;
        m_Slider.value = m_PlayerHp;
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
        m_PowerValue.text = "Power: "+ Mathf.Round(m_DragPower * m_PowerMultiplier*4.3f).ToString();
        m_AngleValue.text = "Angle: "+ Mathf.Round(m_Angle).ToString();
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

    public void TakeDamage(float damageAmount)
    {
        m_PlayerHp -= damageAmount;
        m_Slider.value = m_PlayerHp;
        if (m_PlayerHp <= 0)
        {
            m_SceneManagement.PauseGame();
            m_SceneManagement.GameOverText.text = "AI Won.  Press Esc to Restart";
        }
    }
    #endregion
}
