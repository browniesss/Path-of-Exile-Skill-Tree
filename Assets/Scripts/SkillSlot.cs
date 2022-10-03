using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// ��ų ���� ��ũ��Ʈ
public class SkillSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
    // �������̽����� ����� �巡�� �� ����� ����
{
    [Header("Slot Skill Info")]
    public Skill skill;

    public int slot_Code;

    private Image image;

    public void OnBeginDrag(PointerEventData eventData) // �巡�� ���۽�
    {
        if (skill != null) // �ش� ������ ��ų�� �־��ٸ� 
        {
            // �巡���ϴ� ��ų�� ����
            DragSkill.Instance.Drag_Image_Set(skill, image);
            DragSkill.Instance.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData) // �巡�� ���϶� 
    {
        DragSkill.Instance.transform.position = eventData.position; // �巡���ϴ� ��ų�� ��ǥ�� ���콺 ��ǥ�� �־���.
    }

    public void OnDrop(PointerEventData eventData) // �� ���Կ� Drop �� �Ͼ���� 
    {
        if (DragSkill.Instance.draging_Skill != null) // �巡�� ���� ��ų�� �־��ٸ�
        {
            skill = DragSkill.Instance.draging_Skill; // �巡�� �ϴ� ��ų�� �־��� �� 

            // Image ���� 
            image.sprite = DragSkill.Instance.own_Image.sprite;
            image.color = new Color(255, 255, 255, 1);

            GameManager.Instance.Quick_Slot[slot_Code - 1] = this; // ���ӸŴ����� �ε����� �̿��� �־���.

            transform.DOScale(1.5f, 0.1f).OnComplete(() => { transform.DOScale(1f, 0.1f); }); // DotWeen�� ����ؼ� �ִϸ��̼� ���� 
        }
    }

    public void OnEndDrag(PointerEventData eventData) // �巡�װ� ���� �� 
    {
        DragSkill.Instance.Drag_Image_End(); // �巡�� �ϴ� ��ų���� �巡�װ� �����ٴ� �Լ��� ȣ����. 
    }

    public void Slot_Clear(int index) // �ش� ������ ����ִ� �Լ�.
    {
        this.skill = null;

        this.image.sprite = null;
        this.image.color = new Color(255, 255, 255, 0);

        DataManager.Instance.userData.quickSlot_Skill[index] = 0;
        DataManager.Instance.userData.sprite[index] = null;
    }

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {

    }
}
