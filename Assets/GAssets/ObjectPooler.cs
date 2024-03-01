using TigerForge;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private EPObjectPoolerScriptableObject _moneyPool;
    
    void Start()
    {
        _moneyPool.InitializePool(2);
    }

    public GameObject RequestMoneyObject()
    {
        return _moneyPool.GetObject();
    }

    public void RemoveMoneyObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
    } 
}
