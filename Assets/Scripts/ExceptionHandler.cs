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
                    //weeklyevent�� ������ �����س��� �����ϸ��� �����ϰ� ������ �� �� ������?
                    //����� ������ DefaultEvent�� �ھƳְ�����
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
