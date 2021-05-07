using UnityEditor;
using UnityEngine;

public class SceneEditorWindow : EditorWindow
{
    protected long sceneId;
    protected long buildingId;
    protected SceneInfoService sceneInfoService;
    [MenuItem("工具/创建关卡")]
    static void CreateWindows()
    {
        EditorWindow.GetWindow(typeof(SceneEditorWindow));
    }

    private void OnEnable()
    {
        sceneInfoService = new SceneInfoService();
        BuildingHandler.Instance.manager.InitData();
    }

    private void OnDestroy()
    {
        DestroyImmediate(BuildingHandler.Instance.gameObject);
    }


    Vector2 scrollPosition = Vector2.zero;
    private void OnGUI()
    {
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        EditorGUILayout.BeginVertical();

        EditorGUILayout.BeginHorizontal();
        EditorUI.GUIText("场景ID");
        sceneId = EditorUI.GUIEditorText(sceneId);
        if (EditorUI.GUIButton("读取场景"))
        {
            LoadSceneData();
        }
        if (EditorUI.GUIButton("保存场景"))
        {
            SaveSceneData();
        }

        EditorGUILayout.EndHorizontal();


        EditorGUILayout.BeginHorizontal();
        EditorUI.GUIText("物件ID");
        buildingId = EditorUI.GUIEditorText(buildingId);
        if (EditorUI.GUIButton("加载物件"))
        {
            GameObject objBuildingModel = BuildingHandler.Instance.manager.GetSceneBuildingModel(buildingId);
            GameObject objBuilding = Instantiate(objBuildingModel, BuildingHandler.Instance.transform);
            BuildingBase baseBuilding =  CptUtil.AddCpt<BuildingBase>(objBuilding);
            baseBuilding.buildingInfo = BuildingHandler.Instance.manager.GetBuildingInfo(buildingId);
        }


        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();
        EditorGUILayout.EndScrollView();
    }

    public void SaveSceneData()
    {
        SceneInfoBean sceneInfo = new SceneInfoBean();
        sceneInfo.id = sceneId;
        sceneInfo.valid = 1;

        SceneDetailsBean sceneDetails = new SceneDetailsBean();
        for (int i = 0; i < BuildingHandler.Instance.transform.childCount; i++)
        {
            Transform tfItem = BuildingHandler.Instance.transform.GetChild(i);
            BuildingBase baseBuilding = tfItem.GetComponent<BuildingBase>();
            if (baseBuilding == null)
                continue;
            SceneDetailsItemBean sceneDetailsItem = new SceneDetailsItemBean();
            sceneDetailsItem.buildingId = baseBuilding.buildingInfo.id;
            sceneDetailsItem.position = new Vector3Bean(tfItem.position);
            sceneDetailsItem.size = new Vector3Bean(tfItem.localScale);
            sceneDetailsItem.angle = new Vector3Bean(tfItem.eulerAngles);
            sceneDetails.listBuildingData.Add(sceneDetailsItem);
        }

        sceneInfo.SetSceneDetilas(sceneDetails);

        sceneInfoService.UpdateData(sceneInfo);
    }

    public void LoadSceneData()
    {
         BuildingHandler.Instance.InitSceneData(sceneId,null);
    }


}