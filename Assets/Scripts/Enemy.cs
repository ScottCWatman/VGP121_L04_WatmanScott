using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public Transform raycastPoint;
    public float maxFallDistance = 1;
    public LayerMask groundLayer;

    Vector3 startPos;

	// Use this for initialization
	public override void Start()
    {
        base.Start();
    }
	
	// Update is called once per frame
	void Update ()
    {
        RaycastHit2D hit = Physics2D.Raycast(raycastPoint.position, Vector2.down, maxFallDistance, groundLayer);
        
        if(!hit || !hit.collider)
        {
            horizontal = -horizontal;
        }

        if(!dead)
        {
            Move();
        }
    }

    protected override void Dead()
    {
        anim.SetBool("Death", true);
        base.Dead();
        Jump();
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
