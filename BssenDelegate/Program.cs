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
        
        // 플레이어 사망 이벤트 발생
        // player.OnPlayerDie?.Invoke();
    }
}

class Player
{
    public string Name { get; }  // 읽기 전용 속성 프로퍼티
    private int _hp;             // 내부 필드
    
    public delegate void PlayerDieHandler();
    public event PlayerDieHandler OnPlayerDie;
    
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
        if (_hp <= 0) 
        {
            OnPlayerDie?.Invoke();
        }
    }
}