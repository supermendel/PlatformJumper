using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton 
{
    private static Singleton instance;

    public static Singleton GetInstance ()
    {
        if (instance == null)
        {
            instance = new Singleton();
        }
        return instance;
    }

    public void Hello()
    {
        Debug.Log("HI");
    }

}
