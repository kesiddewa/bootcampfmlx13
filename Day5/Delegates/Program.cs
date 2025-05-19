using System;
namespace DelegatesDemo
{
    public delegate void CallbackMethodHandler(string message);

    class Program
    {
        static void Main(string[] args)
        {
            Program obj = new Program();
            CallbackMethodHandler del1 = new CallbackMethodHandler(obj.CallbackMethod);

            DoSomework(del1);

        }

        public static void DoSomework(CallbackMethodHandler del)
        {
            Console.WriteLine("Processing some Task");
            del("Pranaya");
        }

        public void     CallbackMethod(string message)
        {
            Console.WriteLine("CallbackMethod Executed");
            Console.WriteLine($"Hello: {message}, Good Morning");
        }
    }
}