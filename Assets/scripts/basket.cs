using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class basket : MonoBehaviour
{
    [Header("Set Dynamically")]
    public scorecounter ScoreCounter; 
    public ApplePicker CurrentPicker; 

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
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos2D = Input.mousePosition;
        // The Camera's z position sets how far to push the mouse into 3D
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;
        
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
            if(CurrentPicker.Level==1)
            {
                //play ending 
                CurrentPicker.EasyEnding(); 
                //delete tree
                //play video 
            }
        }
    } 
}
