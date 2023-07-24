using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndingUI : MonoBehaviour
{
    public GameObject endingText;

    void Awake()
    {
        UpdateText();
    }

    void UpdateText()
    {
        string first = "";
        string second = "";
        int number = GameManager.instance.GetFood();
        if (number > 0)
        {
            first = "식량";
            second = "사람";

        }
        else
        {
            number = GameManager.instance.GetArmy();
            first = "사람";
            second = "식량";
            if(number > 0)
            {
                endingText.GetComponent<TextMeshProUGUI>().text = $"D+{GameManager.instance.GetDay()} 동안 살아남았습니다.\n" +
                                                    $"{number}명의 {first}을 모았지만\n" +
                                                    $"{second}이 없어서 끝났습니다.\n" +
                                                    $"꽤 괜찮은 모험이었네요";

            }

            else
            {
                endingText.GetComponent<TextMeshProUGUI>().text = $"D+{GameManager.instance.GetDay()} 동안 살아남았습니다.\n" +
                                    $"꽤 괜찮은 모험이었네요";
            }
        }
    }
}
