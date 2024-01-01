using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
   
    [SerializeField] float moveSpeed;
    private float endLocation = -4.4f;

   


    void Update()
    {
        Moving();
    }
    public void Moving()
    {
        if (!IsDone())
        {
            transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
            
        }
    }
    private bool IsDone()
    {
        return transform.position.y <= endLocation;
    }
}