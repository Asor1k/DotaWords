using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

 
enum Opponents { Bot,Player} 

public class WordConstructor : MonoBehaviour {

    public Word word;
    private int indexOfWord = 0;
    GameBot bot;

    GameMenegrerScr menegrerScr;

    public string VucNotePath;
    public string KeshNotePath;

    private bool isCorrect = true;

    public char lastLetter;
    public List<string> wordsWasWritten = new List<string>();
    public int numberOfBoard = 1;
    public int numberToChangeBoard = 7;
    public string wordInput;
    int wordsOnBoard = 0;

    FailMeneger fm;

    public Text lastWordText;
    public Text AllWordsText;
    public Text lastLetterText;
    public InputField inputField;
    [SerializeField]
    Opponents opponent = Opponents.Bot;

    void Start () {
        bot = GetComponent<GameBot>();
		VucNotePath = Application.dataPath + @"/DotaVocabulary.txt";
        KeshNotePath = Application.dataPath + @"/DotaKeshPastWords.txt";
        menegrerScr = GetComponent<GameMenegrerScr>();
        CreateFirstLetter();
        wordsWasWritten.Add("***");
        fm = GetComponent<FailMeneger>();
    }

    public void NextBoard()
    {
        AllWordsText.text = "";
        AllWordsText.text = lastWordText.text;
        numberOfBoard++;
    }

    

    public void StartFounding()
    {
        if (menegrerScr.isMyTurn)
        {
            FindWord(inputField.text);
            wordInput = inputField.text;
        }
    }

    void FindWord(string wordIn)
    {
        wordIn = wordIn.ToLower();
        int i = 1;
        int j = 0;
        string correctWord = "ERROR";
        foreach (string input in File.ReadAllLines(VucNotePath))
        {
            if (j == 0) correctWord = input;
            if (input == "/") { i++; j = 0; continue; } 
            if (input != wordIn) {
                j++;
                continue;
            }
            if (input == wordIn)
            {
                
                if (correctWord.Length == 0) return;
                if (correctWord[0]!= lastLetter)
                {
                    
                    fm.WrongLastLetter();
                    return;
                }
                if (wordsWasWritten.Contains(correctWord))
                {
                    fm.WordWasWritten();
                    return;
                }
                
                ShowLastLetter(correctWord);
                wordsWasWritten.Add(correctWord);
                InstansiateWord(correctWord);
                
                if (wordsOnBoard >= numberToChangeBoard)
                {
                    wordsOnBoard = 0;
                    NextBoard();
                }
                InvokeStartTurn();
                menegrerScr.CallBotTurn();
                indexOfWord = i;
                isCorrect = false;
                return;
            }
        }
        fm.WrongWord();

    }

    public void InvokeStartTurn()
    {
        Invoke("GiveTurnToOther", 0.1f);
    }

    void GiveTurnToOther()
    {

        if (opponent == Opponents.Bot) bot.BotsTurn(lastLetter);
        if (opponent == Opponents.Player)
        {
        } //turn of a player
    }

    void CreateFirstLetter()
    {
        lastLetter = lastLetterText.text.ToCharArray()[0];
        wordsWasWritten.Add(lastWordText.text);
        /* string t = lastLetter.ToString();
         t = t.ToLower();
         lastLetter = t.ToCharArray()[0];
        */
    }
        
    private void OnApplicationQuit()
    {
        ClearFile();
    }
    void ClearFile()
    {
        
    }

    public void ShowLastLetter(string last)
    {
        wordsOnBoard++;
        string t = last;
        t = t.ToUpper();
        lastLetter = t[t.Length - 1];
        lastWordText.text = last;
        string stLast = lastLetter.ToString().ToUpper();
        lastLetterText.text = stLast;

    }



    public void InstansiateWord(string wordToInstansiate)
    {
        AllWordsText.text += "\n" + wordToInstansiate;
    }

    void Update () {
		
	}
}
