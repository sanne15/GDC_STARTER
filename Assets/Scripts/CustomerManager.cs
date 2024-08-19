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
        // CustomerPrefabs가 비어 있으면 오류 메시지 출력
        if (CustomerPrefabs == null || CustomerPrefabs.Count == 0)
        {
            Debug.LogError("CustomerPrefabs 리스트가 비어 있습니다. 프리팹을 할당하세요.");
        }
    }

    public void InitializeDay(int day)
    {
        // day에 따라 손님 리스트를 생성하고 큐에 추가
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
            // 모든 손님이 처리되었을 때 DayManager를 통해 다음 날로 이동
            FindObjectOfType<DayManager>().NextDay();
        }
    }

    void StartDialogue(Customer customer)
    {
        dialogueManager.StartDialogue(customer.dialogue, () => StartCoroutine(HandleCustomerExit(customer)));
    }

    IEnumerator HandleCustomerExit(Customer customer)
    {
        // 손님이 대화를 마치고 나가도록 함
        yield return customer.ExitShop(Random.value > 0.5f);

        // 손님이 퇴장한 후 다음 손님이 입장
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
                Debug.LogError("customerPrefab에 Customer 컴포넌트가 없습니다.");
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

        // 랜덤 이름 지정
        string[] names = { "얄랴셩", "서휼", "이진환", "프레드리크 프랑수아 쇼팽", "네페르피트" };
        dialogue.namae = names[Random.Range(0, names.Length)];

        // 랜덤 대사 생성
        string[] possibleSentences = {
            "오랜만이오 주인장.",
            "오늘 장사는 어떤가?",
            "라면 좀 주실 수 있소 주인장?",
            "날씨가 좋구려.",
            "오늘의 특선 메뉴 있소?"
        };

        int sentenceCount = Random.Range(3, 6); // 3~5개의 대사 생성
        dialogue.sentences = new string[sentenceCount];
        for (int i = 0; i < sentenceCount; i++)
        {
            dialogue.sentences[i] = possibleSentences[Random.Range(0, possibleSentences.Length)];
        }

        return dialogue;
    }
}
