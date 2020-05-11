using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameBot : MonoBehaviour {

    FailMeneger fm;
    GameMenegrerScr gameMenegrer;
    WordConstructor constructor;
    List<string> vucabulary;
	void Start () {
        constructor = GetComponent<WordConstructor>();
        gameMenegrer = GetComponent<GameMenegrerScr>();
        vucabulary = new List<string>();
        //vucabulary = File.ReadAllLines(wdc.VucNotePath));
        bool isCorect = true;
        foreach (string input in File.ReadAllLines(constructor.VucNotePath))
        {
            if (input == "/") { isCorect = true; continue; }
            if (isCorect)
            {
                vucabulary.Add(input);
                isCorect = false;
            }
        }
        fm = GetComponent<FailMeneger>();

    }

    public void BotsTurn(char lastLetter)
    {
        List<string> rightWords = new List<string>();
        foreach (string input in vucabulary)
        { 
            if (input[0] == lastLetter && !constructor.wordsWasWritten.Contains(input)) rightWords.Add(input);
        }
        if (rightWords.Count == 0)
        {
            string word = constructor.lastWordText.text.ToUpper();
            foreach (string input in vucabulary)
            {
                if (input[0] == word[word.Length-2] && !constructor.wordsWasWritten.Contains(input)) rightWords.Add(input);
            }
        }
        int randomInd = Random.Range(0, rightWords.Count-1);
        string outputWord = rightWords[randomInd];
        constructor.ShowLastLetter(outputWord);
        constructor.wordsWasWritten.Add(outputWord);
        constructor.InstansiateWord(outputWord);
        gameMenegrer.CallMyTurn();
        Invoke("CheckForAcces", 0.05f);
    } 



    public void CheckForAcces()
    {
            List<string> rightWords = new List<string>();
            foreach (string input in vucabulary)
            {
                if (input[0] == constructor.lastLetter && !constructor.wordsWasWritten.Contains(input)) rightWords.Add(input);
            }
            
            if (rightWords.Count == 0)
            {
                fm.NoWordWithLastLetter();
                //constructor.InvokeStartTurn();
                return;
            }


        

        }

        
}
