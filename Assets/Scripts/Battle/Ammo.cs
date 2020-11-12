using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Ammo
{
    public AmmoType ammoType;
    public int ammoAmount;
    public int Clip;
    public int currentClip;

    public void ReduceCurrentAmmo()
    {
       ammoAmount--;
       currentClip--;
    }

    public void IncreaseCurrentAmmo(int ammoAmount)
    {
        ammoAmount += ammoAmount;
    }

    public void Reload()
    {
        int reloadAmount = Clip - currentClip;
        currentClip = Clip;
        ammoAmount -= reloadAmount;
    }

    public int GetCurrentAmmo()
    {
        return ammoAmount;
    }

    public int GetCurrentClip()
    {
        return currentClip;
    }

}
