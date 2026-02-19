using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Button_manager : MonoBehaviour
{
    public Button Button_easy;
    public Button Button_medium;
    public Button Button_hard; 
    // Start is called before the first frame update
    void Start()
    {
        //pull buttons
        Button_easy= GameObject.Find("easy").GetComponent<Button>(); 
        Button_medium=GameObject.Find("medium").GetComponent<Button>();
        Button_hard=GameObject.Find("hard").GetComponent<Button>();
        //listeners
        Button_easy.onClick.AddListener(Easy_Clicked);
        Button_medium.onClick.AddListener(Medium_Clicked);
        Button_hard.onClick.AddListener(Hard_Clicked);
    }

    void Easy_Clicked()
    {
        //load easy scene 
        SceneManager.LoadScene("easy");
    }

        void Medium_Clicked()
    {
        //load medium scene 
        SceneManager.LoadScene("medium");
    }

        void Hard_Clicked()
    {
        //load hard scene 
        SceneManager.LoadScene("hard");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
