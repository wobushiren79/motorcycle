using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : BaseMonoBehaviour
{
    protected PlayerControl playerControl;

    protected List<GameObject> listPerson = new List<GameObject>();

    int number = 0;
    private void Awake()
    {
        listPerson.Clear();
        playerControl = CptUtil.AddCpt<PlayerControl>(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(TagInfo.Tag_BuildingPerson))
        {
            HandleForAddPerson(other.gameObject);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag.Equals(TagInfo.Tag_BuildingEnd))
        {
            HandleForEnd();
        }
    }

    public void HandleForAddPerson(GameObject objPersonBuilding)
    {
        BuildingPerson buildingPerson = objPersonBuilding.GetComponent<BuildingPerson>();
        number++;
        PersonHandler.Instance.SetPersonNumber(number);
    }

    public void HandleForEnd()
    {
        GameHandler.Instance.EndGame();
    }

    public void SubPerson()
    {
        number--;
        PersonHandler.Instance.SetPersonNumber(number);
    }

}