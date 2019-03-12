using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Entity
{ 
    public int maxJumps = 2;
    public bool isAirborne;
    public Vector3 startPos;

    public float invincibleTime = 1;

    public bool playShot = false;
   
    // Use this for initialization
	public override void Start()
    {
        startPos = transform.position;
        base.Start();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Move();

        anim.SetBool("Idle", Mathf.Abs(horizontal) < 0.1f);

        if (Input.GetButtonDown("Jump") && jumpCount < 2)
        {
            Jump();
        }

        if(playShot == true)
        {
            if (Input.GetButtonDown("Blast"))
            {
                fireBlast();
            }
        }

        if(playShot == false)
        {
            UIBlastIcon.SetActive(false);
        }
    }

    public override void Move()
    {
        horizontal = Input.GetAxis("Horizontal");
        base.Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
        isAirborne = false;
        jumpCount = 0;
        anim.SetBool("Jump", false);

        if(collision.gameObject.layer == enemyLayer)
        {
            OnEnemyHit(collision.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isAirborne = true;
        jumpCount = 1;
    }

    protected override void Dead()
    {
        base.Dead();
        //transform.position = startPos;
        SceneManager.LoadScene(0);
    }

    public override void OnEnemyHit(GameObject enemy)
    {
        base.OnEnemyHit(enemy);

        float playerHeight = GetComponent<BoxCollider2D>().size.y * transform.localScale.y;
        float enemyHeight = enemy.GetComponent<BoxCollider2D>().size.y * enemy.transform.localScale.y;

        if(transform.position.y - enemy.transform.position.y >= playerHeight / 2 + enemyHeight / 2)
        {
            enemy.GetComponent<Entity>().hitDirection = Mathf.Sign(enemy.transform.position.x - transform.position.x);
            enemy.GetComponent<Entity>().CheckHearts(); //kill enemy
        }
        else
        {
            if(dying)
            {
                src.PlayOneShot(dying);
            }
            hitDirection = -Mathf.Sign(enemy.transform.position.x - transform.position.x);
            StartCoroutine("MakeInvincible");
            CheckHearts(); //kill player
        }
    }

    IEnumerator MakeInvincible()
    {
        gameObject.layer = 11;
        StartCoroutine("Flash");
        yield return new WaitForSeconds(invincibleTime);
        gameObject.layer = 8;
        StopCoroutine("Flash");
    }

    IEnumerator Flash()
    {
        bool toggle = false;
        while(true)
        {
            anim.GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(0.2f);
            toggle = true;
        }
    }

    //Allows the player to shoot for 5 seconds
    IEnumerator BlastPowerUp()
    {
        playShot = true;
        while (playShot == true)
        {
            UIBlastIcon.SetActive(true);
            yield return new WaitForSeconds(5f);
            playShot = false;
        }
    }
}