using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("StateSetting")]
    public StateSO CurrentState;
    public GameObject imageStatePrefab;
    public GameObject textStatePrefab;
    private GameObject state;
    private StateSO[] nxtStates = new StateSO[3];


    [Header("UISetting")]
    public GameObject canvas;
    public int day;
    private UISetting UIScript;
    private StateSO.Set[] nxtChanges = new StateSO.Set[3];
    private StatusSetting statusScript;
    public GameObject popup;

    [HideInInspector]
    public bool[] optionTrigers = new bool[3];

    // Start is called before the first frame update
    void Start()
    {
        statusScript = popup.GetComponent<StatusSetting>();
        UIScript = canvas.GetComponent<UISetting>();
        GameObject status = GameObject.Find("StatusPopup");
        if(status!=null) status.SetActive(false);
        day = 1;

        StateSetting();
        for(int i=0;i<3;i++) optionTrigers[i] = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(optionTrigers[0]==true) Option1Action();
        else if(optionTrigers[1]==true) Option2Action();
        else if(optionTrigers[2]==true) Option3Action();

        for(int i=0;i<3;i++) optionTrigers[i] = false;
    }



    void StateSetting()
    {
        Destroy(GameObject.Find("State"));
        if(CurrentState.StateImage==null)state = Instantiate(textStatePrefab);
        else {
            state = Instantiate(imageStatePrefab);
            state.transform.Find("Image").GetComponent<Image>().sprite = CurrentState.StateImage;
        }
        state.name = "State";

        string[] optionTexts = new string[3];
        (optionTexts, nxtStates, nxtChanges) = GetRandomOption();
        state.GetComponent<StateSetting>().SetStateText(CurrentState.StateText,optionTexts);
        day+=CurrentState.DayUpdate;
        UIScript.DayUpdate();
    }

    private (string[], StateSO[], StateSO.Set[]) GetRandomOption()
    {
        //조건에 따른 변화 추가할 것
        string[] returnArray = new string[3];
        StateSO[] returnState = new StateSO[3];
        StateSO.Set[] returnChange = new StateSO.Set[3];
        List<int> indexes = new List<int>();
        for (int j = 0;j<CurrentState.options.Length;++j)
        {
            var option = CurrentState.options[j];
            bool isIn = true;
            if(statusScript.GetStatus()!=option.Condition.status && option.Condition.status!="")isIn = false;
            if(isIn && option.Condition.team!="")
            {
                string[] team = option.Condition.team.Split("/");
                int[] _team = new int[team.Length];
                for (int i = 0; i < team.Length; ++i) _team[i] = int.Parse(team[i]);
                foreach (var member in team)
                {
                    if (!statusScript.GetTeamCode().Contains(int.Parse(member)))
                    {
                        isIn = false;
                        break;
                    }
                }
            }
            if(isIn && option.Condition.item != "")
            {
                string[] item = option.Condition.item.Split("/");
                int[] _item = new int[item.Length];
                for (int i = 0; i < item.Length; ++i) _item[i] = int.Parse(item[i]);
                foreach (var tmp in item)
                {
                    if (!statusScript.GetHavingCode().Contains(int.Parse(tmp)))
                    {
                        isIn = false;
                        break;
                    }
                }
            }
            if(isIn) indexes.Add(j);

        }

        for (int i = 0;i<3;++i)
        {
            var index = Random.Range(0,indexes.Count);
            var _index = indexes[index];
            returnArray[i] = CurrentState.options[_index].optionText;
            returnState[i] = CurrentState.options[_index].optionState;
            returnChange[i] = CurrentState.options[_index].Change;
            indexes.RemoveAt(index);
        }

        return (returnArray, returnState, returnChange);
    }

    void Option1Action()
    {
        if(nxtStates[0])
        {
            UIScript.StatusUpdate(nxtChanges[0]);
            CurrentState = nxtStates[0];
            StateSetting();

        }
    }

    void Option2Action()
    {
        if(nxtStates[1])
        {
            UIScript.StatusUpdate(nxtChanges[1]);
            CurrentState = nxtStates[1];
            StateSetting();

        }
    }

    void Option3Action()
    {
        if(nxtStates[2])
        {
            UIScript.StatusUpdate(nxtChanges[2]);
            CurrentState = nxtStates[2];
            StateSetting();

        }
    }
}
