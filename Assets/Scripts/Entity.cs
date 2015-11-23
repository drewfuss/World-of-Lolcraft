using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

    protected float health;
    protected float maxHealth;
    
   // protected int defense;
    public string entity_Name;


    public float getHealth()
    {
        return health;
    }
    public void resetHealth()
    {
        health = 100;
    }
    public void setHealth(int hp)
    {
        health = hp;
    }
    public void heal(int hp)
    {
        health += hp;
    }
    public string getName()
    {
        return name;
    }
    public bool isAlive()
    {
        if (health > 0)
            return true;

        return false;
    }
    public virtual void takeDamage(int damage)
    {
        if (health <= 0)
        {
            return;
        }
        health -= damage;
        //print(health);
    }
    public void dealDamage(Entity entity, int damage)
    {
        entity.takeDamage(damage);
    }
    public void printTest()
    {
        print("test");
    }
    
	
}
