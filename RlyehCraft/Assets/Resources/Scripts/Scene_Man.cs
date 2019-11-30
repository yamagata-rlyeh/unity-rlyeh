using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Scene_Man : MonoBehaviour
{
    // シーン管理用、やること多すぎるので適宜分離していくこと

    private Vector3 box_pos = new Vector3(0, -3.4f, -3);
    public static int charas;
    public GameObject[] chara_pref = new GameObject[charas];
    public GameObject BoxPrefab;
    [SerializeField] TextAsset TextAsset;
    Character_Ctrl[] c_ctrl;
    GameObject[] chara_inst;
    // int chara_act_No = 0;
    List<int> chara_no = new List<int>();
    List<int> chara_action = new List<int>();
    public string[] text_buf;
    public string[][] masterCommand;
    int max = -1;
    int sceneSeq = 0;
    Text_Box tb;
    GameObject thing;
    public RlyehBackScreenCtrl Bctrl;
    public RlyehAudioctrl Actrl;
    int commandNo =-1;
    int last_command = -1;

    // Start is called before the first frame update
    void Start()
    {
        
        sceneSeq = 0;
        c_ctrl = new Character_Ctrl[chara_pref.Length];
        chara_inst = new GameObject[chara_pref.Length];

        thing = Instantiate(BoxPrefab, box_pos, Quaternion.identity);
        tb = thing.GetComponent<Text_Box>();
        Set_chara_data();
        ReadTask(TextAsset.text);

    }


    // Update is called once per frame
    void Update()
    {

        if (max >= 0)
        {
            
            // Actionを動かして打キー毎に進める
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (sceneSeq < max-1)
                {
                    sceneSeq++;
                }
                Actrl.CallRlyehSE(0);
            }
            if (last_command != sceneSeq) { AwakeAction(sceneSeq); }
            last_command = sceneSeq;
        }


    }
    public void ReadTask(string Sourcetext)
    {
        text_buf = Sourcetext.Split('\n');
        masterCommand = new string[text_buf.Length][];
        for (int i = 0; i < text_buf.Length; i++)
        {
            if (text_buf[i].Contains(";"))
            {
                masterCommand[i] = text_buf[i].Split(';');
            }
            else
            {
                masterCommand[i] = new string[1];
                masterCommand[i][0] = text_buf[i];
            }
        }
        max = text_buf.Length;
       
    }

    // キャラクタを登場させる奴
    void Set_chara_data()
    {

        Vector3 move_pos;
        for (int i = 0; i < chara_pref.Length; i++)
        {
            move_pos = new Vector3(10 * i - 5, 0, -1);


            chara_inst[i] = Instantiate(chara_pref[i], move_pos, Quaternion.identity);
            c_ctrl[i] = chara_inst[i].GetComponent<Character_Ctrl>();
        }
    }

    public void AwakeAction(int i)
    {
        // 受け取った文に合わせて各オブジェクトの対応するメソッドを動かす
        // ためにSceneマネからオブジェクト（またはそのセット）を受け取らなければいけない
        foreach (string command in masterCommand[i])
        {
            if (command.Contains(","))
            {
                    tb.CharacterTalk(command);  
            }
            else
            {
                    CommandSelect(command);
            }
        }

    }

    /*// 改行ごとに分けた文字列をさらに細かく区分けする
    void SplitJaggyArray(string[] strbuf, string[][] JagArray)
    {
        JagArray = new string[strbuf.Length][];
        for (int i = 0; i < strbuf.Length; i++)
        {
            if (strbuf[i].Contains(";"))
            {
                JagArray[i] = strbuf[i].Split(';');
            }
            else
            {
                JagArray[i] = new string[1];
                JagArray[i][0] = strbuf[i];
            }
        }
        max = strbuf.Length;
    }
    */
    bool CharaTalkTrue(string text)
    {
        if (text.Contains(","))
        {
            return true;
        }
        return false;
    }

    void CommandSelect(string command)
    {

        string[] buf = command.Split('.');
        string CName = buf[0];
        string CCord = buf[1];

       // Debug.Log(CName);
        switch (CName)
        {
            case "Audio":
                Actrl.CallMethod(CCord);

                break;
            case "BackScreen":
                Bctrl.CallMethod(CCord);
                break;
            default:
                break;
        }
    }
}