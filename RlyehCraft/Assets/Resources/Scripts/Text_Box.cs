using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class Text_Box : MonoBehaviour
{
    // 文字送りシステムつくります！
    [SerializeField]
    [Range(0.001f, 0.3f)]
    float textPushInterval = 0.05f; // 文字送りインターバル

    public Text TalkText;
    public Text CharaNameText;

    void Start()
    {

    }

    void Update()
    {

    }



    // 外部から名前と発言を受け取りまnす
    public void CharacterTalk(string command)
    {
        string[] buf = command.Split(',');
        string CName = buf[0];
        string CCord = TextSplit(buf[1]);

        CharaNameText.text = CName;
        StartCoroutine(PushText(CCord));
    }

    //テキストを一定の文字数で改行して返す
    string TextSplit(string str)
    {
        string ss = "";

        // テキストを折り返すため任意の文字数ごとに改行を挟む
        foreach (string s in Regex.Split(str, @"(?<=\G.{20})(?!$)"))
        {
            ss += s + "\n";
        }

        return ss;
    }

    private IEnumerator PushText(string talk)
    {
        int message_count = 0;
        TalkText.text = "";

        while (talk.Length > message_count)
        {
            TalkText.text += talk[message_count];
            message_count++;
            yield return new WaitForSeconds(textPushInterval);
        }

    }
}
