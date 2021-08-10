using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float runSpeed = 20.0f;
    public GameObject[] towerPrefab;
    public GameObject healthCrop;
    public float offset = 1.0f;
    public int money = 400;

    public int healthCrops = 10;
    public int roundReward = 100;

    enum State {Crops, Wave}; // Can only place health crops in the crop phase

    private State state;
    private Vector3 gridOffset = new Vector3(0.0f, 0.0f, 0.0f); // offset for the grid. Currently at 0.0 to center each tile 
    private int selectedTower;
    private KeyCode[] keyMapping;
    private Rigidbody2D body;
    private float horizontal;
    private float vertical;
    private int dir; // cardinal direciton player is facing for tower placement. 0 = E, 1 = N, 2 = W, 3 = S
    private GameObject selector; // Child selector

    private void Start () {
        body = GetComponent<Rigidbody2D>(); 
        
        Transform[] transforms = this.GetComponentsInChildren<Transform>();
        foreach(Transform t in transforms) {
            if (t.gameObject.name == "Selector") {
                selector  = t.gameObject;
            }
        }

        keyMapping = new KeyCode[towerPrefab.Length];
        for (int i = 0; i < towerPrefab.Length; i++) {
            keyMapping[i] = (KeyCode) System.Enum.Parse(typeof(KeyCode), "Alpha" + (i + 1)) ;
        }

        state = State.Crops;
    }

    private void Update () {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical"); 
        // update direciton facing
        if (horizontal != 0) {
            dir = (int) Mathf.Floor(horizontal + 1);
        } else if (vertical != 0) { 
            dir = (int) Mathf.Floor(vertical + 2);
        }

        // Adjust the selector position based on movement
        switch (Dir()) {
            case 0:
                selector.transform.position = Snap(transform.position + new Vector3(-offset, 0.0f, 0)) ;
            break;
            case 1:
                selector.transform.position = Snap(transform.position + new Vector3(0.0f, -offset, 0));
            break;
            case 2:
                selector.transform.position = Snap(transform.position + new Vector3(offset, 0.0f, 0));
            break;
            case 3:
                selector.transform.position = Snap(transform.position + new Vector3(0.0f, offset, 0));
            break;
        }

        // Place a tower when the button is pressed
        if (Input.GetButtonDown("Jump") && towerPrefab.Length > selectedTower) {
            PlaceTower();
        } else {
            // Change the currently active tower
            for (int i = 0; i < keyMapping.Length; i++) {
                if (Input.GetKeyDown(keyMapping[i])) {
                    selectedTower = i;
                    Debug.Log("Selected tower is = " + selectedTower);
                }
            }
        }

    }

    private void FixedUpdate() {  
        body.velocity = new Vector2(horizontal, vertical);
        body.velocity = body.velocity.normalized * runSpeed * Time.deltaTime;
    }

    public int Dir() {
        return dir;
    }

    /// <summary>
    /// Pay for a tower with given cost
    /// </summary>
    /// <param name="cost">cost of the tower</param>
    /// <returns>true if there is enough money, false otherwise</returns>
    public bool PayForTower(int cost) {
        if (money - cost >= 0) {
            money -= cost;
            Debug.Log("Payed for tower. Money left = " + money);
            return true;
        }
        return false;
    }

    // Attempts to plant crop, decrementing number avaialble
    // returns false if no more crops to plant
    public bool PlantCrop() {
        healthCrops--;
        if (healthCrops <= 0) {
            state = State.Wave;
            RoundManager.StartWavePhase();
        }
        return healthCrops >= 0;
    }

    /// <summary>
    /// Snap Vector3 to nearest grid position
    /// </summary>
    /// <param name="vector3">Sloppy position</param>
    /// <param name="gridSize">Grid size</param>
    /// <returns>Snapped position</returns>
    public Vector3 Snap(Vector3 vector3, float gridSize = 1.0f) {
        Vector3 snapped = vector3;
        snapped = new Vector3(
            Mathf.Round(vector3.x / gridSize) * gridSize,
            Mathf.Round(vector3.y / gridSize) * gridSize,
            Mathf.Round(vector3.z / gridSize) * gridSize);
        return snapped + gridOffset ;
    }

    /// <summary>
    /// Award the player money for copmpleting a round
    /// </summary>
    public void AwardRoundMoney() {
        money += roundReward;
    }

    // Places a tower at the current selector position
    private void PlaceTower() {
        if (state == State.Crops) {
            Instantiate(healthCrop, selector.transform.position, Quaternion.identity);
        } else {
            Instantiate(towerPrefab[selectedTower], selector.transform.position, Quaternion.identity);
        }
    }
}
