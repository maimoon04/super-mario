using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakobject : MonoBehaviour
{
    ParticleSystem p;
    SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        p = GetComponent<ParticleSystem>();
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player" && collision.contacts[0].normal.y>0.5f)
        {
            p.Play();
            rend.enabled = false;
            StartCoroutine(des());
        }
    }

    IEnumerator des()
    {
        yield return new WaitForSeconds(p.main.startLifetime.constantMax);
        Destroy(this.gameObject);
    }
}
