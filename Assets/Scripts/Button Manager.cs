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

    public void SetButton(GameEventSO.Button[] buttons, GameEventSO gameEvent)
    {
        if (GameManager.instance.GetCurrentState() == "Default") this.nxtButtons = gameEvent.GetNextButtons();

        this.nxtButtons = buttons;
        switch (this.nxtButtons.Length)
        {
            case 3:
                TopBtn.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = nxtButtons[2].text;
                MidBtn.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = nxtButtons[1].text;
                BotBtn.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = nxtButtons[0].text;
                break;
            case 2:
                TopBtn.SetActive(false);
                MidBtn.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = nxtButtons[1].text;
                BotBtn.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = nxtButtons[0].text;
                break;
            case 1:
                TopBtn.SetActive(false);
                MidBtn.SetActive(false);
                BotBtn.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = nxtButtons[0].text;
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
        GameManager.instance.SetGameEvent(this.nxtButtons[buttonIndex].nxtGameEvent);
    }

    public void ClickTopButton() { ButtonEvent(2); }
    public void ClickMidButton() { ButtonEvent(1); }
    public void ClickBotButton() { ButtonEvent(0); }

}
