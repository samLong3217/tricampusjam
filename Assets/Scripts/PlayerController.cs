using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    Rigidbody2D body;
    float horizontal;
    float vertical;
    int dir; // cardinal direciton player is facing for tower placement. 0 = E, 1 = N, 2 = W, 3 = S

    GameObject selector; // Child selector


    public float runSpeed = 20.0f;

    public GameObject[] towerPrefab;
    int selectedTower;
    
    KeyCode[] keyMapping;


    public float offset = 1.0f;
    Vector3 gridOffset = new Vector3(0.0f, 0.0f, 0.0f); // offset for the grid. Currently at 0.0 to center each tile 

    void Start () {
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
    }

    void Update () {
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

    // Places a tower at the current selector position
    public void PlaceTower() {
        Instantiate(towerPrefab[selectedTower], selector.transform.position, Quaternion.identity);
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
}
