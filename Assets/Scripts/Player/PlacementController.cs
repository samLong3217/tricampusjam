using UnityEngine;

public class PlacementController : MonoBehaviour
{
    private static PlacementController _instance;

    public SpriteRenderer Ghost;
    public GameObject[] PlaceablePrefabs;
    public Sprite[] PlaceableSprites;
    public GameObject SellPrefab;
    public Sprite SellSprite;

    public int SelectionIndex = 0;

    private AudioSource source;

    private void Awake()
    {
        Destroy(_instance);
        _instance = this;
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Vector2 myScreenPosition = CameraFollow.MainCamera.WorldToScreenPoint(transform.position);
        Vector2 mousePosition = Input.mousePosition;
        Vector2 normDelta = (mousePosition - myScreenPosition).normalized;

        Vector2Int location = new Vector2Int(
            Mathf.RoundToInt(transform.position.x + normDelta.x),
            Mathf.RoundToInt(transform.position.y + normDelta.y));

        float scroll = Input.mouseScrollDelta.y;
        if (scroll < 0)
        {
            SelectionIndex = (SelectionIndex + 1) % PlaceablePrefabs.Length;
        }
        else if (scroll > 0)
        {
            SelectionIndex = (SelectionIndex + PlaceablePrefabs.Length - 1) % PlaceablePrefabs.Length;
        }
        
        for (int i = 0; i < PlaceablePrefabs.Length; i++) {
            if (Input.GetKeyDown("" + (i + 1)))
            {
                SelectionIndex = i;
            }
        }

        if (SelectionIndex < 0)
        {
            Ghost.sprite = null;
            return;
        }

        // Place a tower when the button is pressed
        if (Input.GetMouseButtonDown(0)) {
            Instantiate(PlaceablePrefabs[SelectionIndex], (Vector2) location, Quaternion.identity);
            source.Play();
        }
        else
        {
            if (GridManager.IsValidLocation(location) && GridManager.Get(location) == null)
            {
                Ghost.sprite = PlaceableSprites[SelectionIndex];
                Ghost.transform.position = (Vector2)location;
            }
            else
            {
                Ghost.sprite = null;
            }
        }
    }

    /// <summary>
    /// Removes the crop from the placeable object list and replaces it with the sell command
    /// </summary>
    public static void ReplaceCrop()
    {
        _instance.PlaceablePrefabs[0] = _instance.SellPrefab;
        _instance.PlaceableSprites[0] = _instance.SellSprite;
        _instance.SelectionIndex = -1;
    }
}
