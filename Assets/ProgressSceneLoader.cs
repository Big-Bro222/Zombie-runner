using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProgressSceneLoader : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Slider slider;


    private AsyncOperation asyncOperation;
    private Canvas canvas;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    public void LoadScene(string sceneName)
    {
        UpdateProgressUI(0);
        gameObject.SetActive(true);
        StartCoroutine(BeginLoad(sceneName));

    }

    private void UpdateProgressUI( float progress)
    {
        textMeshPro.text = (int)(progress * 100f) + "%";
        slider.value = progress;
    }

    IEnumerator BeginLoad(string sceneName)
    {
        asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncOperation.isDone)
        {
            UpdateProgressUI(asyncOperation.progress);
            yield return null;

        }
        UpdateProgressUI(asyncOperation.progress);
        asyncOperation = null;
        gameObject.SetActive(false);
    }
}
