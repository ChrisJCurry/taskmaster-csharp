using System;
using System.Collections.Generic;
using Models;
using Repositories;

namespace Services
{
    public class TodosService
    {

        private readonly TodosRepository _todoRepo;
        private readonly GoalsRepository _goalRepo;

        public TodosService(TodosRepository todoRepo)
        {
            _todoRepo = todoRepo;
        }

        internal IEnumerable<Todo> Get()
        {
            return (_todoRepo.Get());
        }

        internal Todo Get(int id)
        {
            return (_todoRepo.Get(id));
        }

        internal IEnumerable<TodoGoal> GetByTodo(int id)
        {
            return (_goalRepo.GetByTodo(id));
        }

        internal Todo Create(Todo todo)
        {
            return (_todoRepo.Create(todo));
        }

        internal Todo Edit(Todo todo)
        {
            Todo original = Get(todo.Id);
            if (original.CreatorId != todo.CreatorId)
            {
                throw new Exception("You can't edit this.");
            }
            original.Title = todo.Title != null ? todo.Title : original.Title;
            original.Completed = todo.Completed != null ? todo.Completed : original.Completed;

            return (_todoRepo.Edit(todo));
        }

        internal void Delete(int id, string userId)
        {
            Todo original = Get(id);
            if (original.CreatorId != userId)
            {
                throw new Exception("You can't delete this.");
            }
            _todoRepo.Delete(id);
        }
    }
}