using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Resource—ounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _resourceCount;
    [SerializeField] private Base _base;

    private void OnEnable()
    {
        _base.ResourceCountChanged += OnResourceCountChanged;
    }

    private void OnDisable()
    {
        _base.ResourceCountChanged -= OnResourceCountChanged;
    }

    private void OnResourceCountChanged(int resourceCount)
    {
        _resourceCount.text = resourceCount.ToString();
    }
}
