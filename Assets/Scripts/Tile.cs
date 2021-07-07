using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{   
    private void OnMouseUpAsButton() {
        GameController.S.MoveTile(gameObject);
    }

}
