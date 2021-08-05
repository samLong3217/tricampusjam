using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour {
    GameObject parent;
    PlayerController pController;

    public float offset = 0.5f;

    Vector3 gridOffset = new Vector3(0.0f, 0.0f, 0.0f); // offset for the grid. Currently at 0.0 to center each tile 

    void Start() {
        parent = transform.parent.gameObject;
        pController = parent.GetComponent<PlayerController>();
    }

    void LateUpdate() { 
        if (pController != null) {
            switch (pController.Dir()) {
                case 0:
                    transform.position = Snap(parent.transform.position + new Vector3(-offset, 0.0f, 0)) ;
                break;
                case 1:
                    transform.position = Snap(parent.transform.position + new Vector3(0.0f, -offset, 0));
                break;
                case 2:
                    transform.position = Snap(parent.transform.position + new Vector3(offset, 0.0f, 0));
                break;
                case 3:
                    transform.position = Snap(parent.transform.position + new Vector3(0.0f, offset, 0));
                break;
            }
        }
        Debug.Log(transform.position);
    }

    /// <summary>
     /// Snap Vector3 to nearest grid position
     /// </summary>
     /// <param name="vector3">Sloppy position</param>
     /// <param name="gridSize">Grid size</param>
     /// <returns>Snapped position</returns>
     public Vector3 Snap(Vector3 vector3, float gridSize = 1.0f)
     {
         Vector3 snapped = vector3;
         snapped = new Vector3(
             Mathf.Round(vector3.x / gridSize) * gridSize,
             Mathf.Round(vector3.y / gridSize) * gridSize,
             Mathf.Round(vector3.z / gridSize) * gridSize);
        return snapped + gridOffset ;
     }
}
