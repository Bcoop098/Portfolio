using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackV2 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject attackPrefab;
    public Transform attackLocation;
    public Transform alternateAttackLocation;
    GameObject attack;
    public void Attack(float direction)
    {
        if(direction > 0)
        {
            //attackLocation.Rotate(new Vector3(0f, 0f, 180f));
            attack = Instantiate(attackPrefab, attackLocation) as GameObject;
            attack.GetComponent<EnemyDamager>().direction = direction;
            attack.transform.parent = null;
            Destroy(attack, 0.15f);
            //attackLocation.Rotate(new Vector3(0f, 0f, -180f));
        }
        else if (direction < 0)
        {
            //float x = attackLocation.transform.localPosition.x;
            
            //attackLocation.Rotate(new Vector3(0f, 0f, 180f));
            attack = Instantiate(attackPrefab, alternateAttackLocation) as GameObject;
            attack.GetComponent<EnemyDamager>().direction = -direction;
            attack.transform.parent = null;

            //attackLocation.transform.Translate(new Vector3(1f * (x + x), 0.0f, 0.0f));
            Destroy(attack, 0.15f);
            //attackLocation.Rotate(new Vector3(0f, 0f, -180f));
        }

    }
}
