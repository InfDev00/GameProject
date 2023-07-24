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
    [Header("Life")]
    public GameObject Life1;
    public GameObject Life2;
    public GameObject Life3;


    void Start()
    {
        SpeechText = Status.transform.Find("SpeechText").gameObject;
        ForceText = Status.transform.Find("ForceText").gameObject;
        TacticsText = Status.transform.Find("TacticsText").gameObject;

        UpdateFood();
        UpdateArmy();
        UpdateDay();
        UpdateStatus();
        UpdateEnemy();
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
        UpdateArmy();
        UpdateDay();
        UpdateStatus();
        UpdateEnemy();
        UpdateLife();
    }

    public void UpdateFood()
    {
        FoodText.GetComponent<TextMeshProUGUI>().text = $"�ķ� : {GameManager.instance.GetFood()}";
    }

    public void UpdateArmy()
    {
        ArmyText.GetComponent<TextMeshProUGUI>().text = $"���� : {GameManager.instance.GetArmy()}";
    }

    public void UpdateDay()
    {
        DayText.GetComponent<TextMeshProUGUI>().text = $"D+{GameManager.instance.GetDay()}";
    }

    public void UpdateStatus()
    {
        SpeechText.GetComponent<TextMeshProUGUI>().text = $"ȭ�� : {GameManager.instance.GetSpeech()}";
        ForceText.GetComponent<TextMeshProUGUI>().text = $"���� : {GameManager.instance.GetForce()}";
        TacticsText.GetComponent<TextMeshProUGUI>().text = $"���� : {GameManager.instance.GetTactics()}";
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
            string text = $"{temp.GetName()}\n�ķ� : {temp.GetFood()}\n���� : {temp.GetArmy()}";
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

    public void UpdateLife() 
    {
        Life1.SetActive(true);
        Life2.SetActive(true);
        Life3.SetActive(true);
        switch (GameManager.instance.GetLife())
        {
            case 3:
                break;
            case 2:
                Life1.SetActive(false);
                break;
            case 1:
                Life1.SetActive(false);
                Life2.SetActive(false);
                break;
        }
    }
}
