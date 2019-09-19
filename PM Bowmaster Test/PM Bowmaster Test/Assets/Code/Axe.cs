using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : Weapon {

    private void Update()
    {
        transform.Rotate(0,0, -800 * Time.deltaTime);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
}
