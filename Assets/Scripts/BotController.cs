using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BotMover))]
public class BotController : Bot
{
    private BotMover _botMover;
    private Transform _resourceContainer;

    private void Awake()
    {
        _botMover = GetComponent<BotMover>();
        _resourceContainer = Base.ResourceContainer;
    }

    public override void CollectResource() => StartCoroutine(StartResourceCollection());

    private IEnumerator StartResourceCollection()
    {
        yield return StartCoroutine(_botMover.MoveTo(TargetResource.position, PickUpResource));
        yield return StartCoroutine(_botMover.MoveTo(_resourceContainer.position, PutResource));
    }

    private void PickUpResource()
    {        
        TargetResource.position = default;
        TargetResource.SetParent(transform);
        TargetResource.localPosition = transform.position;
        TargetResource.gameObject.SetActive(false);
    }

    private void PutResource()
    {
        Base.AddResource(TargetResource.GetComponent<Resource>());
        TargetResource = null;
    }
}
