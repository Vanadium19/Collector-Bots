using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Base : MonoBehaviour
{
    [SerializeField] private Ground _ground;
    [SerializeField] private List<Bot> _bots;
    [SerializeField] private Transform _resourceContainer;

    public event UnityAction<int> ResourceCountChanged;

    private Resource _currentResource;
    private List<Resource> _resources = new List<Resource>();

    public Transform ResourceContainer => _resourceContainer;

    private void Update()
    {
        if (_currentResource == null)
            Scan();

        if (_currentResource != null)
            TrySendBot();
    }

    public void AddResource(Resource resource)
    {
        resource.transform.SetParent(_resourceContainer);
        resource.transform.localPosition = _resourceContainer.position;
        _resources.Add(resource);        
        ResourceCountChanged?.Invoke(_resources.Count);
    }

    private void Scan()
    {       
        if (_ground.HasResource)
            _currentResource = _ground.GetResource();        
    }

    private void TrySendBot()
    {
        var bot = _bots.FirstOrDefault(bot => bot.IsBusy == false);

        if (bot != null)
            SendBot(bot);
    }   

    private void SendBot(Bot bot)
    {
        bot.SetResource(_currentResource.transform);
        bot.CollectResource();
        _currentResource = null;
    } 
}
