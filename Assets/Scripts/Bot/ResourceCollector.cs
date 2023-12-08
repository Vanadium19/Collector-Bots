using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BotMover))]
[RequireComponent(typeof(Bot))]
public class ResourceCollector : MonoBehaviour
{
    private BotMover _botMover;
    private Bot _bot;

    private void Awake()
    {
        _botMover = GetComponent<BotMover>();
        _bot = GetComponent<Bot>();
    }

    public void StartCollecting(Vector3 startPosition, Transform resource)
    {
        StartCoroutine(StartResourceCollection(startPosition, resource));
    }

    private IEnumerator StartResourceCollection(Vector3 startPosition, Transform resource)
    {
        yield return StartCoroutine(_botMover.MoveTo(resource.position, resource, PickUpResource));
        StartCoroutine(_botMover.MoveTo(startPosition, resource, PutResource));
    }

    private void PickUpResource(Transform resource)
    {
        resource.position = default;
        resource.SetParent(transform);
        resource.localPosition = transform.position;
        resource.gameObject.SetActive(false);
    }

    private void PutResource(Transform resource)
    {
        _bot.HandOverResource(resource);
    }
}
