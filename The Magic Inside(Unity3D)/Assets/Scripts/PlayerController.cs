using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    public int walkAccelerate = 50;
    public int walkVelocity_MAX = 200; 

    public Sprite[] walkingAnimation;

    private int time;
    private int nowForm;
    private int direction;
    //keys recored.
    private bool key_Z;
    private bool key_C;

    //################################################
    //                     Start
    //################################################
    // Start is called before the first frame update
    void Start(){
        walkingAnimation = Resources.LoadAll<Sprite>("Character/Hilda");
        direction = 2;
        nowForm = 0;

        key_Z = false;
        key_C = false;
    }

    //################################################
    //                     Updata
    //################################################
    // Update is called once per frame
    void Update(){
        MoveController();
        SkillController();
        WalkAnimation();

        ZAxiAsYAxi();
        SyncKey();
    }

    //============================================
    //               MoveController
    //============================================
    //Make a control to player.
    void MoveController() {
        float playerVX = GetComponent<Rigidbody2D>().velocity.x;
        float playerVY = GetComponent<Rigidbody2D>().velocity.y;

        int directionGoZero = 1;
        
        if(Input.GetKey(KeyCode.DownArrow)) {
            direction = 2;
        } else if(Input.GetKey(KeyCode.UpArrow)) {
            direction = 8;
        }
        if(Input.GetKey(KeyCode.LeftArrow)) {
            //decide direction composition.
            switch(direction) {
                case 2: direction = 1; break;
                case 8: direction = 7; break;
                default: direction = 4; break;
            }
        } else if(Input.GetKey(KeyCode.RightArrow)) {
            switch(direction) {
                case 2: direction = 3; break;
                case 8: direction = 9; break;
                default: direction = 6; break;
            }
        }
        if(!(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))) {
            directionGoZero = 0;
        }

        switch(direction * directionGoZero) {
            case 1:
                if(playerVX > -(int)((walkVelocity_MAX - walkAccelerate) * 0.71))  {
                    playerVX -= (int)(walkAccelerate * 0.71);
                } else if(playerVX > -(int)(walkVelocity_MAX * 0.71)) {
                    playerVX = -(int)(walkVelocity_MAX * 0.71);
                }
                if(playerVY > -(int)((walkVelocity_MAX - walkAccelerate) * 0.71)) {
                    playerVY -= (int)(walkAccelerate * 0.71);
                } else if(playerVY > -(int)(walkVelocity_MAX * 0.71)) {
                    playerVY = -(int)(walkVelocity_MAX * 0.71);
                }
                break;
            case 2:
                if(playerVY > -(walkVelocity_MAX - walkAccelerate)) {
                    playerVY -= walkAccelerate;
                } else if(playerVY > -walkVelocity_MAX) {
                    playerVY = -walkVelocity_MAX;
                }
                //---------reduce slide--------
                if(playerVX > 0) {
                    playerVX -= walkAccelerate;
                    if(playerVX < 0) playerVX = 0;
                } else if(playerVX < 0) {
                    playerVX += walkAccelerate;
                    if(playerVX > 0) playerVX = 0;
                }
                break;
            case 3:
                if(playerVX < (int)((walkVelocity_MAX - walkAccelerate) * 0.71)) {
                    playerVX += (int)(walkAccelerate * 0.71);
                } else if(playerVX < (int)(walkVelocity_MAX * 0.71)) {
                    playerVX = (int)(walkVelocity_MAX * 0.71);
                }
                if(playerVY > -(int)((walkVelocity_MAX - walkAccelerate) * 0.71)) {
                    playerVY -= (int)(walkAccelerate * 0.71);
                } else if(playerVY > -(int)(walkVelocity_MAX * 0.71)) {
                    playerVY = -(int)(walkVelocity_MAX * 0.71);
                }
                break;
            case 4:
                if(playerVX > -(walkVelocity_MAX - walkAccelerate)) {
                    playerVX -= walkAccelerate;
                } else if(playerVX > -walkVelocity_MAX) {
                    playerVX = -walkVelocity_MAX;
                }
                //---------reduce slide--------
                if(playerVY > 0) {
                    playerVY -= walkAccelerate;
                    if(playerVY < 0) playerVY = 0;
                } else if(playerVY < 0) {
                    playerVY += walkAccelerate;
                    if(playerVY > 0) playerVY = 0;
                }
                break;
            case 6:
                if(playerVX < (walkVelocity_MAX - walkAccelerate))  {
                    playerVX += walkAccelerate;
                } else if(playerVX < walkVelocity_MAX) {
                    playerVX = walkVelocity_MAX;
                }
                //---------reduce slide--------
                if(playerVY > 0) {
                    playerVY -= walkAccelerate;
                    if(playerVY < 0) playerVY = 0;
                } else if(playerVY < 0) {
                    playerVY += walkAccelerate;
                    if(playerVY > 0) playerVY = 0;
                }
                break;
            case 7:
                if(playerVX > -(int)((walkVelocity_MAX - walkAccelerate) * 0.71)) {
                    playerVX -= (int)(walkAccelerate * 0.71);
                } else if(playerVX > -(int)(walkVelocity_MAX * 0.71)) {
                    playerVX = -(int)(walkVelocity_MAX * 0.71);
                }
                if(playerVY < (int)((walkVelocity_MAX - walkAccelerate) * 0.71)) {
                    playerVY += (int)(walkAccelerate * 0.71);
                } else if(playerVY < (int)(walkVelocity_MAX * 0.71)) {
                    playerVY = (int)(walkVelocity_MAX * 0.71);
                }
                break;
            case 8:
                if(playerVY < (walkVelocity_MAX - walkAccelerate)) {
                    playerVY += walkAccelerate;
                } else if(playerVY < walkVelocity_MAX) {
                    playerVY = walkVelocity_MAX;
                }
                //---------reduce slide--------
                if(playerVX > 0) {
                    playerVX -= walkAccelerate;
                    if(playerVX < 0) playerVX = 0;
                } else if(playerVX < 0) {
                    playerVX += walkAccelerate;
                    if(playerVX > 0) playerVX = 0;
                }
                break;
            case 9:
                if(playerVX < (int)((walkVelocity_MAX - walkAccelerate) * 0.71)) {
                    playerVX += (int)(walkAccelerate * 0.71);
                } else if(playerVX < (int)(walkVelocity_MAX * 0.71)) {
                    playerVX = (int)(walkVelocity_MAX * 0.71);
                }
                if(playerVY < (int)((walkVelocity_MAX - walkAccelerate) * 0.71)) {
                    playerVY += (int)(walkAccelerate * 0.71);
                } else if(playerVY < (int)(walkVelocity_MAX * 0.71)) {
                    playerVY = (int)(walkVelocity_MAX * 0.71);
                }
                break;
            default: break;
        }

        //Quick Move
        if(Input.GetKey(KeyCode.Z) && key_Z == false) {
            playerVX *= 4;
            playerVY *= 4;
        }
        
        GetComponent<Rigidbody2D>().velocity = new Vector2(playerVX, playerVY);
    }

    //============================================
    //              SkillController
    //============================================
    //Make a control to player's skill action.
    void SkillController() {
        if(Input.GetKey(KeyCode.C) && key_C == false) {
            GameObject fireBullet;
            fireBullet = Instantiate(Resources.Load<GameObject>("GameObjects/MagicalBullets/FireMagicalBullet"), transform.position, transform.rotation);
            fireBullet.GetComponent<MagicalBullet>().direction = direction;
            fireBullet.GetComponent<MagicalBullet>().magicType = 0;
        }
    }

    //============================================
    //               WalkAnimation
    //============================================
    //Display character's walking animaiton.
    void WalkAnimation() {
        time += (int)Math.Pow(Math.Sqrt((double)Math.Pow(GetComponent<Rigidbody2D>().velocity.x, 2) + (double)Math.Pow(GetComponent<Rigidbody2D>().velocity.y, 2)),0.75);
        if(time >= 456) {
            time -= 456;
            nowForm += 1;
            if(nowForm >= 4) {
                nowForm = 0;
            }
        }

        if(!(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))) {
            time = 9;
            nowForm = 0;
        }
        if(direction <= 5)
            GetComponent<SpriteRenderer>().sprite = walkingAnimation[(direction - 1) * 4 + nowForm];
        else GetComponent<SpriteRenderer>().sprite = walkingAnimation[(direction - 2) * 4 + nowForm];
        /*
        switch(direction) {
            case 1:
                GetComponent<SpriteRenderer>().sprite = walkingAnimation[(direction - 1) + nowForm];
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = walkingAnimation[3 + nowForm];
                break;
            case 3:
                GetComponent<SpriteRenderer>().sprite = walkingAnimation[6 + nowForm];
                break;
            case 4:
                GetComponent<SpriteRenderer>().sprite = walkingAnimation[4 + nowForm];
                break;
            case 5:
                GetComponent<SpriteRenderer>().sprite = walkingAnimation[8 + nowForm];
                break;
            case 6:
                GetComponent<SpriteRenderer>().sprite = walkingAnimation[8 + nowForm];
                break;
            case 7:
                GetComponent<SpriteRenderer>().sprite = walkingAnimation[12 + nowForm];
                break;
            case 8:
                GetComponent<SpriteRenderer>().sprite = walkingAnimation[12 + nowForm];
                break;
            default: break;
        }
        */
    }

    //============================================
    //                ZAxiAsYAxi
    //============================================
    //Fixed the position to match pixel.
    void ZAxiAsYAxi() {
        GetComponent<Transform>().position = new Vector3(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y, GetComponent<Transform>().position.y);
    }

    //============================================
    //              StaticPosition
    //============================================
    //Fixed the position to match pixel.
    /*
    void StaticPosition() {
        GetComponent<Rigidbody2D>().velocity = new Vector2((int)GetComponent<Rigidbody2D>().velocity.x, (int)GetComponent<Rigidbody2D>().velocity.y);
        if (GetComponent<Rigidbody2D>().velocity.x == 0 && GetComponent<Rigidbody2D>().velocity.y == 0) {
            GetComponent<Transform>().position = new Vector2((int)GetComponent<Transform>().position.x, (int)GetComponent<Transform>().position.y);
        }
    }
    */

    //============================================
    //                  SyncKey
    //============================================
    //Sync the aviliable value to the real key state.
    void SyncKey() {
        key_Z = Input.GetKey(KeyCode.Z);
        key_C = Input.GetKey(KeyCode.C);
    }



}
