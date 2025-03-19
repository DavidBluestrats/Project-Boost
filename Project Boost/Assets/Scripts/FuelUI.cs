using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FuelUI : MonoBehaviour
{
    // Start is called before the first frame update
    // Update is called once per frame
    void Start()
    {
        GetComponent<TMP_Text>().SetText("Fuel: " + FindObjectOfType<Movement>().GetFuel().ToString());
    }
    public void updateFuel(float fuelToShow)
    {
        GetComponent<TMP_Text>().SetText("Fuel: "+ fuelToShow);
    }
}
