using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Models;

namespace Repositories
{
    public class GoalsRepository
    {

        public readonly IDbConnection _db;

        public GoalsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal IEnumerable<Goal> Get()
        {
            string sql = "SELECT * FROM goals;";
            return _db.Query<Goal>(sql);
        }

        internal IEnumerable<TodoGoal> GetByTodo(int id)
        {
            string sql = @"
            SELECT
            g.*,
            t.*
            FROM goals g
            JOIN todos t ON g.todoId = t.id
            WHERE todoId = @id;";

            return _db.Query<TodoGoal, Todo, TodoGoal>(sql, (goal, todo) =>
            {
                goal.Todo = todo;
                return goal;
            }, new { id }, splitOn: "id");
        }

        internal Goal Get(int id)
        {
            string sql = "SELECT * FROM goals WHERE id = @id;";
            return _db.QueryFirstOrDefault<Goal>(sql, new { id });
        }

        internal Goal Edit(Goal goal)
        {
            string sql = @"
            UPDATE goals
            SET
                description = @Description,
                completed = @Completed
            WHERE id = @Id;";
            _db.Execute(sql, goal);
            return goal;
        }

        internal Goal Create(Goal goal)
        {
            string sql = @"
            INSERT INTO goals
            (description, completed, creatorId, todoId)
            VALUES
            (@Description, @Completed, @CreatorId, @TodoId);
            SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, goal);
            goal.Id = id;
            return goal;
        }
    }
}