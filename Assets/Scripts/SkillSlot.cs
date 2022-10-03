using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// 스킬 슬롯 스크립트
public class SkillSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
    // 인터페이스들을 사용해 드래그 앤 드랍을 구현
{
    [Header("Slot Skill Info")]
    public Skill skill;

    public int slot_Code;

    private Image image;

    public void OnBeginDrag(PointerEventData eventData) // 드래그 시작시
    {
        if (skill != null) // 해당 슬롯이 스킬이 있었다면 
        {
            // 드래그하는 스킬을 설정
            DragSkill.Instance.Drag_Image_Set(skill, image);
            DragSkill.Instance.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData) // 드래그 중일때 
    {
        DragSkill.Instance.transform.position = eventData.position; // 드래그하는 스킬의 좌표를 마우스 좌표로 넣어줌.
    }

    public void OnDrop(PointerEventData eventData) // 이 슬롯에 Drop 이 일어났을때 
    {
        if (DragSkill.Instance.draging_Skill != null) // 드래그 중인 스킬이 있었다면
        {
            skill = DragSkill.Instance.draging_Skill; // 드래그 하던 스킬을 넣어준 후 

            // Image 설정 
            image.sprite = DragSkill.Instance.own_Image.sprite;
            image.color = new Color(255, 255, 255, 1);

            GameManager.Instance.Quick_Slot[slot_Code - 1] = this; // 게임매니저의 인덱서를 이용해 넣어줌.

            transform.DOScale(1.5f, 0.1f).OnComplete(() => { transform.DOScale(1f, 0.1f); }); // DotWeen을 사용해서 애니메이션 적용 
        }
    }

    public void OnEndDrag(PointerEventData eventData) // 드래그가 끝날 시 
    {
        DragSkill.Instance.Drag_Image_End(); // 드래그 하던 스킬에게 드래그가 끝났다는 함수를 호출함. 
    }

    public void Slot_Clear(int index) // 해당 슬롯을 비워주는 함수.
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
