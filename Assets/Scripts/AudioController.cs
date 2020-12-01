using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioClip[] bgMs;
    AudioSource audioSource;
    [SerializeField] AudioMixer audioMixer;

    int musicIndex;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        musicIndex =0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            Debug.Log("Change Music");
            audioSource.clip = bgMs[musicIndex];
            audioSource.Play();
            musicIndex++;
            if (musicIndex > bgMs.Length - 1)
            {
                musicIndex = 0;
            }
        }
    }

    public void Switch()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();

        }else
        {
            audioSource.Play();

        }
    }

    public void SetMasterVolume(float volume)    
    {
        audioMixer.SetFloat("MasterMusic", volume);
        GlobalModel.Instance.MasterVolume = volume;
    }

    public void SetBGMVolume(float volume)    // 控制背景音乐音量的函数
    {
        audioMixer.SetFloat("BGM", volume);
        GlobalModel.Instance.BGMVolume = volume;

    }

    public void SetSFXVolume(float volume)    // 控制音效音量的函数
    {
        audioMixer.SetFloat("SFX", volume);
        GlobalModel.Instance.SFXVolume = volume;

    }



}
