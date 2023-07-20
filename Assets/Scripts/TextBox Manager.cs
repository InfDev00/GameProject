using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextBoxManager : MonoBehaviour
{
    public GameObject StateText;

    public void SetTextBox(string stateText)
    {
        int GameYear = System.DateTime.Now.Year + 1000;
        int playerAge = GameYear % 100;
        playerAge = playerAge < 20 ? 20 : playerAge;
        stateText = stateText.Replace("[나이]", $"{playerAge}");
        stateText = stateText.Replace("[연도]", $"{GameYear}");

        StateText.GetComponent<TextMeshProUGUI>().text = stateText;
    }
}
