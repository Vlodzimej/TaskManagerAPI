using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagerAPI.Models
{
    public class Index
    {
        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Значение индекса
        /// </summary>
        public int Value { get; set; }
    }
}
