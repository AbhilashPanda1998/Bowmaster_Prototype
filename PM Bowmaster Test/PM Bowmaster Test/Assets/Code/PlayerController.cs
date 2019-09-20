using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private InitializeDrag m_InitializeDrag;
    private GameObject m_Weapon;
    [SerializeField]
    private float m_PowerMultiplier;
    private float m_DragPower;
    private float m_Angle;
    public Transform ReleasePointTransform;
    public Transform ProjectileSpawnTransform;
    public Transform AimerTransform;
    private GameObject m_WeaponInstance = null;
    private float m_PlayerHp;
    private string m_SelectedCharacterName;
    private CharacterData m_CharacterData;
    public delegate void CameraAction();
    public static event CameraAction CameraActionEvent;
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
    }

    private void OnDisable()
    {
        InitializeDrag.ReleaseDragEvent -= OnWeaponRelease;
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
        m_WeaponInstance = Instantiate(m_Weapon, ProjectileSpawnTransform.position, m_Weapon.transform.rotation) as GameObject;
        Vector2 direction = (AimerTransform.position - ReleasePointTransform.transform.position).normalized;
        m_WeaponInstance.GetComponent<Rigidbody2D>().AddForce(direction * m_DragPower * m_PowerMultiplier, ForceMode2D.Impulse);
        if (CameraActionEvent != null)
            CameraActionEvent.Invoke();
    }
    #endregion
}
