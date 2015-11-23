using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Player : Entity {
    private PlayerUtil playerStats;

    public static Quest c_quest;
    private static Text q_name, q_crit;

    private static int experience;
    public enum CurrentState { AttackState, IdleState, RunState, SwingState, SpecialState, SinisterState, ShivState, BladestormState };
    private static Image xpUI, hpUI;
    public static CurrentState c_state;
    private Button btnSinister, btnShiv, btnBlade, btnMortal;
    private Text hp, points;
    private static float MAX_EXPERIENCE;
    private static Text alert;
    public static bool add;
    public static bool hit;
    public static int special;
    private static int hp_pots;
    private AudioSource[] aud;

    private static List<Item> inventory;
    private static Text hpQuant;

    public static int combo_points;

    private float inputH;
    private float inputV;
    private float inputDelay = .1f;

    private float forwardVel = 1f;
    private float rotateVel = 100f;
    private readonly float WALK_SPEED = 2f;
    private readonly float RUN_SPEED = 5f;
    
    Quaternion targetRotation;

    MeshRenderer sword;


    public Animator anim;
    private Rigidbody rbody;

    //Moving variables
    private bool running = false;

    

    void Awake()
    {
        hp_pots = 0;
        maxHealth = 100;
        special = 0;
        health = 100;
        combo_points = 0;
        points = GameObject.Find("Combo").GetComponent<Text>();
        hp = GameObject.Find("Health").GetComponent<Text>();
        alert = GameObject.Find("Alert").GetComponent<Text>();
        
        aud = GetComponents<AudioSource>();
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
        targetRotation = transform.rotation;
        experience = 0;
        c_state = CurrentState.IdleState;
        MAX_EXPERIENCE = PlayerUtil.GetMaxExperience(PlayerUtil.level);
      

        btnBlade = GameObject.Find("Bladestorm").GetComponent<Button>();
        btnShiv = GameObject.Find("Shiv").GetComponent<Button>();
        btnSinister = GameObject.Find("Sinister Strike").GetComponent<Button>();
        btnMortal = GameObject.Find("Mortal Strike").GetComponent<Button>();

        inventory = new List<Item>();
        hpQuant = GameObject.Find("HP UI/hp_quant").GetComponent<Text>();

        xpUI = GameObject.Find("MyExperience").GetComponent<Image>();
        hpUI = GameObject.Find("MyHealth").GetComponent<Image>();

        q_name = GameObject.Find("Quest Name").GetComponent<Text>();
        q_crit = GameObject.Find("Quest Criteria").GetComponent<Text>();
        
    }

    void OnCollisionEnter(Collision c)
    {
        
    }

    public Quaternion TargetRotation
    {
        get { return targetRotation; }
    }

 

	
	void Start () {
        experience = 0;
        health = 100;
        sword = GameObject.Find("sword01").GetComponent<MeshRenderer>();
        sword.enabled = false;
        hp.text = getHealth().ToString();
        
        points.text = combo_points.ToString();

        playerStats = new PlayerUtil();

        //c_quest = new Quest(1, "Skeletons!", "Skeleton", 2, 80);
        UpdateQuests();
	}
	
	
    void Update()
    {
        
        if (isAlive())
        {
            UpdateButtons();
            GetInput();
            Turn();
            if (Input.GetKeyDown("f"))
            {
                if (hp_pots > 0)
                {
                   anim.Play("use_object");
                   heal(10);
                   UpdateHealth();
                   UpdatePotions(true);
                }
               
            }

            if (Input.GetMouseButtonDown(2))
            {
                if (c_state == CurrentState.AttackState)
                {
                    aud[1].Play();
                    anim.Play("knight_sword_back_01");
                    StartCoroutine(changeState(.7f, CurrentState.IdleState));
                }
                else
                {
                    aud[0].Play();
                    anim.Play("knight_sword_out_01");
                    StartCoroutine(changeState(.7f, CurrentState.AttackState));
                }


            }


            if (Input.GetKey(KeyCode.LeftShift))
            {
                running = true;
                forwardVel = RUN_SPEED;
                anim.SetBool("running", true);
            }
            else if (running == true)
            {
                running = false;
                forwardVel = WALK_SPEED;
                anim.SetBool("running", false);
            }

            //attacks
            if (anim.GetBool("attack_state"))
            {
                if (Input.GetMouseButtonDown(1))
                {
                    if (combo_points >= 2)
                    {
                        anim.Play("finisher");
                        StartCoroutine(swingState(.7f, CurrentState.ShivState));
                    }
                }
                if (Input.GetMouseButtonDown(0))
                {
                    int temp = Random.RandomRange(0, 10);
                    if (temp == 3)
                    {
                        if (special < 3)
                        {
                            special++;
                        }
                    }
                    anim.Play("normal");
                    StartCoroutine(swingState(.7f, CurrentState.SinisterState));

                }
                if (Input.GetKeyDown("r"))
                {
                    if (combo_points >= 5)
                    {
                        anim.Play("ultimate");
                        StartCoroutine(swingState(.7f, CurrentState.BladestormState));
                    }
                }
                if (Input.GetKeyDown("q"))
                {
                    if (special > 0)
                    {
                        anim.Play("normal_2");
                        StartCoroutine(swingState(1f, CurrentState.SpecialState));
                    }
                }

            }
        }
 }
    

    IEnumerator changeState(float second, CurrentState state)
    {
        if (state == CurrentState.IdleState)
        {
            c_state = state;
            anim.SetBool("attack_state", false);
            yield return new WaitForSeconds(second);
            sword.enabled = false;

        }
        else
        {
            c_state = state;
            anim.SetBool("attack_state", true);
            yield return new WaitForSeconds(second);
            sword.enabled = true;
        }
    }
    IEnumerator swingState(float second, CurrentState state)
    {
        //c_state = CurrentState.SwingState;
        c_state = state;
        anim.SetBool("attack_state", false);
        yield return new WaitForSeconds(second);
        c_state = CurrentState.AttackState;
        anim.SetBool("attack_state", true);
        add = false;
        hit = false;
        UpdatePoints();


    }

    void GetInput()
    {

        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");

        anim.SetFloat("inputH", inputH);
        anim.SetFloat("inputV", inputV);

    }

    void FixedUpdate()
    {
        if(isAlive())
            Run();
    }

    void UpdatePoints()
    {
        if (combo_points <= 0)
        {
            combo_points = 0;
        }
        points.text = combo_points.ToString();
    }
    void Run()
    {
        if (Mathf.Abs(inputV) > inputDelay)
        {
            rbody.velocity = transform.forward * inputV * forwardVel;
        }
        else
        {
            rbody.velocity = Vector3.zero;
        }
    }
    void Turn()
    {
        
        targetRotation *= Quaternion.AngleAxis(rotateVel * inputH * Time.deltaTime, Vector3.up);
        transform.rotation = targetRotation;
    }

    public override void takeDamage(int damage)
    {
        if (health <= 0)
        {
            return;
        }
        health -= damage;
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        if (getHealth() > 100)
        {
            setHealth(100);
        }
        hp.text = getHealth().ToString();
        hpUI.fillAmount = getHealth() / maxHealth;
    }
    public static void UpdateExperience(int xp)
    {
        if (experience + xp >= MAX_EXPERIENCE)
        {
            LevelUp();
            experience = experience + xp - (int)MAX_EXPERIENCE;
            MAX_EXPERIENCE = PlayerUtil.GetMaxExperience(PlayerUtil.level);
            xpUI.fillAmount = experience / MAX_EXPERIENCE;

        }
        else
        {
            experience += xp;
            xpUI.fillAmount = experience / MAX_EXPERIENCE;
        }
        
        

    }
    private static void LevelUp()
    {
        PlayerUtil.level++;
        PlayerUtil.UpdateStrength();
       
        
        
    }

    private void UpdateButtons()
    {
        if (combo_points >= 2)
        {
            btnShiv.image.color = Color.white;
        }
        else
            btnShiv.image.color = Color.black;

        if (special > 0)
        {
            btnMortal.image.color = Color.white;
        }
        else
            btnMortal.image.color = Color.black;


        if (combo_points >= 5)
        {
            btnBlade.image.color = Color.white;
        }
        else
            btnBlade.image.color = Color.black;
    }

    public static void addItem(Item item)
    {
        bool found = false;
        foreach (Item i in inventory)
        {
            if (i.GetName() == item.GetName())
            {
                i.addQuantity(item.getQuantity());
                found = true;
                return;
            }
        }
        if (!found)
            inventory.Add(item);

        
        
        
    }
    public static IEnumerator NewItem(string message)
    {
        UpdatePotions(false);
        alert.text = message;
        yield return new WaitForSeconds(2f);
        alert.text = "";
      

    }
    private static void UpdatePotions(bool use)
    {
        if (inventory.Count == 0)
        {
            hpQuant.text = "0";
            return;
        }
        foreach (Item i in inventory)
        {
            if (i.GetName() == "Healing Potion")
            {
                hp_pots = i.getQuantity();
                
                if (use)
                {
                    i.quanitity--;
                    hp_pots--;
                }
                hpQuant.text = hp_pots.ToString();
                return;
            }
        }
        
    }

    public static void UpdateQuests()
    {
        if (c_quest == null)
        {
            q_name.text = "No Quests Available";
            q_crit.text = "";
            return;
        }
        else if (c_quest.IsFinished())
        {
            q_name.text = "Quest Completed!";
            q_crit.text = "";
            return;
        }

        q_name.text = c_quest.getName();
        q_crit.text = c_quest.getCrit();


    }

    public static void AddKill(){
        c_quest.AddKill();
        if (c_quest.IsFinished())
        {
            UpdateExperience(c_quest.xpType());
        }
        UpdateQuests();
        
    }





}
