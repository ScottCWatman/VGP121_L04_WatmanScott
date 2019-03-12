using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float speed;
    public float horizontal = 1;
    public float vertical = 1;

    public Animator anim;
    public Rigidbody2D rb;

    public int enemyLayer;

    public AudioSource src;
    public AudioClip laserShot;
    public AudioClip jumper;
    public AudioClip dying;
    public AudioClip boom;

    //Jump
    public float jumpForce = 100;
    public int jumpCount = 0;
    protected bool isGrounded;

    //Shot
    public GameObject Blast;
    public float blastForce = 10;
    public Transform spawnPoint;

    //Death
    public bool dead = false;

    float horizontalSpeed;
    float verticalSpeed;

    //Hearts
    public int maxHearts = 3;
    public int currentHearts;
    public List<GameObject> UIHeartIcons = new List<GameObject>();

    //Bump Force
    public float bumpForceUp = 100;
    public float bumpForceHorizontal = 200;
    public float hitDirection = 1;

    //Blast PowerUp
    public GameObject UIBlastIcon;

    // Use this for initialization
    public virtual void Start ()
    {
        currentHearts = maxHearts;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

     public float GetHorizontalSpeed()
    {
        return horizontalSpeed;
    }

    public float GetVerticalSpeed()
    {
        return verticalSpeed;
    }

    public virtual void Move()
    {
        horizontalSpeed = speed * Time.deltaTime * horizontal;
        transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime * horizontal;

        if (horizontal > 0.1f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);

        }
        else if (horizontal < -0.1f)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        verticalSpeed = Time.deltaTime * rb.velocity.y;

    }

    public virtual void Jump()
    {
        isGrounded = false;
        anim.SetBool("Jump", true);
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(new Vector2(0, 1) * jumpForce, ForceMode2D.Impulse);
        jumpCount++;
        if(jumper)
        {
            src.PlayOneShot(jumper);
        }
    }

    public void CheckHearts()
    {
        currentHearts--;
        if (currentHearts > 0)
        {
            if(UIHeartIcons.Count > 0)
            {
                rb.AddForce(new Vector3(hitDirection * bumpForceHorizontal, bumpForceUp));
                UIHeartIcons[currentHearts].SetActive(false);
            }
        }
        else if (currentHearts == 0)
        {
            Dead();
        }
    }

    public bool IsMaxHealth()
    {
        return currentHearts == maxHearts;
    }

    public void AddHeart()
    {
        if(currentHearts < maxHearts)
        {
            UIHeartIcons[currentHearts].SetActive(true);
            currentHearts++;
        }
    }

    public void GetBlast()
    {
        StartCoroutine("BlastPowerUp");
    }

    protected virtual void Dead()
    {
        dead = true;
    }

    public virtual void fireBlast()
    {
        GameObject instantiatedBlast = Instantiate(Blast, spawnPoint.position, Quaternion.identity);
        instantiatedBlast.GetComponent<Rigidbody2D>().AddForce(blastForce * transform.right, ForceMode2D.Impulse);
        if (laserShot)
        {
            src.PlayOneShot(laserShot);
        }
    }

    public virtual void OnEnemyHit(GameObject enemy)
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        jumpCount = 0;
        isGrounded = true;

        if(collision.gameObject.layer == enemyLayer)
        {
            OnEnemyHit(collision.gameObject);
        }
    }
}
