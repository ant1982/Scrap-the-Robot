using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseCharacter, IStun {

    private float moveSpeed = 2.5f;
    private float attackDelay = 2.0f;
    private float Range;

    public GameObject TurretPosition = null;
    public GameObject Projectile = null;
    public GameObject Missile1;
    public GameObject Missile2;
    private int basicAttackCount = 0;
    private bool alternateAttack = false, returnedToStartLoc = true;

    private GameObject player;


    public Transform rayStart, rayEnd;
    public Transform initialStartLoc;
    public bool targetFound;
    bool PlayerWins = false;

    private bool GameStarted = true;
    private bool CanFire = false;
    private bool Stunned = false;

    int Seconds = 0;
    float attackTimer = 5.0f;
    float delayTimer = 5.0f;

    public enum State { Attack1, Attack2, MissileAttack, Returning, PlayerWinState, DelayBeforeAttack, HitByPlayer };

    private State currentState = State.Attack1;

    void ProcessState()
    {
        switch (currentState)
        {
            case State.Attack1:
                currentState = NormalMovement();
                break;

            case State.Attack2:
                currentState = ChasePlayer();
                break;

            case State.MissileAttack:
                //missile attack function
                break;

            case State.Returning:
                currentState = Returning();
                break;

            case State.PlayerWinState:
                currentState = PlayerWinsEvent();
                break;
            case State.DelayBeforeAttack:
                currentState = DelayAIAttack();
                break;
            case State.HitByPlayer:
                currentState = StunEnemy();
                break;
        }
    }



    private State NormalMovement()
    {
        //follow along X axis      
        moveSpeed = 2.5f;
        float rate = moveSpeed * Time.deltaTime;
        Vector3 position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, position, rate);
        return State.Attack1;

    }
    private State ChasePlayer()
    {
        //chase player test
        moveSpeed = 2.0f;
        float rate = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, rate);
        return State.Attack2;
    }

    private State Returning()
    {

        float rate = moveSpeed * Time.deltaTime;
        //while (Vector3.Distance(transform.position, initialStartLoc.position) > 0.5f)
        //{
        //    returnedToStartLoc = false;
        //    transform.position = Vector3.MoveTowards(transform.position, initialStartLoc.position, rate);           
        //}
        if (Vector3.Distance(transform.position, initialStartLoc.position) > 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, initialStartLoc.position, rate);
            returnedToStartLoc = false;
        }
        else
        {
            returnedToStartLoc = true;
            CanFire = true;
        }
        return State.Returning;
    }

    private State PlayerWinsEvent()
    {
        CanFire = false;
        float rate = moveSpeed * Time.deltaTime;
        if (Vector3.Distance(transform.position, initialStartLoc.position) > 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, initialStartLoc.position, rate);
        }
        return State.PlayerWinState;
    }

    private State DelayAIAttack()
    {
        float rate = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, initialStartLoc.position, rate);
        return State.DelayBeforeAttack;
    }

    private State StunEnemy()
    {        
        //if (Time.time > delayTimer + 1)
        //{            
        //    delayTimer = Time.time;
        //    Seconds++;
        //    Debug.Log("stunned");
        //    if (Seconds == 5)
        //    {
        //        basicAttackCount = 0;
        //        Seconds = 0;
        //        //currentState = State.Returning;
        //        Stunned = false;
        //        CanFire = true;
        //        alternateAttack = false;
        //        returnedToStartLoc = false;
                
        //    }
        //}        
        return State.HitByPlayer;
        
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        attackTimer = Time.time;
        //StartCoroutine(StartMoving());
    }

    void OnEnable()
    {
        GameManager.PlayerWins += OnPlayerWins;
        Player.Death += OnPlayerDeath;
    }

    // Update is called once per frame
    void Update()
    {

        ProcessState();
        Raycasting();
        //if (Time.time > attackTimer + 1)
        //{
        //    attackTimer = Time.time;
        //    Seconds++;
        //    Debug.Log(Seconds);
        //}

        //if(PlayerWins == true)
        //{
        //    currentState = State.PlayerWinState;
        //}
        if(Stunned == true)
        {            
            currentState = State.HitByPlayer;
            StunTimer();
        }

        else if (GameStarted == true)
        {
            currentState = State.DelayBeforeAttack;
            DelayTimer();
        }

        else if (alternateAttack == false && returnedToStartLoc == true)
        {
            //StartCoroutine(Movement());
            currentState = State.Attack1;
        }
        else if (alternateAttack == true && !PlayerWins)
        {
            //ChasePlayer();
            //StartCoroutine(Chase());
            currentState = State.Attack2;
            AttackTime();
        }

    else if (PlayerWins == true)
        {
            currentState = State.PlayerWinState;
        }


    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.tag == "Player")
        //{
        //    alternateAttack = false;
        //    currentState = State.Returning;
        //    returnedToStartLoc = false;
        //}
        IDamageable damagePlayer = other.gameObject.GetComponent<Player>();
        if (damagePlayer != null)
        {
            damagePlayer.Damage(20);
            alternateAttack = false;
            currentState = State.Returning;
            returnedToStartLoc = false;
        }

    }

    void Raycasting()
    {
        Debug.DrawLine(rayStart.position, rayEnd.position, Color.green);
        targetFound = Physics2D.Linecast(rayStart.position, rayEnd.position, 1 << LayerMask.NameToLayer("Player"));
        if (targetFound == true)
        {
            FirePrimaryWeapon();
        }
    }

    void FirePrimaryWeapon()
    {
        if (CanFire == true)
        {
            GameObject obj = ProjectilePool.current.returnObject();

            if (obj == null) return;

            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;

            obj.SetActive(true);

            //Instantiate(Projectile, TurretPosition.transform.position, Projectile.transform.rotation);

            CanFire = false;
            targetFound = false;
            Invoke("BasicAttack", attackDelay);
        }
    }

    void BasicAttack()
    {
        if (currentState == State.Attack2)
        {
            CanFire = false;
        }
        else
        {
            CanFire = true;
            basicAttackCount += 1;
        }
        if (basicAttackCount == 3)
        {
            alternateAttack = true;
            basicAttackCount = 0;
        }
    }

    void AttackTime()
    {
        if (Time.time > attackTimer + 1)
        {
            attackTimer = Time.time;
            Seconds++;
        }
        if (Seconds == 5)
        {
            Seconds = 0;
            currentState = State.Returning;
            alternateAttack = false;
            attackTimer = 5.0f;
        }
    }

    void DelayTimer()
    {
        CanFire = false;
        if (Time.time > delayTimer + 1)
        {
            delayTimer = Time.time;
            Seconds++;
        }
        if (Seconds == 2)
        {
            GameStarted = false;
            CanFire = true;
            Seconds = 0;
            currentState = State.Attack1;
        }
    }

    void StunTimer()
    {
        CanFire = false;        
        if (Time.time > delayTimer + 1)
        {
            delayTimer = Time.time;
            Seconds++;
        }
        if (Seconds == 5)
        {
            Seconds = 0;
            currentState = State.Returning;
            alternateAttack = false;
            Stunned = false;
            Debug.Log(Stunned);
            delayTimer = 5.0f;
        }
    }

    void OnPlayerWins(bool playerWins)
    {
        PlayerWins = playerWins;
        if (PlayerWins == true)
        {
            PlayerWins = true;
            CanFire = false;
            currentState = State.PlayerWinState;
        }
        
        //gameObject.SetActive(false);
    }

    //Interface called for Stun

    public void StunAttack()
    {
        currentState = State.HitByPlayer;
        Stunned = true;
        CanFire = false;
        Seconds = 0;
    }

    void OnPlayerDeath()
    {
        currentState = State.PlayerWinState;
    }
}

