using UnityEngine;

public class FlagSetter : MonoBehaviour
{
    [SerializeField] private GameObject _flag;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))        
            SetFlag();        
    }

    private void SetFlag()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.GetComponent<Ground>() != null)
            {
                _flag.transform.position = hitInfo.point;

                if (_flag.activeSelf == false)                                    
                    _flag.GetComponent<Flag>().SwitchOn();                
            }
        }

        enabled = false;
    }
}
