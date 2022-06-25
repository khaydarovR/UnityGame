using UnityEngine;
using System;

public class Events : MonoBehaviour
{
    //для нескольких слушателей!
    public static event Action<bool> Air; // 

    public static void OnAir(bool value)
    {
        Air?.Invoke(value);
        //Debug.Log("Оповещение - Сенсор не в земле");
    }

    public static event Action<float> HealthChanged; //создание канала дл¤ подписок

    public static void OnHealthChanged(float value) //Информация идет от персонажа к - хпбару
    {
        HealthChanged?.Invoke(value);
    }

    public static event Action MoneyChange; // 

    public static void OnMoneyChange()
    {
        MoneyChange?.Invoke();
    }
}
