using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public List<GameObject> Apps;
    public GameObject OKButton;

    [SerializeField]
    int currentAppsIndex = 0;

    [SerializeField]
    bool OkeyButtonOn = false;

    [SerializeField]
    int lastIndex;

    private void Start()
    {
        StartApp(); 
    }

    private void Update()
    {
        ActionListener();
    }

    public void ActionListener()
    {
        if(PhoneScript.instance.ChangedState)
        {
            PhoneScript.instance.ChangedState = false;
            OperateApp(PhoneScript.instance.lastDirection);
        }
    }
    public void OperateApp(Direction dir)
    {
        if(dir == Direction.Up)
        {
            OkeyButtonOn = false;
            OKButton.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            OKButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            if (currentAppsIndex > 0)
            {
                if (lastIndex == 0)
                {
                    Apps[currentAppsIndex + 2].SetActive(false);
                    Apps[currentAppsIndex - 1].SetActive(true);
                }
                else
                {
                    lastIndex--;
                }
                SelectApp(currentAppsIndex - 1);
            }
            else
            {
                Apps.FirstOrDefault().SetActive(false);
                SelectApp(Apps.Count - 1);
                ResetPanel(Apps.Count - 3);
            }
        }
        if (dir == Direction.Down)
        {
            OkeyButtonOn = false;
            OKButton.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            OKButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            if (currentAppsIndex < Apps.Count - 1)
            {
                if (lastIndex == 2)
                {
                    Apps[currentAppsIndex- lastIndex].SetActive(false);
                    Apps[currentAppsIndex + 1].SetActive(true);
                }
                else
                {
                    lastIndex++;
                }
                SelectApp(currentAppsIndex + 1);
            }
            else
            {
                Apps.LastOrDefault().SetActive(false);
                SelectApp(0);
                ResetPanel(0);
            }
        }
        if (dir == Direction.Call)
        {
            if(!OkeyButtonOn)
            {
                Apps[currentAppsIndex].GetComponent<Image>().color = new Color(0, 0, 0, 0);
                Apps[currentAppsIndex].GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
                OkeyButtonOn =true;
                OKButton. GetComponent<Image>().color = Color.black;
                OKButton.GetComponentInChildren<TextMeshProUGUI>().color = PhoneScript.instance.DefaultColor;
            }
            else
            {
                PhoneScript.instance.ApplyMode(Apps[currentAppsIndex].GetComponent<AppScript>().gameMode);
            }
        }
        if (dir == Direction.Reject)
        {
            if (OkeyButtonOn)
            {
                Apps[currentAppsIndex].GetComponent<Image>().color = Color.black;
                Apps[currentAppsIndex].GetComponentInChildren<TextMeshProUGUI>().color = PhoneScript.instance.DefaultColor;
                OkeyButtonOn = false;
                OKButton.GetComponent<Image>().color = new Color(0, 0, 0, 0);
                OKButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            }
            else
            {
                PhoneScript.instance.ApplyMode(Application.MainMenu);
            }

        }
    }

    public void SelectApp(int index)
    {
        Apps[currentAppsIndex].GetComponent<Image>().color = new Color(0,0,0,0);
        Apps[currentAppsIndex].GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
        currentAppsIndex = index;
        Apps[index].GetComponent<Image>().color = Color.black;
        Apps[index].GetComponentInChildren<TextMeshProUGUI>().color = PhoneScript.instance.DefaultColor;
    }

    public void StartApp()
    {
        currentAppsIndex = 0;
        SelectApp(0);
        if (OkeyButtonOn)
        {
            OkeyButtonOn = false;
            OKButton.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            OKButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
        }
        ResetPanel(0);
    }

    public void ResetPanel(int a)
    {
        for (int i = a; i < 3+a; i++)
        {
            Apps[i].SetActive(true);
        }
        lastIndex = 2 * a;
    }
}
