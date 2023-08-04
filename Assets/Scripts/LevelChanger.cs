using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelChanger : MonoBehaviour
{
    public int LevelNumber { get; private set; }

    public event UnityAction Changed;
}
