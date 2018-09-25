using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagerAPI.Models
{
    public class Task
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Порядковый номер
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// Срок
        /// </summary>
        public DateTime? TermDate { get; set; }
        /// <summary>
        /// Признак закрытой задачи
        /// </summary>
        public bool IsClosed { get; set; }
    }
}
