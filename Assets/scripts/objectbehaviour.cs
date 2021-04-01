using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectbehaviour : MonoBehaviour
{
    public enum Objects
    {
        star,
        coin,
        Mushroom,
        flower,
        energyflower
    }
    public bool left, right;
    public Objects name;
    bool coin = false;
    public bool multiplecoins;
    Vector2 coindist, temp;
    bool active;
    bool mushroom, mushroomactive;
    // Start is called before the first frame update
    private void OnEnable()
    {
        func();
    }

    // Update is called once per frame
    void Update()
    {
        if (coin)
        {
            this.transform.position = Vector2.Lerp(transform.position, coindist, Time.deltaTime * 10f);
            StartCoroutine(coins(0.2f));
        }
        if (mushroomactive)
        {
            if (mushroom)
            {
                this.transform.position = Vector2.Lerp(transform.position, coindist, Time.deltaTime * 2f);
                StartCoroutine(mushrooms());
            }
            else if (!mushroom)
            {
                if (right)
                {
                    this.transform.position += new Vector3(-0.03f, 0, 0);
                }
                if (left)
                {
                    this.transform.position += new Vector3(0.03f, 0, 0);
                }

            }
        }
            if (active)
            {
                this.transform.position = Vector2.Lerp(transform.position, coindist, Time.deltaTime * 10f);
                StartCoroutine(active_());
            }

        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (mushroomactive && collision.gameObject.tag!="ground")
        {
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
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }

    void func()
    {
        switch (name)
        {
            case Objects.coin:
                temp = transform.position;
                coindist = transform.position;
                coindist.y += 4;
                coin = true;
                break;
            case Objects.Mushroom:
                mushroomactive = true;
                mushroom = true;
                coindist = transform.position;
                coindist.y += 1;
                break;
            case Objects.star:
                mushroomactive = true;
                mushroom = true;
                coindist = transform.position;
                coindist.y += 1;
                break;
            case Objects.flower:
                active = true;
                coindist = transform.position;
                coindist.y += 1;
                break;
            case Objects.energyflower:
                active = true;
                coindist = transform.position;
                coindist.y += 1;
                break;
        }
    }


    IEnumerator coins(float time)
    {
        yield return new WaitForSeconds(time);
        coin = false;
        if (multiplecoins)
        {
            transform.position = temp;
            this.gameObject.SetActive(false);
        }
        else
        {
            Destroy(this.transform.parent.gameObject);
        }

    }

    IEnumerator mushrooms()
    {
        yield return new WaitForSeconds(0.2f);
        mushroom = false;
        this.gameObject.transform.parent = null;
      
    }
    IEnumerator active_()
    {
        yield return new WaitForSeconds(0.2f);
        active = false;
       

    }
}




