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
    public bool isInfinity;
    public void ReduceCurrentAmmo()
    {
        if (!isInfinity)
        {
            ammoAmount--;
        }
        currentClip--;
    }

    public void IncreaseCurrentAmmo(int ammoAmount)
    {
        if (isInfinity)
        {
            Debug.LogError("Infinity weapon should not have this type of ammo");
            return;
        }
        this.ammoAmount += ammoAmount;
    }

    public void Reload()
    {
        if (!isInfinity)
        {
            int reloadAmount = Clip - currentClip;
            if (ammoAmount-currentClip >= reloadAmount)
            {
                currentClip = Clip;
            }
            else
            {
                Debug.Log("yes");
                currentClip = ammoAmount;
            }
        }
        else
        {
            currentClip = Clip;
        }


    }

    public int GetCurrentAmmo()
    {
        return ammoAmount;
    }

    public int GetCurrentRemain()
    {
        if (ammoAmount > currentClip)
        {
            return ammoAmount - currentClip;
        }
        else
        {
            return 0;
        }
    }

    public int GetCurrentClip()
    {
        return currentClip;
    }

}
