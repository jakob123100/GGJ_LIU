using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuffs : MonoBehaviour
{
    Bullet bullet;
    Shoot shoot;
    BulletLogic dumb;

    public void BulletLifetimeAndFirerate()
    {
        float speed = bullet.speed;
        speed += speed * (20 / 100);
    }
    public void IncreaseHealthAndSize()
    {

    }
}
