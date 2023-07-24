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
    private GameObject SpeechText;
    private GameObject ForceText;
    private GameObject TacticsText;
    [Header("Enemy")]
    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject Enemy3;

    void Start()
    {
        SpeechText = Status.transform.Find("SpeechText").gameObject;
        ForceText = Status.transform.Find("ForceText").gameObject;
        TacticsText = Status.transform.Find("TacticsText").gameObject;

        UpdateGameInfo();
    }

    void Update()
    {
        if (GameManager.instance.UITriger)
        {
            UpdateGameInfo();
            GameManager.instance.UITriger = false;
        }
    }

    public void StatusPopUp()
    {
        var y = Status.GetComponent<RectTransform>().anchoredPosition.y;
        if (y == -390) Status.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,500,0);
        else Status.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,-390,0);
    }

    public void UpdateGameInfo() 
    {
        UpdateFood();
        UpdateArmy();
        UpdateDay();
        UpdateStatus();
        UpdateEnemy();
    }

    

    public void UpdateFood()
    {
        int food = GameManager.instance.GetFood();
        food = food < 0 ? 0 : food;
        FoodText.GetComponent<TextMeshProUGUI>().text = $"식량 : {food}";
    }

    public void UpdateArmy()
    {
        int army = GameManager.instance.GetArmy();
        army = army < 0 ? 0 : army;
        ArmyText.GetComponent<TextMeshProUGUI>().text = $"병력 : {army}";
    }

    public void UpdateDay()
    {
        DayText.GetComponent<TextMeshProUGUI>().text = $"D+{GameManager.instance.GetDay()}";
    }

    public void UpdateStatus()
    {
        SpeechText.GetComponent<TextMeshProUGUI>().text = $"화술 : {GameManager.instance.GetSpeech()}";
        ForceText.GetComponent<TextMeshProUGUI>().text = $"무력 : {GameManager.instance.GetForce()}";
        TacticsText.GetComponent<TextMeshProUGUI>().text = $"전술 : {GameManager.instance.GetTactics()}";
    }

    public void UpdateEnemy()
    {
        Enemy1.GetComponent<TextMeshProUGUI>().text = "";
        Enemy2.GetComponent<TextMeshProUGUI>().text = "";
        Enemy3.GetComponent<TextMeshProUGUI>().text = "";

        Dictionary<string, Group> enemy = GameManager.instance.GetEnemy();
        int idx = 0;
        foreach (Group temp in enemy.Values)
        {
            if (temp.GetName() == "player") continue;
            string text = $"{temp.GetName()}\n식량 : {temp.GetFood()}\n병력 : {temp.GetArmy()}";
            switch (idx % 3)
            {
                case 0:
                    Enemy1.GetComponent<TextMeshProUGUI>().text = text;
                    break;
                case 1:
                    Enemy2.GetComponent<TextMeshProUGUI>().text =text;
                    break;
                case 2:
                    Enemy3.GetComponent<TextMeshProUGUI>().text =text;
                    break;
            }
            idx++;
        }
    }
}
