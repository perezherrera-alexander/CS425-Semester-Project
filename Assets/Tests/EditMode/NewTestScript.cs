using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class NewTestScript
{
    [SetUp]

    [Test]
    public void Setup()
    {
        SceneManager.LoadScene("Game View");
    }
}
