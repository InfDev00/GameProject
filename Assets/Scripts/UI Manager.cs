using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Food")]
    public GameObject FoodText;
    [Header("Army")]
    public GameObject ArmyText;
    [Header("Day")]
    public GameObject DayText;
    [Header("Status")]
    public GameObject Status;
    private GameObject LevelText;
    private GameObject JobText;
    [Header("Skill")]
    private GameObject Skill1;
    private GameObject Skill2;
    private GameObject Skill3;
    [Header("Item")]
    private GameObject Item1;
    private GameObject Item2;
    private GameObject Item3;



    void Start()
    {
        LevelText = Status.transform.Find("LevelText").gameObject;
        JobText = Status.transform.Find("JobText").gameObject;

        Skill1 = Status.transform.Find("Skill1").gameObject;
        Skill2 = Status.transform.Find("Skill2").gameObject;
        Skill3 = Status.transform.Find("Skill3").gameObject;

        Item1 = Status.transform.Find("Item1").gameObject;
        Item2 = Status.transform.Find("Item2").gameObject;
        Item3 = Status.transform.Find("Item3").gameObject;

        UpdateFood();
        UpdateGold();
        UpdateDay();
        UpdateStatus();
        UpdateItem();
    }

    public void StatusPopUp()
    {
        var y = Status.GetComponent<RectTransform>().anchoredPosition.y;
        if (y == -390) Status.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,500,0);
        else Status.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,-390,0);
    }

    void Update() 
    {
        UpdateFood();
        UpdateGold();
        UpdateDay();
        UpdateStatus();
        UpdateItem();
    }

    public void UpdateFood()
    {
        FoodText.GetComponent<TextMeshProUGUI>().text = $"½Ä·® : {GameManager.instance.GetFood()}";
    }

    public void UpdateGold()
    {
        ArmyText.GetComponent<TextMeshProUGUI>().text = $"°ñµå : {GameManager.instance.GetGold()}";
    }

    public void UpdateDay()
    {
        DayText.GetComponent<TextMeshProUGUI>().text = $"D+{GameManager.instance.GetDay()}";
    }

    public void UpdateStatus()
    {
        LevelText.GetComponent<TextMeshProUGUI>().text = $"Level : {GameManager.instance.GetLevel()}";
        JobText.GetComponent<TextMeshProUGUI>().text = GameManager.instance.GetJob();
    }

    public void UpdateItem()
    {
        Item1.GetComponent<TextMeshProUGUI>().text = "";
        Item2.GetComponent<TextMeshProUGUI>().text = "";
        Item3.GetComponent<TextMeshProUGUI>().text = "";

        List<string> item = GameManager.instance.GetItem();
        int idx = 0;
        foreach (string itemName in item)
        {
            switch (idx % 3)
            {
                case 0:
                    Item1.GetComponent<TextMeshProUGUI>().text += itemName;
                    break;
                case 1:
                    Item2.GetComponent<TextMeshProUGUI>().text +=itemName;
                    break;
                case 2:
                    Item3.GetComponent<TextMeshProUGUI>().text +=itemName;
                    break;
            }
        }
    }
}
