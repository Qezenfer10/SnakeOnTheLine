using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{

    #region
    public static GameManager instance;

    public int Win = -1;
    private void Awake()
    {
        if (instance == null)
        instance = this;
    }

    #endregion

    public float GameTime;
    public AudioSource musicSound;
    public AudioSource SFXSound;
    public AudioSource BackGroundSound;
    public float phoneBatery;

    public GameObject player;
    public GameObject snake;

    public GameObject phone;

    public bool lightOn;
    public GameObject flashLight;

    public Camera MainCamera;

    public DateTime TimeSpan = DateTime.Now;

    public List<String> numbers;



    public bool isPhone;
    void Start()
    {
        GameTime = 0;
        StartCoroutine(Timer());
        isPhone = true;
        //snake.GetComponent<NavMeshAgent>().SetDestination(GameManager.instance.player.transform.position);
    }

    private void Update()
    {
        phoneBatery -= Time.deltaTime * (flashLight ? 0.1f : 0.05f);
        SetActivePhone();

        
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1);

        GameTime += 1;
        PhoneScript.instance.MainMenu.GetComponent<MainMenuScript>().SetTimer();
        StartCoroutine(Timer());
    }

    public void SetMainCamera(Camera camera)
    {
        MainCamera.gameObject.SetActive(false);
        MainCamera = camera;
        MainCamera.gameObject.SetActive(true);
    }

    public void SetActivePhone()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            ActivePhone();
        }
    }

    public void ActivePhone()
    {
        phone.GetComponent<Animator>().SetBool("isPhone", isPhone);

        Debug.Log(phone.GetComponent<Animator>());
        Debug.Log(isPhone + " --- ");
            Debug.Log(phone.GetComponent<Animator>());
            Debug.Log(isPhone + " --- ");
            Debug.Log(phone.GetComponent<Animator>());

        isPhone = !isPhone;

        //phone.SetActive(!phone.activeSelf);
    }

    public string FunctionsOfNumbers(int index)
    {
        switch (index)
        {
            case 0: SnakeManager.instance.GetNewApple(); return "Apple added!";
            case 1: StartSnake(); return "I have a gift for you. Play SnakeGame";
            case 2: SnakeManager.instance.GetNewApple(); return "Apple added!";
            case 3: Debug.Log("3"); return CheckWinCondition();

            default: return "Wrong number, Snake is Comming!";
        }

    }

    public void StartSnake()
    {
        snake.SetActive(true);
        //Play gate sound
    }
    public string CheckWinCondition()
    {
        Win++;  
        snake.SetActive(false);
        switch (Win)
        {
            case 0: return "Win!";
            case 1: return "Don't escape from me!";
            case 2: return "Find Gate";
            default: return "error";
        }

        
    }

}
