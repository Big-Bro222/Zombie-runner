using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public sealed class GlobalModel
{
    static GlobalModel _instance = null;
    static readonly object _padlock = new object();

    public GlobalModel()
    {
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
    public bool soundOn { get; set; }
}