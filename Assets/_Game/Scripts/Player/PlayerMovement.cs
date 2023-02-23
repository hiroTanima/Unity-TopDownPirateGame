using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	
///<summary>
/// PlayerMovement description
///</summary>
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Vector2 _input;
    private Rigidbody2D _rigidbody2D;
    private Camera _mainCamera;

    //Awake is called when the script instance is being loaded
    private void Awake()
	{
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _mainCamera = Camera.main;
        GameManager.Instance.player = this.gameObject.GetComponent<Transform>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        LookAtMouse();
    }

    private void FixedUpdate()
    {
        if(_rigidbody2D != null)
        {
            _rigidbody2D.velocity = _speed * Time.deltaTime * _input;
        }
    }

    private void LookAtMouse()
    {
        Vector2 direction = _mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;
        //Slerp to rotation - Lerp to v2 
    }


}
