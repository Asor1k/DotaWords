using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class RestartScr : MonoBehaviour {
    public int score;
    int highScore;

    public Text scoreText;

	void Start () {
        score = PlayerPrefs.GetInt("score");
        highScore = PlayerPrefs.GetInt("highScore");
        scoreText.text = score.ToString();
    }



    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
	
}
