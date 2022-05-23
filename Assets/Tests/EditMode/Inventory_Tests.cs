using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using ORCAS;

public class Inventory_Tests
{
    private Resource _moneyResource;
    private Resource _electricityResource;
    
    [OneTimeSetUp]
    public void SetupResources()
    {
        _moneyResource = ScriptableObject.CreateInstance<Resource>();
        _moneyResource.name = "Money";
        
        _electricityResource = ScriptableObject.CreateInstance<Resource>();
        _electricityResource.name = "Electricity";
    }

    [OneTimeTearDown]
    public void TearDownResources()
    {
        Object.DestroyImmediate(_moneyResource);
        Object.DestroyImmediate(_electricityResource);
    }

    [Test]
    [TestCase(0f, 0f), TestCase(0f, 567f), TestCase(420f, 0f), TestCase(69f, 10e10f)]
    public void Inventory_Add_Positive_Test(float initialAmount, float amountToAdd)
    {
        var inventory = new Inventory(new List<Inventory.ResourceAmount>()
        {
            new Inventory.ResourceAmount(_moneyResource, initialAmount)
        });

        inventory.Add(_moneyResource, amountToAdd);

        Assert.AreEqual(initialAmount + amountToAdd, inventory.GetAmount(_moneyResource));
    }

    [Test]
    [TestCase(0f, -567f), TestCase(69f, -10e10f)]
    public void Inventory_Add_Negative_Test(float initialAmount, float amountToAdd)
    {
        var inventory = new Inventory(new List<Inventory.ResourceAmount>()
        {
            new Inventory.ResourceAmount(_moneyResource, initialAmount)
        });

        Assert.Throws<System.ArgumentOutOfRangeException>(()=>inventory.Add(_moneyResource, amountToAdd));
        Assert.AreEqual(initialAmount, inventory.GetAmount(_moneyResource));
    }

    [Test]
    [TestCase(0f, -567f), TestCase(69f, -10e10f), TestCase(10f, -1f)]
    public void Inventory_Remove_Negative_Test(float initialAmount, float amountToAdd)
    {
        var inventory = new Inventory(new List<Inventory.ResourceAmount>()
        {
            new Inventory.ResourceAmount(_moneyResource, initialAmount)
        });

        Assert.Throws<System.ArgumentOutOfRangeException>(() => inventory.TryRemove(_moneyResource, amountToAdd));
        Assert.AreEqual(initialAmount, inventory.GetAmount(_moneyResource));
    }

    [Test]
    [TestCase(420f, 69f), TestCase(10e10f, 0f), TestCase(10f, 10f)]
    public void Inventory_Remove_Enough_Test(float initialAmount, float amountToRemove)
    {
        var inventory = new Inventory(new List<Inventory.ResourceAmount>()
        {
            new Inventory.ResourceAmount(_moneyResource, initialAmount)
        });

        bool removedSuccessfully = inventory.TryRemove(_moneyResource, amountToRemove);
        
        Assert.IsTrue(removedSuccessfully);
        Assert.AreEqual(initialAmount - amountToRemove, inventory.GetAmount(_moneyResource));
    }

    [Test]
    [TestCase(0f, 1f), TestCase(5f, 5.1f)]
    public void Inventory_Remove_Not_Enough_Test(float initialAmount, float amountToRemove)
    {
        var inventory = new Inventory(new List<Inventory.ResourceAmount>()
        {
            new Inventory.ResourceAmount(_moneyResource, initialAmount)
        });

        bool removedSuccessfully = inventory.TryRemove(_moneyResource, amountToRemove);

        Assert.IsFalse(removedSuccessfully);
        Assert.AreEqual(initialAmount, inventory.GetAmount(_moneyResource));
    }

    [Test]
    [TestCase(0f), TestCase(6f)]
    public void Inventory_Add_Does_Not_Effect_Other_Resources_Test(float amountToAdd)
    {
        const float initialElectricityAmount = 10f;
        
        var inventory = new Inventory(new List<Inventory.ResourceAmount>()
        {
            new Inventory.ResourceAmount(_moneyResource, 0f),
            new Inventory.ResourceAmount(_electricityResource, initialElectricityAmount)
        });

        inventory.Add(_moneyResource, amountToAdd);

        Assert.AreEqual(initialElectricityAmount, inventory.GetAmount(_electricityResource));
    }

    [Test]
    [TestCase(0f, 6f), TestCase(10f, 10f), TestCase(5f, 10f)]
    public void Inventory_Remove_Does_Not_Effect_Other_Resources_Test(float initialAmount, float amountToRemove)
    {
        const float initialElectricityAmount = 10f;

        var inventory = new Inventory(new List<Inventory.ResourceAmount>()
        {
            new Inventory.ResourceAmount(_electricityResource, initialElectricityAmount),
            new Inventory.ResourceAmount(_moneyResource, initialAmount)
        });

        inventory.TryRemove(_moneyResource, amountToRemove);

        Assert.AreEqual(initialElectricityAmount, inventory.GetAmount(_electricityResource));
    }
}
