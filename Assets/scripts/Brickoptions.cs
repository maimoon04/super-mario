using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brickoptions : MonoBehaviour
{

    public enum Objects
    {
        star,
        coin,
        Mushroom,
        flower,
        energyflower
    }

    public Objects getobjects;
    public GameObject player;
    public RuntimeAnimatorController checkanimator;
    public bool stop = false;
    

    // Start is called before the first frame update
    void Start()
    {
       

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name=="player" && collision.contacts[0].normal.y > 0.5f && !stop)
        {
            var temp = player.GetComponent<Animator>().runtimeAnimatorController; 
            switch (getobjects)
            {
                case Objects.star:
                    if (temp != checkanimator)
                    {
                        this.transform.GetChild(0).gameObject.SetActive(true);
                    }
                    else
                    {
                        this.transform.GetChild(2).gameObject.SetActive(true);
                    
                    }
                    stop = true;
                    break;
                case Objects.coin:
                    
                    this.transform.GetChild(1).gameObject.SetActive(true);
                    break;
                case Objects.Mushroom:

                    this.transform.GetChild(2).gameObject.SetActive(true);
                    stop = true;
                    break;
                case Objects.flower:
                    if (temp != checkanimator)
                    {
                        this.transform.GetChild(3).gameObject.SetActive(true);
                    }
                    else
                    {
                        this.transform.GetChild(2).gameObject.SetActive(true);
                    }
                    stop = true;
                    break;
                case Objects.energyflower:
                    if (temp != checkanimator)
                    {
                        this.transform.GetChild(4).gameObject.SetActive(true);
                    }
                    else
                    {
                        this.transform.GetChild(2).gameObject.SetActive(true);
                    }
                    stop = true;
                    break;
                
            }
            
            
        }

    }
}
