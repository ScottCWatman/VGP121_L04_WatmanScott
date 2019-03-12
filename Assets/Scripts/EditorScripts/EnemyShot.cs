using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : Entity
{
    public LayerMask playerLayer;
    public float range = 5;
    public float shootTime;
    public bool shooting;

    Enemy me;

	// Use this for initialization
	void Start ()
    {
        me = GetComponent<Enemy>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        RaycastHit2D playerDetected = Physics2D.Raycast(transform.position, transform.right, range, playerLayer);
        if(playerDetected && playerDetected.collider)
        {
            if (!shooting)
            {
                StartCoroutine("Shoot");
                me.horizontal = 0;
                shooting = true;
                if(boom)
                {
                    src.PlayOneShot(boom);
                }
            }
        }
        else if (shooting)
        {
            StopCoroutine("Shoot");
            me.horizontal = 1;
            shooting = false;
        }
	}

    IEnumerator Shoot()
    {
        while(true)
        {
            me.fireBlast();
            yield return new WaitForSeconds(shootTime);
        }
    }
}
