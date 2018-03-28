using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class moneyManager : MonoBehaviour
{
    [SerializeField]
    private int _moneyStorage = 0;
    [SerializeField]
    private Text moneyTxt;

    public int moneyStorage
    {
        get
        {
            return _moneyStorage;
        }
        set
        {
            _moneyStorage = value;
            moneyTxt.text = _moneyStorage.ToString();
        }
    }

    public bool cutOffMoney(int cutOff)
    {
        bool canCutOff = !(moneyStorage - cutOff < 0);
        if(canCutOff)
        {
            moneyStorage -= cutOff;
        }
        return canCutOff;
    }
}
