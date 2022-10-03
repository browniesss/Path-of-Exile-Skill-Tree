using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 스킬정보 Panel 스크립트
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

    public void Panel_Setting(GameObject skill_obj) // 스킬에 마우스 포인터를 올릴시 호출할 함수
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
