using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Weapon {

    private void Update()
    {
        Vector3 vel = GetComponent<Rigidbody2D>().velocity;
        float angle = Mathf.Atan2(vel.y, vel.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle - 143);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

}
