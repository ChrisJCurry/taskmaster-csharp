using System;
using System.Collections.Generic;
using Models;
using Repositories;

namespace Services
{
    public class GoalsService
    {

        private readonly GoalsRepository _goalRepo;

        public GoalsService(GoalsRepository goalRepo)
        {
            _goalRepo = goalRepo;
        }

        internal IEnumerable<Goal> Get()
        {
            return (_goalRepo.Get());
        }

        internal Goal Get(int id)
        {
            return (_goalRepo.Get(id));
        }

        internal IEnumerable<TodoGoal> GetByTodo(int id)
        {
            return (_goalRepo.GetByTodo(id));
        }

        internal Goal Create(Goal goal)
        {
            return (_goalRepo.Create(goal));
        }

        internal Goal Edit(Goal goal)
        {
            Goal original = Get(goal.Id);
            if (original.CreatorId != goal.CreatorId)
            {
                throw new Exception("you can't edit this.");
            }
            return (_goalRepo.Edit(goal));
        }
    }
}