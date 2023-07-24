using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ExceptionHandler
{
    int[] nullArray = new int[3];
    int arrayIndex = 0;
    public void EventThrow(GameEventSO gameEvent)
    {

        if (GameManager.instance.GetFood() < 0 || GameManager.instance.GetArmy() < 0) throw new Exception("DeadException");
        
        if (gameEvent == null && GameManager.instance.GetIsAttacked() == true && GameManager.instance.GetAttackAvailable() == false) throw new Exception("EnemyException");

        else if (gameEvent == null && GameManager.instance.GetDay() % 7 == 6) throw new Exception("WeeklyEventException");

        else if (gameEvent == null) throw new Exception("NullException");
    }

    public void EventCatch(Exception exception) 
    {
        switch(exception.Message)
        {
            case "NullException":
                var randomEvent = GameManager.instance.GetRandomEvent();
                if (randomEvent.Length == 0)
                {
                    Debug.LogWarning("NullException Activated");
                    break;
                }
                int key = -1;
                do
                {
                    key = UnityEngine.Random.Range(0, randomEvent.Length);
                } while (Array.IndexOf(nullArray, key)!=-1);

                nullArray[arrayIndex] = key;
                arrayIndex = arrayIndex == 2 ? 0 : arrayIndex + 1;

                GameManager.instance.SetGameEvent(randomEvent[key]);
                break;
            case "DeadException":
                GameManager.instance.GameEnding();
                break;
            case "EnemyException":
                GameManager.instance.SetIsAttacked(false);
                GameManager.instance.PlayerAttacked();
                GameManager.instance.SetAttackAvailable(true);
                break;
            case "WeeklyEventException":
                GameManager.instance.AddDay(1);
                var weeklyEvent = GameManager.instance.GetWeeklyEvent();
                try
                {
                    GameManager.instance.SetGameEvent(weeklyEvent["DefaultEvent"]);
                }
                catch (Exception e)
                {
                    Debug.LogWarning($"WeeklyKeyError : {e.Message}");
                }
                break;
        }
    }
}
