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
    private List <MemberCodeSO.Set> teamList;
    private List <int> teamCode;
    private int teamMaxLength;
    [SerializeField]
    private GameObject[] items;
    private List <string> HavingList;
    private List <int> HavingCode;

    public MemberCodeSO MemberCode;
    public ItemCodeSO ItemCode;
    void Start() {
        cooperationPoint = 0;
        preperationPoint = 0;
        powerPoint = 0;

        teamMaxLength = 3;
        teamList = new List<MemberCodeSO.Set>();
        teamCode = new List<int>();

        HavingList = new List<string>();
        HavingCode = new List<int>();
    }

    public void UpdateCooperation(int newPoint)
    {      
        switch (newPoint)
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
        cooperationPoint = newPoint;
    }

    
    public void UpdatePreparation(int newPoint)
    {
        switch (newPoint)
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
        preperationPoint = newPoint;
    }

    public void UpdatePower(int newPoint)
    {
        switch (newPoint)
        {
            case 0:
                power.GetComponent<TextMeshProUGUI>().text = "중립";
                break;
            case 1:
                power.GetComponent<TextMeshProUGUI>().text = "강력";
                break;
            case -1:
                power.GetComponent<TextMeshProUGUI>().text = "유연";
                break;
        }
            powerPoint = newPoint;
    }

    public void UpdateTeam(int[] newMemberCode) // 추가 제거 구분할 것
    {
 
        foreach (var code in newMemberCode)
        {
            if(teamCode.Contains(code)){
                teamList.RemoveAt(teamCode.IndexOf(code));
                teamCode.Remove(code);
            }

            else if(teamCode.Count <= teamMaxLength){
                MemberCodeSO.Set Member = MemberCode.code[code];
                teamCode.Add(code);
                teamList.Add(Member);
            }

            for(int i=0;i<teamCode.Count;++i){
                team[i].GetComponent<TextMeshProUGUI>().text = teamList[i].name;
            }
        }
    }

    public void UpdateItem(int[] newItemCode)
    {
        foreach (var code in newItemCode)
        {
            if(HavingCode.Contains(code)){
                HavingList.RemoveAt(HavingCode.IndexOf(code));
                HavingCode.Remove(code);
            }

            else {
                string item = ItemCode.code[code];
                HavingCode.Add(code);
                HavingList.Add(item);
            }

            for(int i=0;i<HavingCode.Count;++i){
                int idx = i%3;
                string tmp = items[idx].GetComponent<TextMeshProUGUI>().text;
                items[idx].GetComponent<TextMeshProUGUI>().text = $"{tmp}\n{HavingList[i]}";
            }
        }
    }
}
