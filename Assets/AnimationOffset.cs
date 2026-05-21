using UnityEngine;

public class AnimationOffset : MonoBehaviour
{
    public Animator anim;
    float timer;
    float limit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        limit = Random.Range(0f,3f);

        anim.speed = Random.Range(0.8f,1.2f);
    }

    // Update is called once per frame
    void Update()
    {
        //if(timer >= limit)
        //{
        //    anim.SetBool("StartNeutral",true);
        //    print("startneutral");
        //}
            
    }
}
