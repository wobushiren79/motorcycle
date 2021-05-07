using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BuildingManager : BaseManager, IBuildingInfoView,ISceneInfoView
{
    public Dictionary<string, GameObject> dicSceneBuilding = new Dictionary<string, GameObject>();
    public Dictionary<long, BuildingInfoBean> dicBuildingInfo = new Dictionary<long, BuildingInfoBean>();

    protected BuildingInfoController buildingInfoController;
    protected SceneInfoController sceneInfoController;
    private void Awake()
    {
        InitData();
    }

    public void InitData()
    {
        buildingInfoController = new BuildingInfoController(this, this);
        sceneInfoController = new SceneInfoController(this,this);

        buildingInfoController.GetAllBuildingInfoData(InitBuildingInfo);
    }

    public void GetSceneInfo(long sceneId,Action<SceneInfoBean> action)
    {
        sceneInfoController.GetSceneInfoDataById(sceneId, action);
    }

    public GameObject GetSceneBuildingModel(long buildingId)
    {
        BuildingInfoBean buildingInfo = GetBuildingInfo(buildingId);
        if (buildingInfo != null)
        {
            GameObject gameObject = GetModel(dicSceneBuilding, "building/building", buildingInfo.model_name, "Assets/Prefabs/Building/" + buildingInfo.model_name + ".prefab");
            return gameObject;
        }
        return null;
    }
    public BuildingInfoBean GetBuildingInfo(long buildingId)
    {
        if (dicBuildingInfo.TryGetValue(buildingId, out BuildingInfoBean buildingInfo))
        {
            return buildingInfo;
        }
        return null;
    }


    public void InitBuildingInfo(List<BuildingInfoBean> listData)
    {
        dicBuildingInfo.Clear();
        for (int i = 0; i < listData.Count; i++)
        {
            BuildingInfoBean itemInfo = listData[i];
            dicBuildingInfo.Add(itemInfo.id, itemInfo);
        }
    }


    public void GetBuildingInfoSuccess<T>(T data, Action<T> action)
    {
        action?.Invoke(data);
    }

    public void GetBuildingInfoFail(string failMsg, Action action)
    {
    }

    public void GetSceneInfoSuccess<T>(T data, Action<T> action)
    {
        action?.Invoke(data);
    }

    public void GetSceneInfoFail(string failMsg, Action action)
    {
    }
}