using System.Collections.Generic;
using UnityEngine;


public class PlayerShoot : MonoBehaviour
{

    public GameObject bulletPrefab;
    private List<Bullets> Bullets = new List<Bullets>();
    private float timeBetweenShots;
    public float ShotDelay;
    //private HurtEnemy P;

    public enum SHOOTING_DIRECTION
    {
        NONE,
        UP,
        DOWN,
        LEFT,
        RIGHT
    };

    // Update is called once per frame
    void Update()
    {
        timeBetweenShots += Time.deltaTime * 50f;
        SHOOTING_DIRECTION dir = Input.GetKey(KeyCode.RightArrow) ? SHOOTING_DIRECTION.RIGHT :
                                 Input.GetKey(KeyCode.LeftArrow) ? SHOOTING_DIRECTION.LEFT :
                                 Input.GetKey(KeyCode.UpArrow) ? SHOOTING_DIRECTION.UP :
                                 Input.GetKey(KeyCode.DownArrow) ? SHOOTING_DIRECTION.DOWN : SHOOTING_DIRECTION.NONE;
        if (timeBetweenShots > ShotDelay)
        {
            if (dir != SHOOTING_DIRECTION.NONE)
            {
                GameObject B = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                //HurtEnemy bScript = B.GetComponent<HurtEnemy>();
                Bullets b = new Bullets(B, dir);
                Bullets.Add(b);
                timeBetweenShots = 0;
            }
        }

        List<Bullets> deleteList = new List<Bullets>();

        foreach (Bullets goBullet in Bullets)
        {
            if (goBullet.IsBulletDead())
            {
                //Agregar a projectiles a borrar
                deleteList.Add(goBullet);
            }
            else
            {
                goBullet.Update(Time.deltaTime);
            }
        }
        //Borrar todos los projectiles que corresponda
        foreach (Bullets delete in deleteList)
        {
            Bullets.Remove(delete);
            //DestroyObject(delete);
        }

    }

}
