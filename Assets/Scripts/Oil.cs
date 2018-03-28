using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oil : MonoBehaviour
{
    [SerializeField]
    private int oilInBlock;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private float oilTranferCount = 0;
    private int tranferSpeed = 1;
    private int tranfer = 2;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public int getOil()
    {
        if (oilInBlock > 0)
        {
            int outOil = 0;
            if (oilTranferCount <= 0)
            {
                outOil = tranfer;
                oilTranferCount = tranferSpeed;
                oilInBlock -= tranferSpeed;
            }
            oilTranferCount -= Time.deltaTime;
            return outOil;
        }
        else
        {
            spriteRenderer.color = new Color(0, 0, 0, 0);
            return -1;
        }
    }
}
