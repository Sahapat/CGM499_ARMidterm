using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public enum merchantTrend
{
    goUp,
    goDown,
    stay
};
public enum selectedFactory
{
    factory1,
    factory2,
    None
};
public class Merchant : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField]
    private Image factoryImg1;
    [SerializeField]
    private Image factoryImg2;
    [SerializeField]
    private Text merchant1PriceTxt;
    [SerializeField]
    private Text merchant2PriceTxt;


    public selectedFactory selected;

    private float merchant1price;
    private float merchant2price;

    private merchantTrend[] merchant1Trend;
    private merchantTrend[] merchant2Trend;

    private float merchant1Count;
    private float merchant2Count;

    private int merchant1RunIndex = 0;
    private int merchant2RunIndex = 0;

    [SerializeField]
    private int maxDuration = 15;
    [SerializeField]
    private int minDuration = 8;

    private byte merchantLeght;
    public void initmerchantprice(int merchant1Price, int merchant2Price)
    {
        this.merchant1price = merchant1Price;
        this.merchant2price = merchant2Price;
    }
    public void InitTrend()
    {
        merchant1Trend = new merchantTrend[3] { merchantTrend.goDown, merchantTrend.goUp, merchantTrend.stay };
        merchant2Trend = new merchantTrend[3] { merchantTrend.goDown, merchantTrend.goUp, merchantTrend.stay };
        merchantLeght = (byte)merchant1Trend.Length;

        for (int i = 0; i < merchantLeght; i++)
        {
            int swapIndex1 = Random.Range(0, merchantLeght);
            int swapIndex2 = Random.Range(0, merchantLeght);

            merchantTrend temp = merchant1Trend[i];
            merchant1Trend[i] = merchant1Trend[swapIndex1];
            merchant1Trend[swapIndex1] = temp;

            temp = merchant2Trend[i];
            merchant2Trend[i] = merchant2Trend[swapIndex2];
            merchant2Trend[swapIndex2] = temp;
        }
    }
    public void merchantPriceUpdate()
    {
        if (merchant1Count <= 0)
        {
            if (merchant1RunIndex + 1 < merchantLeght)
            {
                merchant1RunIndex += 1;
            }
            else
            {
                merchant1RunIndex = 0;
                InitTrend();
            }
            merchant1Count = Random.Range(minDuration, maxDuration);
        }

        if (merchant2Count <= 0)
        {
            if (merchant2RunIndex + 1 < merchantLeght)
            {
                merchant2RunIndex += 1;
            }
            else
            {
                merchant2RunIndex = 0;
                InitTrend();
            }
            merchant2Count = Random.Range(minDuration, maxDuration);
        }

        switch (merchant1Trend[merchant1RunIndex])
        {
            case merchantTrend.goDown:
                merchant1price -= Time.deltaTime;
                if (merchant1price < 0)
                {
                    merchant1Trend[merchant1RunIndex] = merchantTrend.goUp;
                }
                break;
            case merchantTrend.goUp:
                merchant1price += Time.deltaTime;
                break;
        }
        switch (merchant2Trend[merchant2RunIndex])
        {
            case merchantTrend.goDown:
                merchant2price -= Time.deltaTime;
                if (merchant2price < 0)
                {
                    merchant2Trend[merchant2RunIndex] = merchantTrend.goUp;
                }
                break;
            case merchantTrend.goUp:
                merchant2price += Time.deltaTime;
                break;
        }
        merchant1PriceTxt.text = ((int)merchant1price).ToString();
        merchant2PriceTxt.text = ((int)merchant2price).ToString();
        updateImg();
        merchant1Count -= Time.deltaTime;
        merchant2Count -= Time.deltaTime;
    }
    public int sellingOil(int tranferOil)
    {
        int returnInCome = 0;
        switch(selected)
        {
            case selectedFactory.factory1:
                returnInCome = (int)(merchant1price * tranferOil / 10);
                break;
            case selectedFactory.factory2:
                returnInCome = (int)(merchant2price * tranferOil / 10);
                break;
        }
        return returnInCome;
    }
    public void selectFactory1()
    {
        selected = selectedFactory.factory1;
    }
    public void selectFactory2()
    {
        selected = selectedFactory.factory2;
    }
    public void unSelected()
    {
        selected = selectedFactory.None;
    }
    private void updateImg()
    {
        switch(selected)
        {
            case selectedFactory.factory1:
                factoryImg1.color = Color.white;
                factoryImg2.color = Color.grey;
                break;
            case selectedFactory.factory2:
                factoryImg1.color = Color.grey;
                factoryImg2.color = Color.white;
                break;
            case selectedFactory.None:
                factoryImg1.color = Color.grey;
                factoryImg2.color = Color.grey;
                break;
        }
    }

}
