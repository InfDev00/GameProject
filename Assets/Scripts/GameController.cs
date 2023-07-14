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
    private StateSO.changes[] nxtChanges = new StateSO.changes[3];
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

    private (string[], StateSO[], StateSO.changes[]) GetRandomOption()
    {
        //조건에 따른 변화 추가할 것
        string[] returnArray = new string[3];
        StateSO[] returnState = new StateSO[3];
        StateSO.changes[] returnChange = new StateSO.changes[3];

        int[] indexes = new int[CurrentState.options.Length];
        for(int i=0;i<indexes.Length;++i) indexes[i] = i;
        List<int> _indexes = new List<int>(indexes);
        for(int i = 0;i<3;++i)
        {
            var index = Random.Range(0,_indexes.Count);
            var _index = _indexes[index];
            returnArray[i] = CurrentState.options[_index].optionText;
            returnState[i] = CurrentState.options[_index].optionState;
            returnChange[i] = CurrentState.options[_index].Change;
            _indexes.RemoveAt(index);
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
