using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;



public class GameController : MonoBehaviourPunCallbacks
{
    public GameObject Light, figMov = null, tile = null;
    static public GameController S;
    public Ground ground;
    public List<GameObject> wFigg, bFigg, tiles;

    int turn = 1;

    static float NeededCoord(float a){
        if(a<1.5f) return -1;
        if(a<2) return 2;
        if(Mathf.Abs(a -3)  <= 0.01) return 3;
        return -1;
    }
    void Start()
    {
        S = this;
        //списки фигур здесь инициализируются
        ground.SetGame();
        
    }

    public void MoveFig(GameObject go){
        if((turn%2 == 1)&&(go.tag == "Black figure")) return;
        else if((turn%2 == 0 )&&(go.tag == "White figure")) return;
        figMov = go;
        tile = null;
    }

    public void MoveTile(GameObject go){
        tile = go;
        if(figMov == null) return;
        if(NeededCoord(Vector3.Distance(go.transform.position,figMov.transform.position)) == 2){
            if(figMov.transform.tag == "Black figure"){
                if((figMov.transform.position - go.transform.position).y < 0) return;
            }else
                if((figMov.transform.position - go.transform.position).y > 0) return;
            if(!checTile()) return;
            // Vector3 pos = go.transform.position;
            // pos.z = -1;
            // figMov.transform.position = pos;
            // figMov = null;
            // tile = null;
            // Light.transform.position = Vector3.zero;
            ChangeTurn();
        }else if(NeededCoord(Vector3.Distance(go.transform.position,figMov.transform.position)) == 3){
            if(!checTile()) return;
            if(CheckCut(0)) return;
        }
            Vector3 pos = go.transform.position;
            pos.z = -1;
            figMov.transform.position = pos;
            figMov = null;
            tile = null;
            Light.transform.position = Vector3.zero;
    }

    public void ChangeTurn(){
        
        turn++;
    }
    public bool checTile(){
        print(1);
        Vector3 pos = tile.transform.position;
        pos.z = -1;
        foreach(var fig in wFigg){
            if(fig.transform.position == pos){
                return false;
            }
        }
        foreach(var fig in bFigg){
            if(fig.transform.position == pos){
                return false;
            }
        }
        return true;
    }
    public void SetLight(Vector3 pos){
        Light.transform.position = pos;
    }

    bool CheckCut(int a){
        string tag = figMov.tag;
        List<GameObject> figs = tag == "Black figure"? wFigg:bFigg;
        List<GameObject> firstList, secondList;
        firstList = new List<GameObject>();
        secondList = new List<GameObject>();
        Vector3 pos = tile.transform.position;
        pos.z = -1;
        foreach(var fig in figs){
            
            if(Vector3.Distance(figMov.transform.position, fig.transform.position)<1.5){
                firstList.Add(fig);
            }
            if(Vector3.Distance(pos, fig.transform.position)<1.5){
                secondList.Add(fig);
            }
        }

        foreach(var fig1 in firstList){
            foreach(var fig2 in secondList){
                if(fig1.name == fig2.name){
                    print(fig1.name);
                    figs.Remove(fig1);
                    Destroy(fig1);
                    return false;
                }
        }
        }
        return true;
    }
}
