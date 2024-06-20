using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Script : MonoBehaviour
{
    public enum CommandId
    {
        wait,
        objectSetEnabled,
        objectTeleport,
        objectRotate,
        objectFacing,
        objectTalk,
        setText,
        setTextEmpty,
        setTextLetterByLetter,
        waitTillClick,
        canvasFade,
        setCanvasAlpha,
        playAnimationInt,
        playAnimationTrigger,
        movingLerp,
        copyRotation
    }

    [System.Serializable]
    public struct Command
    {
        public CommandId id;
        public float param1;
        public float param2;
        public float param3;
        public string stringParam1;
        public string stringParam2;
        public string stringParam3;
        public GameObject objectParam1;
        public GameObject objectParam2;
        public GameObject objectParam3;
        public bool executeWithPrevious;
    }

    [SerializeField] Command[] commands;

    bool running;
    int index;
    List<Command> executing = new List<Command>();
    int commandsExecuted;
    int executingCommandIndex;

    float timer;
    Quaternion target;
    string text;
    int letter;
    float value;

    private bool voiceStart = false;

    [Header("Events")]
    [SerializeField] private UnityEvent onStartScript;
    [SerializeField] private UnityEvent onEndScript;
    

    void Update()
    {
        if (running)
        {
            if (index >= commands.Length) { running = false; ScriptingManager.Instance.StopScript(); onEndScript.Invoke(); }
            else
            {
                if (executing.Count>0)
                {
                    for (executingCommandIndex = 0; executingCommandIndex < executing.Count; executingCommandIndex++)
                    {
                        UpdateCommand(executing[executingCommandIndex]);
                    }
                }
                else
                {
                    commandsExecuted = 0;
                    executing.Clear();
                    executing.Add(commands[index]);
                    bool checking = true;
                    for (int i = index+1; i < commands.Length && checking == true; i++)
                    {
                        if (commands[i].executeWithPrevious)
                        {
                            executing.Add(commands[i]);
                        }
                        else
                        {
                            checking = false;
                        }
                    }

                    commandsExecuted = executing.Count;

                    for (executingCommandIndex = 0; executingCommandIndex < executing.Count; executingCommandIndex++)
                    {
                        ExecuteCommand(executing[executingCommandIndex]);
                    }
                }
            }
        }
    }

    public void Run()
    {
        onStartScript.Invoke();
        index = 0;
        commandsExecuted = 0;
        executing.Clear();
        running = true;
    }

    void ExecuteCommand(Command c)
    {
        timer = 0;
        if (c.id == CommandId.wait)
        {

        }
        else if (c.id == CommandId.objectSetEnabled)
        {
            c.objectParam1.SetActive((int)c.param1 != 0);

            StopCommand(c);
        }
        else if (c.id == CommandId.objectTeleport)
        {
            c.objectParam1.transform.position = c.objectParam2.transform.position;

            StopCommand(c);
        }
        else if (c.id == CommandId.objectRotate)
        {
            //target = Quaternion.LookRotation(c.objectParam2.transform.position - c.objectParam1.transform.position);
            //c.objectParam1.transform.rotation = target;
            //c.objectParam1.transform.localScale = new Vector3(-1,1,1);

            StopCommand(c);
        }
        else if (c.id == CommandId.copyRotation)
        {
            c.objectParam1.transform.rotation = c.objectParam2.transform.rotation;

            StopCommand(c);
        }
        else if (c.id == CommandId.objectFacing)
        {
            target = Quaternion.LookRotation(c.objectParam2.transform.position - c.objectParam1.transform.position);
            target.x = 0;
            target.z = 0;
        }
        else if (c.id == CommandId.setText)
        {
            c.objectParam1.GetComponent<TextMeshProUGUI>().text = TextManager.Instance.GetText(c.stringParam1);
            StopCommand(c);
        }
        else if (c.id == CommandId.setTextEmpty)
        {
            c.objectParam1.GetComponent<TextMeshProUGUI>().text = "";

            StopCommand(c);
        }
        else if (c.id == CommandId.setTextLetterByLetter)
        {
            letter = 0;
            text = "";
            c.objectParam1.GetComponent<TextMeshProUGUI>().text = text;
        }
        else if (c.id == CommandId.waitTillClick)
        {

        }
        else if (c.id == CommandId.canvasFade)
        {
            value = c.param1;
        }
        else if (c.id == CommandId.setCanvasAlpha) {

            c.objectParam1.GetComponent<CanvasGroup>().alpha = c.param1;
            StopCommand(c);
        }
        else if (c.id == CommandId.playAnimationInt) {

            c.objectParam1.GetComponent<Animator>().SetInteger(c.stringParam1, (int)c.param1);
            StopCommand(c);
        }
        else if (c.id == CommandId.playAnimationTrigger)
        {
            c.objectParam1.GetComponent<Animator>().SetTrigger(c.stringParam1);
            StopCommand(c);
        }
        else if (c.id == CommandId.movingLerp)
        {
            
        }
        else 
        {
            StopCommand(c);
        }
    }

    void UpdateCommand(Command c)
    {
        if (c.id == CommandId.wait)
        {
            timer += Time.deltaTime;

            if (timer > c.param1) { StopCommand(c); }
        }
        else if (c.id == CommandId.objectFacing)
        {
            timer += Time.deltaTime * (c.param1/20);

            c.objectParam1.transform.rotation = Quaternion.Slerp(c.objectParam1.transform.rotation, target, timer);

            if (Mathf.Abs(Quaternion.Dot(target, c.objectParam1.transform.rotation)) > 0.999) {

                StopCommand(c); 
            }
        }
        else if (c.id == CommandId.setTextLetterByLetter)
        {
            if (!voiceStart) {

                voiceStart = true;
                AudioManager.Instance.playAudio(c.stringParam2);
            }

            timer += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                text = "";
                for (int i = 0; i < TextManager.Instance.GetText(c.stringParam1).Length; i++)
                {
                    if (TextManager.Instance.GetText(c.stringParam1)[i] == '/')
                    {
                        text += '\n';
                    }
                    else
                    {
                        text += TextManager.Instance.GetText(c.stringParam1)[i];
                    }
                }
                c.objectParam1.GetComponent<TextMeshProUGUI>().text = text;
                StopCommand(c);
                AudioManager.Instance.PauseSong(c.stringParam2);
                voiceStart = false;
            }
            else if (timer > c.param1)
            {
                if (TextManager.Instance.GetText(c.stringParam1)[letter] == '/')
                {
                    text += '\n'; 
                }
                else
                {
                    text += TextManager.Instance.GetText(c.stringParam1)[letter];
                }
                timer = 0;
                c.objectParam1.GetComponent<TextMeshProUGUI>().text = text;
                letter++;
                
                if (text.Length == TextManager.Instance.GetText(c.stringParam1).Length)
                {
                    StopCommand(c);
                    AudioManager.Instance.PauseSong(c.stringParam2);
                    voiceStart = false;
                }
            }
        }
        else if (c.id == CommandId.waitTillClick)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                StopCommand(c);
            }
        }
        else if (c.id == CommandId.canvasFade)
        {
            c.objectParam1.GetComponent<CanvasGroup>().alpha = Mathf.MoveTowards(c.objectParam1.GetComponent<CanvasGroup>().alpha, value, Time.deltaTime * c.param2);

            if (c.objectParam1.GetComponent<CanvasGroup>().alpha == value)
            {
                StopCommand(c);
            }
        }
        else if (c.id == CommandId.movingLerp)
        {
            timer += Time.deltaTime;
            c.objectParam1.transform.position = Vector3.Lerp(c.objectParam2.transform.position, c.objectParam3.transform.position, timer / c.param1);
            if (Vector3.Distance(c.objectParam1.transform.position, c.objectParam3.transform.position)<0.3f)
            {
                StopCommand(c);
            }
        }
        else
        {
            StopCommand(c);
        }
    }

    void StopCommand(Command c)
    {
        executing.Remove(c);
        executingCommandIndex--;
        index++;
    }

    public void CheckCommandItsEmpty()
    {
        if (commands.Length == 1)
        {
            commands = new Command[1];
        }
    }

    public Command[] EditorGetCommands()
    {
        return commands;
    }

    public void EditorSetCommands(Command[] _commands)
    {
        commands = _commands;
    }
}
