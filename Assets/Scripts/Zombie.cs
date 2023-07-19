using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Zombie : MonoBehaviour
{
    public event UnityAction<Zombie> Died;
}
