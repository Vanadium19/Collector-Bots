using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Base : MonoBehaviour
{
    [SerializeField] private Ground _ground;
    [SerializeField] private List<Bot> _bots;
    [SerializeField] private Transform _resourceContainer;
    [SerializeField] private float _scanDelay;
    [SerializeField] private float _botSearchDelay;

    public event UnityAction<int> ResourceCountChanged;

    private Resource _currentResource;
    private List<Resource> _resources = new List<Resource>();
    private float _elapsedScanTime;

    public List<Bot> Bots => _bots;
    public Transform ResourceContainer => _resourceContainer;

    private void Start()
    {
        StartCoroutine(ScanGround());
    }   

    public void AddResource(Resource resource)
    {
        resource.transform.SetParent(_resourceContainer);
        resource.transform.localPosition = _resourceContainer.position;
        _resources.Add(resource);        
        ResourceCountChanged?.Invoke(_resources.Count);
    }

    private IEnumerator ScanGround()
    {
        while (true)
        {
            _elapsedScanTime += Time.deltaTime;

            if (_elapsedScanTime >= _scanDelay && _currentResource == null)
            {
                if (TryFindResource())
                {
                    StartCoroutine(SearchBot());
                    _elapsedScanTime = 0;
                }
            }

            yield return null;
        }
    }

    private IEnumerator SearchBot()
    {
        Bot bot = null;
        var wait = new WaitForSeconds(_botSearchDelay);

        while (bot == null)
        {
            bot = _bots.FirstOrDefault(bot => bot.IsBusy == false);
            yield return wait;
        }

        SendBot(bot);
    }

    private void SendBot(Bot bot)
    {
        bot.SetResource(_currentResource.transform);
        bot.CollectResource();
        _currentResource = null;
    }

    private bool TryFindResource()
    {
        if (_ground.HasResource)
        {
            var resource = _ground.transform.Find(NamesData.Resource);

            if (resource != null)
            {
                _currentResource = resource.GetComponent<Resource>();
                _currentResource.transform.parent = null;               
            }
        }

        return _currentResource != null;
    }
}
