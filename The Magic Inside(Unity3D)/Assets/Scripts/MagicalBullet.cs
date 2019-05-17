using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalBullet : MonoBehaviour {
    public int direction;
    public float direction_angles;
    //Magica type: 0~49 only for Hilda. 50~99 for friendly party. 100+ for other.
    public int magicType;
    public int magicLevel;

    private Sprite[] groupSprite;
    private int animationTime;
    private int animationForm;

    //################################################
    //                     Start
    //################################################
    // Start is called before the first frame update
    void Start() {
        animationTime = 0;
        animationForm = 0;
        switch(magicType) {
            case 0: light1_start(); break;
            case 100: magicGas_blue_Attack_start(); break;
            default: break;
        }
    }

    //################################################
    //                     Updata
    //################################################
    // Update is called once per frame
    void Update() {
        ZAxiAsYAxi();
    }

    //################################################
    //                 OnTriggerEnter2D
    //################################################
    // Awake when teigger enter.
    void OnTriggerEnter2D(Collider2D theEvent) {
        if(theEvent.tag == "Wall") {
            if(magicType >= 0) {
                Destroy(this.gameObject);
            }
        }
        if(theEvent.tag == "Enemy") {
            if(magicType >= 0 && magicType < 99) {
                theEvent.GetComponent<EnemyAI>().beHit(10, 0, transform.position.x, transform.position.y);
                Destroy(this.gameObject);
            }
        }
        if(theEvent.tag == "Player") {
            if(magicType >= 100) {
                Destroy(this.gameObject);
            }
        }
    }

    //################################################
    //                   HildaSkill
    //################################################
    //============================================
    //                  light1
    //============================================
    void light1_start() {
        goStraightBullet(300, EightDirectionToAngles(direction));
        groupSprite = Resources.LoadAll<Sprite>("MagicalBullet/FireMagicalBullet1");
        GetComponent<SpriteRenderer>().sprite = groupSprite[0];
    }

    //################################################
    //                   EnemySkill
    //################################################
    //============================================
    //             magicGas_blue_Attack
    //============================================
    void magicGas_blue_Attack_start() {
        goStraightBullet(300, direction_angles);
        groupSprite = Resources.LoadAll<Sprite>("MagicalBullet/FireMagicalBullet1");
        GetComponent<SpriteRenderer>().sprite = groupSprite[0];
    }

    //################################################
    //                CommonFunction
    //################################################
    //============================================
    //               walkAnimation
    //============================================
    //Display the animaiton while walking.
    void walkAnimaiton() {
        animationTime += 1;
        if (animationTime >= 15) {
            animationTime = 0;
            animationForm += 1;
            if (animationForm >= 4) {
                animationForm = 0;
                GetComponent<SpriteRenderer>().sprite = groupSprite[animationForm];
            }
        }
    }


    //============================================
    //              goStraightBullet
    //============================================
    //The magic bullet will go straight.
    void goStraightBullet(float bulletVelocity, float _angles) {
        transform.eulerAngles = new Vector3(0, 0, _angles);
        float velocity_X = bulletVelocity * (float)Math.Cos((_angles - 90) / 57.3);
        float velocity_Y = bulletVelocity * (float)Math.Sin((_angles - 90) / 57.3);
        GetComponent<Rigidbody2D>().velocity = new Vector2(velocity_X, velocity_Y);
    }

    /*
    //============================================
    //            getEightDirection
    //============================================
    //get the direction in eight direction disign depending to rotation.
    int getEightDirection(float _angles) {
        if(0 <= _angles && _angles < 22.5 && 337.5 <= _angles) {
            return 2;
        }
        if(22.5 <= _angles && _angles < 67.5) {
            return 3;
        }
        if(67.5 <= _angles && _angles < 112.5) {
            return 6;
        }
        if(112.5 <= _angles && _angles < 157.5) {
            return 9;
        }
        if(157.5 <= _angles && _angles < 202.5) {
            return 8;
        }
        if(202.5 <= _angles && _angles < 247.5) {
            return 7;
        }
        if(247.5 <= _angles && _angles < 292.5) {
            return 4;
        }
        if(292.5 <= _angles && _angles < 337.5) {
            return 1;
        }
        return 5;
    }
    */

    //============================================
    //          EightDirectionToAngles
    //============================================
    //get the direction in eight direction disign depending to rotation.
    int EightDirectionToAngles(float _direction) {
        switch(_direction) {
            case 1: return 315; break;
            case 2: return 0; break;
            case 3: return 45;  break;
            case 4: return 270; break;
            case 6: return 90; break;
            case 7: return 225; break;
            case 8: return 180; break;
            case 9: return 135; break;
            default: break;
        }
        return 0;
    }

    //============================================
    //                ZAxiAsYAxi
    //============================================
    //Fixed the position to match pixel.
    void ZAxiAsYAxi() {
        GetComponent<Transform>().position = new Vector3(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y, GetComponent<Transform>().position.y);
    }
}
