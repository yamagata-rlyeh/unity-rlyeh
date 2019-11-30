using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RlyehBackScreenCtrl : MonoBehaviour
{
    public Sprite[] backScreens;
    SpriteRenderer mainSprite;
    // time は変化用の秒数。今は一秒
    float time = 10.0f;
    bool on_off = true;
    // Start is called before the first frame update
    void Start()
    {
        mainSprite = gameObject.GetComponent<SpriteRenderer>();
        if (backScreens.Length!=0)
        {
            mainSprite.sprite = backScreens[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            CameraZoom();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            CameraBackDefalut();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (on_off)
            {
                ChangeBackScreenSprite(1);
                on_off = false;
            }
            else
            {
                ChangeBackScreenSprite(0);
                on_off = true;
            }
        }
    }

    IEnumerator ZoomBackScreen(float ZoomMagni)
    {
        Vector3 start_scale = transform.localScale;
        Vector3 end_scale = new Vector3(ZoomMagni, ZoomMagni, 1);

        // ズーム倍率と現在の拡大率を比較、プラマイを指定
        float startTime = Time.timeSinceLevelLoad;
        var diff = Time.timeSinceLevelLoad - startTime;
        var rate = diff / time;
        while (true)
        {
            // ここで倍率処理

            // 背景が指定倍率と同じになったらブレイク
            if (rate>=1)
            {
                transform.localScale = new Vector3(ZoomMagni, ZoomMagni, 1);
                yield break;
            }
            transform.localScale = Vector3.Lerp(start_scale,end_scale,rate);
            diff = Time.timeSinceLevelLoad - startTime;
            rate = diff/time;
            yield return null;
        }
    }

    public void CameraBackDefalut()
    {
        StartCoroutine(ZoomBackScreen(3.0f));
    }

    public void CameraZoom()
    {
        StartCoroutine(ZoomBackScreen(7.0f));
    }

    public void ChangeBackScreenSprite(int i)
    {
        if (i<=backScreens.Length) {
            mainSprite.sprite = backScreens[i];
        }
    }

    // コマンドで呼び出す用のものはここに貯めていく！
    public void CallMethod(string str)
    {
        Debug.Log(str);
        switch (str)
        {
            case string command when command.Contains("Black"):
                ChangeBackScreenSprite(1);
                break;
            case string command when command.Contains("Hospital"):
                ChangeBackScreenSprite(2);
                break;
            case string command when command.Contains("Twilight"):
                Debug.Log("none");
                break;
            case string command when command.Contains("ZoomCamera"):
                CameraZoom();
                break;
            case string command when command.Contains("BackDefalut"):
                CameraBackDefalut();
                break;
            default:
                Debug.Log("faled");
                break;

        }
    }
}
