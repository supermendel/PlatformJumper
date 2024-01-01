using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour, IDataPersistence
{
    public GameManager instance;
    public GameObject platform;
    public GameObject player;
    public GameObject[] platforms;
    public Transform startPosition;
    public GameObject startPlatform;
    public float distance = 5f;
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI deathPointsText;
    public GameObject deathPanel;
    public int points = 0;
    public GameObject menuButton;
    public GameObject restartButton;
    public GameObject soundButton;
    public GameObject background;

    private int maxPlatforms = 10;
    private float distance1 = 3f;
    private Platforms platformScript;
    private int currentPlatforms = 0;
    private int totalPlatforms = 0;
    private GameObject playerClone;
    private GameObject newPlatform;
    
    private SpriteRenderer platformSpriteRender;
    

    [SerializeField] Sprite greenSprite;
    [SerializeField] Sprite dragonTileSprite;
    [SerializeField] Sprite fantasyTileSprite;
    [SerializeField] Sprite blueSprite;
    [SerializeField] Sprite muteButtonSprite;
    [SerializeField] Sprite soundButtonSprite;
    [SerializeField] Transform PlatformStorage;
     

    void Awake()
    {
        instance = this;

    }
    private void Start()
    {
        platforms = new GameObject[10];
        points = 0;
        playerClone = Instantiate(player);
        SpawnPlatforms();
    }

    void Update()
    {
        SpawnPlatforms();
        if (playerClone.GetComponent<PlayerMovement>().isGameOn)
        {
            StartPlatformMovement();
            background.GetComponent<BackgroundScript>().enabled = true;
        }
        PlatformCounter();
        PointsUpdate(points);

        Dead();
    }


    private void setPlayer(GameObject platform)
    {
        platformScript = platform.GetComponent<Platforms>();
        platformScript.player = playerClone;
    }

    private void SpawnPlatforms()
    {
        Vector3 position = new Vector3(UnityEngine.Random.Range(-5, 7), distance1, 1);
        Vector3 startPlatformPos = new Vector3(0, -2, 1);

        while (totalPlatforms < 1000 && currentPlatforms < maxPlatforms)
        {
            if (totalPlatforms == 0)
            {
                GameObject startPlatformClone = Instantiate(startPlatform, startPlatformPos, Quaternion.identity, PlatformStorage);
                startPlatformClone.GetComponent<Platforms>().player = playerClone;
                platforms[totalPlatforms] = startPlatformClone;
                totalPlatforms++;
                currentPlatforms++;

            }

            else
            {
                newPlatform = Instantiate(platform, position, Quaternion.identity, PlatformStorage);
                ChangePlatformSprite(newPlatform);
                setPlayer(newPlatform);
                platforms[currentPlatforms] = newPlatform;
                currentPlatforms++;
                distance1 += 3.5f;
                totalPlatforms++;
                position = new Vector3(UnityEngine.Random.Range(-4, 6), distance1, 0);
            }
        }


    }

    private void StartPlatformMovement()
    {

        if (playerClone.GetComponent<PlayerMovement>().isGameOn && playerClone.GetComponent<PlayerMovement>().isAlive)
        {
            foreach (GameObject platfor in platforms)
            {
                if (platfor == null) continue;
                platfor.GetComponent<Platforms>().PlatformMovement();
            }

        }
    }
    private void PlatformCounter()
    {
        foreach (GameObject platfor in platforms)
        {
            if (platfor == null) continue;
            if (platfor.GetComponent<Platforms>().IsReseting())
            {
                totalPlatforms++;
                ChangePlatformSprite(platfor);
                points += 50;

            }
        }
    }
    private void ChangePlatformSprite(GameObject platforms)
    {
        platformSpriteRender = platforms.GetComponent<SpriteRenderer>();
        int num = totalPlatforms / 100;

        switch (num)
        {
            case 0:
                platformSpriteRender.sprite = greenSprite;

                break;
            case 1:
                platformSpriteRender.sprite = blueSprite;
                break;
            case 2:
                platformSpriteRender.sprite = dragonTileSprite;
                break;
            case 3:
                platformSpriteRender.sprite = fantasyTileSprite;
                break;


        }
    }

    private void Dead()
    {
        if (!playerClone.GetComponent<PlayerMovement>().isAlive)
        {

            StartCoroutine(DeathScreen());
        }
    }
    IEnumerator DeathScreen()
    {
        DataPersistenceManager.instance.SaveGame();
        deathPointsText.text = points.ToString();
        yield return new WaitForSeconds(1);
        deathPanel.SetActive(true);

    }
    private void PointsUpdate(int value)
    {
        pointsText.text = value.ToString();
    }
    public void MenuClick()
    {
        menuButton.GetComponent<AudioSource>().Play();
        StartCoroutine(LoadMenu());
    }
    public void RestartClick()
    {
        restartButton.GetComponent<AudioSource>().Play();
        StartCoroutine(LoadRestart());

    }
    public void SoundClick()
    {
        var audioSource = this.GetComponent<AudioSource>();
        var soundButtonSpriteVar = soundButton.GetComponent<Image>();
        if (audioSource.volume != 0)
        {
            audioSource.volume = 0;
            soundButtonSpriteVar.sprite = muteButtonSprite;
        }
        else
        {
            audioSource.volume = 0.6f;
            soundButtonSpriteVar.sprite = soundButtonSprite;
        }
        
    }
    IEnumerator LoadMenu()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(0);

    }
    IEnumerator LoadRestart()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(1);
    }

    public void LoadData(GameData data)
    {
        this.points = data.score;

    }

    public void SaveData(ref GameData data)
    {
        data.score = this.points;
    }
}
