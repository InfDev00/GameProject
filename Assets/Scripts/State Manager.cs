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
        StateText.GetComponent<TextMeshProUGUI>().text = stateText;
    }
}
