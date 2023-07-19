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

    public void ClickBtn0() { GameManager.instance.SetBtnTriger(0); }
    public void ClickBtn1() { GameManager.instance.SetBtnTriger(1); }
    public void ClickBtn2() { GameManager.instance.SetBtnTriger(2); }

}
