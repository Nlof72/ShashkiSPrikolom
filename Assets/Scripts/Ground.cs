using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Ground : MonoBehaviourPunCallbacks
{
    public GameObject tile;
    public GameObject figure;
    public Material[] mats;

    public void SetGame(){
        float x = -3f,y =3.5f ,z = 0;
        SetGround(x,y,z);
        SetFigure(x,y,z, mats[0]);
        var a = gameObject.GetPhotonView();
        //if(!a.IsMine) SetFigure(x,y-5,z, mats[1]);
        SetFigure(x,y-5,z, mats[1]);
        a.RefreshRpcMonoBehaviourCache();
    }

    void SetFigure(float x, float y, float z, Material mat){
        GameObject tGO;
        Vector3 tPos = new Vector3(x,y,z);
        for(int i = 0; i< 3 ; i++){
            for(int j =0 ; j< 4; j++){
                tGO = Instantiate<GameObject>(figure) as GameObject;
                tGO.name = "Figure "+i+" "+j+" "+y;
                var tileRenderer = tGO.GetComponent<Renderer>();
                tileRenderer.material = mat;
                if(y>0){
                    GameController.S.bFigg.Add(tGO);
                }else{
                    GameController.S.wFigg.Add(tGO);
                }
                tGO.transform.SetParent(y>0?GameObject.FindGameObjectWithTag("Black player").transform:GameObject.FindGameObjectWithTag("White player").transform);
                tPos = y>0?new Vector3(x+(float)j+j+1,y-(float)i,z-1):new Vector3(x+(float)j+j,y-(float)i,z-1);
                // tPos = new Vector3(x+(float)j+j+1,y-(float)i,z-1);
                tGO.transform.tag = y>0? "Black figure":"White figure";
                if(i == 1){
                    tPos = y>0?new Vector3(x+(float)j+j,y-(float)i,z-1):new Vector3(x+(float)j+j+1,y-(float)i,z-1);
                    //tPos = new Vector3(x+(float)j+j,y-(float)i,z-1);
                }
                tGO.transform.position = tPos;
            }
        }
    }

    void SetGround(float x, float y, float z){
        GameObject tGO;
        Vector3 tPos = new Vector3(x,y,z);
        for(int i=0; i<8; i++){
            for(int j=0; j<8;j++){
                tGO = Instantiate<GameObject>(tile) as GameObject;
                tGO.name = "Tile " + i+" "+j;
                tGO.transform.SetParent(gameObject.transform);
                tPos = new Vector3(x+(float)j,y-(float)i,z);
                tGO.transform.position = tPos;
                var tileRenderer = tGO.GetComponent<Renderer>();
                GameController.S.tiles.Add(tGO);
                if((i+j+1)%2 == 0){
                    tileRenderer.material = mats[0];
                }
                else{
                    tileRenderer.material = mats[1];
                }   
            }
        }
    }
}
