using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// csv�� �о�� �� �����͸� ������ ��ũ��Ʈ 
public class SkillLoader : Singleton<SkillLoader> // ���׸� �̱����� ���.
{
    [SerializeField]
    private Canvas mainCanvas;
    List<Dictionary<string, object>> data_Dialog; // csv�� ���Ŀ� �°� List<Dictionary>�� ����
    public List<Skill> skill_List;
    public List<GameObject> created_Skill_List;

    void Start()
    {
        data_Dialog = CSVReader.Read("SkillTree");
        skill_List = new List<Skill>();
        created_Skill_List = new List<GameObject>();

        Skill_Load();
    }

    void Skill_Load()
    {
        for (int i = 0; i < data_Dialog.Count; i++) // csv���� �о�� ��ŭ for�� 
        {
            foreach (Skill skill in SkillManager.Instance.skill_Arr) // �����յ��� ��Ͻ��ѳ��� �迭���� �˻� 
            {
                if ((data_Dialog[i]["Skill_ID"].ToString()) == skill.skill_Code) // csv���� �о�� �ڵ�� ��ġ�ϴٸ� 
                // �ִ� ��ų���� �˻�.
                {
                    skill.Skill_Init(); // �ʱ�ȭ �� 

                    // �����͸� �־���.
                    skill.is_Group = bool.Parse(data_Dialog[i]["Is_Group"].ToString());
                    skill.group_ID = data_Dialog[i]["Group_ID"].ToString();
                    skill.group_Index = int.Parse(data_Dialog[i]["Group_Index"].ToString());
                    skill.parent_ID = (data_Dialog[i]["Parent_ID"].ToString());
                    skill.group_Count = int.Parse(data_Dialog[i]["Group_Count"].ToString());
                    skill.skill_Info = data_Dialog[i]["Skill_Info"].ToString();
                    skill.skill_ID = int.Parse(data_Dialog[i]["Skill_Code"].ToString());
                    skill.skill_Name = data_Dialog[i]["Skill_Name"].ToString();

                    skill_List.Add(skill);
                }
            }
        }

        Skill_UI_Create();
    }

    void Skill_UI_Create() // ��ų UI ( Image )�� Canvas���� ����� �Լ�.
    {
        // ��� UI ���� �� 
        GameObject center = Resources.Load<GameObject>("Skills/Skill_0"); 

        GameObject realCenter = GameObject.Instantiate(center, GameManager.Instance.Skill_Tree_Base.transform);

        created_Skill_List.Add(realCenter); // ������ ������Ʈ ����Ʈ�� �߰�
        skill_List.Remove(center.GetComponent<Skill>());

        foreach (Skill skill in skill_List) 
        {
            GameObject obj = Resources.Load<GameObject>("Skills/Skill_" + skill.skill_Code); // Resources �������� �������� �����ͼ� 
            Skill obj_skill = obj.GetComponent<Skill>(); // �� �������� Skill ������Ʈ�� �޾ƿ� 

            skill.Parent_Connect(); // �θ� ��������� �� 

            // ������ ����
            GameObject realObj = GameObject.Instantiate(obj, GameManager.Instance.Skill_Tree_Base.transform);

            Vector2 parent_pos = obj_skill.parent_Skill.GetComponent<RectTransform>().localPosition; // �θ���ǥ�� �޾ƿ� ��

            if (realObj != null)
                created_Skill_List.Add(realObj); // ������ ������Ʈ ����Ʈ�� �߰�

            // csv������ ���س��� count ��� ������ ������ �� 
            float radian = obj_skill.group_Index * (360 / obj_skill.group_Count) * Mathf.Deg2Rad; 

            Vector2 pos;
            if (obj_skill.parent_ID.ToString() == obj_skill.group_ID) // �׷����� �ƴ����� ���� ��ǥ ����.
            {
                pos = new Vector2(200 * Mathf.Cos(radian), 200 * Mathf.Sin(radian));
            }
            else
                pos = new Vector2(50 * Mathf.Cos(radian), 50 * Mathf.Sin(radian));

            realObj.GetComponent<RectTransform>().localPosition = pos + parent_pos; // �θ� ��ǥ�� ���� ��ǥ�� ������

            LineRenderer line = realObj.GetComponent<LineRenderer>(); // ���η������� �޾ƿ� �� 
            line.positionCount = 2;

            // �θ�� �ڽ��� ������.
            line.SetPosition(0, obj_skill.parent_Skill.GetComponent<RectTransform>().position);
            line.SetPosition(1, realObj.GetComponent<RectTransform>().position);

            if (skill.parent_ID == skill.group_ID) // �ڽ��� ���̵� �������� �θ� ID�� ��쿣 
            {
                // �׷��� �����ִ� �̹����� BackGround�� �޾ƿͼ� ��������.
                GameObject back_ground_Obj = Resources.Load<GameObject>("BackGround/Group_BackGround");

                GameObject back_ground_realObj = GameObject.Instantiate(back_ground_Obj, realObj.transform);
                back_ground_realObj.transform.localPosition = new Vector3(0, 0, 0);
                back_ground_realObj.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
            }
            else // �������� �θ� �ƴҰ�쿣
            {
                if (skill.group_Index == 1)  // �� �׷��� 1�� �ε��� ������Ʈ��
                {
                    // �׷��� �����ִ� �̹����� ������.
                    GameObject back_ground_Obj = Resources.Load<GameObject>("BackGround/Group_BackGround");

                    GameObject back_ground_realObj = GameObject.Instantiate(back_ground_Obj, skill.parent_Skill.transform);

                    back_ground_realObj.transform.localPosition = new Vector3(0, 0, 0);
                    back_ground_realObj.GetComponent<RectTransform>().sizeDelta = new Vector2(150f, 150f);
                }
            }
        }
    }

    void Update()
    {

    }
}
