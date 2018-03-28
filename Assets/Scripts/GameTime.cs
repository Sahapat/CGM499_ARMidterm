using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTime : MonoBehaviour
{
    private float _minute;
    public float minute
    {
        get
        {
            return _minute;
        }
        set
        {
            _minute = value;
        }
    }
    private float _second;
    public float second
    {
        get
        {
            return _second;
        }
        set
        {
            _second = value;

            if(_second >= 60)
            {
                minute += 1;
                _second = 0;
            }
            else if(_second <= 0)
            {
                minute -= 1;
                _second = 59;
            }
        }
    }
    private WaitForSeconds waitAndCount;
    private void Awake()
    {
        waitAndCount = new WaitForSeconds(1f);
    }
    public void stopCount()
    {
        StopAllCoroutines();
    }
    public void startCount(float countDirection)
    {
        StartCoroutine(counting(countDirection));
    }
    private IEnumerator counting(float countDirection)
    {
        while(true)
        {
            second += countDirection;
            yield return waitAndCount;
        }
    }
}
