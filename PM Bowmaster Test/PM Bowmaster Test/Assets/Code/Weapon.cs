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

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        this.enabled = false;
    }
    #endregion
}
        