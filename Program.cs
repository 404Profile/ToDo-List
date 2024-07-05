using System;

namespace TodoListApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "todo_list.txt";
            TodoListManager todoListManager = new TodoListManager(filePath);
            todoListManager.Run();
        }
    }
}