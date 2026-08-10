using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    [SerializeField] Sprite playerSprite;
    [SerializeField] GameObject lvl1proj;
    [SerializeField] GameObject lvl2proj;
    [SerializeField] GameObject lvl3proj;


    [SerializeField] float speed = 5f;
    [SerializeField] float minX = -8f;
    [SerializeField] float maxX = 8f;
    [SerializeField] float minShootX = -6;
    [SerializeField] float maxShootX = 6;

    //Aiming and Firerate
    private List<GameObject> aimLineObjects = new List<GameObject>();
    [SerializeField] GameObject aimLineSprite;
    [SerializeField] GameObject aimEndSprite;
    [SerializeField] float aimOffsetY = 0.5f;
    [SerializeField] float spacing = 0.3f;
    [SerializeField] float spriteRotationOffset = -90;
    [SerializeField] float firerate = 0.3f;
    private float nextFireTime = 0f;
    
    //Shell Organization
    [SerializeField] GameObject[] bulletPrefabs; //Change for 5 bullets if time permits
    private int[] numOfShells = new int[3] { 10, 100, 100};
    private int currentShellIndex = 0;
    private int unlockedShell = 0;

    //ChargeShot
    [SerializeField] float minChargeTime = 0f;
    [SerializeField] float maxChargeTime = 2f; // fully charged at 2 seconds
    private float chargeStartTime;
    private bool isCharging = false;
    [SerializeField] GameObject chargeVisualPrefab;
    [SerializeField] float minScaleY = 0.5f;
    [SerializeField] float maxScaleY = 2f;
    private GameObject currentChargeVisual;

    //Dragging
    private bool isDragging = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        bulletPrefabs = new GameObject[1] { lvl1proj };
    }

    void Update()
    {
        if (ShopMenu.isShopOpen)
        {
            return;
        }
        if (PauseMenu1.isPaused) 
        {
            return;
        }
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

        //LEFT-CLICK AND HOVER
        if (myY + aimOffsetY < currPos.y && !isCharging) //Clamps Angle
        {
            ClearAimLine();
            Aim(pos, currPos);
        }
        if (Input.GetMouseButtonDown(0) && !isCharging && currPos.x >= minShootX && currPos.x <= maxShootX) 
        {
            if (myY + aimOffsetY < currPos.y) //Here Too (If need to edit, so it's not the whole screen)
            {
                Shoot(pos, currPos, currentShellIndex);
            }
        }

        //RIGHT-CLICK
        if (Input.GetMouseButtonDown(1) && currentShellIndex == 1)
        {
            chargeStartTime = Time.time;
            isCharging = true;
        }

        if (isCharging && Input.GetMouseButton(1))
        {
            ClearAimLine();
            float chargeDuration = Time.time - chargeStartTime;
            float chargePercent = Mathf.Clamp01(chargeDuration / maxChargeTime);
            ChargeAim(pos, currPos, chargePercent);
        }

        if (Input.GetMouseButtonUp(1))
        {
            if (currPos.x <= minShootX && currPos.x >= maxShootX && isCharging)
            {
                isCharging = false;
            }
            else if (isCharging)
            {
                float chargeDuration = Time.time - chargeStartTime;
                float chargePercent = Mathf.Clamp01(chargeDuration / maxChargeTime);
                ChargeShot(pos, currPos, currentShellIndex, chargePercent);
                Debug.Log(chargePercent);
                isCharging = false;
            }
            if (currentChargeVisual != null)
            {
                Destroy(currentChargeVisual);
                currentChargeVisual = null;
            }
        }

        //SCROLL
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
        //Debug.Log(currentShellIndex);

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentShellIndex = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && bulletPrefabs.Length >= 2)
        {
            currentShellIndex = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && bulletPrefabs.Length == 3)
        {
            currentShellIndex = 2;
        }
    }

    #region General Stuff
    Vector3 GetMouseWorldPos()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0f;
        return pos;
    }

    Quaternion GetRotation(Vector3 startPos, Vector3 endPos) 
    {
        Vector3 direction = (endPos - startPos).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0, 0, angle + spriteRotationOffset);
    }
    #endregion

    #region Getters and Setters
    public int GetAmmoIndex() 
    { 
        return currentShellIndex;
    }

    public int[] GetAmmoCount()
    {
        return numOfShells;
    }

    #endregion

    #region Aim Methods
    public void Aim(Vector3 startPos, Vector3 endPos) //Starts all Aim
    {
        ClearAimLine();
        SpawnAimLine(startPos, endPos);
    }

    public void SpawnAimLine(Vector3 start, Vector3 end) //Spawns Aim Line
    {
        start.y = start.y + aimOffsetY;
        Vector3 direction = (end - start).normalized;
        float distance = 3f;

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
        GameObject endPiece = Instantiate(aimEndSprite, start + direction * (distance + 0.3f), rotation);
        aimLineObjects.Add(endPiece);
    }

    public void ChargeAim(Vector3 startPos, Vector3 endPos, float chargePercentage) 
    {
        startPos.y = startPos.y + aimOffsetY;
        Quaternion rotation = GetRotation(startPos, endPos);

        if (currentChargeVisual == null)
        {
            currentChargeVisual = Instantiate(chargeVisualPrefab, startPos, Quaternion.identity);
        }

        currentChargeVisual.transform.position = startPos;
        currentChargeVisual.transform.rotation = rotation;

        float scaleY = Mathf.Lerp(minScaleY, maxScaleY, chargePercentage);
        Vector3 newScale = currentChargeVisual.transform.localScale;
        newScale.y = scaleY;
        currentChargeVisual.transform.localScale = newScale;
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

    #region Shoot Methods
    public void Shoot(Vector3 startPos, Vector3 endPos, int currShell) 
    {
        startPos.y = startPos.y + aimOffsetY;
        Quaternion rotation = GetRotation(startPos, endPos);

        if (Time.time < nextFireTime) { return; }

        switch (currShell)
        {
            case 1:
                if (numOfShells[1] > 0) { 
                    GameObject obj2 = Instantiate(lvl2proj, startPos, rotation);
                    Lvl2Projectile proj2 = obj2.GetComponent<Lvl2Projectile>();
                    proj2.Init(startPos, endPos);
                    numOfShells[1] -= 1;
                }
                break;
            case 2:
                if (numOfShells[2] > 0)
                {
                    GameObject obj3 = Instantiate(lvl3proj, startPos, rotation);
                    Lvl3Projectile proj3 = obj3.GetComponent<Lvl3Projectile>();
                    proj3.Init(startPos, endPos);
                    proj3.SetTarget(transform);
                    numOfShells[2] -= 1;
                }
                break;
            default:
                if (numOfShells[0] > 0)
                {
                    GameObject obj = Instantiate(lvl1proj, startPos, rotation);
                    Lvl1Projectile proj = obj.GetComponent<Lvl1Projectile>();
                    proj.Init(startPos, endPos);
                    numOfShells[0] -= 1;
                }
                break;
        }
        
        //Timer for bullet
        nextFireTime = Time.time + firerate;
    }

    public void ChargeShot(Vector3 startPos, Vector3 endPos, int currShell, float percent) 
    {
        if (currentShellIndex != 1) { return; }
        startPos.y = startPos.y + aimOffsetY;
        Quaternion rotation = GetRotation(startPos, endPos);
        GameObject obj2 = Instantiate(lvl2proj, startPos, rotation);
        Lvl2Projectile proj2 = obj2.GetComponent<Lvl2Projectile>();
        proj2.Init(startPos, endPos, percent);
        numOfShells[1] -= 1;
    }
    #endregion

    #region Ammo Methods
    public void AddShell(int shellIndex, int amount)
    {
        numOfShells[shellIndex] += amount;
    }

    public void UnlockShell(int shellLvl) 
    {
        if (shellLvl == 2) 
        {
            bulletPrefabs = new GameObject[2] { lvl1proj, lvl2proj };
        }
        if (shellLvl == 3)
        {
            bulletPrefabs = new GameObject[3] { lvl1proj, lvl2proj, lvl3proj };
        }

    }
    #endregion
}
