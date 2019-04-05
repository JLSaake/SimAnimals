using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal_Base : MonoBehaviour
{

    public int id;
    public string animalName;



    #region Attributes
    //TODO: Move to scriptable objects later down the line

    private bool isAlive = true;

    private float hunger;
    private float hungerMax = 10;
    private float hungerMin = 0;
    private float thirst;
    private float thirstMax = 10;
    private float thirstMin = 0;
    private float energy;
    private float energyMax = 10;
    private float energyMin = 0;

    #endregion


    // Start is called before the first frame update
    private void Start()
    {
        hunger = Random.Range((hungerMax / 2), hungerMax);
        thirst = Random.Range((thirstMax / 2), thirstMax);
        energy = Random.Range((energyMax / 2), energyMax);

        Debug.Log("Animal " + animalName + " spawned with h,t,e(" + hunger + ", " + thirst + ", " + energy + ") {" + id + "}");
    }

    // Update is called once per frame
    private void Update()
    {
        if (isAlive)
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                _RandomDecrease();
            } else
            if (Input.GetKeyDown(KeyCode.E))
            {
                Eat(1f);
                Debug.Log("Animal " + animalName + " has age values h,t,e(" + hunger + ", " + thirst + ", " + energy + ") {" + id + "}");
            } else
            if (Input.GetKeyDown(KeyCode.D))
            {
                Drink(1f);
                Debug.Log("Animal " + animalName + " has drank values h,t,e(" + hunger + ", " + thirst + ", " + energy + ") {" + id + "}");
            } else
            if (Input.GetKeyDown(KeyCode.S))
            {
                Sleep(1f);
                Debug.Log("Animal " + animalName + " has slept values h,t,e(" + hunger + ", " + thirst + ", " + energy + ") {" + id + "}");
            }
            DeathCheck();
        }

    }

    private void FixedUpdate()
    {
        if (isAlive && (Time.fixedTime % 5 == 0))
        {
            Debug.Log(Time.fixedTime);
        }
    }

    #region Basic Functions

    // Eat: Restores amount of hunger
    //TODO: add requirement that input matches edible for this animal
    //      float should change to object
    public void Eat(float food)
    {
        hunger += food;
        if (hunger > hungerMax)
        {
            hunger = hungerMax;
        }
    }

    public void Drink(float water)
    {
        thirst += water;
        if (thirst > thirstMax)
        {
            thirst = thirstMax;
        }
    }

    public void Sleep(float time)
    {
        energy += time;
        if (energy > energyMax)
        {
            energy = energyMax;
        }
    }


    // Temporary funciton for updating animal values
    private void _RandomDecrease()
    {
        hunger -= Random.Range(1f, 3f);
        thirst -= Random.Range(.5f, 2f);
        energy -= Random.Range(.25f, 1f);
        Debug.Log("Animal " + animalName + " has decreased values h,t,e(" + hunger + ", " + thirst + ", " + energy + ") {" + id + "}");

    }

    // Checks to see if animal meets basic attribute minimums
    private void DeathCheck()
    {
        if (hunger <= hungerMin)
        {
            isAlive = false;
            Debug.Log("Animal " + animalName + " has died of hunger. {" + id + "}");
        } else
        if (thirst <= thirstMin)
        {
            isAlive = false;
            Debug.Log("Animal " + animalName + " has died of thirst. {" + id + "}");
        } else
        if (energy <= energyMin)
        {
            isAlive = false;
            Debug.Log("Animal " + animalName + " has died of exhaustion. {" + id + "}");
        }
    }

    #endregion

    #region Get/Set
    // Attributes (may need to be adjusted / removed when attributes move)
    public void SetHunger(float h)
    {
        this.hunger = h;
    }

    public float GetHunger()
    {
        return this.hunger;
    }

    public void SetThirst(float t)
    {
        this.hunger = t;
    }

    public float GetThirst()
    {
        return this.thirst;
    }

    public void SetEnergy(float e)
    {
        this.energy = e;
    }

    public float GetEnergy()
    {
        return this.energy;
    }

    public bool GetIsAlive()
    {
        return this.isAlive;
    }
    #endregion



}
