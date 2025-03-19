using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CallScript : MonoBehaviour
{
    public TextMeshProUGUI NumberText;

    public string number;

    [SerializeField]
    bool OnTheLine = false;

    private void Start()
    {

    }
    void Update()
    {

        ActionListener();
    }

    public void ActionListener()
    {
        if (PhoneScript.instance.ChangedState)
        {
            PhoneScript.instance.ChangedState = false;
            OperateApp(PhoneScript.instance.lastDirection);
        }
    }

    public void StartApp()
    {
        Debug.Log("SAsASasASDasdASD");

        var dir = PhoneScript.instance.lastDirection;
        if (dir >= Direction.Zero && dir <= Direction.Hash)
        {
            if (dir > Direction.Zero && dir <= Direction.Nine)
            {
                NumberText.text += ((int)dir) + "";
                return;
            }
            if (dir == Direction.Hash) NumberText.text += "#";
            if (dir == Direction.Zero) NumberText.text += "0";
            if (dir == Direction.Star) NumberText.text += "*";
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
            CallNumber();
        }
        if (dir == Direction.Reject)
        {
            NumberText.text = "";
            PhoneScript.instance.ApplyMode(Application.MainMenu);
        }
        if (dir >= Direction.Zero && dir <= Direction.Hash)
        {
            if (dir > Direction.Zero && dir <= Direction.Nine)
            {
                NumberText.text += ((int)dir) + "";
                return;
            }
            if (dir == Direction.Hash) NumberText.text += "#";
            if (dir == Direction.Zero) NumberText.text += "0";
            if (dir == Direction.Star) NumberText.text += "*";
        }
    }

    float call_time = 0;
    float functime = 0;
    int countDot = 1;
    int timer = 0;
    public void CallNumber()
    {
        number = NumberText.text;
        call_time = 0;
        functime = 0;
        countDot = 1;
        NumberText.text = "Calling.";

        StartCoroutine( CallTime(0.5f));


    }


    public IEnumerator CallTime(float time)
    {
        yield return new WaitForSeconds(time);
        if (call_time <= 3)
        {
            Debug.Log("calling");
            call_time += time;
            if (countDot < 3)
            {
                NumberText.text += ".";
                countDot++;
            }
            else
            {
                countDot = 1;
                NumberText.text = "Calling.";
            }

            StartCoroutine(CallTime(time));
        }
        else
        {
            string text = "";
            var index = -1;
            for (int i = 0; i < GameManager.instance.numbers.Count; i++)
            {
                if (GameManager.instance.numbers[i] == number)
                {
                    functime = 0;
                    index = i;
                    text = GameManager.instance.FunctionsOfNumbers(index);
                    break;
                }
            }

            Debug.Log("laaaaa" + text);
            if (index == -1)
            {
                StartCoroutine(FunctionActivated("Wrong Number!", 0.1f));
            }
            else
            {
                OnTheLine = true;
                StartCoroutine(FunctionActivated(text, 0.1f));
            }
        }
    }

    public IEnumerator Timer()
    {
        yield return new WaitForSeconds(1);
        if (OnTheLine)
            NumberText.text = ((float)timer / 60 < 10 ? "0" + timer / 60 : timer / 60 + "") + ":" + ((float)timer % 60 < 10 ? "0" + timer % 60 : timer % 60 + "");
        timer++;

        if(OnTheLine) StartCoroutine(Timer());
        else
        {
            StartCoroutine(WaitAlittle(1));
        }
    }

    
    public IEnumerator FunctionActivated(string text, float time)
    {
        yield return new WaitForSeconds(time);

        functime+=time;
        NumberText.text = text;
        if(functime < 3) StartCoroutine(FunctionActivated(text, time));
        else
        {
            NumberText.text = "";
            PhoneScript.instance.ApplyMode(Application.MainMenu);
        }

    }

    public IEnumerator WaitAlittle(float time)
    {
        yield return new WaitForSeconds(time);

        NumberText.text = "Call ended";

        NumberText.text = "";
        PhoneScript.instance.ApplyMode(Application.MainMenu);
    }


}
