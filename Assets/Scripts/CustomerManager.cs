using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public List<GameObject> CustomerPrefabs; // Prefab List
    public DialogueLoader dialogueLoader; // JSON �����͸� �ε��ϴ� ��ũ��Ʈ

    public int numberOfCustomers = 0;
    public Queue<Customer> customerQueue;
    private List<Customer> currentCustomers = new List<Customer>();

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
        dialogueLoader.LoadDialogueData(day); // ���� day�� �´� ��ȭ ������ �ε�

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
        Dialogue dialogue = customer.dialogue;
        dialogueManager.StartDialogue(customer.dialogue, () => StartCoroutine(HandleCustomerExit(customer)));
    }

    IEnumerator HandleCustomerExit(Customer customer)
    {
        // �մ��� ��ȭ�� ��ġ�� �������� ��
        yield return customer.ExitShop(Random.value > 0.5f);

        // �մ��� ������ �� ���� �մ��� ����
        StartNextCustomer();
    }

    public int GetCustomersToday()
    {
        return numberOfCustomers;
    }

    List<Customer> GenerateCustomersForDay(int day)
    {
        List<Customer> customers = new List<Customer>();

        List<string> storyNPCNames = dialogueLoader.GetStoryNPCNames();

        numberOfCustomers = Random.Range(5, 8);

        foreach (string npcName in storyNPCNames)
        {
            GameObject npcPrefab = GetNPCPrefab(npcName);
            if (npcPrefab != null)
            {
                GameObject customerInstance = Instantiate(npcPrefab);
                Customer customer = customerInstance.GetComponentInChildren<Customer>();

                Dialogue dialogue = dialogueLoader.GetStoryNPCDialogue(npcName);
                if (dialogue != null)
                {
                    customer.dialogue = dialogue;
                    // Debug.Log($"Assigned dialogue for {npcName}: {dialogue.characterName}");
                }
                else
                {
                    // Debug.LogError($"Dialogue for {npcName} is null.");
                }

                customers.Add(customer);
            }
            else
            {
                // Debug.LogError($"Prefab for {npcName} not found.");
            }
        }

        int remainingSpots = numberOfCustomers - storyNPCNames.Count;
        for (int i = 0; i < remainingSpots; i++)
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

    GameObject GetNPCPrefab(string npcName)
    {
        return CustomerPrefabs.Find(prefab => prefab.name == npcName);
    }

    Dialogue GenerateRandomDialogue()
    {
        Dialogue dialogue = ScriptableObject.CreateInstance<Dialogue>();

        // ���� �̸� ����
        string[] names = { "�ⷪ��", "����", "����ȯ", "�����帮ũ �������� ����", "��Ʈ", "�����"};
        dialogue.characterName = names[Random.Range(0, names.Length)];

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