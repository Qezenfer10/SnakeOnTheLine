using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public GameObject MenyuButton;

    //public GameObject batery;
    public TextMeshProUGUI timeText;

    public List<GameObject> baterVisuals;

    [SerializeField]
    private bool OnMenyu = false;

    [SerializeField]
    bool lowBateryOn = false;

    [SerializeField]
    bool chargeModeOn = false;

    [SerializeField]
    int batery_level;
    void Start()
    {
        
        SetBateryVisual();
        StartCoroutine(WaitAMinute());
    }

    public void SetTimer()
    {
        timeText.text = (GameManager.instance.GameTime / 60 < 10 ? "0" + (int)GameManager.instance.GameTime / 60: (int)GameManager.instance.GameTime / 60 + "") + ":" + (GameManager.instance.GameTime / 10 < 1 ? "0" + GameManager.instance.GameTime : GameManager.instance.GameTime + "");
    }
    // Update is called once per frame
    void Update()
    {

        ActionListener();

        if (batery_level != (int)GameManager.instance.phoneBatery / 25) SetBateryVisual();
    }

    public IEnumerator WaitAMinute()
    {
        yield return new WaitForSeconds(60);

        timeText.text = (GameManager.instance.GameTime / 10 < 1 ? "0" + GameManager.instance.GameTime : GameManager.instance.GameTime + "") + ":" + (GameManager.instance.GameTime / 10 < 1 ? "0" + GameManager.instance.GameTime : GameManager.instance.GameTime + "");

        StartCoroutine(WaitAMinute());
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
            if (OnMenyu)
            {
                OnMenyu = false;
                MenyuButton.GetComponent<Image>().color = new Color(0, 0, 0, 0);
                MenyuButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            }
        }
        if (dir == Direction.Down)
        {
            if (!OnMenyu)
            {
                OnMenyu = true;
                MenyuButton.GetComponent<Image>().color = Color.black;
                MenyuButton.GetComponentInChildren<TextMeshProUGUI>().color = PhoneScript.instance.DefaultColor;
            }
        }
        if (dir == Direction.Call)
        {
            if (!OnMenyu)
            {
                OnMenyu = true;
                MenyuButton.GetComponent<Image>().color = Color.black;
                MenyuButton.GetComponentInChildren<TextMeshProUGUI>().color = PhoneScript.instance.DefaultColor;
            }
            else
            {
                OnMenyu = false;
                MenyuButton.GetComponent<Image>().color = new Color(0, 0, 0, 0);
                MenyuButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
                PhoneScript.instance.ApplyMode(Application.AppMenu);
            }
        }
        if (dir == Direction.Reject)
        {
            if (OnMenyu)
            {
                OnMenyu = false;
                MenyuButton.GetComponent<Image>().color = new Color(0, 0, 0, 0);
                MenyuButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            }

        }
        if(dir >= Direction.Zero && dir <= Direction.Nine)
        {
            OnMenyu = false;
            MenyuButton.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            MenyuButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;

            PhoneScript.instance.ApplyMode(Application.Call);
            PhoneScript.instance.currentMode.GetComponent<CallScript>().StartApp();
        }
    }

    public IEnumerator LowBateryAnim(int time)
    {
        yield return new WaitForSeconds(time);

        if ((int)GameManager.instance.phoneBatery > 25)
        {
            lowBateryOn = false;
            SetBateryVisual();
        }
        else
        {
            if (baterVisuals[0].GetComponent<Image>().color == Color.black) baterVisuals[0].GetComponent<Image>().color = PhoneScript.instance.DefaultColor + new Color(5, 5, 5) / 256;
            else baterVisuals[0].GetComponent<Image>().color = Color.black;
            StartCoroutine(LowBateryAnim(time));
        }
    }

    
    public void SetBateryVisual()
    {
        batery_level = (int)GameManager.instance.phoneBatery / 25;
        if(batery_level == 0)
        {
            lowBateryOn = true;
            StartCoroutine(LowBateryAnim(1));

        }
        Debug.Log(batery_level);

        for (int i = 0; i < batery_level; i ++)
        {
            baterVisuals[i].GetComponent<Image>().color = Color.black;
        }
        if(batery_level != baterVisuals.Count)
        for (int i = batery_level; i < baterVisuals.Count; i++)
        {
            baterVisuals[i].GetComponent<Image>().color = PhoneScript.instance.DefaultColor + new Color(5,5,5)/256;
        }

    }
}
