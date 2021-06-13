using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<string> instructions;
    public Text tutorialText;
    public int currentIndex = 0;
    bool laserCreated = false;
    bool fusionDone = false;
    bool fusionTimerRunning = false;
    public EnemySpawner enemySpawner;
    public enum State
    {
        movement, splitting, laser, fusion, kill
    }
    State state;

    private void OnEnable()
    {
        MyEventSystem.laserCreated += LaserUsed;
        MyEventSystem.fusion += FusionComplete;
        MyEventSystem.enemyDead += EnemyDeath;
    }

    private void OnDisable()
    {
        MyEventSystem.laserCreated -= LaserUsed;
        MyEventSystem.fusion -= FusionComplete;
        MyEventSystem.enemyDead -= EnemyDeath;
    }
    void Start()
    {
        state = State.movement;
        tutorialText.text = instructions[currentIndex];
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case State.movement:
                CheckForMovement();
                break;
            case State.splitting:
                CheckForSplitting();
                break;
            case State.laser:
                break;
            case State.fusion:
                if(!fusionTimerRunning)
                {
                    StartCoroutine("FusionSkipTimer");
                }
                break;
            case State.kill:
                break;
        }
    }

    void CheckForMovement()
    {
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartCoroutine("NextInstructionTimer");
            state = State.splitting;
        }
    }

    void CheckForSplitting()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine("NextInstructionTimer");
            state = State.laser;
        }
    }

    void LaserUsed(int i)
    {
        if (!laserCreated) { 
            StartCoroutine("NextInstructionTimer");
        state = State.fusion;
        laserCreated = true;
    }
    }
    
    void FusionComplete(int i)
    {
        if (!fusionDone)
        {
            StartCoroutine("FinalInstruction");
            state = State.kill;
            fusionDone = true;
            enemySpawner.enabled = true;
        }
    }

    IEnumerator NextInstructionTimer()
    {
        yield return new WaitForSeconds(3);
        currentIndex++;
        tutorialText.text = instructions[currentIndex];
    }

    void EnemyDeath(int i)
    {

    }

    IEnumerator FinalInstruction()
    {
        currentIndex++;
        tutorialText.text = instructions[currentIndex];
        yield return new WaitForSeconds(3);
        currentIndex++;
        tutorialText.text = instructions[currentIndex];
        yield return new WaitForSeconds(3);
        Destroy(tutorialText.gameObject);
    }

    IEnumerator FusionSkipTimer()
    {
        if (!fusionTimerRunning)
        {
            fusionTimerRunning = true;
            yield return new WaitForSeconds(7);
            if (!fusionDone)
            {
                fusionDone = true;
                StartCoroutine("FinalInstruction");
                state = State.kill;
                enemySpawner.enabled = true;
            }
        }
    }

    

    


}
