using TMPro;
using UnityEngine;

public class MoneyView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _moneyText;

    public void UpdateView(int moneyCount)
    {
        _moneyText.text = moneyCount.ToString();
    }
}
