using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Base))]
public class Scaner : MonoBehaviour
{
    [SerializeField] private float _scanDelay;

    private Resource _currentResource;

    private float _elapsedScanTime;
    private Ground _ground;

    public Resource CurrentResource => _currentResource;

    private void Awake()
    {        
        _ground = GameObject.FindObjectOfType<Ground>();
    }

    private void Start()
    {
        StartCoroutine(ScanGround());
    }

    public void ResetResource()
    {
        _currentResource = null;
    }

    private IEnumerator ScanGround()
    {
        while (true)
        {
            _elapsedScanTime += Time.deltaTime;

            if (_elapsedScanTime >= _scanDelay && _currentResource == null)
            {
                if (TryFindResource())                                    
                    _elapsedScanTime = 0;                
            }

            yield return null;
        }
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
