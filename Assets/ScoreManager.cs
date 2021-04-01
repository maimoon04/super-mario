using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager inst;
    public Text cointext;
    [HideInInspector]
    public bool coins;

    public Text Scoretext;
    [HideInInspector]
    public bool score;

    public Text lives;

    private void Awake()
    {
        
        if (inst != null)
        {
            return;
        }
        else
        {
            inst = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        score = false;
        coins = false;
        cointext.text = "Coin:0";
        Scoretext.text = "Score:0";
        lives.text = "lives: " + PlayerPrefs.GetInt("Lives");
    }

    // Update is called once per frame
    void Update()
    {
        if (coins)
        {
            cointext.text = "Coin:" + PlayerPrefs.GetInt("coincount").ToString();
            coins = false;
        }
        if(score)
        {
            Scoretext.text = "Score" + PlayerPrefs.GetInt("Scorecount").ToString();
            score = false;
        }
    }

  
}
