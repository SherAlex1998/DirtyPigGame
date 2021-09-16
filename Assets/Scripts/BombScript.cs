using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    private Collider2D bombCollider;
    void Start()
    {
        bombCollider = gameObject.GetComponent<Collider2D>();
    }

    void Update()
    {

    }

    public void BoomThisBomb()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PigPlayer temp))
        {
            if(!temp.hasBombs)
                Destroy(gameObject);
        }
    }
}
