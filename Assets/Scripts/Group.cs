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

    public void Attack(Group enemy)
    {
        Debug.Log($"GROUP : {this.GetName()} attaced {enemy.GetName()} with {GameManager.instance.GetIsAttacked()}");
        if (enemy.name == this.name) return;
        int attackArmy = (int)(this.army / 10);
        if(this.army < 100) return; //이게 원인. 병력 증진하는 이벤트 넣어야 할 듯


        if (enemy.name == "player")
        {
            GameManager.instance.SetIsAttacked(true);
            GameManager.instance.SetCurrentEnemy(this);
            GameManager.instance.SetCurrentEnemyArmy(attackArmy);
            Debug.Log($"{GameManager.instance.GetIsAttacked()} PlayerAttacked");
            return;
        }

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
    public void PlayerAttack(Group enemy, int attackArmy)
    {
        if (enemy == null) return;

        enemy.AddArmy((int)(attackArmy / 2) * (-1));
        enemy.AddFood((int)(attackArmy / 2) * (-1));

        int revengedProb = Random.Range(0, 100);
        if (revengedProb < enemy.GetCounterProb())
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
