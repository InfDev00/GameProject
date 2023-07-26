using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject textBoxPrefab;
    public GameObject buttonPrefab;
    
    [Header("Initial Values")]
    public GameEventSO initialGameEvent;
    public int initialFood = 0;
    public int initialArmy = 0;

    [Header("WeeklyEvent")]
    public GameEventSO[] weeklyEventValues;
    public string[] weeklyEventKeys;
    private Dictionary<string, GameEventSO> weeklyEvent = new Dictionary<string, GameEventSO>();

    [Header("RandomEvent")]
    public GameEventSO[] randomEvents;

    [Header("BattleEvent")]
    public GameEventSO battleEvent;

    [Header("AttackEvent")]
    public GameEventSO attackEvent;

    [Header("ActualObject")]
    private GameObject textBox;
    private GameObject button;

    [Header("PlayerInfo")]
    private Group player;
    private int day;
    private int speech;
    private int force;
    private int tactics;
    public bool UITriger = false;
    public bool BattleTriger = false;

    [Header("Groups")]
    private Group currentEnemy;
    private int currentEnemyArmy;
    private bool isAttacked;
    private Dictionary<string, Group> enemy = new Dictionary<string, Group>();
    private Dictionary<string, Group> AllEnemyDictionary = new Dictionary<string, Group>
    {
        {"ºÓÀº ¹Ù¶÷", new Group("ºÓÀº ¹Ù¶÷", 1000, 1000, 10, 20) },//string name, int food, int army, int counterProb, int attackProb
        {"³ì»ö ¹ø°³", new Group("³ì»ö ¹ø°³", 1000, 1000, 80, 10) },
        {"Çª¸¥ ºÒ²É", new Group("Çª¸¥ ºÒ²É", 1000, 1000, 5, 50) },
        {"player", new Group("player", 0,1000,0,0) },
    };

    public static GameManager instance;
    private ExceptionHandler exceptionHandler = new ExceptionHandler();

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

    }

    void Start()
    {
        GameInit();
    }

    public void GameInit()
    {
        player = new Group(initialFood, initialArmy);
        day = 0;

        speech = 0;
        force = 0;
        tactics = 0;
        isAttacked = false;

        ClearEnemy();
        ClearWeeklyEvent();
        for (int i = 0; i < weeklyEventKeys.Length; ++i) AddWeeklyEvent(weeklyEventKeys[i], weeklyEventValues[i]);
        exceptionHandler.weeklyEventIndex = weeklyEventKeys;

        Destroy(GameObject.Find("Button"));
        Destroy(GameObject.Find("TextBox"));
        button = Instantiate(buttonPrefab);
        textBox = Instantiate(textBoxPrefab);
        button.name = "Button";
        textBox.name = "TextBox";

        SetGameEvent(initialGameEvent);
    }

    public void SetGameEvent(GameEventSO gameEvent)
    {
        try
        {
            exceptionHandler.EventThrow(gameEvent);


            button.GetComponent<ButtonManager>().SetButton(gameEvent.GetNextButtons());
            textBox.GetComponent<TextBoxManager>().SetTextBox(gameEvent.stateText);
            AddDay(gameEvent.dayUpdate);
        }
        catch(Exception e) 
        {
            exceptionHandler.EventCatch(e);
        }

        UITriger = true;
    }

    public void GroupBattle()
    {
        List<(Group, Group)> attackList = new List<(Group, Group)>();
        List<string> keys = new List<string>(enemy.Keys);
        foreach(var group in enemy.Values) 
        {
            int prob = UnityEngine.Random.Range(0, 100);
            if (prob < group.GetAttackProb()) 
            {
                prob = UnityEngine.Random.Range(0, enemy.Count);
                attackList.Add((group, enemy[keys[prob]]));
                Debug.Log($"MANAGER : {group.GetName()} attaced {enemy[keys[prob]].GetName()} with {this.isAttacked}");
            }
        }

        foreach(var (attackGroup, attackedGroup) in attackList)
        {
            attackGroup.Attack(attackedGroup);
        }

        foreach(var group in enemy)
        {
            if(group.Value.GetArmy()<=10) RemoveEnemy(group.Key);
        }
    }

    public void GroupPlunder()
    {
        foreach (var group in enemy.Values)
        {
            int plunderProb = UnityEngine.Random.Range(0, 100);
            if(plunderProb < 30)
            {
                group.AddArmy(100);
                group.AddFood(100);
            }
        }
    }

    public void PlayerAttacked()
    {
        Debug.Log("PlayerAttacked");
        SetGameEvent(battleEvent);
    }

    public void PlayerCounter()
    {
        if(currentEnemyArmy > this.GetArmy() && this.GetArmy() != 0) 
        { 
            player.PlayerAttack(currentEnemy, this.GetArmy());
            this.AddArmy(GetArmy()*(-1));
        }
        else
        {
            player.PlayerAttack(currentEnemy, currentEnemyArmy);
            this.AddArmy(currentEnemyArmy * (-1));
        }

        int attackFood = (int)(currentEnemyArmy / 4);
        if (attackFood > this.GetFood() && this.GetFood() != 0)
        {
            this.AddFood(GetFood() * (-1));
        }
        else
        {
            this.AddFood(attackFood * (-1));
        }
    }

    public void PlayerDefence()
    {
        int attackFood = (int)(currentEnemyArmy / 2);
        if (attackFood > this.GetFood() && this.GetFood()!=0)
        {
            this.AddFood(GetFood() * (-1));
        }
        else
        {
            this.AddFood(attackFood * (-1));
        }
        int lossArmy = (int)(currentEnemyArmy / 2);
        if (lossArmy > this.GetArmy() && this.GetArmy() != 0)
        {
            this.AddArmy(GetArmy() * (-1));
        }
        else
        {
            this.AddArmy(lossArmy * (-1));
        }
    }

    public void PlayerAttack()
    {
        SetGameEvent(attackEvent);
    }

    public void EnemyAttacked(string name)
    {
        int attackFood = (int)(this.GetFood() / 10);
        int attackArmy = (int)(this.GetArmy() / 10);
        this.GetEnemy()[name].AddFood(attackFood*(-1));
        this.GetEnemy()[name].AddArmy(attackArmy*(-1));
        this.AddFood(attackFood);
        this.AddArmy(attackArmy);
    }

    public void GameEnding()
    {
        SceneValue.globalDay = this.GetDay();
        SceneValue.globalArmy = this.GetArmy();
        SceneValue.globalFood = this.GetFood();
        SceneManager.LoadScene("EndingScene");
        //#if UNITY_EDITOR
        //        UnityEditor.EditorApplication.isPlaying = false;
        //#else
        //        Application.Quit();
        //#endif  
    }

    public void AddDay(int day) 
    {
        this.day += day;
        this.GroupBattle();
        this.GroupPlunder();
    }
    public int GetDay() { return day; }

    public void AddFood(int food) { this.player.AddFood(food); }
    public int GetFood() { return this.player.GetFood(); }

    public void AddArmy(int army) 
    {
        this.player.AddArmy(army);
        if (this.player.GetArmy() < 0) GameEnding();
      
    }
    public int GetArmy() { return this.player.GetArmy(); }

    public void AddSpeech(int speech) { this.speech += speech; }
    public int GetTactics() { return this.tactics; }

    public void AddForce(int force) { this.force += force; }
    public int GetForce() { return this.force; }

    public void AddTactics(int tactics) { this.tactics += tactics; }
    public int GetSpeech() { return this.speech;}

    public void AddEnemy(string enemy) {this.enemy.Add(enemy, AllEnemyDictionary[enemy]); }
    public void RemoveEnemy(string enemy) { this.enemy.Remove(enemy); }
    public Dictionary<string, Group> GetEnemy() { return this.enemy;}
    public void ClearEnemy() { this.enemy.Clear();}
    public bool isEnemy(string enemy) { return this.enemy.ContainsKey(enemy); }

    public void AddWeeklyEvent(string key, GameEventSO value) { this.weeklyEvent.Add(key, value); }
    public void RemoveWeeklyEvent(string key) { this.weeklyEvent.Remove(key); }
    public void ClearWeeklyEvent() { this.weeklyEvent.Clear(); }
    public Dictionary<string, GameEventSO> GetWeeklyEvent() { return this.weeklyEvent; }

    public void SetCurrentEnemy(Group enemy) { this.currentEnemy = enemy;}
    public Group GetCurrentEnemy() { return this.currentEnemy; }

    public GameEventSO[] GetRandomEvent() { return this.randomEvents; }

    public void SetCurrentEnemyArmy(int army) { this.currentEnemyArmy = army;}
    public int GetCurrentEnemyArmy() { return this.currentEnemyArmy; }

    public bool GetIsAttacked() { return this.isAttacked; }
    public void SetIsAttacked(bool tmp) { this.isAttacked = tmp; }
}
