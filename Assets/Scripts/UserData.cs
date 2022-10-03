using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// json 데이터를 저장할 클래스
[Serializable]
public class UserData
{
    public bool[] is_Skill = new bool[100]; // 해당하는 번호의 스킬이 찍혀있는지 저장할 bool 변수 배열

    [SerializeField]
    private int skill_Point = 50; // 현재 남아있는 스킬 포인트 

    public Sprite[] sprite = new Sprite[8]; // 퀵슬롯에 담겨있던 sprite 를 저장할 배열 

    public int[] quickSlot_Skill = new int[8]; // 퀵슬롯에 담겨있던 skill_id 를 저장할 배열 

    public int Skill_Point // skill_Point 는 프로퍼티를 이용해서 
    {
        get => skill_Point;
        set
        {
            skill_Point = value;

            GameManager.Instance.skill_Point_Text.text = "Point :" + skill_Point; // 값이 들어올때마다 UI 텍스트 변경후

            if (skill_Point == 0)  // 0이 됐을떄는 DEBUG 출력
                Debug.Log("스킬 포인트를 전부 사용했습니다.");
        }
    }

}
