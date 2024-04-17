using UnityEngine;

public class spiderweb : MonoBehaviour
{
#pragma warning disable 0414

    [SerializeField] public StatusEffects data;    



    public float speed = 35f;
    private float lifeTime = 2f;
    private float pierceAmount = 3f;
    public float damage = 0f;

    public bool isActive = false;

    //public AudioSource audioSource;
    //public AudioClip audioClip;

#pragma warning restore 0414

    private void Start()
    {
        isActive = true;
        //transform.LookAt(transform.position);
        transform.Rotate(Vector3.right, 90f);
    }
    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (lifeTime > 0)
            {
                lifeTime -= Time.deltaTime;
                transform.position += transform.up * speed * Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }

        }

    }


    //When encountering an enemy, apply the effect and reduce health
    private void OnTriggerEnter(Collider other)
    {
        //check for enemy tag
        if (other.gameObject.tag == "Enemy")
        {
            var effect = other.GetComponent<Effectable>();
            if (effect != null)
            {
                //play sound
                //audioSource.PlayOneShot(audioClip);
                //Deal with pierce amount
                if (pierceAmount > 0)
                {
                    //slowness
                    effect.applyEffect(data);
                    //damage
                    other.GetComponent<BaseEnemyLogic>().reduceHealth(damage);
                    //reduce peirce amount
                    pierceAmount -= 1;
                }
                else if (pierceAmount == 0)
                {
                    Destroy(gameObject);
                }
            }

            
        }

        

        
    }


}
