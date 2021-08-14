using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float runSpeed = 20.0f;
    public int money = 400;

    public int roundReward = 100;

    enum State {Crops, Wave}; // Can only place health crops in the crop phase

    private State state;
    private KeyCode[] keyMapping;
    private Rigidbody2D body;
    private float horizontal;
    private float vertical;
    private int dir; // cardinal direction player is facing for tower placement. 0 = E, 1 = N, 2 = W, 3 = S
    private Animator animator;

    private void Start () {
        body = GetComponent<Rigidbody2D>();

        state = State.Crops;
        animator = GetComponent<Animator>();
    }

    private void Update () {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical"); 
        // update direction facing
        if (horizontal != 0) {
            dir = (int) Mathf.Floor(horizontal + 1);
            Vector3 direciton = new Vector3((int) Mathf.RoundToInt(horizontal),1,1);
            transform.localScale = direciton;
        } else if (vertical != 0) { 
            dir = (int) Mathf.Floor(vertical + 2);
        }
        if (horizontal != 0 || vertical != 0) {
            animator.SetFloat("speed", 1.0f);
        } else {
            animator.SetFloat("speed", 0.0f);
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    public bool IsWavePhase() {
        return State.Wave == state;
    }

    private void FixedUpdate() {  
        body.velocity = new Vector2(horizontal, vertical);
        body.velocity = body.velocity.normalized * runSpeed * Time.deltaTime;
    }

    public int Dir() {
        return dir;
    }

    /// <summary>
    /// Award the player money for copmpleting a round
    /// </summary>
    public void AwardRoundMoney() {
        money += roundReward;
    }
}
