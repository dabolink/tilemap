using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[RequireComponent(typeof(InventoryManager))]
public class Generator : MonoBehaviour
{
    [SerializeField]
    public Item item;
    public int generationAmount; // amount of item to be generated
    public float timeTilGeneration = 1000; // in milliseconds
    public float cooldown; // in milliseconds

    private InventoryManager inventoryManager;

    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
        if(cooldown <= 0)
        {
            bool completed = inventoryManager.GetInventory(0).AddItemStack(new ItemStack(generationAmount, item));
            cooldown = timeTilGeneration;
            //TODO allow generation to be restarted if inventory is emptied
        }
    }
}
