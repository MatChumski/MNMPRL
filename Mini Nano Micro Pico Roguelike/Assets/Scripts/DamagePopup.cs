using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{

    [SerializeField] private Transform pfDamagePopup;

    public DamagePopup Create(Vector3 position, int damageAmount)
    {
        Transform damagePopupTr = Instantiate(pfDamagePopup, position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTr.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount);

        return damagePopup;
    }

    private TextMeshPro textMesh;
    private float dissapearTimer;
    private Color textColor;

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(int damageAmount)
    {
        textMesh.SetText(damageAmount.ToString());
        textColor = textMesh.color;
        dissapearTimer = 0.5f;
    }

    private void Update()
    {
        float speed = 2f;
        transform.position += new Vector3(0, speed) * Time.deltaTime;

        dissapearTimer -= Time.deltaTime;
        if (dissapearTimer <= 0)
        {
            float dissapearSpeed = 5f;
            textColor.a -= dissapearSpeed * Time.deltaTime;
            textMesh.color = textColor;

            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
