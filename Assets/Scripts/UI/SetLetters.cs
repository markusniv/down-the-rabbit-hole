using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetLetters : MonoBehaviour
{
    Text _dialog;
    string dialogText;
    char[] dialogArray;

    int counter;
    // Start is called before the first frame update
    void Start()
    {
        _dialog = GetComponent<Text>();
        dialogText = "Buy something, will ya?";
        dialogArray = dialogText.ToCharArray();

        counter = 0;
    }

    private void FixedUpdate()
    {
        if (counter < dialogText.Length)
        {
            AddLetter(counter);
            counter++;
        }
    }

    void AddLetter(int index)
    {
        _dialog.text += dialogArray[index];
    }
}
