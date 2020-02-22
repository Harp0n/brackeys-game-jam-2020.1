using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSystem : MonoBehaviour
{
    [System.Serializable]
    public class Thing
    {
        public Sprite[] sprites;
        public RuntimeAnimatorController animController;
        public AudioClip sound;
        public float time;
        public float startingVolume;
        public int minSpriteLayer, maxSpriteLayer;
        public float minSize, maxSize;
        public float minY, maxY;
        public float minSpeed, maxSpeed;
        public float spawnPerSecond;
        public bool keepUnderWater;
        public bool keepAboveWater;
    }

    public float startingX = 5f;
    public Thing[] things;

    private List<GameObject> spawnedThings = new List<GameObject>();
    private Transform thingsContainer;
    private float[] spawnThingTimes;
    private float spawnCooldownTimer;
    private float waterLevel = -10f;

    public void SetWaterLevel(float surfacePos)
    {
        waterLevel = surfacePos;
    }

    private void Awake()
    {
        spawnThingTimes = new float[things.Length];
        thingsContainer = new GameObject("things").transform;
    }

    private GameObject GetObject()
    {
        foreach (GameObject g in spawnedThings) if (!g.activeSelf) return g;
        GameObject newThing = new GameObject("thing");
        newThing.transform.SetParent(thingsContainer);
        newThing.AddComponent<Animator>();
        newThing.AddComponent<AudioSource>().loop = true;
        newThing.AddComponent<SpriteRenderer>();
        Rigidbody2D r = newThing.AddComponent<Rigidbody2D>();
        r.angularDrag = 0;
        r.drag = 0;
        r.freezeRotation = true;
        r.gravityScale = 0;
        spawnedThings.Add(newThing);
        return newThing;
    }

    private void CreateThing(Thing thing)
    {
        GameObject newThing = GetObject();
        if (thing.animController != null) newThing.GetComponent<Animator>().runtimeAnimatorController = thing.animController;
        SpriteRenderer spriteRenderer = newThing.GetComponent<SpriteRenderer>();
        if (thing.sprites.Length != 0) spriteRenderer.sprite = thing.sprites[Random.Range(0, thing.sprites.Length)];
        spriteRenderer.sortingOrder = Random.Range(thing.minSpriteLayer, thing.maxSpriteLayer);
        AudioSource a = newThing.GetComponent<AudioSource>();
        a.clip = thing.sound;
        float scaleMod = Random.Range(thing.minSize, thing.maxSize);
        newThing.transform.localScale *= scaleMod;
        a.volume = Mathf.Clamp(thing.startingVolume * scaleMod, 0, 1);
        if (thing.sound != null) a.Play();
        float posY = Random.Range(thing.minY, thing.maxY);
        if (thing.keepUnderWater) posY = Mathf.Min(posY, waterLevel);
        if (thing.keepAboveWater) posY = Mathf.Max(posY, waterLevel);
        newThing.transform.position = new Vector2(startingX, posY);
        newThing.SetActive(true);
        float speedMod = Random.Range(thing.minSpeed, thing.maxSpeed);
        newThing.GetComponent<Rigidbody2D>().velocity = Vector2.left * speedMod;
        StartCoroutine(Disabler(newThing, thing.time));
    }

    IEnumerator Disabler(GameObject toDisable, float time)
    {
        yield return new WaitForSeconds(time);
        Resetter(toDisable);
        toDisable.SetActive(false);
    }

    private void Resetter(GameObject toReset)
    {
        toReset.GetComponent<Animator>().runtimeAnimatorController = null;
        toReset.GetComponent<SpriteRenderer>().sprite = null;
        toReset.transform.localScale = Vector2.one;
        toReset.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        toReset.GetComponent<AudioSource>().Stop();
    }

    void Update()
    {

        for(int i=0; i<spawnThingTimes.Length; i++)
        {
            spawnThingTimes[i] += Time.deltaTime;
            if(spawnThingTimes[i] >= 1f/things[i].spawnPerSecond)
            {
                CreateThing(things[i]);
                spawnThingTimes[i] = 0;
            }
        }
    }
}
