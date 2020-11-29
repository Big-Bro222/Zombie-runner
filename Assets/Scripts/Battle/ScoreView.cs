using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Update View
public class ScoreView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ScoreText;

    private static ScoreView _instance;
    public static ScoreView Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    private int currentScore;
    private void Start()
    {
        currentScore = 0;
        StartCoroutine(UpdateCurrentScore());
    }
    public void UpdateScore()
    {
        string score = ScoreSystem.Instance.TotalScore().ToString();
        ScoreText.text = score;

    }


    public IEnumerator UpdateCurrentScore()
    {
        //while you are far enough away to move
        while (currentScore <= ScoreSystem.Instance.TotalScore())
        {
            ScoreText.text = currentScore.ToString();
            currentScore++;
            //wait for a frame and repeat
            yield return 0;
        }
    }
}
