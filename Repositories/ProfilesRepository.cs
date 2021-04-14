using System;
using System.Collections.Generic;
using System.Data;
using Models;
using Dapper;

namespace Repositories
{
    public class ProfilesRepository
    {
        private readonly IDbConnection _db;

        public ProfilesRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Profile GetById(string id)
        {
            string sql = "SELECT * FROM profiles WHERE id = @id";
            return _db.QueryFirstOrDefault<Profile>(sql, new { id });
        }

        internal Profile Create(Profile newProfile)
        {
            string sql = @"
            INSERT INTO profiles
              (name, nickName, picture, email, id)
            VALUES
              (@Name, @NickName, @Picture, @Email, @Id)";
            _db.Execute(sql, newProfile);
            return newProfile;
        }

        internal Profile Edit(Profile update)
        {
            string sql = @"
            UPDATE profiles
            SET 
              name = @Name,
              nickName = @NickName,
              picture = @Picture
            WHERE id = @Id;";
            _db.Execute(sql, update);
            return update;
        }
    }
}