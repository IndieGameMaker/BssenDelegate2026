namespace BssenDelegate;

// 델리게이트 
// - 메서드 참조를 저장할 수 있는 형식
// int a = 10;
// 델리게이트 sum = Sum(10, 20);
// public void Sum (int a, int b) 

class Program
{
    // 1. 델리게이트 선언
    private delegate void LoggerDelegate(string msg);
    
    static void Main(string[] args)
    {
        // 2. 델리게이트 할당
        LoggerDelegate? log = null;
        
        // 3. 델리게이트 호출
        //log("델리게이트 호출 성공 1");
        
        // 4. 델리게이트 체인
        log += Logger;
        log += LoggerTime;
        
        log?.Invoke("델리게이트 호출 성공 : 체인");
        
        Console.WriteLine("정상 종료");
        
        // 5. 델리게이트 제거
        log -= Logger;
        log?.Invoke("델리게이트 호출 성공 : 체인 제거 후");
    }

    static void Logger(string msg)
    {
        Console.WriteLine(msg);
    }

    static void LoggerTime(string msg)
    {
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {msg}");
    }
}