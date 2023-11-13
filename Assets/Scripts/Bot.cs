using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bot : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Base _base;

    private Transform _resourceContainer;
    private Transform _targetResource;
    private int _distance = 1;

    public bool IsBusy => _targetResource != null;

    private void Awake()
    {
        _resourceContainer = _base.ResourceContainer;
    }

    public void SetResource(Transform resource)
    {
        _targetResource = resource;
    }

    public void ExecuteMission()
    {
        StartCoroutine(StartResourceCollection());
    }

    private IEnumerator StartResourceCollection()
    {
        yield return StartCoroutine(MoveTo(_targetResource.position, PickUpResource));
        yield return StartCoroutine(MoveTo(_resourceContainer.position, PutResource));        
    }    

    private IEnumerator MoveTo(Vector3 targetPosition, UnityAction botAction)
    {                
        transform.LookAt(targetPosition);

        while (Vector3.Distance(transform.position, targetPosition) > _distance)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(targetPosition.x, transform.position.y, targetPosition.z), _speed * Time.deltaTime);

            yield return null;
        }

        botAction?.Invoke();        
    }

    private void PickUpResource()
    {
        _targetResource.position = default;
        _targetResource.SetParent(transform);
        _targetResource.localPosition = transform.position;
        _targetResource.gameObject.SetActive(false);
    }

    private void PutResource()
    {
        _base.AddResource(_targetResource.GetComponent<Resource>());
        _targetResource = null;
    }
}
