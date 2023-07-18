using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISetting : MonoBehaviour
{
    private GameObject dayText;
    [HideInInspector]
    public GameController mainScript;

    public GameObject statusPopup;
    private GameObject popup;
    private StatusSetting statusScript;
    private GameObject team;
    private List<string> teamList = new List<string>();

    void Start()
    {
        mainScript = GameObject.Find("GameController").GetComponent<GameController>();
        dayText = transform.Find("Day").gameObject;
        DayUpdate();

        popup = statusPopup.transform.Find("Popup").gameObject;
        statusScript = popup.GetComponent<StatusSetting>();
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
        dayText.GetComponent<TextMeshProUGUI>().text = $"D+{mainScript.day}";
    }

    public void StatusUpdate(StateSO.Set newChange)
    {
        if(newChange.status!=""){
            string[] newStatus = newChange.status.Split("/");
            int newCooperation = int.Parse(newStatus[0]);
            int newPreparation = int.Parse(newStatus[1]);
            int newPower = int.Parse(newStatus[2]);

            statusScript.UpdateCooperation(newCooperation);
            statusScript.UpdatePreparation(newPreparation);
            statusScript.UpdatePower(newPower);
        }

        if(newChange.team!=""){

            string[] newMemberCode = newChange.team.Split("/");
            int[] _newMemberCode = new int[newMemberCode.Length];
            for(int i=0;i< newMemberCode.Length; ++i)_newMemberCode[i] = int.Parse(newMemberCode[i]);

            statusScript.UpdateTeam(_newMemberCode);  
        }

        if(newChange.item!=""){
            string[] newItem = newChange.item.Split("/");
            int[] _newItem = new int[newItem.Length];
            for(int i=0;i<newItem.Length;++i)_newItem[i] = int.Parse(newItem[i]);

            statusScript.UpdateItem(_newItem);
        }
      
    }

}
