using UnityEngine;

public abstract class Bot : MonoBehaviour
{
    [SerializeField] protected Base Base;    
   
    protected Transform TargetResource;    

    public bool IsBusy => TargetResource != null;

    public void SetResource(Transform resource) => TargetResource = resource;

    public abstract void CollectResource();    
}
