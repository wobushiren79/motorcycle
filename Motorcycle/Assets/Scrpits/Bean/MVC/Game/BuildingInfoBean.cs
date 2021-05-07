/*
* FileName: BuildingInfo 
* Author: AppleCoffee 
* CreateTime: 2021-05-07-11:54:46 
*/

using UnityEngine;
using UnityEditor;
using System;

[Serializable]
public class BuildingInfoBean : BaseBean
{
    public int building_type;
    public string model_name;


    public BuildingTypeEnum GetBuildingType()
    {
        return (BuildingTypeEnum)building_type;
    }
}