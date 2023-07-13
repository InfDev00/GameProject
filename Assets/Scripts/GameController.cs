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

    [HideInInspector]
    public bool[] optionTrigers = new bool[3];
    // Start is called before the first frame update
    void Start()
    {
        GameObject status = GameObject.Find("StatusPopup");
        if(status!=null) status.SetActive(false);

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
        (optionTexts, nxtStates) = GetRandomOption();
        state.GetComponent<StateSetting>().SetStateText(CurrentState.StateText,optionTexts);
    }

    private (string[], StateSO[]) GetRandomOption()
    {
        string[] returnArray = new string[3];
        StateSO[] returnState = new StateSO[3];

        int[] indexes = new int[CurrentState.options.Length];
        for(int i=0;i<indexes.Length;++i) indexes[i] = i;
        List<int> _indexes = new List<int>(indexes);
        for(int i = 0;i<3;++i)
        {
            var index = Random.Range(0,_indexes.Count);
            var _index = _indexes[index];
            returnArray[i] = CurrentState.options[_index].optionText;
            returnState[i] = CurrentState.options[_index].optionState;
            _indexes.RemoveAt(index);
        }

        return (returnArray, returnState);
    }

    void Option1Action()
    {
        if(nxtStates[0])
        {
            CurrentState = nxtStates[0];
            StateSetting();

        }
    }

    void Option2Action()
    {
        if(nxtStates[1])
        {
            CurrentState = nxtStates[1];
            StateSetting();

        }
    }

    void Option3Action()
    {
        if(nxtStates[2])
        {
            CurrentState = nxtStates[2];
            StateSetting();

        }
    }

}
