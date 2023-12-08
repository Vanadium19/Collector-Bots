using UnityEngine;
using UnityEngine.Events;

public class Flag : MonoBehaviour
{
    public event UnityAction Installed;

    private void Awake()
    {
        SwitchOff();
    }

    private void OnEnable()
    {
        Installed?.Invoke();
    }

    public void SwitchOff()
    {
        gameObject.SetActive(false);
        enabled = false;
    }

    public void SwitchOn()
    {
        gameObject.SetActive(true);
        enabled = true;
    }
}
