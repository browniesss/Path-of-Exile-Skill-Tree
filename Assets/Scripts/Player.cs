using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

// key�� �´� ������ ȣ���ϱ����� ���� ����ü 
class QuickSlot_Using_Key
{
    public Dictionary<KeyCode, SkillSlot> key_Dictionary; // Ű���� ������ �����ϱ� ���� Dictionary�� ����

    public void Init() // �� �ʱ�ȭ
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

// �÷��̾� ��ũ��Ʈ
public class Player : Singleton<Player> // ���׸� �̱����� Ȱ���� �̱��� ����
{
    QuickSlot_Using_Key quickSlot_Using_Key = new QuickSlot_Using_Key();

    void Start()
    {
        quickSlot_Using_Key.Init();
    }

    void Skill_Active()
    {
        if (Input.anyKeyDown) // Ű�� ������ 
        {
            foreach (var key in quickSlot_Using_Key.key_Dictionary) // ������ Ű ��ųʸ����� Ű������ ã��
            {
                if (Input.GetKeyDown(key.Key)) // Ű���� �����ϴٸ� 
                {
                    key.Value.skill?.use_Skill(); // skill�� null���� �ƴ϶�� skill ���
                    if (key.Value.skill != null) // ��ų�� null���� �ƴϾ��ٸ� 
                    {
                        // DotWeen�� �̿��� UI �ִϸ��̼� ����.
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
