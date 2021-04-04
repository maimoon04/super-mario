using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    public enum enemy
    {
        basicenmey,
        turtle
    }
    public enemy enemy_;
    public  bool killed ;
    public int enemyspeed;
    public bool left, right;
      Rigidbody2D rig;
    Animator anim;
    bool isdead=false;
    public int turtlecount;
    // Start is called before the first frame update
    void Start()
    {
        killed = false;
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isdead)
           {
            if (right)
            {
                transform.position += new Vector3(-enemyspeed, 0, 0) * Time.deltaTime;
                if (enemy_ == enemy.turtle)
                {
                 transform.localScale = new Vector3(5, transform.localScale.y, transform.localScale.z);
                }
            }
            else if (left)
            {
                transform.position += new Vector3(enemyspeed, 0, 0) * Time.deltaTime;
                if (enemy_ == enemy.turtle)
                {
                    transform.localScale = new Vector3(-5, transform.localScale.y, transform.localScale.z);
                }
            }

            

        }
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemy_ == enemy.basicenmey)

        {
            if (collision.gameObject.name == "killenemy")
            {
                killed = true;
                isdead = true;
                anim.SetBool("Enemydeath", true);
                Debug.Log("die");
                collision.gameObject.transform.parent.gameObject.GetComponent<Animator>().SetBool("Jump", false);
                StartCoroutine(destroyenemy(0.5f));
            }
        }
        else
        {
            if (collision.gameObject.name == "killenemy")
            {
                killed = true;
                isdead = true;
                anim.SetBool("Enemydeath", true);
                Debug.Log("die");
                collision.gameObject.transform.parent.gameObject.GetComponent<Animator>().SetBool("Jump", false);
                StartCoroutine(turtlecourt());
            }
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
     //   Debug.Log(killed);
        if (left  && collision.gameObject.tag!="ground")
        {

            left = false;
            right = true;
        }
        else if (right && collision.gameObject.tag != "ground")
        {

            left = true;
            right = false;

        }
        if (turtlecount == 1 && collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(turtlecourt());
        }
        {

        }
        if (collision.gameObject.CompareTag("ball") || collision.gameObject.CompareTag("Invincible")
           || collision.gameObject.CompareTag("Turtlepower"))
        {
            killed = true;
            isdead = true;
            anim.SetBool("Enemydeath", true);
            this.transform.rotation = new Quaternion(0, 0, 180, 0);
            rig.velocity = new Vector2(rig.velocity.x, 5);
            GetComponent<Collider2D>().enabled = false;
            rig.constraints = RigidbodyConstraints2D.FreezePositionX;
            StartCoroutine(destroyenemy(2f));
        }
    }

    IEnumerator destroyenemy(float time)
    {
        this.gameObject.tag = "Untagged";
        score();
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
            
    }
    IEnumerator turtlecourt()
    {
        turtlecount++;
        yield return new WaitForSeconds(1f);
        if(turtlecount == 2)
        {
            isdead = false;
            this.gameObject.tag = "Turtlepower";
            yield return new WaitForSeconds(0.5f);
            killed = false;
            enemyspeed = 5;
            turtlecount = 0;
        }
    }

    void score()
    {
        ScoreManager.inst.score = true;
        PlayerPrefs.SetInt("Scorecount", PlayerPrefs.GetInt("Scorecount")+ 100);
        PlayerPrefs.Save();
    }

}
