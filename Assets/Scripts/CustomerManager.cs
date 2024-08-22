using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public List<GameObject> CustomerPrefabs; // Prefab List
    public DialogueLoader dialogueLoader; // JSON 데이터를 로드하는 스크립트

    public int numberOfCustomers = 0;
    public Queue<Customer> customerQueue;
    private List<Customer> currentCustomers = new List<Customer>();

    private Customer nextCustomer;

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
        dialogueLoader.LoadDialogueData(day); // 현재 day에 맞는 대화 데이터 로드

        // day에 따라 손님 리스트를 생성하고 큐에 추가
        customerQueue = new Queue<Customer>(GenerateCustomersForDay(day));
        StartNextCustomer();
    }

    void StartNextCustomer()
    {
        if (customerQueue.Count > 0)
        {
            nextCustomer = customerQueue.Dequeue();
            if (dialogueManager != null)
            {
                dialogueManager.currentCustomer = nextCustomer;
            }
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
        Dialogue dialogue = customer.dialogue;
        dialogueManager.StartDialogue(customer.dialogue, () => StartCoroutine(HandleCustomerExit(customer)));
    }

    IEnumerator HandleCustomerExit(Customer customer)
    {
        // 손님이 대화를 마치고 나가도록 함
        yield return customer.ExitShop(Random.value > 0.5f);

        // 손님이 퇴장한 후 다음 손님이 입장
        StartNextCustomer();
    }

    public int GetCustomersToday()
    {
        return numberOfCustomers;
    }

    // 현재 대화 중인 캐릭터를 강제 퇴장시키는 메서드
    public void ForceExitCurrentCustomer()
    {
        if (nextCustomer != null)
        {
            StartCoroutine(ForceExitAndStartNextCustomer());
        }
    }

    private IEnumerator ForceExitAndStartNextCustomer()
    {
        yield return StartCoroutine(nextCustomer.ExitShop(Random.value > 0.5f));

        nextCustomer = null; // currentCustomer를 null로 설정하여 퇴장 처리 완료

        // 다음 손님을 불러옴
        StartNextCustomer();
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
                    Debug.Log($"Assigned dialogue for {npcName}: {dialogue.characterName}");
                }
                else
                {
                    Debug.LogError($"Dialogue for {npcName} is null.");
                }

                customers.Add(customer);
            }
            else
            {
                Debug.LogError($"Prefab for {npcName} not found.");
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
                Debug.LogError("customerPrefab에 Customer 컴포넌트가 없습니다.");
                continue;
            }
            customer.dialogue = GenerateRandomDialogue();
            customer.dialogue.characterName = customerPrefab.name;

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

        // 랜덤 이름 지정
        // string[] names = { "얄랴셩", "서휼", "이진환", "프레드리크 프랑수아 쇼팽", "피트", "김민재"};
        // dialogue.characterName = names[Random.Range(0, names.Length)];

        // 랜덤 대사 생성
        string[] possibleSentences = {
            "오랜만이오 주인장.",
            "오늘 장사는 어떤가?",
            "라면 좀 주실 수 있소 주인장?",
            "날씨가 좋구려.",
            "오늘의 특선 메뉴 있소?",
            "하하, 라면은 역시 짜고 매워야지 안그래?"
        };

        int sentenceCount = Random.Range(3, 6); // 3~5개의 대사 생성
        dialogue.sentences = new List<SentenceData>(); // 새로운 List<SentenceData>로 초기화

        for (int i = 0; i < sentenceCount; i++)
        {
            SentenceData newSentence = new SentenceData
            {
                text = possibleSentences[Random.Range(0, possibleSentences.Length)], // 랜덤 대사 선택
                speaker = 0, // 기본 발화자 설정 (필요에 따라 0 또는 1로 설정 가능)
                emotion = "neutral" // 기본 감정 설정 (필요에 따라 다른 감정으로 설정 가능)
            };

            dialogue.sentences.Add(newSentence); // 리스트에 추가
        }

        return dialogue;
    }
}
