using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] WeaponSwitcher weaponSwitcher;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] TextMeshProUGUI textMesh;

    private static DeathHandler _instance;
    public static DeathHandler Instance { get { return _instance; } }


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

    private void Start()
    {
        gameOverCanvas.enabled = false;
    }

    public void HandleDeath(DeathReason deathReason)
    {
        gameOverCanvas.enabled = true;
        Time.timeScale = 0;
        weaponSwitcher.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GlobalModel.Instance.Score.Add(ScoreSystem.Instance.TotalScore());
        audioMixer.SetFloat("MasterMusic", -60);


        switch (deathReason)
        {
            case DeathReason.BaseMentDestroyed:
                textMesh.text = "No one can survive without supplies";
                break;
            case DeathReason.PlayerDead:
                textMesh.text = "You have become Z's brunch !!!";
                break;
            case DeathReason.TeammatesDead:
                textMesh.text = "Life doesn't worth more than friendship";
                break;
            default:
                Debug.LogError("deathreason not included");
                break;

        }

    }

}
