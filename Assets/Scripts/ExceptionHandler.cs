using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ExceptionHandler
{
    public string[] weeklyEventIndex;
    int weeklyKey = 0;
    int[] nullArray = new int[3];
    int arrayIndex = 0;
    public void EventThrow(GameEventSO gameEvent)
    {
        int prob = UnityEngine.Random.Range(0, 100);

        if (GameManager.instance.GetFood() < 0 || GameManager.instance.GetArmy() < 0) throw new Exception("DeadException");

        if (gameEvent == null)
        {
            if ( GameManager.instance.GetIsAttacked() == true) throw new Exception("EnemyException");

            else if (GameManager.instance.GetDay() % 7 == 6) throw new Exception("WeeklyEventException");

            else if (prob < 30 && GameManager.instance.GetEnemy().Count > 1 && GameManager.instance.GetArmy() >= 100) throw new Exception("PlayerAttackException");

            else throw new Exception("NullException");
        }

    }

    public void EventCatch(Exception exception) 
    {
        switch(exception.Message)
        {
            case "NullException":
                this.NullEvent();
                break;
            case "DeadException":
                GameManager.instance.GameEnding();
                break;
            case "EnemyException":
                GameManager.instance.SetIsAttacked(false);
                GameManager.instance.PlayerAttacked();
                break;
            case "WeeklyEventException":
                GameManager.instance.AddDay(1);
                var weeklyEvent = GameManager.instance.GetWeeklyEvent();
                try
                {
                    GameManager.instance.SetGameEvent(weeklyEvent[weeklyEventIndex[weeklyKey++]]);
                }
                catch
                {
                    this.NullEvent();
                }
                break;
            case "PlayerAttackException":
                GameManager.instance.PlayerAttack();
                break;
        }
    }

    void NullEvent()
    {
        var randomEvent = GameManager.instance.GetRandomEvent();
        if (randomEvent.Length == 0)
        {
            Debug.LogWarning("NullException Activated");
            return;
        }
        int nullKey = -1;
        do
        {
            nullKey = UnityEngine.Random.Range(0, randomEvent.Length);
        } while (Array.IndexOf(nullArray, nullKey) != -1);

        nullArray[arrayIndex] = nullKey;
        arrayIndex = arrayIndex == 2 ? 0 : arrayIndex + 1;

        GameManager.instance.SetGameEvent(randomEvent[nullKey]);
    }
}
