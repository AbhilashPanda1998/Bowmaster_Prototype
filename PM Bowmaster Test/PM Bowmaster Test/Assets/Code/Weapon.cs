using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {

    #region Variables
    [SerializeField]
    private int DamageAmount;
    private GameObject m_WeaponAssigner;
    #endregion

    #region Properties
    public GameObject WeaponAssigner
    {
        get { return m_WeaponAssigner; }
        set { m_WeaponAssigner = value; }
    }
    #endregion

    #region UnityCallbacks
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {

    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        this.enabled = false;
        Invoke("LerpCamera", 2f);
    }
    #endregion

    #region ClassFunction
    private void LerpCamera()
    {
        if (m_WeaponAssigner.GetComponent<PlayerController>())
        {
            Camera.main.gameObject.GetComponent<CameraController>().SetState(CameraController.CameraStates.AI);
        }
        else
        {
            Camera.main.gameObject.GetComponent<CameraController>().SetState(CameraController.CameraStates.PLAYER);
        }
        Destroy(gameObject);
    }
    #endregion
}
