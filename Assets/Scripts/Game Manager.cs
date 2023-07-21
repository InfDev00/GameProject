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
    private List<string> jobs = new List<string>(new string[] {"aa", "bb"});

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
    private int day;
    private int food;
    private int gold;
    private int level;
    private string job;
    private List<string> skill;
    private List<string> item;
    private string currentEnemy;
    public string currentstate;

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
        day = 0;
        food = 0;
        gold = 0;
        level = 1;
        job = "ÃÊº¸ÀÚ";
        currentEnemy = "1";
        currentstate = "Default";

        skill = new List<string>();
        item = new List<string>();
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

            textBox.GetComponent<TextBoxManager>().SetTextBox(gameEvent.stateText);            
            button.GetComponent<ButtonManager>().SetButton(gameEvent.GetNextButtons(), gameEvent);


            AddDay(1);
        }
        catch(Exception e) 
        {
            exceptionHandler.EventCatch(e);
        }

    }

    public void GameEnding()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif  
    }

    public void AddDay(int day) { this.day += day; }
    public int GetDay() { return day; }

    public void AddFood(int food) { this.food+=food; }
    public int GetFood() { return this.food; }

    public void AddGold(int gold) { this.gold+=gold; }
    public int GetGold() { return this.gold; }

    public void AddLevel(int level) { this.level += level;}
    public int GetLevel() { return this.level;}

    public void SetJob(string job) { this.job = job; }
    public string GetJob() { return this.job; }

    public void AddSkill(string skill) { this.skill.Add(skill); }
    public void RemoveSkill(string skill) { this.skill.Remove(skill); }
    public List<string> GetSkill() { return this.skill;}
    public bool isSkill(string skill) { return this.skill.Contains(skill); }

    public void AddItem(string item) { this.item.Add(item); }
    public void RemoveItem(string item) { this.item.Remove(item); }
    public List<string> GetItem() { return this.item; }
    public bool isItem(string item) { return this.item.Contains(item); }

    public void SetCurrentEnemy(string enemy) { this.currentEnemy = enemy; }
    public string GetCurrentEnemy() { return this.currentEnemy; }

    public void SetCurrentState(string state) { this.currentstate = state; }
    public string GetCurrentState() { return this.currentstate; }

    public void AddWeeklyEvent(string key, GameEventSO value) { this.weeklyEvent.Add(key, value); }
    public void RemoveWeeklyEvent(string key) { this.weeklyEvent.Remove(key); }
    public Dictionary<string, GameEventSO> GetWeeklyEvent() { return this.weeklyEvent; }

    public GameEventSO[] GetRandomEvent() { return this.randomEvents; }
}
