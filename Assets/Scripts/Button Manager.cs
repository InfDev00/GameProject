using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    public GameObject TopBtn;
    public GameObject MidBtn;
    public GameObject BotBtn;

    private GameEventSO.Button[] nxtButtons;

    public void SetButton(GameEventSO.Button[] buttons)
    {

        this.nxtButtons = buttons;
        switch (buttons.Length)
        {
            case 3:
                TopBtn.SetActive(true);
                MidBtn.SetActive(true);
                BotBtn.SetActive(true);
                TopBtn.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = buttons[2].text;
                MidBtn.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = buttons[1].text;
                BotBtn.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = buttons[0].text;
                break;
            case 2:
                TopBtn.SetActive(false);
                MidBtn.SetActive(true);
                BotBtn.SetActive(true);
                MidBtn.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = buttons[1].text;
                BotBtn.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = buttons[0].text;
                break;
            case 1:
                TopBtn.SetActive(false);
                MidBtn.SetActive(false);
                BotBtn.SetActive(true);
                BotBtn.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = buttons[0].text;
                break;
            case 0:
                TopBtn.SetActive(false);
                MidBtn.SetActive(false);
                BotBtn.SetActive(false);
                Debug.Log($"{buttons.Length}");
                break;
        }
    }

    void ButtonEvent(int buttonIndex)
    {
        if (nxtButtons[buttonIndex].text.Contains("반격한다"))
        {
            GameManager.instance.PlayerCounter();
        }
        else if (nxtButtons[buttonIndex].text.Contains("수비한다"))
        {
            GameManager.instance.PlayerDefence();
        }

        else if (nxtButtons[buttonIndex].text.Contains("붉은 바람을 공격한다"))
        {
            GameManager.instance.EnemyAttacked("붉은 바람");
        }
        else if (nxtButtons[buttonIndex].text.Contains("녹색 번개를 공격한다"))
        {
            GameManager.instance.EnemyAttacked("녹색 번개");
        }
        else if (nxtButtons[buttonIndex].text.Contains("푸른 불꽃을 공격한다"))
        {
            GameManager.instance.EnemyAttacked("푸른 불꽃");
        }

        else
        {
            GameManager.instance.AddSpeech(this.nxtButtons[buttonIndex].change.speech);
            GameManager.instance.AddForce(this.nxtButtons[buttonIndex].change.force);
            GameManager.instance.AddTactics(this.nxtButtons[buttonIndex].change.tactics);
            GameManager.instance.AddFood(this.nxtButtons[buttonIndex].change.food);
            GameManager.instance.AddArmy(this.nxtButtons[buttonIndex].change.army);

            foreach (var enemy in this.nxtButtons[buttonIndex].change.enemy)
            {
                if (!GameManager.instance.isEnemy(enemy)) GameManager.instance.AddEnemy(enemy);
            }
        }


        GameManager.instance.SetGameEvent(this.nxtButtons[buttonIndex].nxtGameEvent);
    }

    public void ClickTopButton() { ButtonEvent(2); }
    public void ClickMidButton() { ButtonEvent(1); }
    public void ClickBotButton() { ButtonEvent(0); }

}
