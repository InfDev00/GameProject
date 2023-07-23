using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ExceptionHandler
{

    public void EventThrow(GameEventSO gameEvent)
    {
        if(GameManager.instance.GetLife() == 0) throw new Exception("LifeZeroException");

        else if (gameEvent==null) throw new Exception("NullException");

        else if(GameManager.instance.GetDay()% 7 == 6) throw new Exception("WeeklyEventException");
    }

    public void EventCatch(Exception exception) 
    {
        switch(exception.Message)
        {
            case "LifeZeroException":
                GameManager.instance.GameEnding();
                break;
            case "NullException":
                var randomEvent = GameManager.instance.GetRandomEvent();
                if (randomEvent.Length == 0)
                {
                    Debug.LogWarning("NullException Activated");
                    break;
                }
                int key = UnityEngine.Random.Range(0, randomEvent.Length);
                GameManager.instance.SetGameEvent(randomEvent[key]);
                break;
            case "WeeklyEventException":
                GameManager.instance.AddDay(1);
                var weeklyEvent = GameManager.instance.GetWeeklyEvent();
                try
                {
                    //weeklyevent를 여러개 설정해놓고 일주일마다 랜덤하게 나오게 할 수 있을듯?
                    //현재는 무조건 DefaultEvent를 박아넣고있음
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
