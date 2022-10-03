using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

// JSON ������ ���� ��ũ��Ʈ
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
            // ����
            string FromJsonData = File.ReadAllText(filePath);
            _userData = JsonUtility.FromJson<UserData>(FromJsonData);
        }
        else
        {
            _userData = new UserData();
        }
    }

    public void Skill_Update(bool value) // JSON �����͸� �ҷ��� �� ������ ó�����ִ� �Լ�.
    {
        Skill_Off();

        foreach (Skill skill in SkillLoader.Instance.skill_List) // �̹� ������ִ� ��ų���� OutLine�� Ȱ��ȭ ���ְ�
        {
            if (userData.is_Skill[skill.skill_ID])
            {
                skill.outLine.SetActive(value);
            }
        }

        foreach (Skill skill in SkillLoader.Instance.skill_List) // �����Կ� ��ϵ��ִ� ��ų�� Ȱ��ȭ
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
