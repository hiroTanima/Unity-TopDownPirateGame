using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	
///<summary>
/// ChangeSprite description
///</summary>
public class ChangeSprite : MonoBehaviour
{
    public int _index = 0;
    [SerializeField] private Sprite[] _base;
    private SpriteRenderer _spriteRenderer;
    private Damageable _damageable;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _damageable = GetComponentInParent<Damageable>();
    }

    public void ChangeSpriteOnHit()
    {
        if(_damageable.health < _damageable.maxHealth - 2 && _damageable.health > _damageable.maxHealth - 4)
        {
            _index = 1;
        }
        else if (_damageable.health < _damageable.maxHealth - 4 && _damageable.health > 1)
        {
            _index = 2;
        }else if(_damageable.health == 1)
        {
            _index = 3;
        }else if (_damageable.health < 1)
        {
            _index = 3;
        }

        _spriteRenderer.sprite = _base[_index];
    }
}
