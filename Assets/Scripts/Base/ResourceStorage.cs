using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResourceStorage : MonoBehaviour
{
    [SerializeField] private Transform _resourceContainer;

    private readonly int _oneResourceCount = 1;
    private List<Resource> _resources = new List<Resource>();
    
    public Vector3 ResourceContainerPosition => _resourceContainer.position;

    public event UnityAction<int> ResourceCountChanged;

    private void Awake()
    {
        GameObject.FindObjectOfType<ResourceÑounter>().AddResourceStorage(this);
    }

    public void AddResource(Resource resource)
    {
        resource.transform.SetParent(_resourceContainer);
        resource.transform.localPosition = _resourceContainer.position;
        _resources.Add(resource);
        ResourceCountChanged?.Invoke(_oneResourceCount);
    }

    public void RemoveResource(int resourceCount)
    {
        Resource resource;

        for (int i = 0; i < resourceCount; i++)
        {
            resource = _resources[0];
            _resources.Remove(resource);
            Destroy(resource.gameObject);
        }

        ResourceCountChanged?.Invoke(-resourceCount);
    }
}
