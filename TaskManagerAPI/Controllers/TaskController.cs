using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.Models;

namespace TaskManager.Controllers
{
    [Produces("application/json")]
    [Route("api/Task")]
    public class TaskController : Controller
    {
        private readonly TaskManagerContext _taskManagerContext;
        public TaskController(TaskManagerContext taskManagerContext)
        {
            _taskManagerContext = taskManagerContext;
        }
        // GET: api/Task
        [HttpGet]
        public IEnumerable<object> Get()
        {
            var tasks = _taskManagerContext.Tasks;
            if (tasks != null && tasks.Any())
            {
                var result = tasks
                    .OrderBy(x => x.Index)
                    .Select(x => new
                    {
                        id = x.Id,
                        index = x.Index,
                        title = x.Title,
                        content = x.Content,
                        isClosed = x.IsClosed,
                        termDate = x.TermDate.HasValue ? x.TermDate.Value.ToString("yyyy-MM-dd") : ""
                    });
                return result;
            }
            return null;
        }

        // GET: api/Task/5
        [HttpGet("{taskId}", Name = "Get")]
        public Task Get(Guid taskId)
        {
            return _taskManagerContext.Tasks.FirstOrDefault(x => x.Id == taskId);
        }

        // POST: api/Task
        [HttpPost]
        public IEnumerable<object> Post([FromBody]Task task)
        {
            if (task != null)
            {
                var savedTask = _taskManagerContext.Tasks.FirstOrDefault(x => x.Id == task.Id);
                if (savedTask != null)
                {
                    savedTask.Title = task.Title;
                    savedTask.Content = task.Content;
                    savedTask.TermDate = task.TermDate;
                    savedTask.IsClosed = task.IsClosed;
                    savedTask.Index = task.Index;
                }
                else
                {
                    var tasks = _taskManagerContext.Tasks;
                    var lastIndex = tasks.Any() ? tasks.Select(x => x.Index).Max() : 0;
                    var number = tasks.Count() + 1;

                    task.Id = Guid.NewGuid();
                    task.Title = $"Новая задача {number}";
                    task.TermDate = null;
                    task.Index = lastIndex > 0 ? lastIndex + 1 : 0;

                    _taskManagerContext.Tasks.Add(task);
                }
                _taskManagerContext.SaveChanges();
            }
            var result = Get();
            return result;
        }

        // PUT: api/Task/5
        [HttpPut("{taskId}")]
        public void Put(Guid taskId, [FromBody]Task modifiedTask)
        {
            var task = _taskManagerContext.Tasks.FirstOrDefault(x => x.Id == taskId);
            if (task != null && task.Id == modifiedTask.Id)
            {
                task.Title = modifiedTask.Title;
                task.Content = modifiedTask.Content;
                task.TermDate = modifiedTask.TermDate;
                _taskManagerContext.Update(task);
                _taskManagerContext.SaveChanges();
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{taskId}")]
        public IEnumerable<object> Delete(Guid taskId)
        {
            var task = _taskManagerContext.Tasks.FirstOrDefault(x => x.Id == taskId);
            if (task != null)
            {
                var tasks = _taskManagerContext.Tasks.Where(x => x.Index > task.Index).ToList();
                foreach(var item in tasks)
                {
                    item.Index = item.Index - 1;
                }

                _taskManagerContext.Tasks.Remove(task);
                _taskManagerContext.SaveChanges();
            }
            return Get();
        }

        [HttpPost("newindexes")]
        public IEnumerable<object> ChangeIndexes([FromBody]IEnumerable<Index> indexes)
        {
            var tasks = _taskManagerContext.Tasks;
            foreach (var task in tasks)
            {
                var index = indexes.FirstOrDefault(x => x.Id == task.Id);
                task.Index = index.Value;
            }
            _taskManagerContext.SaveChanges();
            return Get();
        }
    }
}