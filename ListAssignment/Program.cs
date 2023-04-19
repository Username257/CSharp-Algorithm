namespace ListAssignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ListAssignment.List<string> list = new ListAssignment.List<String>();
            string? findValue = list.Find(x => x.Contains('4'));
        }
    }
}