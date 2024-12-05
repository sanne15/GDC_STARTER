using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public AudioManager audioManager;
    public List<GameObject> CustomerPrefabs; // Prefab List
    public GameObject MoneyEffectPrefab;
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
            audioManager.PlaySFX("DoorOpen");
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
        audioManager.PlaySFX("Money");
        GameObject Eff = Instantiate(MoneyEffectPrefab);        

        yield return customer.ExitShop(Random.value > 0.5f);
        audioManager.PlaySFX("DoorClose");
        Destroy(Eff);

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

        if(FindObjectOfType<DayManager>().currentDay == 8)
        {
            numberOfCustomers = 2;
        }
        else
        {
            numberOfCustomers = Random.Range(4, 6);
        }
        

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
            customer.dialogue.characterName = customerPrefab.GetComponent<Customer>().hangeulname + "_랜덤방문";

            customers.Add(customer);
        }

        if (FindObjectOfType<DayManager>().currentDay != 8 && FindObjectOfType<DayManager>().currentDay != 1)
        {
            List<Customer> lister = new List<Customer>(customers);

            // List의 요소들을 섞음
            for (int i = lister.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                Customer temper = lister[i];
                lister[i] = lister[j];
                lister[j] = temper;
            }

            // 섞인 List를 Queue로 변환
            customers = lister;
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
            "오랜만이에요 (플레이어).",
            "오늘 장사는 좀 잘 되어가나요?",
            "라면 좀 주실 수 있나요, (이름)?",
            "요새 날씨가 나쁘지 않네요.",
            "요새 좀 습하지 않아요?",
            "요새 가장 인기 있는 메뉴가 뭐에요?",
            "하하, 라면은 역시 짜고 매워야죠.",
            "듣기 싫은 말은 Shift + X로 스킵할 수 있다더군요.",
            "이 집이 이 마을에서 라면을 가장 잘 끓이는 맛집이에요."
        };

        int sentenceCount = Random.Range(2, 4); // 3~5개의 대사 생성
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
