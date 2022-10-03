using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

// ��ų ��ũ��Ʈ
public class Skill : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,
    IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler // �������̽��� ����� �巡��, Ŭ�� �� ����
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

    public void Skill_Init() // ��ų���� �ʱ�ȭ
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

    public void Parent_Connect() // �θ� ��ų�� ����
    {
        foreach (GameObject skill_obj in SkillLoader.Instance.created_Skill_List)
        {
            Skill skill = skill_obj.GetComponent<Skill>();

            if (parent_ID == skill.skill_Code) // �θ�� ���� ��ų�ڵ带 �������ִ� ������Ʈ�� 
                parent_Skill = skill; // �θ�� ����
        }
    }

    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData) // ���콺 �����Ͱ� ������  
    {
        if (skill_ID == 0) 
            return;

        Image my_Image = this.GetComponent<Image>();

        GameManager.Instance.Skill_Info_Panel.GetComponent<SkillinfoPanel>(). // ��ų ���� Panel�� �����.
            Panel_Setting(this.gameObject);

        transform.DOScale(1.5f, 0.5f); // Dotween���� �ִϸ��̼� ����
    }

    public void OnPointerExit(PointerEventData eventData) // ���콺 �����Ͱ� ������ 
    {
        if (skill_ID == 0)
            return;

        GameManager.Instance.Skill_Info_Panel.SetActive(false); // �ǳ��� �ٽ� �����ְ�

        transform.DOScale(1f, 0.5f); // Dotween �ִϸ��̼� ����
    }

    public void OnPointerClick(PointerEventData eventData) // ���콺 Ŭ����
    {
        if (skill_ID == 0)
            return;

        if (DataManager.Instance.userData.is_Skill[parent_Skill.skill_ID]
            && DataManager.Instance.userData.Skill_Point > 0
            && !DataManager.Instance.userData.is_Skill[skill_ID]) // �θ� ��ų�� ����� ��ų����Ʈ�� 1 �̻��̸� �̹� ���� ��ų��
                                                                  // �ƴ϶��
        {
            Debug.Log("��ų��� > " + this.gameObject.name);
            DataManager.Instance.userData.Skill_Point--; // ��ų����Ʈ ����.
            DataManager.Instance.userData.is_Skill[skill_ID] = true; // ��ų�� ���.
            outLine.SetActive(true); // OutLine Ȱ��ȭ

            transform.DOScale(2f, 0.25f).OnComplete(() => { transform.DOScale(1f, 0.25f); }); // Dotween �ִϸ��̼� ����
        }
    }

    public void OnBeginDrag(PointerEventData eventData) // �巡�� ���۽�
    {
        if (skill_ID == 0)
            return;

        if (DataManager.Instance.userData.is_Skill[skill_ID]) // �̹� ����� ��ų�� ��쿡�� 
        {
            is_Draging = true; // drag������ �˸��� ���� true
            DragSkill.Instance.Drag_Image_Set(this.GetComponent<Skill>(), GetComponent<Image>()); // �巡���ϴ� ��ų��
            // �̹����� ��������.
            DragSkill.Instance.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData) // �巡�� ���Ͻ�
    {
        if (skill_ID == 0)
            return;

        if (is_Draging)
        {
            DragSkill.Instance.transform.position = eventData.position; // �巡���ϴ� ��ų�� ��ǥ�� ���콺 ��ǥ�� ��������
        }
    }

    public void OnEndDrag(PointerEventData eventData) // �巡�װ� ������
    {
        if (skill_ID == 0)
            return;

        DragSkill.Instance.Drag_Image_End(); // �巡���ϴ� ��ų���� �����ٴ� �Լ� ȣ��.
        is_Draging = false;
    }

    public void OnDrop(PointerEventData eventData)
    {

    }

    public void use_Skill() // ��ų����Լ�
    {
        Debug.Log(skill_Name + "�� ����߽��ϴ�.");
    }

    public void outLine_Off() // OutLine ���� �Լ�
    {
        outLine.gameObject.SetActive(false);
    }
}
