using System;
using UnityEditor;
using UnityEngine;

public class BuildingHandler : BaseHandler<BuildingHandler, BuildingManager>
{

    private void Update()
    {

    }

    public void InitSceneData(long sceneId, Action callBack)
    {
        manager.GetSceneInfo(sceneId, (sceneInfo) => {

            SceneDetailsBean sceneDetails = sceneInfo.GetSceneDetilas();
            for (int i = 0; i < sceneDetails.listBuildingData.Count; i++)
            {
                SceneDetailsItemBean sceneDetailsItem = sceneDetails.listBuildingData[i];
                GameObject objModel = manager.GetSceneBuildingModel(sceneDetailsItem.buildingId);
                GameObject objItem = Instantiate(gameObject, objModel);

                objItem.transform.position = sceneDetailsItem.position.GetVector3();
                objItem.transform.localScale = sceneDetailsItem.size.GetVector3();
                objItem.transform.eulerAngles = sceneDetailsItem.angle.GetVector3();

                BuildingInfoBean buildingInfo = manager.GetBuildingInfo(sceneDetailsItem.buildingId);

                BuildingBase baseBuilding;
                switch (buildingInfo.GetBuildingType())
                {
                    case BuildingTypeEnum.Ground:
                        baseBuilding = objItem.AddComponent<BuildingGround>();
                        break;
                    case BuildingTypeEnum.Person:
                        baseBuilding = objItem.AddComponent<BuildingPerson>();
                        break;
                    case BuildingTypeEnum.Obstacle:
                        baseBuilding = objItem.AddComponent<BuildingObstacle>();
                        break;
                    default:
                        baseBuilding = objItem.AddComponent<BuildingBase>();
                        break;
                }
                baseBuilding.buildingInfo = buildingInfo;
            }

            callBack?.Invoke();

        });
    }

}