using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

// JSON 데이터 관리 스크립트
public class DataManager : Singleton<DataManager>
{
    static GameObject _container;
    static GameObject Container
    {
        get
        {
            return _container;
        }
    }

    public string GameDataFileName = "SkillTree.json";

    public UserData _userData;
    public UserData userData
    {
        get
        {
            if (_userData == null)
            {
                LoadUserData();
                SaveUserData();
            }
            return _userData;
        }
    }

    void Start()
    {
        LoadUserData();
        SaveUserData();
    }

    public void LoadUserData()
    {
        string filePath = Application.persistentDataPath + GameDataFileName;

        if (File.Exists(filePath))
        {
            // 성공
            string FromJsonData = File.ReadAllText(filePath);
            _userData = JsonUtility.FromJson<UserData>(FromJsonData);
        }
        else
        {
            _userData = new UserData();
        }
    }

    public void Skill_Update(bool value) // JSON 데이터를 불러온 후 데이터 처리해주는 함수.
    {
        Skill_Off();

        foreach (Skill skill in SkillLoader.Instance.skill_List) // 이미 찍어져있던 스킬들의 OutLine을 활성화 해주고
        {
            if (userData.is_Skill[skill.skill_ID])
            {
                skill.outLine.SetActive(value);
            }
        }

        foreach (Skill skill in SkillLoader.Instance.skill_List) // 퀵슬롯에 등록돼있던 스킬들 활성화
        {
            for (int i = 0; i < userData.quickSlot_Skill.Length; i++)
            {
                if (userData.quickSlot_Skill[i] == skill.skill_ID)
                {
                    GameManager.Instance.Skill_Slot[i].skill = skill;

                    Image image = GameManager.Instance.Skill_Slot[i].GetComponent<Image>();

                    image.sprite = userData.sprite[i];

                    image.color = new Color(255, 255, 255, 1);

                    GameManager.Instance.Quick_Slot[i] = GameManager.Instance.Skill_Slot[i];
                }
            }
        }

        GameManager.Instance.skill_Point_Text.text = "Point :" + userData.Skill_Point;
    }

    public void Skill_Off()
    {
        foreach (Skill skill in SkillLoader.Instance.skill_List)
        {
            skill.outLine.SetActive(false);
        }
    }

    public void SaveUserData()
    {
        string ToJsonData = JsonUtility.ToJson(userData);
        string filePath = Application.persistentDataPath + GameDataFileName;

        File.WriteAllText(filePath, ToJsonData);

        Skill_Update(true);
    }

    private void OnApplicationQuit()
    {
        SaveUserData();
    }
}
