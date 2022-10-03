using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// json �����͸� ������ Ŭ����
[Serializable]
public class UserData
{
    public bool[] is_Skill = new bool[100]; // �ش��ϴ� ��ȣ�� ��ų�� �����ִ��� ������ bool ���� �迭

    [SerializeField]
    private int skill_Point = 50; // ���� �����ִ� ��ų ����Ʈ 

    public Sprite[] sprite = new Sprite[8]; // �����Կ� ����ִ� sprite �� ������ �迭 

    public int[] quickSlot_Skill = new int[8]; // �����Կ� ����ִ� skill_id �� ������ �迭 

    public int Skill_Point // skill_Point �� ������Ƽ�� �̿��ؼ� 
    {
        get => skill_Point;
        set
        {
            skill_Point = value;

            GameManager.Instance.skill_Point_Text.text = "Point :" + skill_Point; // ���� ���ö����� UI �ؽ�Ʈ ������

            if (skill_Point == 0)  // 0�� �������� DEBUG ���
                Debug.Log("��ų ����Ʈ�� ���� ����߽��ϴ�.");
        }
    }

}
