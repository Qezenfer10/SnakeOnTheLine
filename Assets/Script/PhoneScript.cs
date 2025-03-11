using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneScript : MonoBehaviour
{
    public Renderer PhoneScreen;

    #region
    public static PhoneScript instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    #endregion

    public Application Mode;
    public GameObject currentMode;
    public Color DefaultColor;

    public Material PhoneTexture;
    public Material PackmanTexture;

    public GameObject MainMenu;
    public GameObject AppMenu;
    public GameObject Packman;
    public GameObject Tetris;
    public GameObject Flash;
    public GameObject Music;
    public GameObject Call;

    public bool ChangedState;
    public Direction lastDirection;

    private void Start()
    {
        ApplyMode(Application.MainMenu);
    }

    private void Update()
    {
        GetButtons();
    }

    void GetButtons()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0) || Input.GetKeyDown(KeyCode.Alpha0))
        {
            ButtonGesture(0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            ButtonGesture(1);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            ButtonGesture(2);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            ButtonGesture(3);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4))
        {
            ButtonGesture(4);
        }
        if (Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Alpha5))
        {
            ButtonGesture(5);
        }
        if (Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.Alpha6))
        {
            ButtonGesture(6);
        }
        if (Input.GetKeyDown(KeyCode.Keypad7) || Input.GetKeyDown(KeyCode.Alpha7))
        {
            ButtonGesture(7);
        }
        if (Input.GetKeyDown(KeyCode.Keypad8) || Input.GetKeyDown(KeyCode.Alpha8))
        {
            ButtonGesture(8);
        }
        if (Input.GetKeyDown(KeyCode.Keypad9) || Input.GetKeyDown(KeyCode.Alpha9))
        {
            ButtonGesture(9);
        }

        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            ButtonGesture(10);
        }
        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            ButtonGesture(11);
        }
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            ButtonGesture(16);
        }
        if (Input.GetKeyDown(KeyCode.Plus))
        {
            ButtonGesture(17);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ButtonGesture(12);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ButtonGesture(13);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ButtonGesture(14);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ButtonGesture(15);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ButtonGesture(16);

        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            ButtonGesture(17);
        }

    }
    //0 yuxari, 1 sag, 2 asagi, 3 sol
    public void ButtonGesture(int direction)
    {
        
        Debug.Log(direction.ToString());
        Direction direction1;

        switch (direction)
        {
            case 0: direction1 = Direction.Zero; break;
            case 1: direction1 = Direction.One; break;
            case 2: direction1 = Direction.Two; break;
            case 3: direction1 = Direction.Three; break;
            case 4: direction1 = Direction.Four; break;
            case 5: direction1 = Direction.Five; break;
            case 6: direction1 = Direction.Six; break;
            case 7: direction1 = Direction.Seven; break;
            case 8: direction1 = Direction.Eight; break;
            case 9: direction1 = Direction.Nine; break;
            case 10: direction1 = Direction.Star; break;
            case 11: direction1 = Direction.Hash; break;
            case 12: direction1 = Direction.Up; break;
            case 13: direction1 = Direction.Down; break;
            case 14: direction1 = Direction.Left; break;
            case 15: direction1 = Direction.Right; break;
            case 16: direction1 = Direction.Call; break;
            case 17: direction1 = Direction.Reject; break;
            case 18: direction1 = Direction.Yes; break;
            case 19: direction1 = Direction.No; break;
            default: direction1 = Direction.No; break;
        }

        DirectionListener(direction1);
    }

    public void DirectionListener(Direction direction)
    {
        ChangedState = true;
        lastDirection = direction;
    }
    public void ApplyMode(Application mode)
    {
        Mode = Application.AppMenu;

        switch(mode)
        {
            case Application.MainMenu:
                currentMode.SetActive(false);
                currentMode = MainMenu; currentMode.SetActive(true);
                PhoneScreen.material = PhoneTexture;
                currentMode.GetComponent<MainMenuScript>();
                break;
            case Application.AppMenu: 
                currentMode.SetActive(false); 
                currentMode = AppMenu; currentMode.SetActive(true);
                PhoneScreen.material = PhoneTexture;
                currentMode.GetComponent<MenuScript>().StartApp();
                break;
            case Application.Snake:
                currentMode.SetActive(false);
                currentMode = Packman; currentMode.SetActive(true);
                PhoneScreen.material = PackmanTexture;
                break;
            case Application.Tetris:
                currentMode.SetActive(false);
                currentMode = Tetris; currentMode.SetActive(true);
                PhoneScreen.material = PhoneTexture;
                break;
            case Application.Flash:
                currentMode.SetActive(false);
                currentMode = Flash; currentMode.SetActive(true);
                currentMode.GetComponent<FlashMenuScript>().OnStart();
                PhoneScreen.material = PhoneTexture;
                GameManager.instance.lightOn = !GameManager.instance.lightOn;
                break;
            case Application.Music:
                currentMode.SetActive(false);
                currentMode = Music; currentMode.SetActive(true);
                PhoneScreen.material = PhoneTexture;
                break;
            case Application.Call:
                currentMode.SetActive(false);
                currentMode = Call; currentMode.SetActive(true);
                PhoneScreen.material = PhoneTexture;
                break;
            default: currentMode = MainMenu; currentMode.SetActive(true); break;
        }
    }
}

[System.Serializable]

public enum Application
{
    None,
    MainMenu,
    AppMenu,
    Snake,
    Tetris,
    Flash,
    Music,
    Call
}

public enum App
{
    None,
    Snake,
    Tetris,
    Flash
}


public enum Direction
{
    
    Zero,
    One,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Star,
    Hash,
    Up,
    Down,
    Left,
    Right,
    Call,
    Reject,
    Yes,
    No,
    
}

