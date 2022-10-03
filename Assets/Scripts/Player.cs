using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

// key에 맞는 슬롯을 호출하기위해 만든 구조체 
class QuickSlot_Using_Key
{
    public Dictionary<KeyCode, SkillSlot> key_Dictionary; // 키값과 슬롯을 연결하기 위해 Dictionary를 만듬

    public void Init() // 값 초기화
    {
        key_Dictionary = new Dictionary<KeyCode, SkillSlot>
        {
            { KeyCode.Alpha1, GameManager.Instance.Skill_Slot[0] },
            { KeyCode.Alpha2, GameManager.Instance.Skill_Slot[1] },
            { KeyCode.Alpha3, GameManager.Instance.Skill_Slot[2] },
            { KeyCode.Alpha4, GameManager.Instance.Skill_Slot[3] },
            { KeyCode.Alpha5, GameManager.Instance.Skill_Slot[4] },
            { KeyCode.Alpha6, GameManager.Instance.Skill_Slot[5] },
            { KeyCode.Alpha7, GameManager.Instance.Skill_Slot[6] },
            { KeyCode.Alpha8, GameManager.Instance.Skill_Slot[7] },
        };
    }
}

// 플레이어 스크립트
public class Player : Singleton<Player> // 제네릭 싱글톤을 활용해 싱글톤 적용
{
    QuickSlot_Using_Key quickSlot_Using_Key = new QuickSlot_Using_Key();

    void Start()
    {
        quickSlot_Using_Key.Init();
    }

    void Skill_Active()
    {
        if (Input.anyKeyDown) // 키를 누르면 
        {
            foreach (var key in quickSlot_Using_Key.key_Dictionary) // 퀵슬롯 키 딕셔너리에서 키값으로 찾음
            {
                if (Input.GetKeyDown(key.Key)) // 키값이 동일하다면 
                {
                    key.Value.skill?.use_Skill(); // skill이 null값이 아니라면 skill 사용
                    if (key.Value.skill != null) // 스킬이 null값이 아니었다면 
                    {
                        // DotWeen을 이용해 UI 애니메이션 적용.
                        key.Value.transform.DOScale(1.5f, 0.25f).OnComplete(() =>
                        {
                            key.Value.GetComponent<Image>().DOColor(new Color(0, 0, 255), 0.25f).OnComplete(() =>
                            {
                                key.Value.GetComponent<Image>().DOColor(new Color(255, 255, 255), 0.25f).OnComplete(() =>
                                {
                                    key.Value.transform.DOScale(1f, 0.25f);
                                });
                            });
                        });
                    }
                }
            }
        }
    }

    void Update()
    {
        Skill_Active();
    }
}
