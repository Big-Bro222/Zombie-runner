using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClipboardController : MonoBehaviour
{
    [SerializeField] TextMeshPro Score;
    public void UpdateUI()
    {
        Score.text = GlobalModel.Instance.HighestScore.ToString();
    }
}
