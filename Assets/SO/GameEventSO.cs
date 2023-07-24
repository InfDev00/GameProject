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
        public int life;
        public int speech;
        public int force;
        public int tactics;
        public int food;
        public int army;
        public string[] enemy;
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
    [TextArea(2,15)]
    public string stateText;
    public Button[] buttons;

    public Button[] GetNextButtons()
    {
        int speech = GameManager.instance.GetSpeech();
        int force = GameManager.instance.GetForce();
        int tactics = GameManager.instance.GetTactics();
        int food = GameManager.instance.GetFood();
        int army = GameManager.instance.GetArmy();
        Dictionary<string, Group> enemy = GameManager.instance.GetEnemy();

        List<Button> nextButtons = new List<Button> ();

        foreach (Button button in buttons) 
        {
            if(isMeet(button, speech, force, tactics, food, army, enemy)) { nextButtons.Add(button); }
        }
        return nextButtons.ToArray();
    }

    bool isMeet(Button button, int speech, int force, int tactics, int food, int army, Dictionary<string, Group> enemy)
    {
        if(button.condition.speech > speech) { return false; }
        if(button.condition.force > force) { return false; }
        if(button.condition.tactics > tactics) {  return false; }
        if(button.condition.food > food) { return false; }
        if(button.condition.army > army) { return false; }

        if(enemy.Count == 0 && button.condition.enemy.Length!=0) { return false; }
        foreach(string mem in button.condition.enemy)
        {
            if (!enemy.ContainsKey(mem)) { return false; }
        }

        return true;
    }
}


