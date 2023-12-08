using UnityEngine;

[RequireComponent(typeof(Base))]
[RequireComponent(typeof(ResourceStorage))]
public class ObjectCreator : MonoBehaviour
{
    [Header("Base")]
    [SerializeField] private Flag _flag;
    [SerializeField] private int _basePrice;

    [Header("Bot")]
    [SerializeField] private int _botPrice;
    [SerializeField] private Bot _botPrefab;
    [SerializeField] private Transform _botCreationPoint;

    private int _currentResourceCount;
    private Base _base;
    private ResourceStorage _resourceStorage;    
    private bool _isBuildBase = false;

    private void Awake()
    {
        _base = GetComponent<Base>();
        _resourceStorage = GetComponent<ResourceStorage>();
    }

    private void OnEnable()
    {
        _resourceStorage.ResourceCountChanged += TryCreateObject;
        _flag.Installed += SwitchToBaseBuilding;
    }

    private void OnDisable()
    {
        _resourceStorage.ResourceCountChanged -= TryCreateObject;
        _flag.Installed -= SwitchToBaseBuilding;
    }

    private void SwitchToBaseBuilding()
    {
        _isBuildBase = true;
    }

    private void TryCreateObject(int resourceCount)
    {
        int currentPrice = _isBuildBase ? _basePrice : _botPrice;
        _currentResourceCount += resourceCount;

        if (_currentResourceCount >= currentPrice)
            CreateObject();
    }

    private void CreateObject()
    {
        if (_isBuildBase)
            CreateBase();
        else
            CreateBot();
    }

    private void CreateBot()
    {        
        _resourceStorage.RemoveResource(_botPrice);
        var bot = Instantiate(_botPrefab, _botCreationPoint.position, Quaternion.identity);
        _base.AddBot(bot);
    }

    private void CreateBase()
    {        
        _resourceStorage.RemoveResource(_basePrice);
        _base.SetFlag(_flag.transform);
        _isBuildBase = false;
    }
}
