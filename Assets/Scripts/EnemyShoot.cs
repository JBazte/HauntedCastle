using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyShoot : MonoBehaviour
{

    public GameObject bulletPrefab;
    public GameObject bulletPrefab1;
    public GameObject bulletPrefab2;
    public GameObject bulletPrefab3;
    public GameObject bulletPrefab4;
    public GameObject bulletPrefab5;
    public GameObject bulletPrefab6;
    public GameObject bulletPrefab7;
    public GameObject bulletPos;
    public GameObject bulletPos1;
    public GameObject bulletPos2;
    public GameObject bulletPos3;
    public GameObject bulletPos4;
    public GameObject bulletPos5;
    public GameObject bulletPos6;
    public GameObject bulletPos7;
    public float timeBetweenShots;
    public float ShotDelay;
    public int currentDamage;
    private int i;
    private EnemyMovement shotToPlayer;
    private Scene theScene;
    private string sceneName;
    void Start(){
        theScene = SceneManager.GetActiveScene();
        shotToPlayer = GetComponent<EnemyMovement>();
        sceneName = theScene.name;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.deltaTime == 0f){
            timeBetweenShots = 70f;
        }

        if(sceneName == "Level4"){
            if(shotToPlayer.followPlayer){
                timeBetweenShots++;
                if(i > 2){
                    i = 0;
                    timeBetweenShots = 0f;
                }
                else if (timeBetweenShots > ShotDelay && i <= 3)
                {
                    Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                    Instantiate(bulletPrefab1, transform.position, Quaternion.identity);
                    Instantiate(bulletPrefab2, transform.position, Quaternion.identity);
                    Instantiate(bulletPrefab3, transform.position, Quaternion.identity);
                    Instantiate(bulletPrefab4, transform.position, Quaternion.identity);
                    Instantiate(bulletPrefab5, transform.position, Quaternion.identity);
                    Instantiate(bulletPrefab6, transform.position, Quaternion.identity);
                    Instantiate(bulletPrefab7, transform.position, Quaternion.identity);
                    timeBetweenShots = 75f;
                    i +=1; 
                }
            } else {
            timeBetweenShots = 70f;
            }
        }
        else if(sceneName == "Level3")
        {
            if(shotToPlayer.followPlayer){
                timeBetweenShots++;
                if(i > 2){
                    i = 0;
                    timeBetweenShots = 0f;
                }
                else if (timeBetweenShots > ShotDelay && i <= 2)
                {
                    Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                    Instantiate(bulletPrefab1, transform.position, Quaternion.identity);
                    Instantiate(bulletPrefab2, transform.position, Quaternion.identity);
                    Instantiate(bulletPrefab3, transform.position, Quaternion.identity);
                    Instantiate(bulletPrefab4, transform.position, Quaternion.identity);
                    Instantiate(bulletPrefab5, transform.position, Quaternion.identity);
                    Instantiate(bulletPrefab6, transform.position, Quaternion.identity);
                    Instantiate(bulletPrefab7, transform.position, Quaternion.identity);
                    timeBetweenShots = 48f;
                    i +=1; 
                }
            } else {
            timeBetweenShots = 30f;
            }
        }
        else if(sceneName == "Level2")
        {
            if(shotToPlayer.followPlayer){
                timeBetweenShots++;
                if(i > 2){
                    i = 0;
                    timeBetweenShots = 0f;
                }
                else if (timeBetweenShots > ShotDelay && i <= 2)
                {
                    Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                    Instantiate(bulletPrefab1, transform.position, Quaternion.identity);
                    Instantiate(bulletPrefab2, transform.position, Quaternion.identity);
                    Instantiate(bulletPrefab3, transform.position, Quaternion.identity);
                    Instantiate(bulletPrefab4, transform.position, Quaternion.identity);
                    Instantiate(bulletPrefab5, transform.position, Quaternion.identity);
                    Instantiate(bulletPrefab6, transform.position, Quaternion.identity);
                    Instantiate(bulletPrefab7, transform.position, Quaternion.identity);
                    timeBetweenShots = 70f;
                    i +=1; 
                }
            } else {
                timeBetweenShots = 70f;
            }
        }
        else if(sceneName == "Level1")
        {
            if(shotToPlayer.followPlayer){
                timeBetweenShots++;
                if(i > 2){
                    i = 0;
                    timeBetweenShots = 0f;
                }
                else if (timeBetweenShots > ShotDelay && i <= 2)
                {
                    Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                    Instantiate(bulletPrefab2, transform.position, Quaternion.identity);
                    Instantiate(bulletPrefab4, transform.position, Quaternion.identity);
                    Instantiate(bulletPrefab6, transform.position, Quaternion.identity);
                    timeBetweenShots = 70f;
                    i +=1; 
                }
            } else {
                timeBetweenShots = 70f;
            }
        }
        
    }
}