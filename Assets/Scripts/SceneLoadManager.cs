using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class SceneLoadManager : MonoBehaviour
{
    [SerializeField] GameObject loadingCanvas;
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] WeaponCarousel weaponCarousel;
    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        audioMixer.SetFloat("MasterMusic", GlobalModel.Instance.MasterVolume);
        Debug.Log("weapon is "+ GlobalModel.Instance.startWeapon);
        weaponCarousel.SetWeapon(GlobalModel.Instance.startWeapon);

    }
    public void LoadScene()
    {
        StartCoroutine(LoadAscychronously(GlobalModel.Instance.map));
    }

    IEnumerator LoadAscychronously(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        loadingCanvas.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            textMeshProUGUI.text = ((int)(progress*100)).ToString();
            yield return null;
        }
    }
}
