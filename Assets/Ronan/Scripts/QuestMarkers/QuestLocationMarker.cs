using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLocationMarker : MonoBehaviour
{
    [Header("Marker Data")]
    public float StartingHeight;
    public Sprite Icon;
    public float Scale = 0.5f;

    [Header("Movement Settings")]
    public float RotationSpeed = 130f;
    [Space(5f)]
    public bool IsBobbing = false;
    private bool IsGoingUp = false;
    public float MaxBobHeight = 0f;
    public float MinBobHeight = 0f;
    public float BobSpeed = 1f;

    public void Initialise()
    {
        transform.localPosition = new Vector3(transform.localPosition.x,StartingHeight,transform.localPosition.z);
        transform.localScale = new Vector3(Scale, Scale, Scale);
        GetComponent<SpriteRenderer>().sprite = Icon;
    }

    private void Start()
    {
        Initialise();
    }

    public void Update()
    {
        transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + (RotationSpeed * Time.deltaTime), transform.rotation.eulerAngles.z);

        if (IsBobbing)
        {
            if (IsGoingUp)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + (BobSpeed * Time.deltaTime), transform.position.z);
                if (transform.localPosition.y >= MaxBobHeight)
                {
                    IsGoingUp = false;
                    //Debug.Log("Going Up False");
                }
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - (BobSpeed * Time.deltaTime), transform.position.z);
                if (transform.localPosition.y <= MinBobHeight)
                {
                    IsGoingUp = true;
                    //Debug.Log("Going Up True");
                }
            }

        }
    }
}
