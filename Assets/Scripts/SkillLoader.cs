using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// csv를 읽어온 후 데이터를 저장할 스크립트 
public class SkillLoader : Singleton<SkillLoader> // 제네릭 싱글톤을 사용.
{
    [SerializeField]
    private Canvas mainCanvas;
    List<Dictionary<string, object>> data_Dialog; // csv의 형식에 맞게 List<Dictionary>를 만듬
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
        for (int i = 0; i < data_Dialog.Count; i++) // csv에서 읽어온 만큼 for문 
        {
            foreach (Skill skill in SkillManager.Instance.skill_Arr) // 프리팹들을 등록시켜놓은 배열에서 검사 
            {
                if ((data_Dialog[i]["Skill_ID"].ToString()) == skill.skill_Code) // csv에서 읽어온 코드와 일치하다면 
                // 있는 스킬인지 검사.
                {
                    skill.Skill_Init(); // 초기화 후 

                    // 데이터를 넣어줌.
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

    void Skill_UI_Create() // 스킬 UI ( Image )를 Canvas내에 만드는 함수.
    {
        // 가운데 UI 설정 후 
        GameObject center = Resources.Load<GameObject>("Skills/Skill_0"); 

        GameObject realCenter = GameObject.Instantiate(center, GameManager.Instance.Skill_Tree_Base.transform);

        created_Skill_List.Add(realCenter); // 생성된 오브젝트 리스트에 추가
        skill_List.Remove(center.GetComponent<Skill>());

        foreach (Skill skill in skill_List) 
        {
            GameObject obj = Resources.Load<GameObject>("Skills/Skill_" + skill.skill_Code); // Resources 폴더에서 프리팹을 가져와서 
            Skill obj_skill = obj.GetComponent<Skill>(); // 그 프리팹의 Skill 컴포넌트를 받아옴 

            skill.Parent_Connect(); // 부모를 연결시켜준 후 

            // 프리팹 생성
            GameObject realObj = GameObject.Instantiate(obj, GameManager.Instance.Skill_Tree_Base.transform);

            Vector2 parent_pos = obj_skill.parent_Skill.GetComponent<RectTransform>().localPosition; // 부모좌표를 받아온 후

            if (realObj != null)
                created_Skill_List.Add(realObj); // 생성된 오브젝트 리스트에 추가

            // csv내에서 정해놨던 count 대로 각도를 나눠준 후 
            float radian = obj_skill.group_Index * (360 / obj_skill.group_Count) * Mathf.Deg2Rad; 

            Vector2 pos;
            if (obj_skill.parent_ID.ToString() == obj_skill.group_ID) // 그룹인지 아닌지에 따라 좌표 설정.
            {
                pos = new Vector2(200 * Mathf.Cos(radian), 200 * Mathf.Sin(radian));
            }
            else
                pos = new Vector2(50 * Mathf.Cos(radian), 50 * Mathf.Sin(radian));

            realObj.GetComponent<RectTransform>().localPosition = pos + parent_pos; // 부모 좌표에 본인 좌표를 더해줌

            LineRenderer line = realObj.GetComponent<LineRenderer>(); // 라인렌더러를 받아온 후 
            line.positionCount = 2;

            // 부모와 자신을 연결함.
            line.SetPosition(0, obj_skill.parent_Skill.GetComponent<RectTransform>().position);
            line.SetPosition(1, realObj.GetComponent<RectTransform>().position);

            if (skill.parent_ID == skill.group_ID) // 자신의 아이디가 누군가의 부모 ID일 경우엔 
            {
                // 그룹을 묶어주는 이미지인 BackGround를 받아와서 생성해줌.
                GameObject back_ground_Obj = Resources.Load<GameObject>("BackGround/Group_BackGround");

                GameObject back_ground_realObj = GameObject.Instantiate(back_ground_Obj, realObj.transform);
                back_ground_realObj.transform.localPosition = new Vector3(0, 0, 0);
                back_ground_realObj.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
            }
            else // 누군가의 부모가 아닐경우엔
            {
                if (skill.group_Index == 1)  // 그 그룹의 1번 인덱스 오브젝트가
                {
                    // 그룹을 묶어주는 이미지를 생성함.
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
