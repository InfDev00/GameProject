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
            first = "식량";
            second = "사람";

        }
        else
        {
            number = this.army;
            first = "사람";
            second = "식량";
        }
        if (number > 0)
        {
            endingText.GetComponent<TextMeshProUGUI>().text = $"D+{this.day} 동안 살아남았습니다.\n" +
                                                $"{number}명의 {first}을 모았지만\n" +
                                                $"{second}이 없어서 끝났습니다.\n" +
                                                $"꽤 괜찮은 모험이었네요";

        }

        else
        {
            endingText.GetComponent<TextMeshProUGUI>().text = $"D+{this.day} 동안 살아남았습니다.\n" +
                                $"꽤 괜찮은 모험이었네요";
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
