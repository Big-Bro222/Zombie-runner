using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClipboardController : MonoBehaviour
{
    [SerializeField] TextMeshPro Score;
    [SerializeField] GameObject textlistPrefab;
    [SerializeField] Transform textlistPanel;


    private void Start()
    {
        List<float> scoreList = GlobalModel.Instance.Score;
        scoreList.Sort();
        scoreList.Reverse();
        for(int i = 0; i < scoreList.Count; i++)
        {
            GameObject text = Instantiate(textlistPrefab, textlistPanel);
            string listText = (i+1) + ". " + scoreList[i] + " :)";
            text.GetComponent<TextMeshProUGUI>().text = listText;
        }
    }
    //public void UpdateUI()
    //{
    //    Score.text = GlobalModel.Instance.HighestScore.ToString();
    //}
}
