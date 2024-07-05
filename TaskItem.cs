using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListItem
{
    /// <summary>
    /// Класс, представляющий отдельную задачу в списке задач.
    /// </summary>
    internal class TaskItem
    {
        /// <summary>
        /// Описание задачи.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Статус выполнения задачи.
        /// </summary>
        public bool Completed { get; set; }

        /// <summary>
        /// Создает новый экземпляр класса TaskItem с указанным описанием.
        /// По умолчанию задача считается невыполненной.
        /// </summary>
        /// <param name="description">Описание задачи.</param>
        public TaskItem(string description)
        {
            Description = description;
            Completed = false;
        }
    }
}
