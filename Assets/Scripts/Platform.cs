using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    public int rows = 2;
    public int columns = 5;
    public float tileWidth = 1;
    public float tileHeight = 1;
    public GameObject tileObjectTopLeft;
    public GameObject tileObjectTopMiddle;
    public GameObject tileObjectTopRight;
    public GameObject tileObjectMiddleLeft;
    public GameObject tileObjectMiddleMiddle;
    public GameObject tileObjectMiddleRight;
    public GameObject tileObjectBottomLeft;
    public GameObject tileObjectBottomMiddle;
    public GameObject tileObjectBottomRight;
    public float scalingFactor = 1;

	// Use this for initialization
	void Start ()
    {
        //GeneratePlatform();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    public void DestroyExistingTiles()
    {
        foreach (Transform t in this.transform.GetComponentInChildren<Transform>())
        {
            Destroy(t.gameObject);
        }
    }
    public void GeneratePlatform()
    {
        GenerateCollider();
        for(int i = 0; i < rows; i++)
        {
            for(int j = 0; j < columns; j++)
            {
                float x = tileWidth / 2 + j * tileWidth;
                float y = tileHeight / 2 + i * tileHeight;
                Vector3 tilePosition = transform.position + new Vector3(x, y, 0);
                GameObject tileToSpawn;
                if(j == 0)
                {
                    if(i == rows - 1)
                    {
                        tileToSpawn = tileObjectTopLeft;
                    }
                    else if(i == 0)
                    {
                        tileToSpawn = tileObjectBottomLeft;
                    }
                    else
                    {
                        tileToSpawn = tileObjectMiddleLeft;
                    }
                }
                else if(j == columns - 1)
                {
                    if (i == rows - 1)
                    {
                        tileToSpawn = tileObjectTopRight;
                    }
                    else if (i == 0)
                    {
                        tileToSpawn = tileObjectBottomRight;
                    }
                    else
                    {
                        tileToSpawn = tileObjectMiddleRight;
                    }
                }
                else
                {
                    if (i == rows - 1)
                    {
                        tileToSpawn = tileObjectTopMiddle;
                    }
                    else if (i == 0)
                    {
                        tileToSpawn = tileObjectBottomMiddle;
                    }
                    else
                    {
                        tileToSpawn = tileObjectMiddleMiddle;
                    }
                }
                GameObject instantiatedPlatform = Instantiate(tileToSpawn, tilePosition, Quaternion.identity);
                instantiatedPlatform.transform.parent = this.transform;
            }
        }
    }

    void GenerateCollider()
    {
        if(!GetComponent<BoxCollider2D>())
        {
            gameObject.AddComponent<BoxCollider2D>();
        }

        BoxCollider2D myCollider = GetComponent<BoxCollider2D>();
        myCollider.size = new Vector2(columns * tileWidth, rows * tileHeight) / scalingFactor;
        myCollider.offset = new Vector2(columns * tileWidth, rows * tileHeight) / 2;
    }
}
