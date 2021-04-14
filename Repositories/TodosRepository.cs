using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Models;

namespace Repositories
{
    public class TodosRepository
    {

        public readonly IDbConnection _db;

        public TodosRepository(IDbConnection db)
        {
            _db = db;
        }

        internal IEnumerable<Todo> Get()
        {
            string sql = "SELECT * FROM todos;";
            return _db.Query<Todo>(sql);
        }

        internal Todo Create(Todo todo)
        {
            string sql = @"
            INSERT INTO todos
            (title, completed, creatorId)
            VALUES
            (@Title, @Completed, @CreatorId);
            SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, todo);
            todo.Id = id;
            return todo;
        }

        internal Todo Get(int id)
        {
            string sql = "SELECT * FROM todos WHERE id = @id;";
            return (_db.QueryFirstOrDefault<Todo>(sql, new { id }));
        }

        internal Todo Edit(Todo todo)
        {
            string sql = @"
            UPDATE todos
            SET
                title = @Title,
                completed = @Completed
            WHERE id = @Id;";
            _db.Execute(sql, todo);
            return todo;
        }

        internal void Delete(int id)
        {
            string sql = @"DELETE FROM todos WHERE id = @Id LIMIT 1;";
            _db.Execute(sql, new { id });
        }
    }
}