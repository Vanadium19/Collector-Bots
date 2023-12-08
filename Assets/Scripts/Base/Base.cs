using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(ResourceStorage))]
[RequireComponent(typeof(Scaner))]
public class Base : MonoBehaviour
{
    [SerializeField] private List<Bot> _bots;       
    [SerializeField] private float _botSearchDelay;    
    
    private Scaner _scaner;
    private ResourceStorage _resourceStorage;      
    private Transform _flag;

    private void Awake()
    {
        _resourceStorage = GetComponent<ResourceStorage>();
        _scaner = GetComponent<Scaner>();        
    }

    private void Start()
    {
        StartCoroutine(CommandBots());
    }

    public void AddResource(Resource resource)
    {
        _resourceStorage.AddResource(resource);
    }

    public void RemoveBot(Bot bot)
    {
        _bots.Remove(bot);
    }

    public void AddBot(Bot bot)
    {        
        _bots.Add(bot);
        bot.SetBase(this);
    }

    public void SetFlag(Transform flag)
    {
        _flag = flag;
    }    

    private IEnumerator CommandBots()
    {
        var wait = new WaitForSeconds(_botSearchDelay);

        while (true)
        {
            if (_flag != null || _scaner.CurrentResource != null)
            {
                Bot bot = null;

                while (bot == null)
                {
                    bot = _bots.FirstOrDefault(bot => bot.IsBusy == false);
                    yield return wait;
                }

                SendBot(bot);
            }

            yield return null;
        }
    }

    private void SendBot(Bot bot)
    {
        if (_flag != null)
        {
            bot.CreateBase(_flag);
            _flag = null;
        }
        else
        {
            bot.CollectResource(_resourceStorage.ResourceContainerPosition, _scaner.CurrentResource.transform);
            _scaner.ResetResource();
        }
    }
}
