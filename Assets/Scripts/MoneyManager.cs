using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    private static MoneyManager instance = null;
    public int moneyUp;
    // Singleton Instance에 접근하기 위한 프로퍼티
    public static MoneyManager Instance
    {
        get
        {
            return instance;
        }
    }
    
    void Awake()
    {
        if(instance)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
