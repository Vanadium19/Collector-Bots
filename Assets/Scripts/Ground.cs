using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private List<Resource> _resources = new List<Resource>();
    private int _firstIndexNumber = 0;

    public bool HasResource => _resources.Count > 0;

    public void AddResource(Resource resource)
    {
        _resources.Add(resource);
    }

    public Resource GetResource()
    {
        Resource resource = _resources[_firstIndexNumber];       
        _resources.Remove(resource);
        return resource;
    }
}
