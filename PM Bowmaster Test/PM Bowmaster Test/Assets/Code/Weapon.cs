using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {

    #region Variables
    [SerializeField]
    private int DamageAmount;
    #endregion

    #region Class Functions
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {

    }
    #endregion
}
        