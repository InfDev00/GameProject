using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fanatics : Group
{
    public Fanatics(int food, int army, int friendship)
    {
        this.food = food;
        this.army = army;
        this.friendship = friendship;
        this.member = new List<string>(){"fanatic1", "fanatic2"};
        this.counterProb = 30;
        this.attackProb = 10;
    }
}
