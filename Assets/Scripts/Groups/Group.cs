using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group
{
    protected int food;
    protected int army;
    protected int friendship;
    protected int counterProb;
    protected int attackProb;
    protected List<string> member;

    public Group() { }

    public Group(int food, int army) 
    {
        this.food = food;
        this.army = army;
    }

    public virtual void Attack(Group enemy)
    {
        if (enemy == null) return;

        int attackArmy = (int)(this.army / 5);
        if(attackArmy < 10) return;
        


        int revengedProb = Random.Range(0, 100);
        if(revengedProb < enemy.GetCounterProb()) 
        {
            enemy.Attack(this);
        }
    }

    public void AddFood(int food) { this.food += food; }
    public int GetFood() { return this.food;}

    public void AddArmy(int army) { this.army += army; }
    public int GetArmy() { return this.army;}

    public void AddFriendship(int friendship) { this.friendship += friendship; }
    public int GetFriendship() { return this.friendship;}

    public void AddCounterProb(int counterProb) { this.counterProb += counterProb; }
    public int GetCounterProb() { return this.counterProb;}

    public void AddAttackProb(int attackProb) { this.attackProb += attackProb; }
    public int GetAttackProb() { return this.attackProb;}

    public void AddMember(string member) { this.member.Add(member);}
    public void RemoveMember(string member) { this.member.Remove(member);}
    public List<string> GetMember() { return this.member;}
}
