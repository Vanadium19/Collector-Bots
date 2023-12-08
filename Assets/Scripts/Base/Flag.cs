using UnityEngine;
using UnityEngine.Events;

public class Flag : MonoBehaviour
{
    public event UnityAction Installed;

    private void Awake()
    {
        Switch(false);
    }

    private void OnEnable()
    {
        Installed?.Invoke();
    }   

    public void Switch(bool isIncluded)
    {
        gameObject.SetActive(isIncluded);
        enabled = isIncluded;
    }
}
