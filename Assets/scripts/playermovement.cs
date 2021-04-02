﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class playermovement : MonoBehaviour
{
    
    public float speed;
    Rigidbody2D rigbody;
    Animator anim;
    private SpriteRenderer sprite_rend;
    static int scorecount;
    bool IsGrounded,_dead;
    public GameObject breakable;
    //bool to make mario big
    [Header("to make mario big")]
    public bool bigMario = false,redmario=false;
    public Sprite bigmariosprite;
    public RuntimeAnimatorController bigmarioanim, tempanim,bigredmarioanim, greenAnimator;
    bool running;
    [Header("throwable")]
    bool throwable = false;
    public GameObject prefabBall;
    delegate void  mydelegate(bool check);
    mydelegate Mydelegate;
    BoxCollider2D box;
    public int count;
    int lives = 3;
    public bool jumping=false,jumpcancel=false;
    //random variables
    float tempspeed;
    bool moveright = true;
    private void Awake()
    {
       // PlayerPrefs.SetInt("Lives", 3);
        if (PlayerPrefs.GetInt("Lives") != 0)
        {
            lives = PlayerPrefs.GetInt("Lives");
        }
        else
        {
            PlayerPrefs.SetInt("Lives", lives);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
        _dead = false;
        IsGrounded = false;
        rigbody = GetComponent<Rigidbody2D>();
        sprite_rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        tempanim = anim.runtimeAnimatorController;
        Mydelegate = changesprite;
        box = GetComponent<BoxCollider2D>();
        scorecount = 0;
        running = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            thorwable_object();
        }
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
            {
            jumping = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space) && !IsGrounded)
        {
            jumpcancel = true;
        }
        if (running)
        {
            var tempcampos = Camera.main.transform.position;
            tempcampos.x += 6.32f;
            if (transform.position.x >= tempcampos.x)
            {
                moveright = false;
            }
           
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        PlayerMovement();

    }

    void PlayerMovement()
    {
        if (!_dead || bigMario)
        {

        
         if (Input.GetKey(KeyCode.RightArrow) && moveright)
         {
           transform.position += new Vector3(speed,0);
           Vector2 localscale = new Vector2(-transform.localScale.x, transform.localScale.y);
           cameraFollow.checkcam = false;
           running = true;
           anim.SetBool("Runleft", true);
            Mydelegate(true);

          }
         else if (Input.GetKey(KeyCode.LeftArrow))
         {
                moveright = true;
                transform.position += new Vector3(-speed, 0);
                anim.SetBool("Runleft", true);
                running = false;
                Mydelegate(false);
          }
         else
        {
                running = false;
           anim.SetBool("Runleft", false);
          }
           
         if (jumping )
         {
                float tempspeed = speed;
                speed = 0.05f;
               /* rigbody.AddForce(Vector3.up * 1000f*Time.deltaTime)*/;
                rigbody.velocity = new Vector2(rigbody.velocity.x, 8.7f);
                jumping = false;
                IsGrounded = false;
        
          }
            else if (jumpcancel)
            {
                if (rigbody.velocity.y > 3)
                    rigbody.velocity = new Vector2(rigbody.velocity.x, 3f);
                jumpcancel = false;
                speed = 0.089f;
            }

        }
    }

    void thorwable_object()
    {
        if (throwable)
        {
            var obj = this.transform.GetChild(1);
            var temp = Instantiate(prefabBall, obj.transform.position, Quaternion.identity);
        Debug.Log(temp.name);
        if (IsGrounded) { temp.GetComponent<Rigidbody2D>().AddForce(new Vector2(300f, 200f)); }
        else { temp.GetComponent<Rigidbody2D>().AddForce(new Vector2(-300f, -200f)); }
        
        }
    }
    void changesprite(bool check)
    {
        if (check)
        {
            Vector2 localscale= new Vector2(-4.942672f, 4.942672f);
            gameObject.transform.localScale = localscale;
            
        }
        if (!check)
        {
            Vector2 localscale = new Vector2(4.942672f, 4.942672f);
            gameObject.transform.localScale = localscale;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground") || collision.gameObject.CompareTag("hurdle"))
        {
            IsGrounded = true;
        }
        
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Turtlepower"))
        {
            var temp = collision.gameObject.GetComponent<enemyMovement>();
            if (!temp.killed && !bigMario)
            {
                dead();
                _dead = true;
            }
            if (bigMario)
            {
                if(transform.position.y< -3.028f)
                {
                    
                    StartCoroutine(blink());
                    GetComponent<Animator>().runtimeAnimatorController = tempanim;
                    box.offset = new Vector2(0, 0.00321893f);
                    box.size = new Vector2(box.size.x, 0.1707298f);
                    this.transform.GetChild(0).gameObject.transform.localPosition = new Vector2(0, -0.0f);
                    bigMario = false;
                    redmario = false;
                }
                
            }
            
        }
        if (collision.gameObject.CompareTag("Mushroom"))
        {
            StartCoroutine(blink());
            score(1000);
            redmario = true;
                bigMario = true;
                gameObject.GetComponent<SpriteRenderer>().sprite = bigmariosprite;
                GetComponent<Animator>().runtimeAnimatorController = bigmarioanim;
            box.offset = new Vector2(0, -0.03f);
             box.size = new Vector2(box.size.x, 0.2410522f);
            this.transform.GetChild(0).gameObject.transform.localPosition= new Vector2(0, -0.083f);
        }


        if (collision.gameObject.CompareTag("flower"))
        {
            score(1000);
            StartCoroutine(blink());
            if (redmario)
            {
                GetComponent<Animator>().runtimeAnimatorController = bigredmarioanim;
            }
            else
            {
                redmario = true;
                bigMario = true;
                gameObject.GetComponent<SpriteRenderer>().sprite = bigmariosprite;
                GetComponent<Animator>().runtimeAnimatorController = bigmarioanim;
            }
            box.offset = new Vector2(0, -0.03f);
            box.size = new Vector2(box.size.x, 0.2410522f);
            this.transform.GetChild(0).gameObject.transform.localPosition = new Vector2(0, -0.083f);
        }
        if (collision.gameObject.CompareTag("star"))
        {
            throwable = true;
            score(1000);
        }

        if (collision.gameObject.CompareTag("energyflower"))
        {
            this.gameObject.tag = "Invincible";
            score(1000);
            StartCoroutine(blinkInvincible());

        }

    }


    void dead()
    {
        anim.SetBool("Isdead", true);
        IsGrounded = false;
        lives--;
        this.transform.GetChild(0).gameObject.SetActive(false);
        rigbody.velocity = new Vector2(rigbody.velocity.x, 5);
        GetComponent<Collider2D>().enabled = false;
        rigbody.constraints = RigidbodyConstraints2D.FreezePositionX;
        if (lives != 0)
        {
            PlayerPrefs.SetInt("Lives", lives);
            SceneManager.LoadScene(0);
        }
        else
        {
            PlayerPrefs.SetInt("Lives", 0);
        }
    }


    IEnumerator blink()
    {
        yield return new WaitForSeconds(0.1f);
       
        while (count!=7)
        {
            Debug.Log("fddd");
            //co.a = 0f;
            //sprite_rend.color=co;
            sprite_rend.enabled =!sprite_rend.enabled;
            count += 1;
            yield return new WaitForSeconds(0.1f);
             }
        sprite_rend.enabled = true;
        count = 0;
    }

    IEnumerator blinkInvincible()
    {
        var tempanim = GetComponent<Animator>().runtimeAnimatorController;
        GetComponent<Animator>().runtimeAnimatorController = greenAnimator;
        float temp = 0;
        yield return new WaitForSeconds(0.1f);
        while (temp != 100)
        {
            sprite_rend.enabled = !sprite_rend.enabled;
            temp += 1;
            yield return new WaitForSeconds(0.1f);
        }
        sprite_rend.enabled = true;
        this.gameObject.tag = "Player";
        GetComponent<Animator>().runtimeAnimatorController = tempanim;
    }

    void score(int value)
    {
       
        
        
           
           ScoreManager.inst.score = true;
            scorecount += value;
            PlayerPrefs.SetInt("Scorecount", scorecount);
            PlayerPrefs.Save();
          
        
        
    }
}
