using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextBoxManager : MonoBehaviour
{
    public GameObject TextBox;

    public void SetTextBox(string stateText)
    {
        if (stateText.Contains("<전투>")) GameManager.instance.SetCurrentState("Battle");
        else if (stateText.Contains("<전직>")) GameManager.instance.SetCurrentState("Job");
        else GameManager.instance.SetCurrentState("Default");
        stateText = stateText.Replace("[적]", $"{GameManager.instance.GetCurrentEnemy()}");

        TextBox.GetComponent<TextMeshProUGUI>().text = stateText;
    }
}
