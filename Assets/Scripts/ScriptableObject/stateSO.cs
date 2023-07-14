using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StateSO", menuName = "GameProject/StateSO", order = 0)]
public class StateSO : ScriptableObject {
    
    [System.Serializable]
    public struct changes
    {
        public string statusChange;
        public string teamChange;
        public string itemChange;
    }

    [System.Serializable]
    public struct Option
    {
        public string name;
        public string optionText;
        public StateSO optionState;
        public changes Change;

    }

    public Sprite StateImage;
    [TextArea]
    public string StateText;
    public int DayUpdate;

    public Option[] options = new Option[3];

}