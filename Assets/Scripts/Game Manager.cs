using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject textBoxPrefab;
    public GameObject buttonPrefab;
    
    [Header("Initial Values")]
    public GameEventSO initialGameEvent;

    [Header("WeeklyEvent")]
    public GameEventSO[] weeklyEventValues;
    public string[] weeklyEventKeys;
    private Dictionary<string, GameEventSO> weeklyEvent = new Dictionary<string, GameEventSO>();

    [Header("RandomEvent")]
    public GameEventSO[] randomEvents;

    [Header("ActualObject")]
    private GameObject textBox;
    private GameObject button;

    [Header("PlayerInfo")]
    private Group player;
    private int day;
    private int speech;
    private int force;
    private int tactics;
    private int life;

    [Header("Groups")]
    private Dictionary<string, Group> enemy = new Dictionary<string, Group>();
    private Dictionary<string, Group> AllEnemyDictionary = new Dictionary<string, Group>
    {
        {"test1", new Group("test1", 100, 100, 10, 10) },//string name, int food, int army, int counterProb, int attackProb
        {"test2", new Group("test2", 100, 100, 80, 10) },
        {"test3", new Group("test3", 100, 100, 5, 10) },
        {"player", new Group("player", 0,1000,0,0) },
    };

    public static GameManager instance;
    private ExceptionHandler exceptionHandler = new ExceptionHandler();

    void Awake()
    {
        instance = this;
    }

    void Destroy()
    {
        instance = null;
    }

    void Start()
    {
        player = new Group(100,100);
        day = 0;

        speech = 0;
        force = 0;
        tactics = 0;
        life = 3;

        for (int i = 0; i < weeklyEventKeys.Length; ++i) AddWeeklyEvent(weeklyEventKeys[i], weeklyEventValues[i]);

        SetGameEvent(initialGameEvent);
    }

    public void SetGameEvent(GameEventSO gameEvent)
    {
        try
        {
            exceptionHandler.EventThrow(gameEvent);

            Destroy(GameObject.Find("Button"));
            Destroy(GameObject.Find("TextBox"));
            button = Instantiate(buttonPrefab);
            textBox = Instantiate(textBoxPrefab);
            button.name = "Button";
            textBox.name = "TextBox";


            button.GetComponent<ButtonManager>().SetButton(gameEvent.GetNextButtons());
            textBox.GetComponent<TextBoxManager>().SetTextBox(gameEvent.stateText);
            AddDay(gameEvent.dayUpdate);
        }
        catch(Exception e) 
        {
            exceptionHandler.EventCatch(e);
        }
    }

    public void GroupBattle()
    {
        List<string> keys = new List<string>(enemy.Keys);
        foreach(var group in enemy.Values) 
        {
            int prob = UnityEngine.Random.Range(0, 100);
            if (prob < group.GetAttackProb()) 
            {
                prob = UnityEngine.Random.Range(0, enemy.Count);
                if (keys[prob] == "player") PlayerAttacked((int)(group.GetArmy()/5));
                else group.Attack(enemy[keys[prob]]);

            }
        }

        foreach(var group in enemy)
        {
            if(group.Value.GetArmy()<=10) RemoveEnemy(group.Key);
        }
    }

    public void PlayerAttacked(int enemyArmy)
    {
        AddArmy(enemyArmy * (-1));
        
    }

    public void GameEnding()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif  
    }

    public void AddDay(int day) 
    {
        this.day += day;
        this.GroupBattle();
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

    public void AddEnemy(string enemy) {if(this.enemy.Count < 3)this.enemy.Add(enemy, AllEnemyDictionary[enemy]); }
    public void RemoveEnemy(string enemy) { this.enemy.Remove(enemy); }
    public Dictionary<string, Group> GetEnemy() { return this.enemy;}
    public bool isEnemy(string enemy) { return this.enemy.ContainsKey(enemy); }

    public void AddLife(int life) { this.life += life; }
    public int GetLife() { return this.life; }

    public void AddWeeklyEvent(string key, GameEventSO value) { this.weeklyEvent.Add(key, value); }
    public void RemoveWeeklyEvent(string key) { this.weeklyEvent.Remove(key); }
    public Dictionary<string, GameEventSO> GetWeeklyEvent() { return this.weeklyEvent; }

    public GameEventSO[] GetRandomEvent() { return this.randomEvents; }
}
