using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class scorecounter : MonoBehaviour
{
    public int score =0; 
    private TextMeshProUGUI uiText; 
    // Start is called before the first frame update
    void Start()
    {
        uiText = this.GetComponent<TextMeshProUGUI>(); 

    }

    // Update is called once per frame
    void Update()
    {
        uiText.text = score.ToString(); 
    }
}
