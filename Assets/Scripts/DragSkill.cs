using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 드래그중인 스킬 스크립트
public class DragSkill : Singleton<DragSkill>
{
    [Header("Draging SKill Info")]
    public Skill draging_Skill;

    [Header("own Info")]
    public Image own_Image; 

    void Start()
    {

    }

    public void Drag_Image_Set(Skill skill,Image image)
    {
        draging_Skill = skill;
        own_Image.sprite = image.sprite;

        own_Image.color = new Color(255, 255, 255, 1f);
    }

    public void Drag_Image_End()
    {
        draging_Skill = null;
        own_Image.color = new Color(255, 255, 255, 0f);
    }

    void Update()
    {
        
    }
}