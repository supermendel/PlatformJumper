using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TMPro;
using UnityEditor;
using UnityEngine;


public class ScoreManager : MonoBehaviour,IDataPersistence
{
    [SerializeField] int[] highScores;
    
    private static ScoreManager scoreManagerinstance;
    public TextMeshProUGUI textOne;
    public TextMeshProUGUI textTwo;
    public TextMeshProUGUI textThree;
    public TextMeshProUGUI textFour;
    public TextMeshProUGUI textFive;
    int score = 0;
    

    private void Awake()
    {     
        scoreManagerinstance = this;
    }
    private void Start()
    {
        AddPoints();
    }
    
     
    public  void AddPoints()
    {
        if (score > highScores.Min())
        {         
            SetPoints(score);
        }


        textOne.text = highScores[0].ToString();
        textTwo.text = highScores[1].ToString();
        textThree.text = highScores[2].ToString();
        textFour.text = highScores[3].ToString();
        textFive.text = highScores[4].ToString();

    }
    public void SetPoints(int points)
    {
       highScores[Array.IndexOf(highScores, highScores.Min())] = points;
       Array.Sort(highScores);
       Array.Reverse(highScores);

    }

    public void LoadData(GameData data)
    {
        this.score= data.score;
        for(int i = 0;i<highScores.Length;i++) 
        {
            this.highScores[i] = data.highScore[i];
        }
       
    }

    public void SaveData(ref GameData data)
    {
        for (int i = 0; i < highScores.Length; i++)
        {
            data.highScore[i] = this.highScores[i];
        }

    }
   
}
