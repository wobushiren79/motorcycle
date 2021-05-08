/*
* FileName: PersonInfo 
* Author: AppleCoffee 
* CreateTime: 2021-05-08-13:42:21 
*/

using UnityEngine;
using System;
using System.Collections.Generic;

public interface IPersonInfoView
{
	void GetPersonInfoSuccess<T>(T data, Action<T> action);

	void GetPersonInfoFail(string failMsg, Action action);
}