using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Skeleton : Enemy {



    private Animator fx;
    private Transform myTransform;
    private Transform target;
    private int maxDistance = 5;
    private float killDistance = 2f;
    private float enemySpeed = 1f;
    private bool dead;
    private Image health_bar;
    private HealthPotion reward;
    private int drop_rate;


    void Awake()
    {
        health = 100;
        maxHealth = 100;
        myTransform = transform;
        health_bar = transform.FindChild("EnemyCanvas").FindChild("HealthBG").FindChild("EnemyHealth").GetComponent<Image>();
        reward = new HealthPotion(10, 0, 0, 8);

    }

    public override Item getReward()
    {
        if (drop_rate > reward.GetStats()[3])
        {
            reward.quanitity = 0;
        }
        else
        {
            reward.quanitity = 1;
        }

        return reward;
    }

	// Use this for initialization
	void Start () {
        expGiven = 100;
        fx = GetComponent<Animator>();


        GameObject follow = GameObject.Find("knight");
        target = follow.transform;
        drop_rate = Random.RandomRange(0, 11);
	}
	
	void Update () {
        if (!isAlive())
        {

        }
        else
        {


            if (Vector3.Distance(target.position, myTransform.position) > maxDistance)
            {
                fx.SetBool("walking", false);
                fx.SetBool("attacking", false);

            }
            else if (Vector3.Distance(target.position, myTransform.position) < killDistance)
            {
                myTransform.LookAt(new Vector3(target.position.x, 0, target.position.z));

                fx.SetBool("attacking", true);
                fx.SetBool("walking", false);


            }
            else
            {

                fx.SetBool("walking", true);
                fx.SetBool("attacking", false);
                myTransform.LookAt(new Vector3(target.position.x, 0, target.position.z));
                myTransform.position += myTransform.forward * enemySpeed * Time.deltaTime;


            }
        }
	}
    public override IEnumerator Die()
    {

        fx.SetBool("dead", true);
        yield return new WaitForSeconds(2f);
        if (!dead)
        {
            Destroy(this.gameObject);
            dead = true;
        }
    }
    public override void takeDamage(int damage)
    {
        if (health <= 0)
        {
            return;
        }
        health -= damage;
        health_bar.fillAmount = health / maxHealth;
    }

}


