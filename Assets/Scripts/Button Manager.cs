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
                TopBtn.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = buttons[2].text;
                MidBtn.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = buttons[1].text;
                BotBtn.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = buttons[0].text;
                break;
            case 2:
                TopBtn.SetActive(false);
                MidBtn.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = buttons[1].text;
                BotBtn.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = buttons[0].text;
                break;
            case 1:
                TopBtn.SetActive(false);
                MidBtn.SetActive(false);
                BotBtn.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = buttons[0].text;
                break;
            case 0:
                TopBtn.SetActive(false);
                MidBtn.SetActive(false);
                BotBtn.SetActive(false);
                break;
        }
    }

    void ButtonEvent(int buttonIndex)
    {
        if (nxtButtons[buttonIndex].text.Contains("[¿£µù]"))
        {
            GameManager.instance.GameEnding();
            return;
        }
        GameManager.instance.AddLife(this.nxtButtons[buttonIndex].change.life);
        GameManager.instance.AddSpeech(this.nxtButtons[buttonIndex].change.speech);
        GameManager.instance.AddForce(this.nxtButtons[buttonIndex].change.force);
        GameManager.instance.AddTactics(this.nxtButtons[buttonIndex].change.tactics);
        GameManager.instance.AddFood(this.nxtButtons[buttonIndex].change.food);
        GameManager.instance.AddArmy(this.nxtButtons[buttonIndex].change.army);

        foreach (var member in this.nxtButtons[buttonIndex].change.team)
        {
            if(!GameManager.instance.isTeam(member)) GameManager.instance.AddTeam(member);
        }


        GameManager.instance.SetGameEvent(this.nxtButtons[buttonIndex].nxtGameEvent);
    }

    public void ClickTopButton() { ButtonEvent(2); }
    public void ClickMidButton() { ButtonEvent(1); }
    public void ClickBotButton() { ButtonEvent(0); }

}
