using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    private int hp;
    private int attackTimer;
    private int attack;
    private bool damaged;
    private Transform bomb;
    public Trap trap1, trap2;
    public Hud hud;

    public int maxHp;
    public int interval;
    public int maxAttack;
    public float rate;

    protected Animator animator;

    public bool ikActive = false;
    public Transform rightHandObj = null;
    public float rightHandWeightPosition = 1;
    public float rightHandWeightRotation = 1;

    // Use this for initialization
    void Start () {
        hp = maxHp;
        attackTimer = 0;
        attack = 0;
        damaged = false;
        bomb = transform.GetChild(0);
        animator = GetComponent<Animator>();
        ikActive = false;
	}
	
	// Update is called once per frame
	void Update () {
        attackTimer++;
        if (attackTimer > interval)
        {
            Attack();
            attackTimer = 0;
            damaged = false;
            ikActive = false;
        } 
        else if(!ikActive&& attackTimer > interval * 0.7)
        {
            ikActive = true;
        }
	}

    private void Attack()
    {
        switch (attack)
        {
            case 0://周囲
                Debug.Log("attack: 0");
                bomb.localPosition = new Vector3(0, 5, 0);
                bomb.gameObject.SetActive(true);
                break;
            case 1://壁際
                Debug.Log("attack: 1");
                for(int i = 0; i < 4; i++)
                { transform.parent.GetChild(i).GetComponent<Wall>().timer = 100; }
                break;
            case 2://地面
                Debug.Log("attack: 2");
                transform.parent.GetComponent<Board>().timer = 100;
                break;
            case 3://trap
                var y1 = Random.Range(-0.25f, 0.25f);
                trap1.transform.localPosition = new Vector3(0.25f, 0.25f, y1);
                trap1.gameObject.SetActive(true);
                trap1.timer = 400;
                var y2 = Random.Range(-0.25f, 0.25f);
                trap2.transform.localPosition = new Vector3(-0.25f, 0.25f, y2);
                trap2.gameObject.SetActive(true);
                trap2.timer = 400;
                break;
            default:
                break;
        }
        attack = (attack + 1) % maxAttack;
    }

    public int GetHp()
    {
        return hp;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!damaged && collision.gameObject.tag == "Player")
        {
            hp -= (int) (rate*Mathf.Log(1f+collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude));
            damaged = true;
            Debug.Log("left: "+hp);
            if (hp <= 0)
            {
                hp = 0;
                hud.GameOver();
                gameObject.SetActive(false);
            } 
        }
    }

    void OnAnimatorIK(int layerIndex)
    {
        if (animator)
        {
            if (ikActive)
            {
                
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandWeightPosition);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, rightHandWeightRotation);                

                if (rightHandObj != null)
                {
                    animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
                }
            }
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 0);

                animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 0);

                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);

                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);

                animator.SetLookAtWeight(0.0f);
            }
        }
    }
}
