/*
* FileName: BuildingInfo 
* Author: AppleCoffee 
* CreateTime: 2021-05-07-11:54:46 
*/

using UnityEngine;
using System;
using System.Collections.Generic;

public interface IBuildingInfoView
{
	void GetBuildingInfoSuccess<T>(T data, Action<T> action);

	void GetBuildingInfoFail(string failMsg, Action action);
}