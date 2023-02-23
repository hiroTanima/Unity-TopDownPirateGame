using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	
///<summary>
/// Enemy_AI description
///</summary>
///

public enum EnemyType
{
    Chaser,
    Shooter
}
public class Enemy_AI : MonoBehaviour
{
    [SerializeField] private float _speed;
    public EnemyType _enemyType;
    [SerializeField] private float _minimumDistance;
    [SerializeField] private Projectile _projectile;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float timeBetweenShoots;

    private float nextShotTime;
    private Rigidbody2D _rigidbody2D;
    private Transform _target;

    [SerializeField] private GameObject _particle;

    //Awake is called when the script instance is being loaded
    void Awake()
	{
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Initialize()
    {
        _target = GameManager.Instance.player;
        transform.GetComponent<Damageable>().Heal(5);
        foreach(ChangeSprite cs in transform.GetComponentsInChildren<ChangeSprite>())
        {
            cs._index = 0;
            cs.ChangeSpriteOnHit();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
        if(_enemyType == EnemyType.Chaser)
        {
            if (Vector2.Distance(transform.position, _target.position) > _minimumDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
            }
            else
            {
                //ATTACK
                Explode();
            }
        }

        if (_enemyType == EnemyType.Shooter)
        {


            if (Vector2.Distance(transform.position, _target.position) > _minimumDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
            }
            else
            {
                //ATTACK
                Attack();
            }
        }
    }

    private void Explode()
    {
        var player = _target.GetComponent<Damageable>();
        player.Damage(2f);
        //Destroy(gameObject);
        VFXOnDeath();
    }

    private void Attack()
    {
        if (_projectile == null || shootPoint == null) return;
        if(Time.time > nextShotTime)
        {
            //var _bullet = Instantiate(_projectile.gameObject, shootPoint.position, transform.rotation);
            GameObject _bullet = PoolManager.Instance.GetPooledObject();
            if (_bullet != null)
            {
                _bullet.transform.position = shootPoint.position;
                _bullet.transform.rotation = transform.rotation;
                _bullet.SetActive(true);
                _bullet.GetComponent<Projectile>().Initialize();
            }
            else
            {
                GameObject obj = Instantiate(_projectile.gameObject);
                obj.transform.position = shootPoint.position;
                obj.transform.rotation = transform.rotation;
                obj.transform.SetParent(PoolManager.Instance.transform);
                obj.GetComponent<Projectile>().Initialize();
                PoolManager.Instance.AddToPoolList(obj);
            }
            nextShotTime = Time.time + timeBetweenShoots;
        }
    }

    private void LookAtPlayer()
    {
        Vector2 direction = _target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;
        //Slerp to rotation - Lerp to v2 
    }

    public void VFXOnDeath()
    {
        Instantiate(_particle, transform.position, transform.rotation);

        gameObject.SetActive(false);
    }
}
