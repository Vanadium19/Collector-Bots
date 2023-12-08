using UnityEngine;

public class Ground : MonoBehaviour
{    
    public bool HasResource => transform.childCount > 0;    

    public void AddResource(Resource resource)
    {
        resource.name = NamesData.Resource;
        resource.transform.SetParent(transform);
    }
}
