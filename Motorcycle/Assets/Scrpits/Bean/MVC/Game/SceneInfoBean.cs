/*
* FileName: SceneInfo 
* Author: AppleCoffee 
* CreateTime: 2021-05-07-14:14:26 
*/

using UnityEngine;
using UnityEditor;
using System;

[Serializable]
public class SceneInfoBean : BaseBean
{
    public string building_data;

    public SceneDetailsBean GetSceneDetilas()
    {
        SceneDetailsBean sceneDetails = JsonUtil.FromJson<SceneDetailsBean>(building_data);
        return sceneDetails;
    }

    public void SetSceneDetilas(SceneDetailsBean sceneDetails)
    {
        building_data = JsonUtil.ToJson(sceneDetails);
    }
}