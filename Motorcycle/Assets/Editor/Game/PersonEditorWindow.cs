using UnityEditor;
using UnityEngine;

public class PersonEditorWindow : EditorWindow
{
    protected PersonInfoService personInfoService;
    protected long personId;
    protected Vector2 scrollPosition = Vector2.zero;
    [MenuItem("工具/创建造型")]
    static void CreateWindows()
    {
        EditorWindow.GetWindow(typeof(PersonEditorWindow));
    }

    private void OnEnable()
    {
        personInfoService = new PersonInfoService();

        PersonHandler.Instance.manager.InitData();
    }

    private void OnDestroy()
    {
        DestroyImmediate(PersonHandler.Instance.gameObject);
    }

    private void OnGUI()
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        GUILayout.BeginVertical();
        if (EditorUI.GUIButton("刷新"))
        {
            PersonHandler.Instance.manager.InitData();
        }
        GUILayout.BeginHorizontal();
        EditorUI.GUIText("造型ID");
        personId = EditorUI.GUIEditorText(personId);
        if (EditorUI.GUIButton("读取造型"))
        {
            LoadPersonData();
        }
        if (EditorUI.GUIButton("保存造型"))
        {
            SavePersonData();
        }
        if (EditorUI.GUIButton("删除造型"))
        {
            RemovePersonData();
        }

        GUILayout.EndHorizontal();

        GUILayout.EndVertical();
        GUILayout.EndScrollView();
    }

    public void LoadPersonData()
    {
        PersonInfoBean personInfo = PersonHandler.Instance.manager.GetPersonInfoById(personId);
        PersonHandler.Instance.SetPersonNumber( personInfo);
    }

    public void SavePersonData()
    {
        PersonInfoBean personInfo = new PersonInfoBean();
        personInfo.id = personId;
        personInfo.valid = 1;

        PersonDetailsBean personDetails = new PersonDetailsBean();

        int number = 0;
        for (int i = 0; i < PersonHandler.Instance.manager.personContainer.transform.childCount; i++)
        {
            Transform tfChild = PersonHandler.Instance.manager.personContainer.transform.transform.GetChild(i);
            Person itemPerson= tfChild.GetComponent<Person>();
            if (!tfChild.tag.Equals(TagInfo.Tag_Person))
                continue;
            //添加人
            PersonDetailsItemBean personDetailsItem = new PersonDetailsItemBean();
            personDetailsItem.position = new Vector3Bean(tfChild.localPosition);
            personDetailsItem.size = new Vector3Bean(tfChild.localScale);
            personDetailsItem.angle = new Vector3Bean(tfChild.localEulerAngles);
            personDetailsItem.bufferTime = itemPerson.bufferTime;
            Transform[] tfListPart = tfChild.GetComponentsInChildren<Transform>();
            for (int f = 0; f < tfListPart.Length; f++)
            {
                //添加部件
                Transform itemPart = tfListPart[f];
                PersonDetailsItemPartBean personDetailsItemPart = new PersonDetailsItemPartBean();
                personDetailsItemPart.position = new Vector3Bean(itemPart.localPosition);
                personDetailsItemPart.size = new Vector3Bean(itemPart.localScale);
                personDetailsItemPart.angle = new Vector3Bean(itemPart.localEulerAngles);
                personDetailsItemPart.partName = itemPart.name;
                personDetailsItem.listPartData.Add(personDetailsItemPart);
            }
            number++;
            personDetails.listPersonData.Add(personDetailsItem);
        }
        personInfo.person_number = number;
        personInfo.SetPersonDetilas(personDetails);

        personInfoService.UpdateData(personInfo);
    }

    public void RemovePersonData()
    {
        personInfoService.DeleteDataById(personId);
    }
}