using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class healthUiFx : MonoBehaviour
{
    [SerializeField] private Transform camTransform;
    private Quaternion originalRotation;
    [SerializeField] private TMP_Text Txt_Health;
    [SerializeField] private Slider Sld_Health;
    private Player player;
    void Start()
    {
        player = GetComponentInParent<Player>();
        camTransform.SetParent(null);
        originalRotation = transform.rotation;
        Sld_Health.maxValue = player.GetHealth.health.Value;
    }

    private void Update()
    {       
        camTransform.transform.position = gameObject.transform.GetComponentInParent<Transform>().position
            + new Vector3(0, 8, -9);
        camTransform.transform.rotation = Quaternion.Euler(40, 0, 0);
        transform.rotation = camTransform.rotation * originalRotation;
        Txt_Health.text = player.GetHealth.health.Value.ToString();
        Sld_Health.value = player.GetHealth.health.Value;
    }
}
