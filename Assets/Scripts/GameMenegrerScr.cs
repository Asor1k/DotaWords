using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenegrerScr : MonoBehaviour {

    public Text TimeText;
    public Text OppPointsText;
    public Text MyPointsText;
    WordConstructor wordConstructor;
    public bool isMyTurn = true;
    public int looseTime;
    int counter = 0;
    private int myPoints = 0;
    private float time;

	void Start ()
    {
        wordConstructor = GetComponent<WordConstructor>();
	}
	
    public void CallBotTurn()
    {
        isMyTurn = false;
        myPoints += wordConstructor.wordsWasWritten[wordConstructor.wordsWasWritten.Count - 1].Length / Mathf.RoundToInt(time)+wordConstructor.wordInput.Length;
        MyPointsText.text = myPoints.ToString();
        TimeText.text = "00:00";
        time = 0;
    }

    public void CallMyTurn()
    {
        isMyTurn = true;
    }

	void Update () {
        time += Time.deltaTime;
        if (counter >= 3 && isMyTurn)
        {
            string timeStr = "";
            if (time < 1f)
            {
                timeStr += "00:";
            }
            else
            if (time < 10)
            {
                timeStr += "0";
                timeStr += Mathf.Round(time);
                timeStr += ":";
            }
            else
            {
                timeStr += Mathf.Round(time);
                timeStr += ":";
            }
            timeStr += System.Math.Round(time%10, 5).ToString()[2]+""+ System.Math.Round(time%10, 4).ToString()[3];
            if (timeStr.Length < 5) timeStr += "0";
            if (time > looseTime)
            {
                Loose();
            }
            if (timeStr[0] == '0' && time >= 9.5f) timeStr = timeStr.Remove(0, 1);
            TimeText.text = timeStr;
            counter = 0;
        }
        counter++;
	}


    void Loose()
    {
        isMyTurn = false;
        PlayerPrefs.SetInt("score", myPoints);
        if(PlayerPrefs.GetInt("highScore")<myPoints)
            PlayerPrefs.SetInt("highScore", myPoints);
        SceneManager.LoadScene("looseScene");
    }

}
