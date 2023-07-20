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
    private List<string> team;

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
        player = new Group(0,0);
        day = 0;

        speech = 0;
        force = 0;
        tactics = 0;
        life = 3;

        team = new List<string>();

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

    public void AddFood(int food) { this.player.AddFood(food); }
    public int GetFood() { return this.player.GetFood(); }

    public void AddArmy(int army) { this.player.AddArmy(army); }
    public int GetArmy() { return this.player.GetArmy(); }

    public void AddSpeech(int speech) { this.speech += speech; }
    public int GetTactics() { return this.tactics; }

    public void AddForce(int force) {  this.force += force; }
    public int GetForce() { return this.force; }

    public void AddTactics(int tactics) { this.tactics += tactics; }
    public int GetSpeech() { return this.speech;}

    public void AddTeam(string team) { this.team.Add(team); }
    public void RemoveTeam(string team) { this.team.Remove(team); }
    public List<string> GetTeam() { return this.team;}
    public bool isTeam(string team) { return this.team.Contains(team); }

    public void AddLife(int life) { this.life += life; }
    public int GetLife() { return this.life; }

    public void AddWeeklyEvent(string key, GameEventSO value) { this.weeklyEvent.Add(key, value); }
    public void RemoveWeeklyEvent(string key) { this.weeklyEvent.Remove(key); }
    public Dictionary<string, GameEventSO> GetWeeklyEvent() { return this.weeklyEvent; }

    public GameEventSO[] GetRandomEvent() { return this.randomEvents; }
}
