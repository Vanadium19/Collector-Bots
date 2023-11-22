using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ground))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private Vector3[] _spawnPoints;
    [SerializeField] private Resource _resourceTemplate;
    [SerializeField] private float _delay;

    private Ground _ground;

    private void Awake()
    {
        _ground = GetComponent<Ground>();
    }

    private void Start()
    {        
        StartCoroutine(Generate());
    }

    private void SpawnResource()
    {
        var resource = Instantiate(_resourceTemplate, _spawnPoints[Random.Range(0, _spawnPoints.Length)], Quaternion.identity);
        _ground.AddResource(resource);
    }

    private IEnumerator Generate()
    {
        var wait = new WaitForSeconds(_delay);

        while (true)
        {
            SpawnResource();
            yield return wait;
        }
    }
}
