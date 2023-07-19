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
    [Header("Team")]
    public GameObject Team1;
    public GameObject Team2;
    public GameObject Team3;

    void Start()
    {
        SpeechText = Status.transform.Find("SpeechText").gameObject;
        ForceText = Status.transform.Find("ForceText").gameObject;
        TacticsText = Status.transform.Find("TacticsText").gameObject;

        UpdateFood();
        UpdateArmy();
        UpdateDay();
        UpdateStatus();
        UpdateTeam();
    }

    public void StatusPopUp()
    {
        var y = Status.GetComponent<RectTransform>().anchoredPosition.y;
        if (y == -390) Status.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,500,0);
        else Status.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,-390,0);
    }


    public void UpdateFood()
    {
        FoodText.GetComponent<TextMeshProUGUI>().text = $"식량 : {GameManager.instance.GetFood()}";
    }

    public void UpdateArmy()
    {
        ArmyText.GetComponent<TextMeshProUGUI>().text = $"병력 : {GameManager.instance.GetArmy()}";
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

    public void UpdateTeam()
    {
        Team1.GetComponent<TextMeshProUGUI>().text = "";
        Team2.GetComponent<TextMeshProUGUI>().text = "";
        Team3.GetComponent<TextMeshProUGUI>().text = "";

        List<string> team = GameManager.instance.GetTeam();
        int idx = 0;
        foreach (string teamName in team)
        {
            switch (idx % 3)
            {
                case 0:
                    Team1.GetComponent<TextMeshProUGUI>().text += teamName;
                    break;
                case 1:
                    Team2.GetComponent<TextMeshProUGUI>().text +=teamName;
                    break;
                case 2:
                    Team3.GetComponent<TextMeshProUGUI>().text +=teamName;
                    break;
            }
        }
    }
}
