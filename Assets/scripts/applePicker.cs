using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplePicker : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject basketPrefab;
    public int numBaskets = 3;
    public float basketBottomY = -14f;
    public float basketSpacingY = 2f;
    public List<GameObject> basketList;
    public int Level =0; 
    string SceneName; 
    public AudioSource explosion;
    public AudioSource KirbExplosion; 
    public NewBehaviourScript appleTree; 
    public GameObject ExplosionPrefab; 


    // Start is called before the first frame update
    void Start()
    {
        SceneName=SceneManager.GetActiveScene().name;
        if(String.Compare(SceneName, "easy")==0)
        {
            Level=1; 
        }
        basketList = new List<GameObject>(); 
        for(int i=0; i<numBaskets; i++)
        {
            GameObject tBasketGO = Instantiate<GameObject>(basketPrefab);
            Vector3 pos = Vector2.zero;
            pos.y = basketBottomY + (basketSpacingY*i);
            tBasketGO.transform.position=pos; 
            basketList.Add(tBasketGO);
        }
        explosion=this.GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AppleDestroyed()
    {
        //returns array of all apples
        GameObject[] tAppleArray = GameObject.FindGameObjectsWithTag("apple");
        //destroy all apple in unity
        foreach(GameObject tGo in tAppleArray)
        {
                Destroy(tGo);
        }

        //destroy basket 
        int basketIndex = basketList.Count-1; 
        //get refrence to object 
        GameObject tBasketGo = basketList[basketIndex];
        // remove basket 
        basketList.RemoveAt(basketIndex);
        Destroy(tBasketGo);

        //if there are no baskets restart 
        if (basketList.Count ==0)
        {
            SceneManager.LoadScene("menu");
        }
    }

    //level 1 ending 
    public void EasyEnding()
    {
        GameObject explode = Instantiate(ExplosionPrefab);
        explode.transform.position=appleTree.transform.position;
        Destroy(appleTree.gameObject);
        explosion.Play(); 
        KirbExplosion.Play();
        Invoke("MainMenu", 3.0f);

    }

    //reload main menu 
    public void MainMenu()
    {
        Time.timeScale=1; 
        SceneManager.LoadScene("menu");
    }
}
