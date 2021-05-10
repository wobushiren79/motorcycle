using DG.Tweening;
using UnityEditor;
using UnityEngine;
using System.Collections;

public class Person : BaseMonoBehaviour
{
    protected Player player;
    protected Rigidbody rbPerson;

    protected Vector3 offsetPosition = Vector3.zero;
    protected Vector3 initLocalPosition = Vector3.zero;
    protected Vector3 lastTarget = Vector3.zero;

    protected bool isFail = false;
    public float bufferTime = 0.1f;

    private void Awake()
    {
        player = GameHandler.Instance.manager.player;
        rbPerson = GetComponent<Rigidbody>();
    }

    public void SetData(Vector3 initLocalPosition, float bufferTime, bool isNew)
    {
        this.bufferTime = bufferTime;
        this.initLocalPosition = initLocalPosition;

        offsetPosition = initLocalPosition - Vector3.zero;

        if (Application.isPlaying)
        {
            if (isNew)
            {
                transform.position = player.transform.position + offsetPosition;
            }
            else
            {
                transform.DOMove(player.transform.position + offsetPosition, bufferTime);
            }
        }
        else
        {
            transform.position = Vector3.zero + offsetPosition;
        }
    }

    private void Update()
    {
        if (isFail)
            return;
        Vector3 movePosition = player.transform.position + offsetPosition;
        if (lastTarget.x - player.transform.position.x != 0)
        {
            transform.DOMoveX(movePosition.x, bufferTime).SetEase(Ease.OutBack);
        }
        if (lastTarget.z - player.transform.position.z != 0)
        {
            transform.DOMoveZ(movePosition.z, bufferTime).SetEase(Ease.OutBack);
        }
        if (lastTarget.y - player.transform.position.y != 0)
        {
            transform.DOMoveY(movePosition.y, bufferTime).SetEase(Ease.OutBack);
        }
        lastTarget = player.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isFail && collision.gameObject.tag.Equals(TagInfo.Tag_Obstacle))
        {
            isFail = true;
            rbPerson.useGravity = true;

            PersonHandler.Instance.manager.RemovePerson(this);
            transform.SetParent(transform.parent.parent);
            player.SubPerson();

            StartCoroutine(CoroutineForDestroy());
        }
    }

    public IEnumerator CoroutineForDestroy()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}