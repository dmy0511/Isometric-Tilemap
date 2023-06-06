using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_attack : MonoBehaviour
{
    public float speed;

    private void Start()
    {
        Invoke("DestroyE_attack", 2);
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void DestroyE_attack()
    {
        Destroy(gameObject);
    }
}
