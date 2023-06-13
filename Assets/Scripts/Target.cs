using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IHitable
{
    public void TakeHit(int damage)
    {
        Destroy(gameObject);
    }
}
