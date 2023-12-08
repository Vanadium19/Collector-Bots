using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(FlagSetter))]
public class ClickReceiver : MonoBehaviour, IPointerClickHandler
{
    private FlagSetter _flagSetter;

    private void Awake()
    {
        _flagSetter = GetComponent<FlagSetter>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _flagSetter.enabled = true;
    }    
}
