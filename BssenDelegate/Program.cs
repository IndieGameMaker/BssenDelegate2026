namespace BssenDelegate;

// 델리게이트 
// - 메서드 참조를 저장할 수 있는 형식
// int a = 10;
// 델리게이트 sum = Sum(10, 20);
// public void Sum (int a, int b) 

class Program
{
    // 1. 델리게이트 선언
    private delegate void LoggerDelegate();
    
    static void Main(string[] args)
    {
        // 2. 델리게이트 할당
        LoggerDelegate log = Logger;
        
        // 3. 델리게이트 호출
        log();
    }

    static void Logger()
    {
        Console.WriteLine("델리게이트 호출 : Hello, World!");
    }
}