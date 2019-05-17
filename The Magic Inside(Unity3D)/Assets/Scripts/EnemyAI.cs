using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class EnemyAI : MonoBehaviour
{
    public int enemyType;
    public GameObject target;

    public Sprite[] DamageNumbers;

    private int direction;

    private int HP;
    private int ACT;

    //################################################
    //                     Start
    //################################################
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
        DamageNumbers = Resources.LoadAll<Sprite>("System/Numbers");
        ACT = 0;
        switch(enemyType){
            case 0: MagicGas_RBG_start(); break;
            default: break;
        }
    }

    //################################################
    //                     Updata
    //################################################
    // Update is called once per frame
    void Update()
    {
        switch(enemyType) {
            case 0: MagicGas_blue_updata(); break;
            default: break;
        }

        ZAxiAsYAxi();
    }

    //################################################
    //               EnemyInitialization
    //################################################
    //=======================================
    //          MagicGas_blue_start
    //=======================================
    void MagicGas_RBG_start() {
        HP = 60;
    }

    //################################################
    //                   EnemyUpdata
    //################################################
    //=======================================
    //            MagicGas_blue_updata
    //=======================================
    void MagicGas_blue_updata() {
        ACT += 1;
        if(ACT >= 60) {
            if (ACT == 60) {
                GameObject fireBullet;
                fireBullet = Instantiate(Resources.Load<GameObject>("GameObjects/MagicalBullets/FireMagicalBullet"), transform.position, transform.rotation);
                fireBullet.GetComponent<MagicalBullet>().direction_angles = 57.3f*(float)Math.Atan2(target.transform.position.y-transform.position.y,target.transform.position.x-transform.position.x)+90;
                fireBullet.GetComponent<MagicalBullet>().magicType = 100;
                ACT = 0;
            }
        }
        closeToPlayer(10);

        if (HP <= 0) {
            downAnimation(1);
        }
    }

    //################################################
    //                CommonFunction
    //################################################
    //============================================
    //                closeToPlayer
    //============================================
    //Get close to player with some velocity.
    void closeToPlayer(float _velocity) {
        //speed = 29 is colse to the speed of player with 200;
        float distance = (float)Math.Sqrt((double)Math.Pow(target.transform.position.x - transform.position.x, 2) + (double)Math.Pow(target.transform.position.y - transform.position.y, 2));
        float velocity_X = _velocity * (target.transform.position.x - transform.position.x) / distance;
        float velocity_Y = _velocity * (target.transform.position.y - transform.position.y) / distance;
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x + velocity_X, GetComponent<Rigidbody2D>().velocity.y + velocity_Y);
    }

    //============================================
    //                   beHit
    //============================================
    //calculate the damages when be hit, should be call by outside.
    public void beHit(int damages, int attackType, float hitX, float hitY) {

        Random rand = new Random();
        damages = (int)(damages * (0.9 + rand.NextDouble() * 0.25));
        HP -= damages;

        //Display damage number.
        int goLeftX = 0;
        int _damage = damages;
        while (_damage >= 10) {
            goLeftX += 7;
            _damage /= 10;
        }

        int goDownY = rand.Next(-10, 10);
        goLeftX += rand.Next(-10, 10);

        while(damages > 0) {
            GameObject displayNumber;
            displayNumber = Instantiate(Resources.Load<GameObject>("GameObjects/SystemNumber"));
            displayNumber.transform.position = new Vector3(hitX + goLeftX, hitY + goDownY, 9999);
            goLeftX -= 14;
            displayNumber.GetComponent<DisplayDamage>().number = DamageNumbers[damages % 10];
            damages /= 10;
        }
    }

    //============================================
    //               downAnimation
    //============================================
    //calculate the damages when be hit.
    void downAnimation(int _index) {
        Destroy(this.gameObject);
    }



    //============================================
    //                ZAxiAsYAxi
    //============================================
    //Fixed the position to match pixel.
    void ZAxiAsYAxi() {
        GetComponent<Transform>().position = new Vector3(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y, GetComponent<Transform>().position.y);
    }
}
