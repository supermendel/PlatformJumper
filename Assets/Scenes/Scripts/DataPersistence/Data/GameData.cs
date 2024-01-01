using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public int score;
    public int[] highScore;

    public GameData()
    {
       this.score = 0;
        this.highScore= new int[]{ 0, 0, 0, 0, 0 };

    }
}
