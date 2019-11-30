using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RlyehAudioctrl : MonoBehaviour
{
    public AudioClip[] RlyehBGM;
    public AudioClip[] RlyehSE;
    

    AudioSource RlyehAudio;
    // Start is called before the first frame update
    void Start()
    {
        RlyehAudio = GetComponent<AudioSource>();
       // CallRlyehBGM(1);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallRlyehBGM(int i)
    {
        if (i<RlyehBGM.Length)
        {
            RlyehAudio.PlayOneShot(RlyehBGM[i]);
        }
    }

    public void CallRlyehSE(int i)
    {
        if (i < RlyehSE.Length)
        {
            RlyehAudio.PlayOneShot(RlyehSE[i]);
        }
    }

    // コマンドで呼び出す用のものはここに貯めていく！
    public void CallMethod(string str)
    {
        Debug.Log(str);
        switch (str)
        {
            case string command when command.Contains("CallBGM01"):
                CallRlyehBGM(1);
                Debug.Log("CallBGM01");
                break;
            default:
                Debug.Log("faled");
                break;

        }
    }
}
