using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ActionBase : MonoBehaviour
{
    [HideInInspector]
    public Character currentCharacter;

    private Vector3 initPosition;
    private bool isDragging = false;

    public event Action OnActionEnd = delegate { };



    private void OnMouseDown()
    {
        if (currentCharacter.type == CharacterType.Friend)
            isDragging = true;
        initPosition = transform.position;
    }
    private void OnMouseUp()
    {
        if (currentCharacter.type == CharacterType.Friend)
        {
            isDragging = false;
            TransformToInitPoint();
        }
    }

    private void OnMouseDrag()
    {
        if (currentCharacter.type == CharacterType.Friend)
        {
            Vector3 mouseOnWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mouseOnWorldPosition.x, transform.position.y, mouseOnWorldPosition.z);
        }
    }

    public void Drag(Character character)
    {
        StartCoroutine(AutoMovement(character.transform.position));
    }

    IEnumerator AutoMovement(Vector3 characterPosition)
    {
        while (Vector3.Distance(transform.position, characterPosition) > 0.1f)
        {
            yield return new WaitForFixedUpdate();
            transform.position = Vector3.MoveTowards(transform.position, characterPosition, Mathf.SmoothStep(0, 1, 10f * Time.deltaTime));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Character character))
        {
            HandleAction(character);
        }
    }

    protected virtual void HandleAction(Character character) { TransformToInitPoint(); }

    void TransformToInitPoint()
    {
        transform.position = initPosition;
    }

    public void DestroyAction()
    {
        OnActionEnd();
        Destroy(gameObject);
    }
}
