using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Ctrl : MonoBehaviour
{
    // キャラクター用スクリプト共通の操作はここに入れる
    private GameObject face;
    private Vector3 chara_pos;
    public static int x;
    int i = 0;
    [SerializeField, Range(0, 10)]
    float time = 1;
    public Sprite[] face_Patt = new Sprite[x];
    // Start is called before the first frame update
    void Start()
    {
        chara_pos = transform.position;
        if (x != 0)
        {
            face = transform.Find("face").gameObject;
            face.GetComponent<SpriteRenderer>().sprite = face_Patt[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Face_Change();
        }
        /*
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Character_move_Right();

        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Character_move_Left();

        }*/
    }

    void Face_Change()
    {
        face.GetComponent<SpriteRenderer>().sprite = face_Patt[i];
        i++;
        if (i >= face_Patt.Length)
        {
            i = 0;
        }
    }

    // キャラクターを動かすコルーチン、汎用性難有要改修
    IEnumerator Character_Move(Vector3 point)
    {
        Vector3 start_pos = transform.position;
        Vector3 end_pos = transform.position;
        end_pos += point;
        float startTime = Time.timeSinceLevelLoad;
        var diff = Time.timeSinceLevelLoad - startTime;
        var rate = diff / time;

        while (true)
        {
            if (rate >= 1)
            {
                yield break;
            }
            transform.position = Vector3.Lerp(start_pos, end_pos, rate);
            diff = Time.timeSinceLevelLoad - startTime;
            rate = diff / time;
            yield return null;
        }

    }

    // 右移動
    public void Character_move_Right()
    {
        StartCoroutine(Character_Move(new Vector3(5,0,0)));
    }
    // 左移動
    public void Character_move_Left()
    {
        StartCoroutine(Character_Move(new Vector3(-5, 0, 0)));
    }

    public void Set_Character_Point(Vector3 point)
    {
        // キャラクターを直接ワープさせるので座標をそのまま飛ばしていい
        transform.position = point;
    }
}
