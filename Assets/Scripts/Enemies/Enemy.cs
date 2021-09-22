using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : LivingBeing
{

    // Enemy type
    public enum enemyTypeEnum
    {
        normal,
        runner,
    }
    public enemyTypeEnum zombieType = enemyTypeEnum.normal;
    private bool isAngry = false;

    public override void TakeDamage(float dmg)
    {
        if (!Dead) GetComponent<EnemyAudioManager>().PlayEnemyAudio("Damage");
        if (zombieType == enemyTypeEnum.runner && !isAngry)
        {
            isAngry = true;
            GetComponent<NavMeshAgent>().speed *= 2.3f;
        }
        base.TakeDamage(dmg);
    }

    public override void Die()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<SimpleZombieAnim>().Dying = true;
        GetComponent<EnemyAudioManager>().PlayEnemyAudio("Died");
        if (BattleManager.instance)
        {
            BattleManager.instance.ZombieKilled();
        }
        StartCoroutine(DyingControl());
    }

    private void Update()
    {
        if (Dead)
        {
            transform.position = Vector3.Lerp(transform.position,
                new Vector3(transform.position.x, -1.5f, transform.position.z), .025f);
        }
    }

    IEnumerator DyingControl()
    {
        yield return new WaitForSeconds(1);
        GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(1);
        Dead = true;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

}
