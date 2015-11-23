using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour {
    
    Enemy skel;
    AudioSource[] impacts;

    void Awake()
    {
        impacts = GetComponents<AudioSource>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider c)
    {
        if (c.name == "player")
        {
            return;
        }
        if (c.tag == "Enemy")
        {
            
            if (c.name == "Skeleton")
            {
                skel = c.gameObject.GetComponent<Skeleton>();
            }
            else
            {
                skel = c.gameObject.GetComponent<Goblin>();
            }



             if (Player.c_state == Player.CurrentState.SinisterState)
                    {
                        if (!Player.hit)
                        {
                            Player.hit = true;
                            int rand = Random.RandomRange(0, 2);
                            impacts[rand].Play();
                           // skel = c.gameObject.GetComponent<Goblin>();
                            skel.takeDamage(10);
                            if (Player.combo_points < 5 && !Player.add)
                            {
                                Player.combo_points += 1;
                                Player.add = true;
                            }
                            if (!skel.isAlive())
                            {
                                if (Player.c_quest != null)
                                {
                                    if (c.name == Player.c_quest.getEnemyName() && !skel.given)
                                    {
                                        Player.AddKill();
                                    }
                                }
                                Player.UpdateExperience(skel.getExp());
                                if (skel.getReward().quanitity == 0)
                                {
                                    print("darn, i got nothing");
                                }
                                else
                                {
                                    Player.addItem(skel.getReward());
                                    StartCoroutine(Player.NewItem("You received " + skel.getReward().getQuantity().ToString()  + "x of " + skel.getReward().GetName()));
                                }
                                StartCoroutine(skel.Die());
                            }
                        }

                        

                    }
                    else if (Player.c_state == Player.CurrentState.ShivState)
                    {
                        if (!Player.hit)
                        {
                            Player.hit = true;
                            if (!Player.add)
                            {
                                Player.combo_points -= 2;
                            }


                            int rand = Random.RandomRange(0, 2);
                            impacts[rand].Play();
                            //skel = c.gameObject.GetComponent<Goblin>();
                            skel.takeDamage(15);
                            if (!skel.isAlive())
                            {
                                if (Player.c_quest != null)
                                {
                                    if (c.name == Player.c_quest.getEnemyName() && !skel.given)
                                    {
                                        Player.AddKill();
                                    }
                                }
                                Player.UpdateExperience(skel.getExp());
                                if (skel.getReward().quanitity == 0)
                                {
                                    print("darn, i got nothing");
                                }
                                else
                                {
                                    Player.addItem(skel.getReward());
                                    StartCoroutine(Player.NewItem("You received x" + skel.getReward().getQuantity().ToString() + " of " + skel.getReward().GetName()));
                                }

                                StartCoroutine(skel.Die());
                            }
                        }

                    }
                    else if (Player.c_state == Player.CurrentState.BladestormState)
                    {
                        if (!Player.hit)
                        {
                            Player.hit = true;
                            if (!Player.add)
                            {
                                Player.combo_points -= 5;
                            }
                            int rand = Random.RandomRange(0, 2);
                            impacts[rand].Play();
                           // skel = c.gameObject.GetComponent<Goblin>();
                            skel.takeDamage(50);
                            if (!skel.isAlive())
                            {
                                if (Player.c_quest != null)
                                {
                                    if (c.name == Player.c_quest.getEnemyName() && !skel.given)
                                    {
                                        Player.AddKill();
                                    }
                                }
                                Player.UpdateExperience(skel.getExp());
                                if (skel.getReward().quanitity == 0)
                                {
                                    print("darn, i got nothing");
                                }
                                else
                                {
                                    Player.addItem(skel.getReward());
                                    StartCoroutine(Player.NewItem("You received x" + skel.getReward().getQuantity().ToString() + " of " + skel.getReward().GetName()));
                                }
                                StartCoroutine(skel.Die());
                            }
                        }

                    }
                    else if (Player.c_state == Player.CurrentState.SpecialState)
                    {
                        if (!Player.hit)
                        {
                            Player.hit = true;
                            if (!Player.add)
                            {
                                Player.combo_points += 2;
                                Player.special--;
                            }
                            int rand = Random.RandomRange(0, 2);
                            impacts[rand].Play();
                            //skel = c.gameObject.GetComponent<Goblin>();
                            skel.takeDamage(20);
                            if (!skel.isAlive())
                            {
                                if (Player.c_quest != null)
                                {
                                    if (c.name == Player.c_quest.getEnemyName() && !skel.given)
                                    {
                                        Player.AddKill();
                                    }
                                }
                                Player.UpdateExperience(skel.getExp());
                                if (skel.getReward().quanitity == 0)
                                {
                                    print("darn, i got nothing");
                                }
                                else
                                {
                                    Player.addItem(skel.getReward());
                                    StartCoroutine(Player.NewItem("You received x" + skel.getReward().getQuantity().ToString() + " of " + skel.getReward().GetName()));
                                }
                                StartCoroutine(skel.Die());
                            }
                        }

                    }

                   
        }

        
    }
}
