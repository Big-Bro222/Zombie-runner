using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float hitPoints=100f;
    
    public void TakeDamage(float damage){
        hitPoints-=damage;
        if(hitPoints<=0){
            Destroy(gameObject);
        }
    }

}
