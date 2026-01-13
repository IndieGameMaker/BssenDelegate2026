namespace BssenDelegate;

// 델리게이트 
// - 메서드 참조를 저장할 수 있는 형식
// int a = 10;
// 델리게이트 sum = Sum(10, 20);
// public void Sum (int a, int b) 

class Program
{
    static void Main(string[] args)
    {
        Player player = new Player("Zack", 100);

        // 다른 클래스에서 발생하는 이벤트를 구독 (Subscribe)
        // 익명 메서드 (Anonymous Method) / 무명 델리게이트
        
        // 람다식 (Lambda Expression)
        // 이름 없는 아주 짧은 메서드 : 한번 사용하고 버리는 메서드
        // 문법 : (매개변수) => { 실행문; }
        // 옵저버 패턴 (Observer Pattern)
        player.OnHpChanged += DisplayHp;

        player.TakeDamage(20);
        player.TakeDamage(40);
        player.TakeDamage(40);

        // 이벤트 연결 해지 (Unsubscribe) : 구독 해지
        player.OnHpChanged -= DisplayHp;
    }

    static void DisplayHp(int hp)
    {
        Console.WriteLine($"플레이어 체력 변경됨: {hp}");
    }
}

class Player
{
    public string Name { get; }  // 읽기 전용 속성 프로퍼티
    private int _hp;             // 내부 필드
    
    // 이벤트를 위한 델리게이트 정의
    public delegate void PlayerDieHandler();
    // 이벤트 선언
    public event PlayerDieHandler OnPlayerDie;
    
    // 이벤트를 정의 및 선언
    // Action 
    // - .NET에서 제공하는 내장 델리게이트 
    public event Action<int> OnHpChanged;
    
    public Player(string name, int hp)
    {
        Name = name;
        _hp = hp;
        OnPlayerDie += Die;
    }

    public void Die()
    {
        Console.WriteLine("플레이어 사망");
    }

    public void TakeDamage(int damage)
    {
        _hp -= damage;
        OnHpChanged?.Invoke(_hp); // 이벤트 발생
        
        // Console.WriteLine($"Damage: {damage}, HP: {_hp}");
        if (_hp <= 0) 
        {
            OnPlayerDie?.Invoke(); // event raise
        }
    }
}