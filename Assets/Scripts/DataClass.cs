#nullable enable
using UnityEngine;
using System;

public class DataClass : MonoBehaviour
{
    private static DataClass? instance;

    public static DataClass Instance
    {
        #nullable disable
        get { return instance; }
        #nullable enable
    }

    private int highscore;

    public int Highscore {
        get { return highscore; }
        set
        {
            if (value < highscore) {
                throw new ArgumentOutOfRangeException("Can't decrease highscore!");
            }
            else
            {
                highscore = value;
            };
        }
    }

    public void Awake()
    {
        instance = this;
    }
}
