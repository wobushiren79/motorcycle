/*
* FileName: PersonInfo 
* Author: AppleCoffee 
* CreateTime: 2021-05-08-13:42:21 
*/

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class PersonInfoService : BaseMVCService
{
    public PersonInfoService() : base("person_info", "")
    {

    }

    /// <summary>
    /// 查询所有数据
    /// </summary>
    /// <returns></returns>
    public List<PersonInfoBean> QueryAllData()
    {
        List<PersonInfoBean> listData = BaseQueryAllData<PersonInfoBean>();
        return listData; 
    }

    /// <summary>
    /// 查询数据
    /// </summary>
    /// <returns></returns>
    public PersonInfoBean QueryData()
    {
        return null; 
    }

    /// <summary>
    /// 通过ID查询数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public List<PersonInfoBean> QueryDataById(long id)
    {
        return BaseQueryData<PersonInfoBean>("link_id", "id", id + "");
    }

    public bool DeleteDataById(long id)
    {
        return BaseDeleteDataById(id);
    }

    /// <summary>
    /// 更新数据
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public bool UpdateData(PersonInfoBean data)
    {
        bool deleteState = BaseDeleteDataById(data.id);
        if (deleteState)
        {
            bool insertSuccess = BaseInsertData(tableNameForMain, data);
            return insertSuccess;
        }
        return false;
    }
}