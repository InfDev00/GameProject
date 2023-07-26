using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndingUI : MonoBehaviour
{
    public GameObject endingText;
    private int day;
    private int food;
    private int army;

    void Awake()
    {
        this.day = SceneValue.globalDay;
        this.food = SceneValue.globalFood;
        this.army = SceneValue.globalArmy;
        UpdateText();
    }

    void UpdateText()
    {
        endingText.GetComponent<TextMeshProUGUI>().text = "";
        Debug.Log("text");
        string first = "";
        string second = "";
        int number = this.food;
        if (number > 0)
        {
            first = "�ķ�";
            second = "���";

        }
        else
        {
            number = this.army;
            first = "���";
            second = "�ķ�";
        }
        if (number > 0)
        {
            endingText.GetComponent<TextMeshProUGUI>().text = $"D+{this.day} ���� ��Ƴ��ҽ��ϴ�.\n" +
                                                $"{number}���� {first}�� �������\n" +
                                                $"{second}�� ��� �������ϴ�.\n" +
                                                $"�� ������ �����̾��׿�";

        }

        else
        {
            endingText.GetComponent<TextMeshProUGUI>().text = $"D+{this.day} ���� ��Ƴ��ҽ��ϴ�.\n" +
                                $"�� ������ �����̾��׿�";
        }
    }

    public void NewGame()
    {
        GameManager.instance.GameInit();
        SceneManager.LoadScene("GameScene");
    }

    public void EndGame()
    {
        GameManager.instance.GameInit();
        SceneManager.LoadScene("InitialScene");
    }
}
