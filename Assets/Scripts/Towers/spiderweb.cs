using UnityEngine;

public class spiderweb : MonoBehaviour
{
#pragma warning disable 0414

    [SerializeField] StatusEffects data;    



    public float speed = 35f;
    private float lifeTime = 2f;
    private float pierceAmount = 3f; 

    public bool isActive = false;

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



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            var effect = other.GetComponent<Effectable>();
            if (effect != null)
            { 
                if (pierceAmount > 0)
                {
                    effect.applyEffect(data);
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
