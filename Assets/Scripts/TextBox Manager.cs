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
        if (stateText.Contains("<����>")) GameManager.instance.SetCurrentState("Battle");
        else if (stateText.Contains("<����>")) GameManager.instance.SetCurrentState("Job");
        else GameManager.instance.SetCurrentState("Default");
        stateText = stateText.Replace("[��]", $"{GameManager.instance.GetCurrentEnemy()}");

        TextBox.GetComponent<TextMeshProUGUI>().text = stateText;
    }
}
