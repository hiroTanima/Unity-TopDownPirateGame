using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	
///<summary>
/// Projectile description
///</summary>
public class Projectile : MonoBehaviour
{
    public float speed;
	private void Start()
	{
        Initialize();
    }

    public void Initialize()
    {
        DisableGameObjectDelayed(2f);
    }

    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.up);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Damageable>() != null )
        {
            var damage = collision.GetComponent<Damageable>();
            damage.Damage(1f);
            this.gameObject.SetActive(false);
        }
    }
    private void DisableGameObjectDelayed(float delay)
    {
        StartCoroutine(DisableGameObjectCoroutine(delay));
    }

    private IEnumerator DisableGameObjectCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        this.gameObject.SetActive(false);
    }
}
