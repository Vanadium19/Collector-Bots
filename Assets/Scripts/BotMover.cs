using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BotMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private int _distance = 1;

    public IEnumerator MoveTo(Vector3 targetPosition, Transform resource, UnityAction<Transform> botAction)
    {
        transform.LookAt(targetPosition);

        while (Vector3.Distance(transform.position, targetPosition) > _distance)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(targetPosition.x, transform.position.y, targetPosition.z), _speed * Time.deltaTime);

            yield return null;
        }

        botAction?.Invoke(resource);
    }
}
