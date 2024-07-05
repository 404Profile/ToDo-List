using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TodoListItem;

namespace TodoListApp
{
    /// <summary>
    /// Класс, управляющий списком задач.
    /// </summary>
    internal class TodoListManager
    {
        private List<TaskItem> tasks;
        private string filePath;

        /// <summary>
        /// Создает экземпляр класса TodoListManager с указанным путем к файлу.
        /// </summary>
        /// <param name="filePath">Путь к файлу списка задач.</param>
        public TodoListManager(string filePath)
        {
            this.filePath = filePath;
            tasks = new List<TaskItem>();
        }

        /// <summary>
        /// Запускает приложение Todo List.
        /// </summary>
        public void Run()
        {
            LoadTasksFromFile();

            while (true)
            {
                Console.WriteLine("=== Todo List App ===");
                Console.WriteLine("1. Добавить задачу");
                Console.WriteLine("2. Просмотреть задачи");
                Console.WriteLine("3. Отметить задачу как выполненную");
                Console.WriteLine("4. Удалить задачу");
                Console.WriteLine("5. Показать выполненные задачи");
                Console.WriteLine("6. Показать невыполненные задачи");
                Console.WriteLine("7. Сохранить и выйти");

                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddTask();
                        break;
                    case "2":
                        ViewTasks();
                        break;
                    case "3":
                        MarkTaskAsCompleted();
                        break;
                    case "4":
                        DeleteTask();
                        break;
                    case "5":
                        FilterTasksByStatus(true);
                        break;
                    case "6":
                        FilterTasksByStatus(false);
                        break;
                    case "7":
                        SaveTasksToFile();
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте еще раз.");
                        break;
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Добавляет новую задачу в список.
        /// </summary>
        public void AddTask()
        {
            Console.Write("Введите новую задачу: ");
            string description = Console.ReadLine();

            TaskItem task = new TaskItem(description);
            tasks.Add(task);

            Console.WriteLine("Задача успешно добавлена.");
        }

        /// <summary>
        /// Выводит список задач на экран.
        /// </summary>
        public void ViewTasks()
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("Список задач пуст.");
            }
            else
            {
                Console.WriteLine("Список задач:");
                for (int i = 0; i < tasks.Count; i++)
                {
                    TaskItem task = tasks[i];
                    Console.WriteLine($"{i + 1}. {task.Description} - {(task.Completed ? "Выполнено" : "Не выполнено")}");
                }
            }
        }

        /// <summary>
        /// Отмечает задачу с указанным номером как выполненную.
        /// </summary>
        public void MarkTaskAsCompleted()
        {
            Console.Write("Введите номер задачи, которую хотите отметить как выполненную: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= tasks.Count)
            {
                TaskItem task = tasks[index - 1];
                task.Completed = true;
                Console.WriteLine("Задача отмечена как выполненная.");
            }
            else
            {
                Console.WriteLine("Неверный номер задачи.");
            }
        }

        /// <summary>
        /// Удаляет задачу с указанным номером из списка.
        /// </summary>
        public void DeleteTask()
        {
            Console.Write("Введите номер задачи, которую хотите удалить: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= tasks.Count)
            {
                TaskItem task = tasks[index - 1];
                tasks.Remove(task);
                Console.WriteLine("Задача успешно удалена.");
            }
            else
            {
                Console.WriteLine("Неверный номер задачи.");
            }
        }

        /// <summary>
        /// Фильтрует задачипо указанному статусу выполнения.
        /// </summary>
        /// <param name="completed">Статус выполнения задачи (true - выполнена, false - не выполнена).</param>
        public void FilterTasksByStatus(bool completed)
        {
            var filteredTasks = tasks.Where(t => t.Completed == completed).ToList();

            if (filteredTasks.Count == 0)
            {
                Console.WriteLine(completed ? "Нет выполненных задач." : "Нет невыполненных задач.");
            }
            else
            {
                Console.WriteLine(completed ? "Выполненные задачи:" : "Невыполненные задачи:");
                for (int i = 0; i < filteredTasks.Count; i++)
                {
                    TaskItem task = filteredTasks[i];
                    Console.WriteLine($"{i + 1}. {task.Description}");
                }
            }
        }

        /// <summary>
        /// Загружает список задач из файла.
        /// </summary>
        private void LoadTasksFromFile()
        {
            if (File.Exists(filePath))
            {
                try
                {
                    string[] lines = File.ReadAllLines(filePath);
                    tasks.Clear();

                    foreach (string line in lines)
                    {
                        string description = line.Trim();
                        if (!string.IsNullOrEmpty(description))
                        {
                            TaskItem task = new TaskItem(description);
                            tasks.Add(task);
                        }
                    }

                    Console.WriteLine("Список задач успешно загружен из файла.");
                }
                catch (IOException ex)
                {
                    Console.WriteLine("Ошибка ввода-вывода при загрузке списка задач из файла: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка при загрузке списка задач из файла: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Файл списка задач не найден. Будет создан новый файл при сохранении.");
            }
        }

        /// <summary>
        /// Сохраняет список задач в файл.
        /// </summary>
        private void SaveTasksToFile()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var task in tasks)
                    {
                        writer.WriteLine(task.Description);
                    }
                }
                Console.WriteLine("Список задач успешно сохранен в файл.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при сохранении списка задач в файл: " + ex.Message);
            }
        }
    }
}