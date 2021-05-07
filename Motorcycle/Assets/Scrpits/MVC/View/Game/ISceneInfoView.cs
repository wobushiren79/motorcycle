/*
* FileName: SceneInfo 
* Author: AppleCoffee 
* CreateTime: 2021-05-07-14:14:26 
*/

using UnityEngine;
using System;
using System.Collections.Generic;

public interface ISceneInfoView
{
	void GetSceneInfoSuccess<T>(T data, Action<T> action);

	void GetSceneInfoFail(string failMsg, Action action);
}