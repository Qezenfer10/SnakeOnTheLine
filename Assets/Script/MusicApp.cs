using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicApp : MonoBehaviour
{
    public List<GameObject> Musics;
    public GameObject OKButton;

    [SerializeField]
    int currentMusicsIndex = 0;

    [SerializeField]
    bool OkeyButtonOn = false;

    [SerializeField]
    bool isMusicPlaying = false;

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
            OkeyButtonOn = false;
            OKButton.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            OKButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            if (currentMusicsIndex > 0)
            {
                if (lastIndex == 0)
                {
                    Musics[currentMusicsIndex + 2].SetActive(false);
                    Musics[currentMusicsIndex - 1].SetActive(true);
                }
                else
                {
                    lastIndex--;
                }
                SelectApp(currentMusicsIndex - 1);
            }
            else
            {
                Musics.FirstOrDefault().SetActive(false);
                SelectApp(Musics.Count - 1);
                ResetPanel(Musics.Count - 3);
            }
        }
        if (dir == Direction.Down)
        {
            OkeyButtonOn = false;
            OKButton.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            OKButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            if (currentMusicsIndex < Musics.Count - 1)
            {
                if (lastIndex == 2)
                {
                    Musics[currentMusicsIndex - lastIndex].SetActive(false);
                    Musics[currentMusicsIndex + 1].SetActive(true);
                }
                else
                {
                    lastIndex++;
                }
                SelectApp(currentMusicsIndex + 1);
            }
            else
            {
                Musics.LastOrDefault().SetActive(false);
                SelectApp(0);
                ResetPanel(0);
            }
        }
        if (dir == Direction.Call)
        {
            if (!OkeyButtonOn)
            {
                Musics[currentMusicsIndex].GetComponent<Image>().color = new Color(0, 0, 0, 0);
                Musics[currentMusicsIndex].GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
                OkeyButtonOn = true;
                OKButton.GetComponent<Image>().color = Color.black;
                OKButton.GetComponentInChildren<TextMeshProUGUI>().color = PhoneScript.instance.DefaultColor;
            }
            else
            {
                PlayMusic();
            }
        }
        if (dir == Direction.Reject)
        {
            if (isMusicPlaying)
            {
                StopMusic();
                return;
            }
            if (OkeyButtonOn)
            {
                Musics[currentMusicsIndex].GetComponent<Image>().color = Color.black;
                Musics[currentMusicsIndex].GetComponentInChildren<TextMeshProUGUI>().color = PhoneScript.instance.DefaultColor;
                OkeyButtonOn = false;
                OKButton.GetComponent<Image>().color = new Color(0, 0, 0, 0);
                OKButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            }
            else
            {
                PhoneScript.instance.ApplyMode(Application.AppMenu);
            }

        }
    }

    public void SelectApp(int index)
    {
        Musics[currentMusicsIndex].GetComponent<Image>().color = new Color(0, 0, 0, 0);
        Musics[currentMusicsIndex].GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
        currentMusicsIndex = index;
        Musics[index].GetComponent<Image>().color = Color.black;
        Musics[index].GetComponentInChildren<TextMeshProUGUI>().color = PhoneScript.instance.DefaultColor;
    }

    public void PlayMusic()
    {
        OkeyButtonOn = false;
        OKButton.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        OKButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
        isMusicPlaying = true;
        Musics[currentMusicsIndex].transform.GetChild(0).gameObject.SetActive(true);

        GameManager.instance.musicSound.clip = Musics[currentMusicsIndex].GetComponent<MusicDataScript>().music;
        GameManager.instance.musicSound.Play();
    }

    public void StopMusic()
    {
        isMusicPlaying = false;
        Musics[currentMusicsIndex].transform.GetChild(0).gameObject.SetActive(false);

        GameManager.instance.musicSound.Stop();
    }


    public void StartApp()
    {
        currentMusicsIndex = 0;
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
        for (int i = a; i < (Musics.Count < 3 ? Musics.Count : 3) + a; i++)
        {
            Musics[i].SetActive(true);
        }
        lastIndex = 2 * a;
    }
}
