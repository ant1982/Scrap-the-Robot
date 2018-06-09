using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseCharacter, IDamageable, ICollectitem, IPowerUp, IMagnetShield
{
    private float speed = 6.0f;
    private int lives = 3;
    private float maxWidth;
    private int health = 100;
    private int PlayerScore;
    public Camera mainCam;    
    bool canMoveLeft = true;    
    bool ShieldActive;
    bool PlayerInvulnerable;
    bool PlayerKilled = false;
    bool DoubleScore;
    bool MagnetActive;
    private int Ammo;
    private int shieldTimer;
    public Vector3 targetPosition;
    public GameObject Attack;

    public GameObject RedShield;
    public GameObject GreenShield;
    public GameObject BlueShield;
    SoundManager soundManager;

    // Use this for initialization
    void Start () {

        if (mainCam == null)
        {
            mainCam = Camera.main;
        }
        lives = 3;
        health = 100;
        PlayerScore = 0;        
        ShieldActive = false;
        Ammo = 0;
        soundManager = SoundManager.instance;        
        if(soundManager == null)
        {
            Debug.Log("No Sound manager found");
        }
        
    }

    void OnEnable()
    {
        GameManager.GameOver += OnGameOver;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.A) && canMoveLeft == true)
        {
            MoveLeft();
            //transform.position += Vector3.left * speed * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
            //transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            MoveUp();
            //transform.position += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            MoveDown();
            //transform.position += Vector3.down * speed * Time.deltaTime;
        }

        if (Input.GetMouseButton(0))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = transform.position.z;
            //Vector3 targetPosition = Input.mousePosition;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Space)|| Input.GetMouseButtonDown(1))
        {
            FireWeapon();
        }

        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                targetPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                targetPosition.z = transform.position.z;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                targetPosition.z = transform.position.z;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                transform.position = transform.position;
            }
        }


        Vector3 pos = transform.position;
        var lowerleftCorner = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        var upperRightCorner = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        var screenBuffer = 0.5f;
        var lowerBuffer = 1.25f;
        var leftboundary = lowerleftCorner.x + screenBuffer;
        var rightboundary = upperRightCorner.x - screenBuffer;
        var upboundary = upperRightCorner.y - screenBuffer;
        var downboundary = lowerleftCorner.y + lowerBuffer;

        pos.x = Mathf.Clamp(pos.x, leftboundary, rightboundary);
        pos.y = Mathf.Clamp(pos.y, downboundary, upboundary);
        transform.position = pos;

    }

    public delegate void ScoreDispatcher(int updateScore);
    public static event ScoreDispatcher UpdateScore;

    public void Call_UpdateScore(int updateScore)
    {
        UpdateScore(updateScore);
    }

    public delegate void HealthDispatcher(int currentHealth);
    public static event HealthDispatcher CurrentHealth;

    public void Call_PlayerHealth(int currentHealth)
    {
        if (CurrentHealth != null)
        {
            CurrentHealth(currentHealth);            
        }
    }

    public delegate void PlayerDead();
    public static event PlayerDead Death;

    public void Call_Death()
    {
        if (Death != null)
        {
            PlayerKilled = true;
            Death();
        }
    }

    //interface call for item picked up

    public void CollectItem(int itemValue)
    {
        PickupSystem(itemValue);
        SoundManager.instance.PlaySound("Test2");
    }

    private void PickupSystem(int value)
    {
        if (DoubleScore == true)
        {
            PlayerScore += value * 2;            
            UpdateScore(PlayerScore);
        }
        else if (DoubleScore == false)
        {
            PlayerScore += value;            
            UpdateScore(PlayerScore);
        }
    }

    //Interface called for Damage

    public void Damage(int damageAmount)
    {
        DamageSystem(damageAmount);
    }

    //Interface called for power up

    public void PowerUpCollected(int powerUpIndex)
    {
        if (!PlayerKilled)
        {
            if (powerUpIndex == 0)
            {
                ToggleRedShield();
            }
            else if (powerUpIndex == 1)
            {
                ToggleGreenShield();
            }
            else if (powerUpIndex == 2)
            {
                ToggleBlueShield();
            }
            else if (powerUpIndex == 3)
            {
                Ammo += 1;
            }
        }
    }

    //Interface check for magnetshield activation

    public bool MagnetShieldActive()
    {
        if (MagnetActive)
        {
            return true;
        }
        else if(!MagnetActive)
        {
            return false;
        }
        return false;
    }

    public Vector3 ReturnPosition()
    {
        Vector3 target = transform.position;
        return target;
    }

    private void DamageSystem(int damage)
    {
        if (!PlayerInvulnerable)
        {
            health -= damage;
            Call_PlayerHealth(health);
            if (health <= 0)
            {
                lives -= 1;
                health = 0;
                if (Death != null)
                {
                    Death();
                    Time.timeScale = 0;
                }
                gameObject.SetActive(false);
            }
        }
    }

    //subscribed events

    public void OnGameOver(bool gameOver)
    {
        if (gameOver == true)
        {
            Debug.Log("Test fire message");
        }
    }


    //movement functions

    void MoveLeft()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    void MoveRight()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
    }

    void MoveUp()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }

    void MoveDown()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }

    // Fire weapon

    void FireWeapon()
    {
        if (Ammo > 0) {
            Instantiate(Attack, transform.position, transform.rotation);
            Ammo -= 1;            
        }        
    }

    void ToggleRedShield()
    {
        if (!RedShield.activeSelf && ShieldActive == false)
        {
            RedShield.SetActive(true);
            ShieldActive = true;
            shieldTimer = 5;
            DoubleScore = true;
            StartCoroutine(ShieldActivateTimer());
        }
    }

    void ToggleGreenShield()
    {
        if (!GreenShield.activeSelf && ShieldActive == false)
        {
            GreenShield.SetActive(true);
            ShieldActive = true;
            MagnetActive = true;
            shieldTimer = 5;
            //CircleCollider2D circle = gameObject.GetComponent<CircleCollider2D>();
            //circle.radius = 0.6f;
            StartCoroutine(ShieldActivateTimer());
        }
    }

    void ToggleBlueShield()
    {
        if(!BlueShield.activeSelf && ShieldActive == false)
        {
            BlueShield.SetActive(true);
            ShieldActive = true;
            PlayerInvulnerable = true;
            shieldTimer = 5;
            StartCoroutine(ShieldActivateTimer());
        }        
    }

    IEnumerator ShieldActivateTimer()
    {        
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            shieldTimer -= 1;            
            if(shieldTimer == 0)
            {                
                DeactivateShield();
                yield return new WaitForSeconds(1.0f);
            }
        }       
    }

    void DeactivateShield()
    {
        StopAllCoroutines();
        //StopCoroutine(ShieldActivateTimer());
        RedShield.SetActive(false);
        GreenShield.SetActive(false);
        BlueShield.SetActive(false);
        ShieldActive = false;
        DoubleScore = false;
        MagnetActive = false;
        PlayerInvulnerable = false;
    }

    void OnDestroy()
    {
        GameManager.GameOver -= OnGameOver;
    }
}
