using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class PackmanScript : MonoBehaviour
{

    [SerializeField]
    bool DoYouWantExit = false;

    public GameObject playerMarker;
    public GameObject snakeMarker;

    private void Start()
    {

    }
    private void FixedUpdate()
    {
        playerMarker.SetActive(true);
        ActionListener();
        if (SnakeManager.instance.target != GameManager.instance.player)
        {
            playerMarker.transform.position = GameManager.instance.player.transform.position;
        }
        else playerMarker.SetActive(false);
        snakeMarker.transform.position = GameManager.instance.snake.transform.position;
    }

    public void ActionListener()
    {
        if (PhoneScript.instance.ChangedState)
        {
            PhoneScript.instance.ChangedState = false;
            OperateApp(PhoneScript.instance.lastDirection);
        }
    }

    public void OperateApp(Direction dir)
    {
        if (dir == Direction.Up)
        {
            
        }
        if (dir == Direction.Down)
        {
            
        }
        if (dir == Direction.Call)
        {
            
        }
        if (dir == Direction.Reject)
        {
            if(DoYouWantExit)
            {
                PhoneScript.instance.ApplyMode(Application.AppMenu);
            }
            else
            {
                DoYouWantExit = true;
            }

        }
    }
}
