using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

// 스킬 스크립트
public class Skill : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,
    IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler // 인터페이스를 사용해 드래그, 클릭 등 적용
{
    [Header("Skill Info")]
    public int skill_ID;
    public string skill_Code;
    public string parent_ID;
    public Skill parent_Skill;
    public bool is_Group;
    public string group_ID;
    public int group_Index;
    public int group_Count;
    public string skill_Name;
    public string skill_Info;
    public bool is_Skill;

    [Header("UI OutLine")]
    public GameObject outLine = null;

    [Header("Skill now State")]
    [SerializeField]
    private bool is_Draging;

    void Start()
    {

    }

    public void Skill_Init() // 스킬정보 초기화
    {
        parent_ID = "";
        group_Index = -1;
        group_Count = -1;
        is_Group = false;
        group_ID = "";
        parent_Skill = null;
        skill_Info = "";
        skill_ID = 0;
        skill_Name = "";
    }

    public void Parent_Connect() // 부모 스킬에 연결
    {
        foreach (GameObject skill_obj in SkillLoader.Instance.created_Skill_List)
        {
            Skill skill = skill_obj.GetComponent<Skill>();

            if (parent_ID == skill.skill_Code) // 부모와 같은 스킬코드를 가지고있는 오브젝트를 
                parent_Skill = skill; // 부모로 연결
        }
    }

    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData) // 마우스 포인터가 들어오면  
    {
        if (skill_ID == 0) 
            return;

        Image my_Image = this.GetComponent<Image>();

        GameManager.Instance.Skill_Info_Panel.GetComponent<SkillinfoPanel>(). // 스킬 설명 Panel을 띄워줌.
            Panel_Setting(this.gameObject);

        transform.DOScale(1.5f, 0.5f); // Dotween으로 애니메이션 적용
    }

    public void OnPointerExit(PointerEventData eventData) // 마우스 포인터가 나가면 
    {
        if (skill_ID == 0)
            return;

        GameManager.Instance.Skill_Info_Panel.SetActive(false); // 판넬을 다시 숨겨주고

        transform.DOScale(1f, 0.5f); // Dotween 애니메이션 적용
    }

    public void OnPointerClick(PointerEventData eventData) // 마우스 클릭시
    {
        if (skill_ID == 0)
            return;

        if (DataManager.Instance.userData.is_Skill[parent_Skill.skill_ID]
            && DataManager.Instance.userData.Skill_Point > 0
            && !DataManager.Instance.userData.is_Skill[skill_ID]) // 부모 스킬을 배웠고 스킬포인트가 1 이상이며 이미 찍힌 스킬이
                                                                  // 아니라면
        {
            Debug.Log("스킬배움 > " + this.gameObject.name);
            DataManager.Instance.userData.Skill_Point--; // 스킬포인트 차감.
            DataManager.Instance.userData.is_Skill[skill_ID] = true; // 스킬을 배움.
            outLine.SetActive(true); // OutLine 활성화

            transform.DOScale(2f, 0.25f).OnComplete(() => { transform.DOScale(1f, 0.25f); }); // Dotween 애니메이션 적용
        }
    }

    public void OnBeginDrag(PointerEventData eventData) // 드래그 시작시
    {
        if (skill_ID == 0)
            return;

        if (DataManager.Instance.userData.is_Skill[skill_ID]) // 이미 배웠던 스킬일 경우에만 
        {
            is_Draging = true; // drag시작을 알리는 변수 true
            DragSkill.Instance.Drag_Image_Set(this.GetComponent<Skill>(), GetComponent<Image>()); // 드래그하는 스킬의
            // 이미지를 설정해줌.
            DragSkill.Instance.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData) // 드래그 중일시
    {
        if (skill_ID == 0)
            return;

        if (is_Draging)
        {
            DragSkill.Instance.transform.position = eventData.position; // 드래그하는 스킬의 좌표를 마우스 좌표로 변경해줌
        }
    }

    public void OnEndDrag(PointerEventData eventData) // 드래그가 끝날시
    {
        if (skill_ID == 0)
            return;

        DragSkill.Instance.Drag_Image_End(); // 드래그하는 스킬에게 끝났다는 함수 호출.
        is_Draging = false;
    }

    public void OnDrop(PointerEventData eventData)
    {

    }

    public void use_Skill() // 스킬사용함수
    {
        Debug.Log(skill_Name + "을 사용했습니다.");
    }

    public void outLine_Off() // OutLine 끄는 함수
    {
        outLine.gameObject.SetActive(false);
    }
}
