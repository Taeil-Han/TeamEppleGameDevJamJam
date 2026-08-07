using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Sprite playerSprite;
    [SerializeField] GameObject projectile;

    [SerializeField] float speed = 5f;
    [SerializeField] float minX = -8f;
    [SerializeField] float maxX = 8f;
    

    [SerializeField] GameObject aimLineSprite;
    [SerializeField] GameObject aimEndSprite;
    [SerializeField] float spacing = 0.3f;
    [SerializeField] float spriteRotationOffset = -90;
    [SerializeField] float firerate = 0.3f;
    private float nextFireTime = 0f;

    private List<GameObject> aimLineObjects = new List<GameObject>();

    //Dragging
    private bool isDragging = false;


    void Start()
    {
        
    }

    void Update()
    {
        float direction = 0f;
        if (Input.GetKey(KeyCode.A))
            direction = -1f;
        else if (Input.GetKey(KeyCode.D))
            direction = 1f;

        Vector3 pos = transform.position;
        pos.x += direction * speed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, minX, maxX);

        transform.position = pos;
        float myY = transform.position.y;

        Vector3 currPos = GetMouseWorldPos();

        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
        }

        if (isDragging && Input.GetMouseButton(0) && myY < currPos.y) //Clamps Angle
        {
            Aim(pos, currPos);
        }
        if (Input.GetMouseButtonUp(0)) 
        {
            if (myY < currPos.y) //Here Too (If need to edit, so it's not the whole screen)
            {
                Shoot(pos, currPos);
            }
            isDragging = false;
            ClearAimLine();
        }

    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0f;
        return pos;
    }


    #region Aim Methods
    public void Aim(Vector3 startPos, Vector3 endPos) //Starts all Aim
    {
        ClearAimLine();
        SpawnAimLine(startPos, endPos);
    }

    public void SpawnAimLine(Vector3 start, Vector3 end) //Spawns Aim Line
    {
        Vector3 direction = (end - start).normalized;
        float distance = Vector3.Distance(start, end);

        // Calculate rotation angle so sprites face along the line
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle + spriteRotationOffset);

        int count = Mathf.FloorToInt(distance / spacing);

        for (int i = 0; i <= count; i++)
        {
            Vector3 spawnPos = start + direction * (spacing * i);
            GameObject piece = Instantiate(aimLineSprite, spawnPos, rotation);
            aimLineObjects.Add(piece);
        }
        GameObject endPiece = Instantiate(aimEndSprite, end, rotation);
        aimLineObjects.Add(endPiece);
    }

    public void ClearAimLine() //Clears Aim Line
    {
        foreach (GameObject obj in aimLineObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        aimLineObjects.Clear();
    }
    #endregion

    public void Shoot(Vector3 startPos, Vector3 endPos) 
    {
        if (Time.time < nextFireTime) { return; }

        GameObject obj = Instantiate(projectile, startPos, Quaternion.identity);
        Projectile proj = obj.GetComponent<Projectile>();
        proj.Init(startPos, endPos);
        
        //Timer for bullet
        nextFireTime = Time.time + firerate;
    }
}
