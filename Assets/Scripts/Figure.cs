using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figure : MonoBehaviour
{
    GameObject tGO;
    public bool isChosen = false;

    private void Start() {
        tGO =  GameObject.FindGameObjectWithTag("Light");
    }
    private void OnMouseDown() {
        print(gameObject.name);
        GameController.S.SetLight(gameObject.transform.position);
        GameController.S.MoveFig(gameObject);
    }

    public GameObject transfer(){
        return gameObject;
    }



    private void OnMouseUp() {
        isChosen = false;
        //tGO.transform.position = Vector3.zero;
        // Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // pos.z = -1;
        // gameObject.transform.position = pos; 

    }
}
