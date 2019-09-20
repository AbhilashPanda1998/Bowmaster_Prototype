using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AI : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private List<GameObject> m_WeaponList = new List<GameObject>();
    [SerializeField]
    private GameObject m_ReleasePoint;
    public static Action<bool> CheckAITurn;
    #endregion

    #region UnityCallbacks
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
        weaponInstance.GetComponent<Rigidbody2D>().AddForce(new Vector3(-0.85f,0.51f,0) * 1 * 20, ForceMode2D.Impulse);
        weaponInstance.GetComponent<Weapon>().WeaponAssigner = this.gameObject;
        Camera.main.gameObject.GetComponent<CameraController>().Target = weaponInstance.gameObject;
        Camera.main.gameObject.GetComponent<CameraController>().SetState(CameraController.CameraStates.AI_WEAPON);
        isPlayerTurn = true;
        if (CheckAITurn != null)
        {
            CheckAITurn(isPlayerTurn);
        }
    }
    #endregion
}
