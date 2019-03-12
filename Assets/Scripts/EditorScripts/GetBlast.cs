using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBlast : MonoBehaviour
{
    public int layerToCheck;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == layerToCheck)
        {
            if (collision.gameObject.GetComponent<Entity>())
            {
                Entity entity = collision.gameObject.GetComponent<Entity>();           
                entity.GetBlast();
                Destroy(gameObject);
            }
        }
    }
}
