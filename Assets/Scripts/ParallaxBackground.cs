using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{

    public float bgWidth;
    public float bgHeight;
    public Player player;

    public int halfNumBgs = 1;

    public GameObject background;
    public List<GameObject> instantiatedBgs = new List<GameObject>();

    public float parallaxScale = 0.5f;
    public float verticalParallaxScale = 0.5f;
    public float zOffset;
    public float yOffset;

    // Use this for initialization
    void Start()
    {
        //Start with player position
        Vector3 startPos = player.transform.position;

        //Move left by half the number of backgrounds
        startPos -= halfNumBgs * bgWidth * Vector3.right;

        //Give z Offset
        startPos += zOffset * Vector3.forward;

        //Give y Offset
        startPos += yOffset * Vector3.forward;

        //Spawn 2n + 1 backgrounds so always have a middle background
        for (int i = 0; i < 2 * halfNumBgs + 1; i++)
        {
            Vector3 pos = startPos + i * bgWidth * Vector3.right;
            GameObject spawnedBg = Instantiate(background, pos, Quaternion.identity);
            instantiatedBgs.Add(spawnedBg);
            spawnedBg.transform.parent = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Check if player has crossed the right boundary of the middle background (instantiatedBgs[halfNumBgs])
        if (player.transform.position.x > instantiatedBgs[halfNumBgs].transform.position.x + bgWidth / 2)
        {
            GameObject firstBg = instantiatedBgs[0];
            instantiatedBgs.Remove(firstBg);
            firstBg.transform.position += bgWidth * (2 * halfNumBgs + 1) * Vector3.right;
            instantiatedBgs.Add(firstBg);
            //checkPoint1 += bgWidth * Vector3.right;
        }

        //Check if player has crossed the left boundary of the middle background (instantiatedBgs[halfNumBgs])
        if (player.transform.position.x < instantiatedBgs[halfNumBgs].transform.position.x + bgWidth / 2)
        {
            GameObject lastBg = instantiatedBgs[2 * halfNumBgs];
            instantiatedBgs.Remove(lastBg);
            lastBg.transform.position -= bgWidth * (2 * halfNumBgs + 1) * Vector3.right;
            instantiatedBgs.Insert(0, lastBg);
            //checkPoint1 += bgWidth * Vector3.right;
        }

        Vector3 offset = parallaxScale * player.GetHorizontalSpeed() * Vector3.right;
        transform.position += offset;

        //Vertical Scroll

        Vector3 offsetTwo = verticalParallaxScale * player.GetVerticalSpeed() * Vector3.up;
        transform.position += offsetTwo;
    }
}
