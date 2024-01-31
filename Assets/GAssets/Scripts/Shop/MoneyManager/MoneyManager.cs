using UnityEngine;
using Zenject;

public class MoneyManager : MonoBehaviour
{
    [Inject] private EventBus _eventBus;
    private int _money;
    private MoneyView[] _moneyViews;
    void Start()
    {
        _eventBus.Subscribe<int>(EventType.AddMoney, AddMoney);
        _eventBus.Unsubscribe<int>(EventType.SubtractMoney, SubtractMoney);
        _money = PlayerPrefs.GetInt("Money", 0);
        _moneyViews = FindObjectsOfType<MoneyView>();
        UpdateViews();

    }
    void OnDestroy()
    {
        _eventBus.Unsubscribe<int>(EventType.AddMoney, AddMoney);
        _eventBus.Unsubscribe<int>(EventType.SubtractMoney, AddMoney);
    }
    private void UpdateViews()
    {
        foreach (var view in _moneyViews)
        {
            view.UpdateView(_money);
        }
    }
    public void AddMoney(int amount)
    {
        _money += amount;

        PlayerPrefs.SetInt("Money", _money);
        UpdateViews();
    }

    public void SubtractMoney(int amount)
    {
        _money -= amount;

        PlayerPrefs.SetInt("Money", _money);
        UpdateViews();
    }
    public int GetMoneyCount()
    {
        return _money;
    }
}
