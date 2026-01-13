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

        player.Heal(30);
        
        player.TakeDamage(40);

        // 이벤트 연결 해지 (Unsubscribe) : 구독 해지
        player.OnHpChanged -= DisplayHp;
    }

    static void DisplayHp(int hp)
    {
        Console.WriteLine($"플레이어 체력 변경됨: {hp}");
    }
}

class Player : IHealable
{
    public string Name { get; }  // 읽기 전용 속성 프로퍼티
    private int _hp;             // 내부 필드
    
    // 이벤트를 위한 델리게이트 정의
    public delegate void PlayerDieHandler();
    // 이벤트 선언
    public event PlayerDieHandler OnPlayerDie;
    
    // 이벤트를 정의 및 선언
    // Action 델리게이트
    // - .NET에서 제공하는 내장 델리게이트 
    // Action<T> : 매개변수는 T형식, 반환형은 void
    // Action<T1, T2, T3, ...> : 매개변수는 T1, T2, T3,... 형식, 16개까지 가능, 반환형은 void
    public event Action<int> OnHpChanged;
    
    // Func 델리게이트 (반환형이 있는 경우)
    // Func<T1, T2, TResult> : 매개변수는 T1, T2 형식, 반환형은 TResult
    
    // (현재 HP, 회복량) => 회복 후 HP
    public event Func<int, int, int> OnHealing = (currHp, healAmount) => currHp + healAmount;
    
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

    public void Heal(int amount)
    {
        _hp = OnHealing(_hp, amount);
        Console.WriteLine($"HP 회복됨: {_hp} [회복량: {amount}]");
    }
}

// 힐러 인터페이스
interface IHealable
{
    void Heal(int amount);
}