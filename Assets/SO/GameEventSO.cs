using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEventSO", menuName = "Scriptable Object/GameEventSO", order = 0)]
public class GameEventSO : ScriptableObject
{

    [System.Serializable]
    public struct Button
    {
        public string text;
        public GameEventSO nxtGameEvent;

        public Button(string text)
        {
            this.text = text;
            this.nxtGameEvent = null;
        }
    }

    [TextArea(3, 15)]
    public string stateText;
    public Button[] buttons;

    public Button[] GetNextButtons()
    {
        List<Button> buttons = new List<Button>();

        int[] indexes = new int[this.buttons.Length];
        for (int i = 0; i < indexes.Length; ++i) indexes[i] = i;
        List<int> _indexes = new List<int>(indexes);
        for (int i = 0; i < 3 && i < indexes.Length; ++i)
        {
            var index = Random.Range(0, _indexes.Count);
            var _index = _indexes[index];
            buttons.Add(this.buttons[_index]);
            _indexes.RemoveAt(index);
        }
        return buttons.ToArray();
    }

    public Button[] GetNextButtons(List<string> inputText)
    {
        List<Button> buttons = new List<Button>();

        int[] indexes = new int[inputText.Count];
        for (int i = 0; i < indexes.Length; ++i) indexes[i] = i;
        List<int> _indexes = new List<int>(indexes);
        for (int i = 0; i < 3 && i < indexes.Length; ++i)
        {
            var index = Random.Range(0, _indexes.Count);
            var _index = _indexes[index];
            Button button = new Button(inputText[_index]);
            buttons.Add(button);
            _indexes.RemoveAt(index);
        }
        return buttons.ToArray();
    }
}
