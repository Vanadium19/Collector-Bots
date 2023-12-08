using UnityEngine;

[RequireComponent(typeof(ResourceCollector))]
[RequireComponent(typeof(BaseCreator))]
public class Bot : MonoBehaviour
{
    [SerializeField] private Base _base;

    private ResourceCollector _resourceCollector;
    private BaseCreator _baseCreator;
    private bool _isBusy = false;

    public bool IsBusy => _isBusy;

    private void Awake()
    {
        _resourceCollector = GetComponent<ResourceCollector>();
        _baseCreator = GetComponent<BaseCreator>();
    }   

    public void SetBase(Base defaultBase)
    {
        if (_base != null)
            _base.RemoveBot(this);

        _base = defaultBase;
        _isBusy = false;
    }

    public void CreateBase(Transform flag)
    {
        _baseCreator.GoCreate(flag);
        _isBusy = true;
    }

    public void CollectResource(Vector3 resourceContainer, Transform resource)
    {
        _resourceCollector.StartCollecting(resourceContainer, resource);
        _isBusy = true;
    }

    public void HandOverResource(Transform resource)
    {
        _base.AddResource(resource.GetComponent<Resource>());
        _isBusy = false;
    }
}
