using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_ctrl : MonoBehaviour
{
    public GameObject attack;
    public Transform pos;
    public float cooltime;
    private float curtime;
    void Update()
    {
        Vector2 len = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float z = Mathf.Atan2(len.y, len.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, z);
        if(curtime <= 0)
        {
            if(Input.GetKey(KeyCode.Alpha1))
            {
                Instantiate(attack, pos.position, transform.rotation);
            }
            curtime = cooltime;
        }
        curtime -= Time.deltaTime;
    }
}
