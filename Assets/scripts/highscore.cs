using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class highscore : MonoBehaviour
{
    static public int score = 1000; 

    //awake 
    void Awake()
    {
        //if highscore exsisits read it 
        if(PlayerPrefs.HasKey("HighScore"))
        {
            score=PlayerPrefs.GetInt("Highscore");  
        }
        //set score to highscore 
        PlayerPrefs.SetInt("Highscore", score);
    }

    //update 
    void update()
    {
        Text gt = this.GetComponent<Text>();
        gt.text = "High Score: "+score;
        //update preferences if required
        if(score> PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TextMeshProUGUI gt = this.GetComponent<TextMeshProUGUI>(); 
        gt.text = "High Score: "+ score; 
    }
}
