using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group
{
    private string name;
    protected int food;
    protected int army;
    protected int counterProb;
    protected int attackProb;

    public Group(string name, int food, int army, int counterProb, int attackProb)
    {
        this.name = name;
        this.food = food;
        this.army = army;
        this.counterProb = counterProb;
        this.attackProb = attackProb;
    }

    public Group(int food, int army) 
    {
        this.food = food;
        this.army = army;
        this.counterProb = 0;
        this.attackProb = 0;
    }

    public virtual void Attack(Group enemy)
    {
        if (enemy == null || enemy.name == this.name) return;

        int attackArmy = (int)(this.army / 5);
        if(this.army < 60) return;

        Debug.LogWarning($"{this.name} attacked {enemy.name}");

        this.AddArmy(attackArmy*(-1));
        enemy.AddArmy((int)(attackArmy/2)*(-1));
        enemy.AddFood((int)(attackArmy / 2)*(-1));
        this.AddFood((int)(attackArmy / 2));


        int revengedProb = Random.Range(0, 100);
        if(revengedProb < enemy.GetCounterProb()) 
        {
            enemy.Attack(this);
        }
    }

    public string GetName() { return name; }

    public void AddFood(int food) { this.food += food; }
    public int GetFood() { return this.food;}

    public void AddArmy(int army) { this.army += army; }
    public int GetArmy() { return this.army;}

    public void AddCounterProb(int counterProb) { this.counterProb += counterProb; }
    public int GetCounterProb() { return this.counterProb;}

    public void AddAttackProb(int attackProb) { this.attackProb += attackProb; }
    public int GetAttackProb() { return this.attackProb;}
}
