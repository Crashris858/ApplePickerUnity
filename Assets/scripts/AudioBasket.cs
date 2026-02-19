using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
public class Audiobasket : MonoBehaviour
{
    [Header("Set Dynamically")]
    public scorecounter ScoreCounter; 
    public ApplePicker CurrentPicker; 
    public float MinMovement=-26; 
    public float MaxMovement=26;
    public float MinSpeed = 2f;
    public float MaxSpeed= 18f; 
    public float neagtiveSpeed = 18f; 
    public audioloudness detector;  
    public float loudnessScale = 1.0f; 
    public float ThresholdLower = 0.5f; 
    public float ThresholdUpper =0.75f;


    // Start is called before the first frame update
    void Start()
    {
        //handle the counter logic 
        //pull score counter 
        GameObject ScoreGo = GameObject.Find("ScoreCounter");
        //get text component and assign to scoregt 
        ScoreCounter = ScoreGo.GetComponent<scorecounter>(); 
        //pull scene manager 
        CurrentPicker=GameObject.Find("Main Camera").GetComponent<ApplePicker>();
        //pull detector 
        detector=GameObject.Find("audioLoudness").GetComponent<audioloudness>(); 
    }


    // Update is called once per frame
    void Update()
    {
        //get the loudness from audio clip 
        float loudness = detector.GetLoudnessFromMicrophone()*loudnessScale;
        //transform basket position  
        Vector3 Pos=this.transform.position; 

        //handle minimal noises and neagtive direction
        if (loudness<ThresholdLower)
        {
            Pos.x = Mathf.Clamp(Pos.x-(neagtiveSpeed*Time.deltaTime), MinMovement, MaxMovement);  
        }
        //handle postive direction
        else
        {
            //caluclate direction 
            float dir = Mathf.Lerp(MinSpeed, MaxSpeed, loudness); 
            //clamp
            Pos.x = Mathf.Clamp(Pos.x+(dir*Time.deltaTime), MinMovement, MaxMovement);  
        }
        //apply movement 
        this.transform.position=Pos; 
    }

    void OnCollisionEnter (Collision coll)
    {
        //destroy apple 
        GameObject collidedWith = coll.gameObject;
        if(collidedWith.tag=="apple")
        {
            Destroy(collidedWith);
            //turn score into int
            ScoreCounter.score+=100; 

            //track high score 
            if (ScoreCounter.score>highscore.score)
            {
                highscore.score=ScoreCounter.score;
            }
        }
    } 
}
