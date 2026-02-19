using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class pibble : MonoBehaviour
{

    //pibble comfort seconds
    float comfortTimeMax = 1.5f;  
    //pibble speed 
    public float pibSpeed = 8f; 
    public float maxPosY = 4.0f;
    //state flags
    bool comforted, running, waiting; 
    public AudioSource yay; 
    public AudioSource washMyBelly; 
    private float comfortTimer=0.0f; 
    public GameObject appleTarget; 
    
    // Start is called before the first frame update
    void Start()
    {
        //play audio 
        washMyBelly.Play(); 
        changeStateRunning(); 
        getAppleTarget();
    }

    // Update is called once per frame
    void Update()
    {
        //state flags
        //wait state 
        if (waiting)
        {
            getAppleTarget();
            return;
        }
        
        //if comforted do nothing 
        if(comforted)
        {
            return; 
        }
        //catch apple desturction 
        if(appleTarget==null)
        {
            getAppleTarget(); 
        }
        //running state: chase apple 
        if(running)
        {
            Debug.Log("getting Apple Target");
            //check current apple is above basket 
            if(appleTarget.transform.position.y<-3.0f
                || appleTarget.transform.position.y>4.0f)
            {
                //find new apple 
                getAppleTarget(); 
            }
            Debug.Log("moving");
            //move toward the target apple 
            Vector3 pos = this.transform.position; 
            Vector3 applePos = appleTarget.transform.position;
            //handy dandy movetowards function
            pos=Vector3.MoveTowards(pos, applePos, pibSpeed*Time.deltaTime);

            //check for y position
            if(transform.position.y>maxPosY)
            {
                pos.y=maxPosY; 
            }
            
            //move
            this.transform.position=pos; 

        }
        
        //get mouse position 
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        //if pibble is being washed 
        if(mousePos3D.x<=transform.position.x+2&&
           mousePos3D.x>=transform.position.x-2&&
           mousePos3D.y<=transform.position.y+2&&
           mousePos3D.y>=transform.position.y-2)
        {
            Debug.Log("entered comfort state");
            //comfort pibble 
            comfortTimer+=Time.deltaTime;
            Debug.Log("pibble calm time"+ comfortTimer);
            if(comfortTimer>=comfortTimeMax)
            {
                Debug.Log("pibble is calm");
                //enter comforted state
                changeStateCalm();
                Invoke("changeStateRunning", 3.5f);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "apple")
        {
            //delete current apple 
            Destroy(collision.gameObject);
            //play yay audio
            yay.Play(); 
            //find closest apple if currenttly not waiting 
            if(!waiting)
            {
                getAppleTarget();   
            }
            changeStateWait();
            Invoke("changeStateWaitEnd", 2.0f);

        }       
    }

    //func: change state to running
    public void changeStateRunning()
    {
        comforted=false;
        running=true;

    }

    //func: change state to calm 
    public void changeStateCalm()
    {
        comfortTimer=0.0f; 
        comforted=true;
        running=false; 
    }

    //func: change state to wait 
    public void changeStateWait()
    {
        waiting=true; 
    }

    public void changeStateWaitEnd()
    {
        waiting=false;
    }

    //func: get apple Target 
    public void getAppleTarget()
    {
        //create list of apple 
        GameObject[] tAppleArray = GameObject.FindGameObjectsWithTag("apple");
        //check therre are apples 
        if (tAppleArray.Length==0)
        {
            waiting=true; 
            return; 
        }
        GameObject closestApple = tAppleArray[0];
        //destroy all apple in unity
        foreach(GameObject tGo in tAppleArray)
        {
            float tGo_mag = (tGo.transform.position-this.transform.position).magnitude;
            float closestApple_mag = (closestApple.transform.position-this.transform.position).magnitude;
            //find closest apple to pibble 
            if(tGo_mag <= closestApple_mag && tGo.transform.position.y>-3.0f
                && tGo.transform.position.y<3.0f)
            {
                closestApple=tGo;
            }
        }
        //if waiting, unpause 
        if (waiting)
        {
            waiting=!waiting;
        }
        //set target 
        appleTarget=closestApple; 
    }
}
