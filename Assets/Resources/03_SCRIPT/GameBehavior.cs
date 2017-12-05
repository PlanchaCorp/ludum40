using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBehavior : MonoBehaviour
{

    public int maxSpawnbound = 10;
    public int spawnrate = 2;
    public int toyCount = 0;

    public int spawn = 10;
    public float speed = 2.1f;
    // Use this for initialization
    public List<Toy> smallToyPool;
    public List<Toy> mediumToyPool;
    public List<Toy> bigToyPool;
    public List<Toy> undesirableToyPool;

    public List<Toy>[] pools;
    void Start()
    {
        smallToyPool = new List<Toy>
        {
           new Toy("Spinning top","Make it spin fast !", 1, "toupis",false),
           new Toy("Bear","So cute !", 1, "ours",false),
        };
        mediumToyPool = new List<Toy>
        {

           new Toy("Console","Let's have a good time", 2, "console",false),
           new Toy("Dovahkiin","Fus Roh Dah !", 2, "dovahkin",false),
           new Toy("Affro duck", "Stylish !", 2, "toy_momysduck",false)
        };
        bigToyPool = new List<Toy>
        {
           new Toy("Wooden horse","Wanna go for a ride ?", 3, "toy_horse",false),
           new Toy("Depressed elf","Send me far away from here", 3, "depressed_elf",false)
        };
        undesirableToyPool = new List<Toy>
        {
           new Toy("Croque monsieur","It's already bitten", 1, "toy_croq",true),
           new Toy("Wooden horse","Two legs are broken", 3, "toy_horse_broken",true),
           new Toy("Bear","I think something is missing", 1, "ours_broken",true),
           new Toy("Spinning top","It won't be spinning anymore", 1, "toupis_broken",true),
           new Toy("Console","This console is defective", 2, "console_broken",true),
           new Toy("Dovahkiin","Is it a cow?", 2, "dovahkin_broken",true)
        };
        pools = new List<Toy>[] { smallToyPool, mediumToyPool, bigToyPool, undesirableToyPool };

      
    }


    private int nextUpdate = 2;
    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextUpdate)
        {
            Debug.Log("update");
            CreateToy();

            nextUpdate = Mathf.FloorToInt(Time.time) + spawn;

            toyCount++;
            if (toyCount == 2 || toyCount == 4 || toyCount == 6 || toyCount == 10 || toyCount == 15 || toyCount == 20 || toyCount == 30)
            {
                increaseDificulty();
            }
        }

    }

    public void increaseDificulty()
    {
        spawn -= 1;
        ToyBehaviour.speed += 0.5f;
    }

    private int GetToyOnBelt()
    {
        GameObject belt = gameObject.transform.Find("belt").gameObject;
        return belt.transform.childCount;
    }
    private int[] generateSequence()
    {
        Random rng = new Random();
        int[] sequence = new int[] { 0, 0, 1, 1, 2, 2, 3 };
        int n = sequence.Length;
        while (n > 1)
        {
            n--;
            int k = Mathf.FloorToInt(Random.value * n);
            int val = sequence[k];
            sequence[k] = sequence[n];
            sequence[n] = val;

        }
        return sequence;
    }

    int[] sequence = new int[4];
    int index = 0;
    void CreateToy()
    {
        if (index > 6)
        {
            index = 0;
        }
        
        if (index == 0)
        {
            sequence = generateSequence();
        }

        if (GetToyOnBelt() <= maxSpawnbound)
        {

            List<Toy> pool = pools[sequence[index]];
            Debug.Log(sequence[index]);
            int i = (int)Mathf.Floor(Random.Range(0, pool.Count));
           
            GameObject belt = gameObject.transform.Find("belt").gameObject;
            GameObject toyObject = GameObject.Instantiate(Resources.Load("10_PREFABS/toyGeneric"), belt.transform) as GameObject;
            Toy toy = pool[i];
            toyObject.GetComponent<ToyBehaviour>().toy = toy;
            toyObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("09_TEXTURE/" + toy.spriteName);
            toyObject.GetComponent<SpriteRenderer>().transform.localScale. Set(0.7F, 0.7F, 1);
            index++;
            
        } else {
            SceneManager.LoadScene("MainGameOver",LoadSceneMode.Single);
        }
    }



}
