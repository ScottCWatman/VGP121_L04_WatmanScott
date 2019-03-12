using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public int layerToHit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == layerToHit)
        {
            if(collision.gameObject.GetComponent<Entity>())
            {
                collision.gameObject.GetComponent<Entity>().CheckHearts();
            }
        }
    }
}
