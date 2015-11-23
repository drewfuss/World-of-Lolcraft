using UnityEngine;
using System.Collections;

public class EnemyWeapon : MonoBehaviour {
    private AudioSource impact;
    private Player player;
    private Animator fx;
    private bool found;
    private bool dead;

	// Use this for initialization
	void Start () {
        impact = GetComponent<AudioSource>();
        if (transform.parent.GetComponent<Animator>() != null)
        {
            fx = transform.parent.GetComponent<Animator>();
            found = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider c){
        if(c.name == "knight"){
            //Check to see if knight has died (prevent further damage if dead)
            if (found)
            {
                if (fx.GetBool("dead") == true)
                {
                    dead = true;
                }
            }
            if (!dead)
            {
                impact.Play();
                player = c.GetComponent<Player>();
                player.takeDamage(2);
                if (!player.isAlive())
                {
                    player.anim.SetBool("dead", true);
                }
            }
        }
    }


}
