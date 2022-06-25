using UnityEngine.UI;
using UnityEngine;

public class HUD : MonoBehaviour
{

    [SerializeField] private Text _textCountMoney;
    private void Awake()
    {
        // Берёт информацию из евент менеджера
        Events.MoneyChange += OnMoneyChange; // подписка на канал EventManager.HealthChanged, вызов OnHealthChanged при оповещении
    }
    // Start is called before the first frame update
    private void Start()
    {
        _textCountMoney = _textCountMoney.GetComponent<Text>();
    }
    private void OnMoneyChange()
    {
        _textCountMoney.text = (GameMeneger.GetGloabalCountMoney()).ToString();
    }



    // Update is called once per frame
    protected void OnDestroy()
    {
        Events.MoneyChange -= OnMoneyChange;
    }
}
