using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] PostProcessVolume postprocessVolume;
    [SerializeField] AudioSource fastHeartBiteSFX;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    [SerializeField] float healingSecondsPerPoint;
    private int hitPoints;
    private bool isDieing;
    private float healingClock;
    private bool TestMode;

    Vignette vignette;
    Bloom bloom;
    private void Start()
    {
        hitPoints = health;
        postprocessVolume.profile.TryGetSettings(out vignette);
        postprocessVolume.profile.TryGetSettings(out bloom);

        vignette.enabled.value = false;
        bloom.intensity.value = 0;
        UpdateHealthView();
        isDieing = false;
        healingClock = 0;
        TestMode = false;

    }

    public void TakeDamage(int damage)
    {
        if (TestMode)
        {
            return;
        }
        hitPoints -= damage;
        if (hitPoints < 0)
        {
            hitPoints = 0;
        }
        UpdateHealthView();

        if (hitPoints <= 0)
        {
            DeathHandler.Instance.HandleDeath(DeathReason.PlayerDead);
        }
    }


    public void Heal(int HealthPoint)
    {
        hitPoints += HealthPoint;
        if (hitPoints>health) { hitPoints = health; }
            UpdateHealthView();
    }

    public void UpdateHealthView()
    {
        textMeshProUGUI.text = hitPoints.ToString();
    }

    private void PlayOneShotContinues(AudioSource audioSource)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TestMode = !TestMode;
        }

        if (((float)hitPoints / (float)health) <= 0.3f)
        {
            isDieing = true;
            bloom.intensity.value = 3;

            if (!vignette.enabled.value)
            {
                vignette.enabled.value = true;
            }

            float intensityValue = Mathf.PingPong(Time.time * 0.3f,0.4f);
            vignette.intensity.value = intensityValue;
            PlayOneShotContinues(fastHeartBiteSFX);
        }
        else
        {
            bloom.intensity.value = 0;
            isDieing = false;
            if (vignette.enabled.value)
            {
                vignette.intensity.value = 0;
                vignette.enabled.value = false;
                fastHeartBiteSFX.Stop();
            }
        }

        if (hitPoints < health)
        {
            SelfHealing(healingSecondsPerPoint);
        }
        else
        {
            healingClock = 0;
        }
    }

    private void SelfHealing(float timePerpoint)
    {
        if (healingClock > timePerpoint)
        {
            healingClock = 0;
            hitPoints+=10;
            UpdateHealthView();
        }
        else
        {
            healingClock += Time.deltaTime;

        }
    }
}
