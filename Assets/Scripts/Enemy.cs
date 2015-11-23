using UnityEngine;
using System.Collections;

public class Enemy : Entity {

    protected int expGiven;
    public bool given;


    void Start()
    {
        
    }

    public int getExp()
    {
        if (given)
        {
            return 0;
        }
        given = true;
        return expGiven;
    }

	

	
	// Update is called once per frame
	void Update () {
	
	}
    public virtual Item getReward()
    {
        return null;
    }
    public virtual IEnumerator Die()
    {
        yield return new WaitForSeconds(1.7f);

    }
}
