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

    public void StatusUpdate(StateSO.changes newChange)
    {
        if(newChange.statusChange!=""){
            string[] newStatus = newChange.statusChange.Split("/");
            int newCooperation = newStatus[0][0]-'0';
            int newPreparation = newStatus[1][0]-'0';
            int newPower = newStatus[2][0]-'0';

            statusScript.UpdateCooperation(newCooperation);
            statusScript.UpdatePreparation(newPreparation);
            statusScript.UpdatePower(newPower);
        }


        if(newChange.teamChange!=""){

            string[] newMemberCode = newChange.teamChange.Split("/");
            int idx = newMemberCode.Length;
            if(idx > statusScript.teamMaxLength)idx = statusScript.teamMaxLength;
            int[] _newMemberCode = new int[idx];
            for(int i=0;i<idx;++i)_newMemberCode[i] = int.Parse(newMemberCode[i]);

            statusScript.UpdateTeam(_newMemberCode);  
        }

        if(newChange.itemChange!=""){
            string[] newItem = newChange.itemChange.Split("/");
            int[] _newItem = new int[newItem.Length];
            for(int i=0;i<newItem.Length;++i)_newItem[i] = int.Parse(newItem[i]);

            statusScript.UpdateItem(_newItem);
        }
      
    }

}
