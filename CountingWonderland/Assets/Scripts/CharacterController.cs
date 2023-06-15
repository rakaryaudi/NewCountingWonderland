using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterController : MonoBehaviour
{
    private float speed = 5f; // Kecepatan pergerakan karakter
    public string [] question, answerKey;
    string[] answer;
    int number = -1;
    public TMP_Text questionText, scoreText;
    public GameObject answers, gameOver, finish;
    int score = 0;
    List<string> answeredQuestions = new List<string>();
    public AudioSource audioSource;


    private void Start() {
        StartCoroutine(nextQuestion());
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontalInput, 0f, 0f) * speed * Time.deltaTime;
        transform.Translate(movement);
    }

    void OnTriggerEnter(Collider obj) {
        if(obj.name == "answers") {
            if(obj.transform.GetChild(0).GetComponent<TMP_Text>().text == answer[0]) {
                score +=10;
                scoreText.text = "Score: " + score.ToString();
            } else {
                gameOver.SetActive(true);
                Time.timeScale = 0;
                audioSource.Stop();
            }
            for (int i=0; i<obj.transform.parent.childCount; i++) {
                obj.transform.parent.GetChild(i).GetComponent<BoxCollider>().enabled = false;
            }
            obj.gameObject.SetActive(false);
            StartCoroutine(nextQuestion());
        }
    }

IEnumerator nextQuestion()
{
    yield return new WaitForSeconds(1.5f);

    if (answeredQuestions.Count == question.Length)
    {
        Debug.Log("Semua pertanyaan sudah terjawab!");
        Time.timeScale = 0;
        finish.SetActive(true);
        yield break; // Keluar dari coroutine jika semua pertanyaan sudah terjawab
    }

    int randomIndex;
    string selectedQuestion;

    do
    {
        randomIndex = Random.Range(0, question.Length);
        selectedQuestion = question[randomIndex];
    } while (answeredQuestions.Contains(selectedQuestion));

    answeredQuestions.Add(selectedQuestion);

    questionText.transform.parent.gameObject.SetActive(true);
    questionText.text = selectedQuestion;

    answers.GetComponent<Animator>().enabled = true;
    answers.GetComponent<Animator>().Play(0);

    answer = answerKey[randomIndex].Split('|');
    for (int i = 0; i < answers.transform.childCount; i++)
    {
        answers.transform.GetChild(i).GetChild(0).GetComponent<TMP_Text>().text = "";
        answers.transform.GetChild(i).gameObject.SetActive(true);
        answers.transform.GetChild(i).GetComponent<BoxCollider>().enabled = true;
    }
    int index = 0;
    for (int i = 0; i < answers.transform.childCount; i++)
    {
        do
        {
            index = Random.Range(0, answers.transform.childCount);
        } while (answers.transform.GetChild(index).GetChild(0).GetComponent<TMP_Text>().text != "");
        answers.transform.GetChild(index).GetChild(0).GetComponent<TMP_Text>().text = answer[i];
    }
}

    public void restart() {
        Time.timeScale = 1;
        Application.LoadLevel(Application.loadedLevelName);
    }
}
