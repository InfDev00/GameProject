using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusSetting : MonoBehaviour
{
    [SerializeField]
    private GameObject cooperation;
    private int cooperationPoint;
    [SerializeField]
    private GameObject preperation;
    private int preperationPoint;
    [SerializeField]
    private GameObject power;
    private int powerPoint;
    [SerializeField]
    private GameObject[] team;
    private List<Dictionary<string, object>> teamList;
    private List<int> teamCode;
    public int teamMaxLength;
    [SerializeField]
    private GameObject[] items;
    private List<Dictionary<string, object>> HavingList;
    private List<int> HavingCode;

    private List<Dictionary<string, object>> MemberCode;
    private List<Dictionary<string, object>> ItemCode;

    void Awake() {
        cooperationPoint = 0;
        preperationPoint = 0;
        powerPoint = 0;

        teamMaxLength = 3;
        teamList = new List<Dictionary<string, object>>();
        teamCode = new List<int>();

        HavingList = new List<Dictionary<string, object>>();
        HavingCode = new List<int>();

        MemberCode = CSVReader.Read("Member");
        ItemCode = CSVReader.Read("Item");
    }

    public string GetStatus()
    {
        return $"{cooperationPoint}/{preperationPoint}/{powerPoint}";
    }

    public List<int> GetHavingCode()
    {
        return HavingCode;
    }

    public List<int> GetTeamCode()
    {
        return teamCode;
    }

    public void UpdateCooperation(int newPoint)
    {
        cooperationPoint += newPoint;
        if (cooperationPoint > 1) { cooperationPoint = 1; }
        else if (cooperationPoint < -1) { cooperationPoint = -1; }
        switch (cooperationPoint)
        {
            case 0:
                cooperation.GetComponent<TextMeshProUGUI>().text = "중립";
                break;
            case 1:
                cooperation.GetComponent<TextMeshProUGUI>().text = "협조";
                break;
            case -1:
                cooperation.GetComponent<TextMeshProUGUI>().text = "배신";
                break;
        }
    }


    public void UpdatePreparation(int newPoint)
    {
        preperationPoint += newPoint;
        if (preperationPoint > 1) { preperationPoint = 1; }
        else if (preperationPoint < -1) {preperationPoint = -1; }
        switch (preperationPoint)
        {
            case 0:
                preperation.GetComponent<TextMeshProUGUI>().text = "중립";
                break;
            case 1:
                preperation.GetComponent<TextMeshProUGUI>().text = "책임";
                break;
            case -1:
                preperation.GetComponent<TextMeshProUGUI>().text = "낭만";
                break;
        }

    }

    public void UpdatePower(int newPoint)
    {
        powerPoint += newPoint;
        if (powerPoint > 1) { powerPoint = 1; }
        else if (powerPoint < -1) { powerPoint = -1; }
        switch (powerPoint)
        {
            case 0:
                power.GetComponent<TextMeshProUGUI>().text = "중립";
                break;
            case 1:
                power.GetComponent<TextMeshProUGUI>().text = "강력";
                break;
            case -1:
                power.GetComponent<TextMeshProUGUI>().text = "연약";
                break;
        }
    }

    public void UpdateTeam(int[] newMemberCode)
    {
        for(int i=0;i<teamMaxLength;++i){
            team[i].GetComponent<TextMeshProUGUI>().text = "";
        }
        foreach (var code in newMemberCode)
        {
            if (code < 0){ 
                int _code = code*-1;
                if(teamCode.Contains(_code)){
                    teamList.RemoveAt(teamCode.IndexOf(_code));
                    teamCode.Remove(_code);
                }
            }else{
                if(!teamCode.Contains(code) && teamCode.Count <= teamMaxLength){
                    var Member = MemberCode[code];
                    teamCode.Add(code);
                    teamList.Add(Member);
                }
            }
            for (int i=0;i<teamCode.Count;++i){
                team[i].GetComponent<TextMeshProUGUI>().text = teamList[i]["Name"].ToString();
            }
        }
    }

    public void UpdateItem(int[] newItemCode)
    {
        foreach (var code in newItemCode)
        {
            if(code < 0){ 
                int _code = code*-1;
                if(HavingCode.Contains(_code)){
                    HavingList.RemoveAt(HavingCode.IndexOf(_code));
                    HavingCode.Remove(_code);
                }
            }else{
                if(!HavingCode.Contains(code)){
                    var newItem = ItemCode[code];
                    HavingCode.Add(code);
                    HavingList.Add(newItem);
                }
            }
            
            for(int i=0;i<3;++i){
                items[i].GetComponent<TextMeshProUGUI>().text = "";
            }

            for(int i=0;i<HavingCode.Count;++i){
                int idx = i%3;
                string tmp = items[idx].GetComponent<TextMeshProUGUI>().text;
                items[idx].GetComponent<TextMeshProUGUI>().text = $"{tmp}\n{HavingList[i]["Name"].ToString()}";
            }
        }
    }
}
