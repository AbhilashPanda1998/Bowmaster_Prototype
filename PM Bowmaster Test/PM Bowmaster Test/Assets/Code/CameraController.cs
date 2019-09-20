using System.Collections;
using System.Collections.Generic;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Variables
    private GameObject m_Weapon;
    private bool m_CanFollow;
    #endregion

    #region Unity Callbacks
    private void OnEnable()
    {
        PlayerController.CameraActionEvent += CameraFollowWeapon;
    }

    private void OnDisable()
    {
        PlayerController.CameraActionEvent -= CameraFollowWeapon;
    }

    void FixedUpdate()
    {
        if (m_CanFollow)
        {
            float interpolation = 10 * Time.deltaTime;
            Vector3 position = this.transform.position;
            position.y = Mathf.Lerp(this.transform.position.y, m_Weapon.transform.position.y, interpolation);
            position.x = Mathf.Lerp(this.transform.position.x, m_Weapon.transform.position.x, interpolation);
            this.transform.position = position;
        }
    }
    #endregion

    #region ClassFunctions
    private void CameraFollowWeapon()
    {
        m_CanFollow = true;
        m_Weapon = GameObject.FindObjectOfType<Weapon>().gameObject;
    }
    #endregion
}
