using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISetting : MonoBehaviour
{
    public GameObject statusPopup;
    private GameObject dayText;
    private int day;
    private GameObject team;
    private List<string> teamList = new List<string>();

    void Start()
    {
        day = 0;
        dayText = transform.Find("Day").gameObject;
        DayUpdate();
    }

    public void OpenStatus()
    {
        statusPopup.SetActive(true);
    }

    public void CloseStatus()
    {
        statusPopup.SetActive(false);
    }

    public void DayUpdate()
    {
        day+=1;
        dayText.GetComponent<TextMeshProUGUI>().text = $"D+{day}";
    }

}
