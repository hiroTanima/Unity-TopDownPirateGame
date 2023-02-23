using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	
///<summary>
/// PlayerShoot description
///</summary>
public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Transform shootPoint1,shootPoint2;
    //Awake is called when the script instance is being loaded
    void Awake()
	{
		
	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot(shootPoint);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Shoot(shootPoint);
            Shoot(shootPoint1);
            Shoot(shootPoint2);
        }
    }

    private void Shoot(Transform point)
    {
        if (bulletPrefab == null || shootPoint == null) return;

        //var _bullet = Instantiate(bulletPrefab, shootPoint.position, transform.rotation);
        GameObject _bullet = PoolManager.Instance.GetPooledObject();

        if(_bullet != null)
        {
            _bullet.transform.position = point.position;
            _bullet.transform.rotation = transform.rotation;
            _bullet.SetActive(true);
            _bullet.GetComponent<Projectile>().Initialize();
        }
        else
        {
            GameObject obj = Instantiate(bulletPrefab);
            obj.transform.position = point.position;
            obj.transform.rotation = transform.rotation;
            obj.transform.SetParent(PoolManager.Instance.transform);
            obj.GetComponent<Projectile>().Initialize();
            PoolManager.Instance.AddToPoolList(obj);
        }
    }


}
