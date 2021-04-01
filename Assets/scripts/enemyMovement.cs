using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    public GenericMovement move;
    public  bool killed ;
    public int enemyspeed;
    public bool left, right;
      Rigidbody2D rig;
    Animator anim;
    bool isdead=false;
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
            }
            if (left)
            {
                transform.position += new Vector3(enemyspeed, 0, 0) * Time.deltaTime;
            }

        }
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name == "killenemy")
        {
            killed = true;
            isdead = true;
            anim.SetBool("Enemydeath", true);
            Debug.Log("die");
            StartCoroutine(destroyenemy(0.5f));

        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
     //   Debug.Log(killed);
        if (left)
        {

            left = false;
            right = true;
        }
        else if (right)
        {

            left = true;
            right = false;

        }
        if (collision.gameObject.CompareTag("ball") || collision.gameObject.CompareTag("Invincible") )
        {
            killed = true;
            isdead = true;
            anim.SetBool("Enemydeath", true);
            this.transform.rotation = new Quaternion(0, 0, 180, 0);
            rig.velocity = new Vector2(rig.velocity.x, 5);
            this.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(destroyenemy(2f));
        }
    }

    IEnumerator destroyenemy(float time)
    {
        this.gameObject.tag = "Untagged";
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
            
    }

}
