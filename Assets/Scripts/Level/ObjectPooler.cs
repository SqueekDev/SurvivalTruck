using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private List<ZombieAttacker> _pooledObjects;

    public bool TryGetObject(out ZombieAttacker pooledObject)
    {
        pooledObject = _pooledObjects.FirstOrDefault(p=>p.gameObject.activeSelf==false);
        return pooledObject != null;
    }
}
