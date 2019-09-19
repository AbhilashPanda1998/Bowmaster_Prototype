using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject m_Weapon;
    private bool m_CanFollow;

    private void OnEnable()
    {
        InitializeDrag.ReleaseDragEvent += CameraFollowWeapon;
    }

    private void OnDisable()
    {
        InitializeDrag.ReleaseDragEvent -= CameraFollowWeapon;
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
    private void CameraFollowWeapon()
    {
        //Camera.main.gameObject.SetActive(false);
        m_CanFollow = true;
        m_Weapon = GameObject.FindObjectOfType<Weapon>().gameObject;
    }
}
