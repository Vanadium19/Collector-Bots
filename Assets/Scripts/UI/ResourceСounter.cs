using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Resource—ounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _resourceCount;

    private int _currentCount;
    private List<ResourceStorage> _resourceStorages = new List<ResourceStorage>();  

    private void OnDisable()
    {
        foreach (var resourceStorage in _resourceStorages)        
            resourceStorage.ResourceCountChanged -= OnResourceCountChanged;        
    }

    public void AddResourceStorage(ResourceStorage resourceStorage)
    {
        resourceStorage.ResourceCountChanged += OnResourceCountChanged;
        _resourceStorages.Add(resourceStorage);
    }

    private void OnResourceCountChanged(int resourceCount)
    {
        _currentCount += resourceCount;
        _resourceCount.text = _currentCount.ToString();
    }
}
