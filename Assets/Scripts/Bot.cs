using UnityEngine;

[RequireComponent(typeof(ResourceCollector))]
public class Bot : MonoBehaviour
{
    [SerializeField] private Base _base;

    private ResourceCollector _resourceCollector;
    private bool _isBusy = false;

    public bool IsBusy => _isBusy;

    private void Awake()
    {
        _resourceCollector = GetComponent<ResourceCollector>();        
    }   

    public void CollectResource(Vector3 _resourceContainer, Transform resource)
    {
        _resourceCollector.StartCollecting(_resourceContainer, resource);
        _isBusy = true;
    }

    public void HandOverResource(Transform resource)
    {
        _base.AddResource(resource.GetComponent<Resource>());
        _isBusy = false;
    }
}
