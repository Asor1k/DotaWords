using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FailMeneger : MonoBehaviour {

    

    WordConstructor constructor;
    public Button errorOfNoWordButton;
    public Button errorOfWrittenWordButton;
    public Button errorOfWrongLastLetterButton;
    public Button errorOfWrongWordButton;

    private void Start()
    {
        constructor = GetComponent<WordConstructor>();

    }

    
    public void NoWordWithLastLetter()
    {
        string word = constructor.lastWordText.text;
        word = word.Remove(word.Length - 1);
        constructor.ShowLastLetter(word);
        errorOfNoWordButton.gameObject.SetActive(true);
    }

    public void WordWasWritten()
    {
        errorOfWrittenWordButton.gameObject.SetActive(true);
    }


    public void WrongLastLetter()
    {
        errorOfWrongLastLetterButton.gameObject.SetActive(true);
    }

    public void WrongWord()
    {
        errorOfWrongWordButton.gameObject.SetActive(true);  
    }
    
    public void DisableErrorButton(Button button)
    {
        button.gameObject.SetActive(false);
    }
	
    public void ExitGame()
    {
        Application.Quit();
    }

    public void DisplayError(string reason)
    {
        Debug.LogWarning("ERROR: " + reason);
    }

}
