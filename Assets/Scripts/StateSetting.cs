using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class StateSetting : MonoBehaviour
{
    private GameController mainScript;
    private Transform[] objects;
    private GameObject stateText;
    private GameObject[] optionTexts = new GameObject[3];
    private Button[] optionBtns = new Button[3];

    void Awake() {
        objects = GetComponentsInChildren<Transform>();
        if(objects.Length==10)
        {
            stateText = objects[3].gameObject;
            optionTexts[0] = objects[5].gameObject;
            optionTexts[1] = objects[7].gameObject;
            optionTexts[2] = objects[9].gameObject;
        }else
        {
            stateText = objects[2].gameObject;
            optionTexts[0] = objects[4].gameObject;
            optionTexts[1] = objects[6].gameObject;
            optionTexts[2] = objects[8].gameObject;
        }
        mainScript = GameObject.Find("GameController").GetComponent<GameController>();
    }
    // Update is called once per frame
    public void SetStateText(string text, string[] optTexts)
    {
        stateText.GetComponent<TextMeshProUGUI>().text = text;
        optionTexts[0].GetComponent<TextMeshProUGUI>().text = optTexts[0];
        optionTexts[1].GetComponent<TextMeshProUGUI>().text = optTexts[1];
        optionTexts[2].GetComponent<TextMeshProUGUI>().text = optTexts[2];
    }

    public void ClickOption1()
    {
        mainScript.optionTrigers[0] = true;
    }
    
    public void ClickOption2()
    {
        mainScript.optionTrigers[1] = true;
    }
    
    public void ClickOption3()
    {
        mainScript.optionTrigers[2] = true;
    }
}
