using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ��ų���� Panel ��ũ��Ʈ
public class SkillinfoPanel : MonoBehaviour
{
    [SerializeField]
    private Image skill_Image;
    [SerializeField]
    private Text skill_name_Text;
    [SerializeField]
    private Text skill_info_Text;

    void Start()
    {

    }

    public void Panel_Setting(GameObject skill_obj) // ��ų�� ���콺 �����͸� �ø��� ȣ���� �Լ�
    {
        Skill skill = skill_obj.GetComponent<Skill>();

        skill_Image.sprite = skill.GetComponent<Image>().sprite;
        skill_name_Text.text = skill.skill_Name;
        skill_info_Text.text = skill.skill_Info;

        Vector3 on_Pos = skill_obj.GetComponent<RectTransform>().position;

        this.GetComponent<RectTransform>().position
            = new Vector3(on_Pos.x + 20f, on_Pos.y, on_Pos.z);

        this.gameObject.SetActive(true);
    }

    void Update()
    {

    }
}
