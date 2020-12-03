using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public sealed class GlobalModel
{
    static GlobalModel _instance = null;
    static readonly object _padlock = new object();

    public GlobalModel()
    {
        HighestScore = 100;
        Score = new List<float>();
        Score.Add(HighestScore);
        ZombieTypes = new List<string>();
        ZombieTypes.Add("Zombie_Underwear");
        startWeapon = "AK47";
        MasterVolume = 0;
        BGMVolume = 0;
        SFXVolume = 0;
        map = "Desert";
    }

    public static GlobalModel Instance
    {
        get
        {
            lock (_padlock)
            {
                if (_instance == null)
                {
                    _instance = new GlobalModel();
                }

                return _instance;
            }
        }
    }

    public string startWeapon { get; set; }
    public string map { get; set; }
    public float MasterVolume { get; set; }
    public float BGMVolume { get; set; }
    public float SFXVolume { get; set; }
    public int HighestScore { get; set; }

    public List<string> ZombieTypes { get; set; }
    public List<float> Score { get; set; }

}