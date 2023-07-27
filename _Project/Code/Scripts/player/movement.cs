using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class movement : NetworkBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private float speed;
    private Animator _anim;

    private void OnMove(float v, float h)
    {
        _anim.SetBool("_isMove", h != 0 || v != 0);
        if (h != 0 && v != 0)
            transform.position += transform.forward * speed * Time.deltaTime;
        // _rb.velocity += transform.forward * speed * Time.deltaTime;
        //_rb.velocity = Vector3.Lerp(transform.position, transform.forward, speed);
    }


    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!IsOwner) return;

        var h = VirtualJoyStick.Horizontal();
        var v = VirtualJoyStick.Vertical();
        OnMove(v, h);

        Vector3 moveDirection = new Vector3(h, 0, v);
        if (moveDirection != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 8);
        }
    }
    //https://medium.com/@nicemoonpool/tips-to-improve-unity-performance-dd1762ece924
}