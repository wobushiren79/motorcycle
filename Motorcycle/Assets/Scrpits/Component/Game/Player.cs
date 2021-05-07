using UnityEditor;
using UnityEngine;

public class Player : BaseMonoBehaviour
{
    protected PlayerControl playerControl;

    private void Awake()
    {
        playerControl = CptUtil.AddCpt<PlayerControl>(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(TagInfo.Tag_Person))
        {
            HandleForAddPerson(other.gameObject);
            Destroy(other.gameObject);
        }
    }

    public void HandleForAddPerson(GameObject gameObject)
    {
        BuildingPerson buildingPerson = gameObject.GetComponent<BuildingPerson>();
    }

}