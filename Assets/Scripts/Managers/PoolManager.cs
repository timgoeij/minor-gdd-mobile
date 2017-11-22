using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ColourRun.Managers
{
    public class PoolManager : MonoBehaviour
    {

        public static Dictionary<string, List<GameObject>> items;

        void Awake()
        {
            PoolManager.items = new Dictionary<string, List<GameObject>>();

            LoadResource("Blood", 200);
            LoadResource("Explosion", 2);
            LoadResource("TouchCircle", 5);

            LoadResource("Spark", 60);
            LoadResource("Point", 300);

            LoadResource("ByeByeItem", 25);
            LoadResource("HellItem", 15);
            LoadResource("HellDebris", 60);
        }

        public static List<GameObject> GetAll(string key, bool includeActive = false)
        {
            if (!PoolManager.items.ContainsKey(key))
            {
                return null;
            }

            if (includeActive)
            {
                return PoolManager.items[key];
            }

            return PoolManager.items[key].Where(i => i.activeInHierarchy == false).ToList();
        }

        public static GameObject GetItem(string key)
        {
            if (!PoolManager.items.ContainsKey(key))
            {
                return null;
            }

            return PoolManager.items[key].Where(i => i.activeInHierarchy == false).FirstOrDefault();
        }

        private void LoadResource(string resourceName, int amount)
        {
            GameObject o = Resources.Load(resourceName) as GameObject;

            if (!PoolManager.items.ContainsKey(resourceName))
            {
                PoolManager.items.Add(resourceName, new List<GameObject>());
            }

            for (int i = 0; i < amount; i++)
            {
                GameObject instance = Instantiate(o);
                instance.SetActive(false);
                PoolManager.items[resourceName].Add(instance);
            }
        }
    }
}


