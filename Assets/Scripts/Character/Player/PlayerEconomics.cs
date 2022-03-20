using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEconomics : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private int _money;

    public int Money
    {
        get { return _money; }
    }

    public void Deposit(int summ)
    {
        if (summ < 0)
            throw new System.Exception("Summ can't negative");

        _money += summ;
    }

    public bool EnoughMoney(int summ)
    {
        return _money >= summ;
    }

    public void TakeMoney(int amount)
    {
        if (amount > _money)
            throw new System.Exception("The amount cannot be more than the invoice");

        _money -= amount;
    }
}
