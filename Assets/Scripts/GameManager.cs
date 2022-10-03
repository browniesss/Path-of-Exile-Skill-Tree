using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 인덱서를 사용하기 위해 만들어준 QuickSlot 클래스
[Serializable]
public class QuickSlot
{
    public bool isStart = false;

    public SkillSlot[] quick_Slot = new SkillSlot[8];

    public SkillSlot this[int index]
    {
        get { return quick_Slot[index]; }

        set
        {
            if (isStart)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (quick_Slot[i]?.skill?.skill_Code == value?.skill?.skill_Code && i != index) // 이미 같은 스킬을 가진
                        // 슬롯이 퀵슬롯에 있다면 그 슬롯을 비워줌.
                    {
                        quick_Slot[i].Slot_Clear(i);
                    }
                }
                DataManager.Instance.userData.quickSlot_Skill[index] = value.skill.skill_ID;
                DataManager.Instance.userData.sprite[index] = value.GetComponent<Image>().sprite;
            }

            quick_Slot[index] = value;
        }
    }
}

// 게임매니저 스크립트
public class GameManager : Singleton<GameManager> // 제네릭 싱글톤을 활용해 싱글톤적용
{
    [SerializeField]
    private GameObject skill_Info_Panel;
    [SerializeField]
    private GameObject skill_Tree_Base;

    public Text skill_Point_Text; 

    public QuickSlot Quick_Slot = new QuickSlot(); 

    [SerializeField]
    private SkillSlot[] skill_Slot;

    // 프로퍼티
    public GameObject Skill_Info_Panel
    { get => skill_Info_Panel; set => skill_Info_Panel = value; }
    public GameObject Skill_Tree_Base
    { get => skill_Tree_Base; set => skill_Tree_Base = value; }
    public SkillSlot[] Skill_Slot
    { get => skill_Slot; set => skill_Slot = value; }

    void Start()
    {
        Quick_Slot.isStart = false;

        for (int i = 0; i < 8; i++)
            Quick_Slot[i] = Skill_Slot[i];

        Quick_Slot.isStart = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) // 스킬창 활성화 비활성화
        {
            if (!Skill_Tree_Base.gameObject.activeSelf)
                Skill_Tree_Base.gameObject.SetActive(true);
            else
                Skill_Tree_Base.gameObject.SetActive(false);
        }
    }

    public void Skilltree_Reset() // 화면의 초기화 버튼을 눌렀을때 작동할 함수.
    {
        DataManager.Instance.userData.Skill_Point = 50; // 스킬포인트 초기화 

        for (int i = 1; i < DataManager.Instance.userData.is_Skill.Length; i++) // 배운 스킬 초기화 
        {
            DataManager.Instance.userData.is_Skill[i] = false;
        }

        foreach (GameObject obj in SkillLoader.Instance.created_Skill_List) // 스킬 OutLine 초기화 
        {
            Skill skill = obj.GetComponent<Skill>();

            skill.outLine_Off();
        }

        for (int i = 0; i < DataManager.Instance.userData.quickSlot_Skill.Length; i++) // 퀵슬롯 초기화
        {
            DataManager.Instance.userData.quickSlot_Skill[i] = 0;
            DataManager.Instance.userData.sprite[i] = null;

            Image image = GameManager.Instance.Skill_Slot[i].GetComponent<Image>();

            image.sprite = null;

            image.color = new Color(255, 255, 255, 0);

            GameManager.Instance.Skill_Slot[i].Slot_Clear(i);
        }
    }
}

