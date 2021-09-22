using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool instance;
    private List<GameObject> pooledBullets = new List<GameObject>();
    [SerializeField] int bulletAmount = 10;
    [SerializeField] GameObject basicBullet;

    private void Awake() {
        if(instance==null) instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // Start the pool
        for (var i = 0; i < bulletAmount; i++)
        {
            GameObject bullet = Instantiate(basicBullet);
            bullet.SetActive(false);
            bullet.transform.SetParent(transform);
            pooledBullets.Add(bullet);
        }
    }

    // GetBulletFromPool returns a non-active bullet from pool
    public GameObject GetBulletFromPool(){
        for (var i = 0; i < pooledBullets.Count; i++)
        {
            if(!pooledBullets[i].activeInHierarchy){
                return pooledBullets[i];
            }
        }
        return null;
    }
}
