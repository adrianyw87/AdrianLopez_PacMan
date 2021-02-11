using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static event Action loseLife = delegate { };

    public float moveSpeed = 7f;

    // Hacia qué dirección va el player al comenzar la partida
    public string initialDirection = "right";


    private Rigidbody2D playerRb;
    private Animator playerAnimator;

    string direction;
    bool moveRight;
    bool moveLeft;
    bool moveUp;
    bool moveDown;

    enum PlayerStates
    {
        playin,        
        stop
    }

    PlayerStates currentState;

    public int currentLifes = 3;

    // Variables para hacer al player inmune cuando ha sido alcanzado
    bool immune = false;
    public float immuneTime = 2f;

    HUDManager hudManger;

    void Awake()
    {        
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

        SetDirection(initialDirection);

        // El jugador empieza parado hasta que se pulse el botón "GO"
        currentState = PlayerStates.stop;

        hudManger = FindObjectOfType<HUDManager>();
    }

    void FixedUpdate()
    {
        // Creamos un movimiento contínuo que vaya calculando las físicas
        if(currentState == PlayerStates.playin)
        ContinuousMovement();
    }

    void Update()
    {
        // Chequeamos a cada frame si se ha cambiado la dirección
        if (currentState == PlayerStates.playin)
            CheckMovement();
    }

    public void ContinuousMovement()
    {
        if (moveUp && CanMove(Vector2.up))
        {
            playerRb.MovePosition(transform.position + transform.up * moveSpeed * Time.deltaTime);
        }

        if (moveRight && CanMove(Vector2.right))
        {
            playerRb.MovePosition(transform.position + transform.right * moveSpeed * Time.deltaTime);
        }

        if (moveDown && CanMove(-Vector2.up))
        {
            playerRb.MovePosition(transform.position + -transform.up * moveSpeed * Time.deltaTime);
        }

        if (moveLeft && CanMove(-Vector2.right))
        {
            playerRb.MovePosition(transform.position + -transform.right * moveSpeed * Time.deltaTime);
        }

    }
    void CheckMovement()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && CanMove(Vector2.up))
        {
            SetDirection("up");
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && CanMove(Vector2.right))
        {
            SetDirection("right");
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && CanMove(Vector2.down))
        {
            SetDirection("down");
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && CanMove(Vector2.left))
        {
            SetDirection("left");
        }
    }

    void SetDirection(string _direction)
    {
        direction = _direction;
        moveUp = false;
        moveRight = false;
        moveDown = false;
        moveLeft = false;

        switch (direction)
        {
            case "up":
                moveUp = true;
                playerAnimator.SetInteger("directionX", 0);
                playerAnimator.SetInteger("directionY", 1);
                break;
            case "right":
                moveRight = true;
                playerAnimator.SetInteger("directionX", 1);
                playerAnimator.SetInteger("directionY", 0);
                break;
            case "down":
                moveDown = true;
                playerAnimator.SetInteger("directionX", 0);
                playerAnimator.SetInteger("directionY", -1);
                break;
            case "left":
                moveLeft = true;
                playerAnimator.SetInteger("directionX", -1);
                playerAnimator.SetInteger("directionY", 0);
                break;
            default:
                moveRight = true;
                playerAnimator.SetInteger("directionX", 1);
                playerAnimator.SetInteger("directionY", 0);
                break;
        }
    }

    // Para saber si no hay obstáculos en la dirección que queremos tomar, si lo hay entonces seguimos con nuestra actual dirección
    bool CanMove(Vector2 dir)
    {
        // Generamos un raycasthit que salga del Player con longitud 0.1f mayor que el radio del objeto (0.5f)
        int layerMask = LayerMask.GetMask("Obstacle");
        float longOffset = 0.6f;

        // Lanzamos Raycast desde el centro
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, longOffset, layerMask);
        Debug.DrawRay(transform.position, dir, Color.cyan);


        if (hit.collider != null)
        {
            return false;
        }
        else
        {
            return true;
        }

        //TODO: Lanzar el raycast desde el punto opuesto a la direccion (ej: voy hacia arriba, el raycast debería salir desde abajo)
        //Vector2 raycastOrigin = Vector2.zero;
        //switch (direction)
        //{
        //    case "up":
        //        raycastOrigin = Vector2.up;
        //        break;
        //    case "right":
        //        raycastOrigin = Vector2.right;
        //        break;
        //    case "down":
        //        raycastOrigin = Vector2.down;
        //        break;
        //    case "left":
        //        raycastOrigin = Vector2.left;
        //        break;
        //    default:
        //        raycastOrigin = Vector2.right;
        //        break;
        //}        
        //RaycastHit2D hit = Physics2D.Raycast(transform.position + raycastOrigin, dir, 0.6f, layerMask);
        //Debug.DrawRay(transform.position + raycastOrigin, dir, Color.green); 
    }

    public void StartGame()
    {
        currentState = PlayerStates.playin;
    }

    public void StopGame()
    {
        currentState = PlayerStates.stop;
    }

    public void PlayerTouched()
    {
        if (!immune)
        {
            currentLifes--;
            hudManger.CheckDamage();
            if (currentLifes == 0)
            {
                currentState = PlayerStates.stop;
            }

            // Comienza a ser inmune y a parpadear
            StartCoroutine(ImmuneForSeconds());
        }
    }

    IEnumerator ImmuneForSeconds()
    {
        immune = true;
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        Color originalColor = sprite.color;
        Color newColor = originalColor;
        newColor.a = 0f;


        sprite.color = newColor;
        yield return new WaitForSeconds(0.1f);
        sprite.color = originalColor;
        yield return new WaitForSeconds(0.1f);

        sprite.color = newColor;
        yield return new WaitForSeconds(0.1f);
        sprite.color = originalColor;
        yield return new WaitForSeconds(0.1f);

        sprite.color = newColor;
        yield return new WaitForSeconds(0.1f);
        sprite.color = originalColor;
        yield return new WaitForSeconds(0.1f);

        sprite.color = newColor;
        yield return new WaitForSeconds(0.1f);
        sprite.color = originalColor;
        yield return new WaitForSeconds(0.1f);

        sprite.color = newColor;
        yield return new WaitForSeconds(0.1f);
        sprite.color = originalColor;
        yield return new WaitForSeconds(0.1f);

        immune = false;
    }
}
