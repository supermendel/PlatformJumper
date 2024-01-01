using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SocialPlatforms;


public class Platforms : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private float fallSpeed = 2f;
    public GameObject player = null;
    public bool startPlatform;
    public bool platformReset = false;


    
    public void Start()
    {
        if (player == null) { }
        else
        {
            playerMovement = player.GetComponent<PlayerMovement>();
          
        }
    }
    private void Update()
    {
        ResetPlatform();

        var t = this.transform.position;
        t.y -= playerMovement.DeltaY;
        this.transform.position = t;   
        
    }
 
    public void PlatformMovement()
    {
        var newPos = this.transform.position;
        newPos.y += Vector3.down.y * fallSpeed * Time.deltaTime;
        this.transform.position = newPos;

    }

    

    public void ResetPlatform()
    {
        platformReset = false;

        Vector3 destination = new Vector3(Random.Range(-5,7),29f,0);
       
        if (transform.position.y < -3f)
        {          
            if (startPlatform == true) Destroy(this.gameObject);
            this.transform.position = Vector2.Lerp(transform.position, destination,1);
            platformReset = true;
            IsReseting();
        }
        
    }
    public bool IsReseting()
    {
        return platformReset;
    }

}

    
    