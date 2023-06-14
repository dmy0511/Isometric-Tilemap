using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTxt : MonoBehaviour
{
    float m_EffTime = 1.1f;

    void Start()
    {

    }

    void Update()
    {
        m_EffTime -= Time.deltaTime;
        if (m_EffTime <= 0.0f)
            Destroy(gameObject);
    }

    public void InitDmgText(float a_Damage, Color a_Color)
    {
        Text a_ThisText = GetComponentInChildren<Text>();

        if (0.0f < a_Damage)
            a_ThisText.text = "+ " + (int)a_Damage;
        else if (a_Damage < 0.0f)
        {
            a_Damage = Mathf.Abs(a_Damage);
            a_ThisText.text = "- " + (int)a_Damage;
        }
        else
        {
            a_ThisText.text = a_Damage.ToString();
        }

        a_ThisText.color = a_Color;
    }
}
