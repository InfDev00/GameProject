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
            first = "�ķ�";
            second = "���";

        }
        else
        {
            number = GameManager.instance.GetArmy();
            first = "���";
            second = "�ķ�";
            if(number > 0)
            {
                endingText.GetComponent<TextMeshProUGUI>().text = $"D+{GameManager.instance.GetDay()} ���� ��Ƴ��ҽ��ϴ�.\n" +
                                                    $"{number}���� {first}�� �������\n" +
                                                    $"{second}�� ��� �������ϴ�.\n" +
                                                    $"�� ������ �����̾��׿�";

            }

            else
            {
                endingText.GetComponent<TextMeshProUGUI>().text = $"D+{GameManager.instance.GetDay()} ���� ��Ƴ��ҽ��ϴ�.\n" +
                                    $"�� ������ �����̾��׿�";
            }
        }
    }
}
