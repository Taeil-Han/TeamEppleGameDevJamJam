using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Sprite playerSprite;
    [SerializeField] GameObject lvl1proj;
    [SerializeField] GameObject lvl2proj;
    [SerializeField] GameObject lvl3proj;


    [SerializeField] float speed = 5f;
    [SerializeField] float minX = -8f;
    [SerializeField] float maxX = 8f;
    
    //Aiming and Firerate
    private List<GameObject> aimLineObjects = new List<GameObject>();
    [SerializeField] GameObject aimLineSprite;
    [SerializeField] GameObject aimEndSprite;
    [SerializeField] float spacing = 0.3f;
    [SerializeField] float spriteRotationOffset = -90;
    [SerializeField] float firerate = 0.3f;
    private float nextFireTime = 0f;
    
    //Shell Organization
    [SerializeField] GameObject[] bulletPrefabs = new GameObject[5];
    private int currentShellIndex = 0;
    private int unlockedShell = 0;
    

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

        if (myY < currPos.y) //Clamps Angle
        {
            ClearAimLine();
            Aim(pos, currPos);
        }
        if (Input.GetMouseButtonDown(0)) 
        {
            if (myY < currPos.y) //Here Too (If need to edit, so it's not the whole screen)
            {
                Shoot(pos, currPos, currentShellIndex);
            }
        }

        float scroll = Input.mouseScrollDelta.y;

        if (scroll > 0f)
        {
            currentShellIndex++;
            if (currentShellIndex >= bulletPrefabs.Length)
                currentShellIndex = 0;
        }
        else if (scroll < 0f)
        {
            currentShellIndex--;
            if (currentShellIndex < 0)
                currentShellIndex = bulletPrefabs.Length - 1;
        }

        Debug.Log(currentShellIndex);

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

    public void Shoot(Vector3 startPos, Vector3 endPos, int currShell) 
    {
        if (Time.time < nextFireTime) { return; }

        switch (currShell)
        {
            case 1:
                GameObject obj2 = Instantiate(lvl2proj, startPos, Quaternion.identity);
                Lvl2Projectile proj2 = obj2.GetComponent<Lvl2Projectile>();
                proj2.Init(startPos, endPos);
                break;
            case 2:
                GameObject obj3 = Instantiate(lvl3proj, startPos, Quaternion.identity);
                Lvl3Projectile proj3 = obj3.GetComponent<Lvl3Projectile>();
                proj3.Init(startPos, endPos);
                proj3.SetTarget(transform);
                break;
            default:
                GameObject obj = Instantiate(lvl1proj, startPos, Quaternion.identity);
                Lvl1Projectile proj = obj.GetComponent<Lvl1Projectile>();
                proj.Init(startPos, endPos);
                break;
        }
        
        //Timer for bullet
        nextFireTime = Time.time + firerate;
    }
}
