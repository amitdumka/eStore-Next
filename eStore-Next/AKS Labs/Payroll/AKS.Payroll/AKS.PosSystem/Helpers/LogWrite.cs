/*
 * @Author: Amit Kumar
 * @Version: 1.0
 * @Date: 30/08/2022
 * @Copyrite: Aks Labs(Amit Kumar)
 * @AuthurEmail: amit.dumka@gmail.com
 */

namespace AKS.PosSystem.Helpers
{
    //TODO: Move to proper location
    public static class LogWrite
    {
        public static void Log(string msg)
        { Console.Write(DateTime.Now.ToString()); Console.WriteLine(msg); }

        public static void LogError(string msg)
        { Console.Write(DateTime.Now.ToString()); Console.WriteLine(msg); }

        public static void LogWarning(string msg)
        { Console.Write(DateTime.Now.ToString()); Console.WriteLine(msg); }

        public static void LogInfo(string msg)
        { Console.Write(DateTime.Now.ToString()); Console.WriteLine(msg); }

        public static void LogError(Exception ex)
        {
            Console.Write(DateTime.Now.ToString());
            Console.WriteLine(ex.Message);
            if (ex.StackTrace != null)
                Console.WriteLine(ex.StackTrace);
        }
    }

}