using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject textBoxPrefab;
    public GameObject buttonPrefab;
    
    [Header("Initial Values")]
    public GameEventSO initialGameEvent;
    private GameEventSO currentGameEvent;

    private GameObject textBox;
    
    [Header("Buttons")]
    private GameObject button;
    private bool[] btnTriger = new bool[3];


    private Group player;

    private int day;
    private int speech;
    private int force;
    private int tactics;

    private List<string> team;

    public static GameManager instance;

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

        team = new List<string>();

        for(int i = 0; i < 3; ++i) btnTriger[i] = false;
        currentState = initialState;
        SetState(initialState);
    }

    void Update()
    {


    }

    void SetState(GameEventSO state)
    {
        Destroy(GameObject.Find("Button"));
        Destroy(GameObject.Find("TextBox"));
        button = Instantiate(buttonPrefab);
        textBox = Instantiate(textBoxPrefab);
        button.name = "Button";
        textBox.name = "TextBox";


        button.GetComponent<ButtonManager>().SetButton(state.GetNextButtons());
        textBox.GetComponent<TextBoxManager>().SetTextBox(state.stateText);
        AddDay(state.dayUpdate);
    }


    public void AddDay(int day) { this.day += day; }
    public int GetDay() { return day; }

    public void AddFood(int food) { this.player.AddFood(food); }
    public void SubFood(int food) { this.player.SubFood(food); }
    public int GetFood() { return this.player.GetFood(); }

    public void AddArmy(int army) { this.player.AddArmy(army); }
    public void SubArmy(int army) { this.player.SubArmy(army); }
    public int GetArmy() { return this.player.GetArmy(); }

    public void SetSpeech(int speech) { this.speech = speech; }
    public int GetTactics() { return this.tactics; }

    public void SetForce(int force) {  this.force = force; }
    public int GetForce() { return this.force; }

    public void SetTactics(int tactics) { this.tactics = tactics; }
    public int GetSpeech() { return this.speech;}

    public void AddTeam(string team) { this.team.Add(team); }
    public void RemoveTeam(string team) { this.team.Remove(team); }
    public List<string> GetTeam() { return this.team;}

    public void SetBtnTriger(int idx) { this.btnTriger[idx] = true;}
}
