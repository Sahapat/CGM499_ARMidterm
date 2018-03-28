using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OilManager : MonoBehaviour
{
    [SerializeField]
    private int _oilStorage = 0;
    [SerializeField]
    private int tranferSpeed = 6;
    [SerializeField]
    private Text OilTxt;

    private float tranferSellCount = 0;
    private float tranferAddCount = 0;

    public int oilStorage
    {
        get
        {
            return _oilStorage;
        }
        set
        {
            _oilStorage = value;
            OilTxt.text = _oilStorage.ToString();
        }
    }
    public void tranferOilForAdd()
    {
        if (tranferAddCount <= 0)
        {
            oilStorage += tranferSpeed;
            tranferAddCount = 1;
        }
        tranferAddCount -= Time.deltaTime;
    }
    public int tranferOilForSell()
    {
        int tranferOil;
        if(tranferSellCount <= 0)
        {
            if(oilStorage > 0)
            {
                if(oilStorage - tranferSpeed < 0)
                {
                    tranferOil = oilStorage;
                    oilStorage = 0;
                }
                else
                {
                    tranferOil = oilStorage - tranferSpeed;
                    oilStorage -= tranferSpeed;
                }
                tranferSellCount = 1;
            }
            else
            {
                tranferOil = 0;
                tranferSellCount = 1;
            }
        }
        else
        {
            tranferOil = -1;
        }
        tranferSellCount -= Time.deltaTime;
        return tranferOil;
    }
}
