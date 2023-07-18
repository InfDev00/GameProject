using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StateSO", menuName = "GameProject/StateSO", order = 0)]
public class StateSO : ScriptableObject {
    
    [System.Serializable]
    public struct Set
    {
        public string status;
        public string team;
        public string item;
    }

    [System.Serializable]
    public struct Option
    {
        public string optionText;
        public StateSO optionState;
        public Set Change;
        public Set Condition;
    }

    public Sprite StateImage;
    [TextArea]
    public string StateText;
    public int DayUpdate;

    public Option[] options = new Option[3];

}