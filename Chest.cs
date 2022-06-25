using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Chest : MonoBehaviour, IDamagable
{

    [SerializeField] private int _moneyCount;
    [SerializeField] private int _strength;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _ObjectTextCount;
    [SerializeField] private TMPro.TextMeshProUGUI _textMesh;
    [SerializeField] private GameObject _particMoney;

    private void Start()
    {
        _moneyCount = Random.Range(10, 100);
        _strength = Random.Range(50, 100);
        _animator = GetComponent<Animator>();
        _textMesh = _ObjectTextCount.GetComponent<TMPro.TextMeshProUGUI>();
        Events.OnMoneyChange();
    }

    private void Update()
    {
        _animator.SetBool("Damage", false);
    }

    public void GetDamage(int value)
    {
        _strength -= value;
        _animator.SetBool("Damage", true);

        if (_strength <= 0)
        {
            Instantiate(_particMoney, transform.position, Quaternion.identity);
            _textMesh.SetText(("+" + _moneyCount).ToString());
            _ObjectTextCount.SetActive(true);
            //передача монет игроку(геймменеджеру)
            GameMeneger.AddCountMoney(_moneyCount);
            Events.OnMoneyChange();
            Destroy(gameObject, 1f);
            //StartCoroutine(Broken());

        }
    }

    IEnumerator Broken()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}