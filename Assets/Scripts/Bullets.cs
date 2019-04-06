﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    private GameObject thisBulletobject;
    public float bulletVelocity = 15f;
    private bool IsDead = false;
    private PlayerShoot.SHOOTING_DIRECTION direction;

    // Use this for initialization
    public Bullets(GameObject bulletObject, PlayerShoot.SHOOTING_DIRECTION dir)
    {
        thisBulletobject = bulletObject;
        direction = dir;
    }

    // Update is called once per frame
    public void Update()
    {
        if (thisBulletobject != null)
        {
            Vector3 transformation = new Vector3(0, 0, 0);
            float transformationVelocity = bulletVelocity * Time.deltaTime;
            switch (direction)
            {
                case PlayerShoot.SHOOTING_DIRECTION.UP:
                    transformation.y = transformationVelocity;
                    break;
                case PlayerShoot.SHOOTING_DIRECTION.DOWN:
                    transformation.y = -transformationVelocity;
                    break;
                case PlayerShoot.SHOOTING_DIRECTION.LEFT:
                    transformation.x = -transformationVelocity;
                    break;
                case PlayerShoot.SHOOTING_DIRECTION.RIGHT:
                    transformation.x = transformationVelocity;
                    break;
            }

            thisBulletobject.transform.Translate(transformation);
        }
    }

    public bool IsBulletDead()
    {
        return IsDead;
    }
}
