using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Enemy
{
    public float amplitude = 5;
    public float frequency = 6;

    public override void Move()
    {
        transform.position += new Vector3(0, amplitude * frequency * Mathf.Cos(frequency * Time.time), 0);
        base.Move();
    }

    protected override void Dead()
    {
        GetComponent<Rigidbody2D>().gravityScale = 1;
        base.Dead();
    }
}
