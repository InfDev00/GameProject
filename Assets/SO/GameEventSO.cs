using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "GameEventSO", menuName = "Scriptable Object/GameEventSO", order = 0)]
public class GameEventSO : ScriptableObject
{
    [System.Serializable]
    public struct Set
    {
        public int speech;
        public int force;
        public int tactics;
        public string[] team;
    }

    [System.Serializable]
    public struct Button
    {
        public string text;
        public Set condition;
        public Set change;
        public GameEventSO nxtGameEvent;
    }

    public int dayUpdate;
    [TextArea]
    public string stateText;
    public Button[] buttons;

    public void Init()
    {
        dayUpdate = 0;
    }

    public Button[] GetNextButtons()
    {
        int speech = GameManager.instance.GetSpeech();
        int force = GameManager.instance.GetForce();
        int tactics = GameManager.instance.GetTactics();
        List<string> team = GameManager.instance.GetTeam();

        List<Button> nextButtons = new List<Button> ();

        foreach (Button button in buttons) 
        {
            if(isMeet(button, speech, force, tactics, team)) { nextButtons.Add(button); }
        }

        return nextButtons.ToArray();
    }

    bool isMeet(Button button, int speech, int force, int tactics, List<string> team)
    {
        if(button.condition.speech > speech) { return false; }
        if(button.condition.force > force) { return false; }
        if(button.condition.tactics > tactics) {  return false; }

        foreach(var member in team)
        {
            if (!Array.Exists(button.condition.team, x =>x == member)) { return false; }
        }

        return true;
    }
}


