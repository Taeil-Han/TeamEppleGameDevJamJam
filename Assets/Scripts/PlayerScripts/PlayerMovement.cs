using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Sprite playerSprite;
    [SerializeField] GameObject projectile;

    public float speed = 5f;
    public float minX = -8f;
    public float maxX = 8f;
    public float firerate = 0.3f;

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

        Vector3 mouseScreenPos = Input.mousePosition;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0f;

        if (Input.GetMouseButtonDown(0)) 
        {
            Shoot(pos, mouseWorldPos);
        }

    }

    public void Shoot(Vector3 startPos, Vector3 endPos) 
    {
        GameObject obj = Instantiate(projectile, startPos, Quaternion.identity);
        Projectile proj = obj.GetComponent<Projectile>();
        proj.Init(startPos, endPos);
    }
}
