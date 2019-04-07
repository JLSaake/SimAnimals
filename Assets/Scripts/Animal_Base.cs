using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal_Base : MonoBehaviour
{

    public int id;
    public string animalName;

    // Decay variables
    [Tooltip("Percentage of frame checks where the animal attributes decay")]
    [Range(0, 100)]
    public float rateOfRandomDecay = 1;
    [Space]
    [Tooltip("If decay, odds that hunger is the decay variable chosen")]
    public int oddsOfHungerDecay = 33;
    [Tooltip("If decay, odds that thirst is the decay variable chosen")]
    public int oddsOfThirstDecay = 33;
    [Tooltip("If decay, odds that energy is the decay variable chosen")]
    public int oddsOfEnergyDecay = 33;


    #region Attributes
    //TODO: Move to scriptable objects later down the line

    private bool isAlive = true;

    private int hunger;
    private int hungerMax = 100;
    private int hungerMin = 0;
    private int thirst;
    private int thirstMax = 100;
    private int thirstMin = 0;
    private int energy;
    private int energyMax = 100;
    private int energyMin = 0;

    #endregion


    #region Helper Variables
    private int oddsSum;
    private float decayCheckNum;
    private float decayChooseNum;
    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        hunger = Random.Range((hungerMax / 2), hungerMax);
        thirst = Random.Range((thirstMax / 2), thirstMax);
        energy = Random.Range((energyMax / 2), energyMax);

        Debug.Log("Animal " + animalName + " spawned with h,t,e(" + hunger + ", " + thirst + ", " + energy + ") {" + id + "}");

        oddsSum = oddsOfHungerDecay + oddsOfThirstDecay + oddsOfEnergyDecay;
        oddsOfThirstDecay += oddsOfHungerDecay;
        oddsOfEnergyDecay += oddsOfThirstDecay;
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
                Eat(1);
                Debug.Log("Animal " + animalName + " has age values h,t,e(" + hunger + ", " + thirst + ", " + energy + ") {" + id + "}");
            } else
            if (Input.GetKeyDown(KeyCode.D))
            {
                Drink(1);
                Debug.Log("Animal " + animalName + " has drank values h,t,e(" + hunger + ", " + thirst + ", " + energy + ") {" + id + "}");
            } else
            if (Input.GetKeyDown(KeyCode.S))
            {
                Sleep(1);
                Debug.Log("Animal " + animalName + " has slept values h,t,e(" + hunger + ", " + thirst + ", " + energy + ") {" + id + "}");
            }
            DeathCheck();
        }

    }

    private void FixedUpdate()
    {
        if (isAlive)
        {
            DecayCheck();
        }
    }

    #region Basic Functions

    // Function for causing animal attributes to decay
    // TODO: run more checks to make things more difficult to decay
    private void DecayCheck()
    {
        decayCheckNum = Random.Range(0, 100);
        if (decayCheckNum < rateOfRandomDecay)
        {
            decayChooseNum = Random.Range(0, oddsSum);
            if (decayChooseNum <= oddsOfHungerDecay)
            {
                // Decrease Hunger
                hunger -= Random.Range(1, 4);
                Debug.Log("Animal " + animalName + " is becoming hungry. {" + id + "}");
            } else
            if (decayChooseNum <= oddsOfThirstDecay)
            {
                // Decrease Thirst
                thirst -= Random.Range(1, 3);
                Debug.Log("Animal " + animalName + " is becoming thirsty. {" + id + "}");
            } else
            if (decayChooseNum <= oddsOfEnergyDecay)
            {
                // Decrease Energy
                energy -= Random.Range(1, 2);
                Debug.Log("Animal " + animalName + " is losing energy. {" + id + "}");
            }
            else
            {
                // If it gets this far, something is wrong with the random decay chooser
                Debug.LogError("Odds chooser is out of range {" + id + "} at " + Time.fixedTime);  
            }
            Debug.Log("Animal " + animalName + " now has attributes h,t,e(" + hunger + ", " + thirst + ", " + energy + ") {" + id + "}");
        }

    }

    // Eat: Restores amount of hunger
    //TODO: add requirement that input matches edible for this animal
    //      float should change to object
    public void Eat(int food)
    {
        hunger += food;
        if (hunger > hungerMax)
        {
            hunger = hungerMax;
        }
    }

    public void Drink(int water)
    {
        thirst += water;
        if (thirst > thirstMax)
        {
            thirst = thirstMax;
        }
    }

    public void Sleep(int time)
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
        hunger -= Random.Range(1, 4);
        thirst -= Random.Range(1, 3);
        energy -= Random.Range(1, 2);
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
    public void SetHunger(int h)
    {
        this.hunger = h;
    }

    public int GetHunger()
    {
        return this.hunger;
    }

    public void SetThirst(int t)
    {
        this.hunger = t;
    }

    public int GetThirst()
    {
        return this.thirst;
    }

    public void SetEnergy(int e)
    {
        this.energy = e;
    }

    public int GetEnergy()
    {
        return this.energy;
    }

    public bool GetIsAlive()
    {
        return this.isAlive;
    }
    #endregion



}
