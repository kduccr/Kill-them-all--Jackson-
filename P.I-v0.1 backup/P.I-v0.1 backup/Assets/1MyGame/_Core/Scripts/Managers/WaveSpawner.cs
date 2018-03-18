using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaveSpawner : MonoBehaviour {
    
    public enum SpawnState { SPAWNING, WAITING,COUNTING}
    
    [System.Serializable]
    public class Wave
    {
        
        public string name;  // nome da wave
        public Transform enemy;
        public Transform fatEnemy;
        public Transform slimeEnemy;
        

        public int count;
        public int fatEnemyCount;
        public int slimeEnemyCount;
        public float rate;   // spawn rate
    }
    [SerializeField] GameObject akPrefab;
    [SerializeField] GameObject rayGunPrefab;
    [SerializeField] GameObject weaponStartPos;
    int akCount = 1 ;
    int rayCount= 1;

    public Text waveCount;
    public Text currentWave;
    public GameObject cuidado;
    public GameObject kill;
    public GameObject bossPrefab;

    public Wave[] waves;
    private int nextWave = 0;
    public Transform[] SpawnPosition;
    public Transform bossSpawnPos;

    public float timeBetweenWaves = 5f;
    public float waveCountDown = 0f;

    private float SearchCountDown = 1f;
    private SpawnState state = SpawnState.COUNTING;

    
    rock_free_master rock;

    int bossCount;


    void DisplayCuidado()
    {
        cuidado.SetActive(true);
        Invoke("StopDisplayCuidado", 2f);
    }

    void StopDisplayCuidado()
    {
        cuidado.SetActive(false);
        
        
    }

    void DisplayKill()
    {
        kill.SetActive(true);
        
        Invoke("StopDisplayKill", 1.5f);
    }
    void StopDisplayKill()
    {
        kill.SetActive(false);

    }



    void Start()
    {
        bossCount = 1;
        rock = this.gameObject.GetComponent<rock_free_master>();
        waveCount.text = "";
        waveCountDown = timeBetweenWaves;
        rock.soft = false;
        rock.med = true;
        
    }

    

    void Update()
    {
        if (nextWave == 1)
        {
            if (akCount == 1)
            {
                Instantiate(akPrefab, weaponStartPos.transform.position, weaponStartPos.transform.rotation);
                akCount++;
            }
        }

        if (nextWave == 2)
        {
            if (rayCount == 1)
            {
                Instantiate(rayGunPrefab, weaponStartPos.transform.position, weaponStartPos.transform.rotation);
                rayCount++;
            }
        }
        if (nextWave == 4)
        {
            DisplayCuidado();
           
            



            rock.soft = false;
            rock.med = false;
            rock.forte = true;
        }
        
        currentWave.text = "Current Wave: " + (nextWave +1);
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive() )
            {
                
              WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (waveCountDown <= 0)
        {
            
            waveCount.text = "";
            DisplayKill();
            
            if (state !=SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
            waveCount.text = "Next Wave in   " + Mathf.Floor(waveCountDown);
           
            
        }
    }

    void WaveCompleted()
    {
        
        state = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            SceneManager.LoadScene("Win");
            nextWave = 0;
            Debug.Log("completed all waves. Looping...");
        }
        else
        {
            nextWave++;
        }

        
    }

    bool EnemyIsAlive()
    {
        SearchCountDown -= Time.deltaTime;
        if (SearchCountDown <= 0f)
        {
            SearchCountDown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null && GameObject.FindGameObjectWithTag("EnemyChase") == null)
            {
                
                return false;
            }
           
        }
        
        return true;
    }

   

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawining wave");
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);

            yield return new WaitForSeconds(1f / _wave.rate);

        }
        for (int i = 0; i < _wave.fatEnemyCount; i++)
        {
            SpawnEnemy(_wave.fatEnemy);

            yield return new WaitForSeconds(1f / _wave.rate);

        }

        for (int i = 0; i < _wave.slimeEnemyCount; i++)
        {
            SpawnEnemy(_wave.slimeEnemy);

            yield return new WaitForSeconds(1f / _wave.rate);

        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Transform _spawnPos = SpawnPosition[Random.Range(0,SpawnPosition.Length) ];
        Instantiate(_enemy, _spawnPos.position, _spawnPos.rotation);

        
        

    }

    
}
