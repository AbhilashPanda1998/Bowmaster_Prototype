using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponProjectile : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private InitializeDrag m_InitializeDrag;
    [SerializeField]
    private GameObject m_Weapon;
    [SerializeField]
    private float m_PowerMultiplier;
    private float m_DragPower;
    public Transform ReleasePointTransform;
    public Transform ProjectileSpawnTransform;
    public Transform AimerTransform;
    private GameObject m_WeaponInstance = null;
    #endregion

    #region Properties
    #endregion

    #region Unity Callbacks
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
    }
    #endregion
}
