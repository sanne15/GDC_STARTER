using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public Queue<Customer> customerQueue;
    public DialogueManager dialogueManager;
    public List<GameObject> CustomerPrefabs; // Prefab List

    void Start()
    {
        // CustomerPrefabs�� ��� ������ ���� �޽��� ���
        if (CustomerPrefabs == null || CustomerPrefabs.Count == 0)
        {
            Debug.LogError("CustomerPrefabs ����Ʈ�� ��� �ֽ��ϴ�. �������� �Ҵ��ϼ���.");
        }
    }

    public void InitializeDay(int day)
    {
        // day�� ���� �մ� ����Ʈ�� �����ϰ� ť�� �߰�
        customerQueue = new Queue<Customer>(GenerateCustomersForDay(day));
        StartNextCustomer();
    }

    void StartNextCustomer()
    {
        if (customerQueue.Count > 0)
        {
            Customer nextCustomer = customerQueue.Dequeue();
            nextCustomer.EnterShop(() => StartDialogue(nextCustomer));
        }
        else
        {
            // ��� �մ��� ó���Ǿ��� �� DayManager�� ���� ���� ���� �̵�
            FindObjectOfType<DayManager>().NextDay();
        }
    }

    void StartDialogue(Customer customer)
    {
        dialogueManager.StartDialogue(customer.dialogue, () => StartCoroutine(HandleCustomerExit(customer)));
    }

    IEnumerator HandleCustomerExit(Customer customer)
    {
        // �մ��� ��ȭ�� ��ġ�� �������� ��
        yield return customer.ExitShop(Random.value > 0.5f);

        // �մ��� ������ �� ���� �մ��� ����
        StartNextCustomer();
    }

    List<Customer> GenerateCustomersForDay(int day)
    {
        List<Customer> customers = new List<Customer>();


        int numberOfCustomers = Random.Range(5, 8);
        for (int i = 0; i < numberOfCustomers; i++)
        {
            GameObject customerPrefab = CustomerPrefabs[Random.Range(0, CustomerPrefabs.Count)];
            GameObject customerInstance = Instantiate(customerPrefab);

            Customer customer = customerInstance.GetComponentInChildren<Customer>();

            if (customer == null)
            {
                Debug.LogError("customerPrefab�� Customer ������Ʈ�� �����ϴ�.");
                continue;
            }
            customer.dialogue = GenerateRandomDialogue();

            customers.Add(customer);
        }

        // Debug.Log(customers.Count);
        return customers;
    }

    Dialogue GenerateRandomDialogue()
    {
        Dialogue dialogue = ScriptableObject.CreateInstance<Dialogue>();

        // ���� �̸� ����
        string[] names = { "�ⷪ��", "����", "����ȯ", "�����帮ũ �������� ����", "���丣��Ʈ" };
        dialogue.namae = names[Random.Range(0, names.Length)];

        // ���� ��� ����
        string[] possibleSentences = {
            "�������̿� ������.",
            "���� ���� ���?",
            "��� �� �ֽ� �� �ּ� ������?",
            "������ ������.",
            "������ Ư�� �޴� �ּ�?"
        };

        int sentenceCount = Random.Range(3, 6); // 3~5���� ��� ����
        dialogue.sentences = new string[sentenceCount];
        for (int i = 0; i < sentenceCount; i++)
        {
            dialogue.sentences[i] = possibleSentences[Random.Range(0, possibleSentences.Length)];
        }

        return dialogue;
    }
}
