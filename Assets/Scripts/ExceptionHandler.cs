using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ExceptionHandler
{

    public void EventThrow(GameEventSO gameEvent)
    {
        if (gameEvent==null)
        {
            throw new Exception("NullExeption");
        }

        else if(GameManager.instance.GetDay()% 7 == 6)
        {
            throw new Exception("WeeklyEventExeption");
        }
    }

    public void EventCatch(Exception exception) 
    {
        switch(exception.Message)
        {
            case "NullExeption":
                var randomEvent = GameManager.instance.GetRandomEvent();
                if (randomEvent.Length == 0)
                {
                    Debug.LogWarning("NullExeption Activated");
                    break;
                }
                int key = UnityEngine.Random.Range(0, randomEvent.Length);
                GameManager.instance.SetGameEvent(randomEvent[key]);
                break;
            case "WeeklyEventExeption":
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
