using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class csvDatabase : MonoBehaviour
{
    // �о� �� ���� �̸�
    //public string csvFileName = "menu";

    // key:value ���·� ����
    // key(�޴���)�� value�� �̾ƿ��� ����
    // ���ϴ� ���·� �����ص� ����
    public Dictionary<string, Question> dicQuestion = new Dictionary<string, Question>(); // ���� dictionary
    /*public Dictionary<string, Answer> dicAnswer = new Dictionary<string, Answer>(); // �亯 dictionary
    public Dictionary<string, Dialogue> dicDialogue = new Dictionary<string, Dialogue>(); // ��ȭ dictionary*/

    // �о� �� �����͸� ���� ����ü
    // ���� Ŭ������ �����߽��ϴ�! struct�� �����ص� �����ؿ�.
    [System.Serializable]
    public class Question
    {
        public string index_code;
        public string question;
    }
    public class Answer
    {
        public string index_code;
        public string answer;
    }
    public class Dialogue
    {
        public string index_code;
        public string dialogue;
    }

    private void Start()
    {
        ReadCSV();
    }

    // ������ �о� ���� �޼���
    private void ReadCSV()
    {
        // ���� �̸�.Ȯ����
        string path1 = "csv/story1_question.csv";
        /*string path2 = "csv/Answer.csv";
        string path3 = "csv/Dialogue.csv";*/

        // �����͸� �����ϴ� ����Ʈ
        // ���ϰ� �����ϱ� ���� List�� ����
        // ���ϴ� ���·� �����Ͻø� �˴ϴ�!
        List<Question> questionList = new List<Question>();

        // stream reader
        // UTF-8�� ���ڵ� �Ϸ��� �ش� StreamReader�� �ʿ���!!
        // Application.dataPath�� Unity�� Assets������ ������
        // �ڿ� �������� ������ �ִ� ��θ� �ۼ�
        // ex) Assets > Files�� menu.csv�� ��������? "/" + "Files/menu.csv"�߰�
        StreamReader reader1 = new StreamReader(Application.dataPath + "/" + path1);
        /*StreamReader reader2 = new StreamReader(Application.dataPath + "/" + path2);
        StreamReader reader3 = new StreamReader(Application.dataPath + "/" + path3);*/

        // ������ ���� �Ǻ��ϱ� ���� bool Ÿ�� ����
        bool isFinish = false;


        while (isFinish == false)
        {
            // ReadLine�� ���پ� �о string���� ��ȯ�ϴ� �޼���
            // ���پ� �о data������ ������
            string data = reader1.ReadLine(); // �� �� �б�

            // data ������ ������� Ȯ��
            if (data == null)
            {
                // ���� ����ٸ�? ������ �� == ������ �����̴�
                // isFinish�� true�� ����� �ݺ��� Ż��
                isFinish = true;
                break;
            }

            // .csv�� ,(�޸�)�� �������� �����Ͱ� ���еǾ� �����Ƿ�
            // ,(�޸�)�� �������� �����͸� ������ list�� ����
            // ex) ������ġ,200��,���־��! => [������ġ][200��][���־��!]
            var splitData = data.Split(','); // �޸��� ������ ����

            // ���� �����ߴ� �޴� ��ü�� �������ְ�
            Question question = new Question();

            // �޴��� ����Ʈ�� �ִ� �����ͷ� �ʱ�ȭ
            // menu.name�� splitData[0]��° �ִ� �����͸� ��´ٴ� �ǹ�
            // ��, menu ��ü name�������� splitData[0]�� ��� "������ġ"�� ���ϴ�.
            question.index_code = splitData[0];
            question.question = splitData[1];

            // menu ��ü�� �� ��Ҵٸ� dictionary�� key�� value������ ����
            // �̷��� �صθ� dicMenu.Add("������ġ");�� menu.name, menu.price .. ���� ����
            dicQuestion.Add(question.index_code, question);
            Debug.Log(question.index_code);
            Debug.Log(dicQuestion.Count); // �� ������ üũ
        }
        /*isFinish = false;
        while (isFinish == false)
        {
            string data = reader2.ReadLine(); // �� �� �б�

            if (data == null)
            {
                isFinish = true;
                break;
            }
            var splitData = data.Split(','); // �޸��� ������ ����

            Answer answer = new Answer();
            answer.index_code = splitData[0];
            answer.answer = splitData[1];

            dicAnswer.Add(answer.index_code, answer);
            Debug.Log(answer.index_code);
            Debug.Log(dicQuestion.Count); // �� ������ üũ
        }
        isFinish = false;
        while (isFinish == false)
        {
            string data = reader3.ReadLine(); // �� �� �б�

            if (data == null)
            {
                isFinish = true;
                break;
            }
            var splitData = data.Split(','); // �޸��� ������ ����

            Dialogue dialogue = new Dialogue();
            dialogue.index_code = splitData[0];
            dialogue.dialogue = splitData[1];

            dicDialogue.Add(dialogue.index_code, dialogue);
            Debug.Log(dialogue.index_code);
            Debug.Log(dicQuestion.Count); // �� ������ üũ
        }*/
    }
}
