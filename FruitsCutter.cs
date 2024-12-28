using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public delegate void MouseOverFruitPlay();

public class FruitsCutter : MonoBehaviour
{

    Camera mainCamera;

    public event MouseOverFruitPlay OnMouseOver;


    void Start()
    {
        mainCamera = Camera.main;
    }




    void Update()
    {

        Ray raytest = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(raytest, out hit, Mathf.Infinity,1 << 8))
        {
            Debug.Log(hit.collider.gameObject.layer);
            FruitScript fruitScript = hit.collider.GetComponent<FruitScript>();

            if(fruitScript != null)
            {
                OnMouseOver += fruitScript.PlayParticle;
                OnMouseOver?.Invoke();
                fruitScript.isPlay = true;

            }
        }
    }
}
