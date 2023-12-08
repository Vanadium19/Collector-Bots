using UnityEngine;

[RequireComponent(typeof(BotMover))]
public class BaseCreator : MonoBehaviour
{
    [SerializeField] private Base _basePrefab;

    private readonly bool _isDefaultIncluded = false;
    private BotMover _botMover;
    private Bot _bot;

    private void Awake()
    {
        _botMover = GetComponent<BotMover>();
        _bot = GetComponent<Bot>();
    }

    public void GoCreate(Transform flag)
    {
        StartCoroutine(_botMover.MoveTo(flag.position, flag, CreateBase));
    }

    private void CreateBase(Transform flag)
    {                
        Base newBase = Instantiate(_basePrefab, flag.position, Quaternion.identity);
        newBase.transform.LookAt(Vector3.zero);
        newBase.AddBot(_bot);
        flag.GetComponent<Flag>().Switch(_isDefaultIncluded);
    }
}
