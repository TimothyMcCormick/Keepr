using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Keep Create(Keep keepData)
    {
      string sql = @"
      INSERT INTO keeps
      (name, description, img, views, kept, creatorId)
      VALUES
      (@Name, @Description, @Img, @Views, @Kept, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, keepData);
      keepData.Id = id;
      keepData.CreatedAt = new DateTime();
      keepData.UpdatedAt = new DateTime();
      return keepData;
    }

    internal void incViews(Keep found)
    {
      string sql = @"
      UPDATE keeps
        SET
        views = @Views
      WHERE id = @Id;
      ";
      _db.Execute(sql, found);
    }

    internal void incKept(Keep foundKeep)
    {
      string sql = @"
      UPDATE keeps
        SET
        kept = @Kept
      WHERE id = @Id;
      ";
      _db.Execute(sql, foundKeep);
    }

    internal void decKept(Keep foundKeep)
    {
      string sql = @"
      UPDATE keeps
        SET
        kept = @Kept
      WHERE id = @Id;
      ";
      _db.Execute(sql, foundKeep);
    }

    internal List<Keep> Get()
    {
      string sql = @"
      SELECT
      a.*,
      k.*
      FROM keeps k
      JOIN accounts a ON a.id = k.creatorId;
      ";
      return _db.Query<Profile, Keep, Keep>(sql, (acct, keep) =>
      {

        keep.Creator = acct;
        return keep;
      }).ToList();
    }



    internal Keep Get(int id)
    {
      string sql = @"
      SELECT
      a.*,
      k.*
      FROM keeps k
      JOIN accounts a ON a.id = k.creatorId
      
      WHERE k.id = @id;
      ";
      return _db.Query<Profile, Keep, Keep>(sql, (acct, keep) =>
      {
        keep.Creator = acct;

        return keep;
      }, new { id }).FirstOrDefault();
    }



    internal List<Keep> GetKeepsByCreatorId(string creatorId)
    {
      string sql = @"
        SELECT 
        a.*,
        k.* 
        FROM keeps k
        JOIN accounts a ON a.id = k.creatorId
        WHERE k.creatorId = @creatorId";
      return _db.Query<Profile, Keep, Keep>(sql, (acct, keeps) =>
      {
        keeps.Creator = acct;
        return keeps;
      }, new { creatorId }).ToList();
    }



    internal object GetAccountVaults(string userId)
    {
      string sql = @"
      SELECT *
      FROM keeps
      WHERE creatorId = @userId
      ";
      return _db.Query<Vault>(sql, new { userId }).ToList();
    }

    internal void Edit(Keep original)
    {
      string sql = @"
      UPDATE keeps
        SET
        name = @Name,
        description = @Description,
        img = @Img
      WHERE id = @Id;
      ";
      _db.Execute(sql, original);
    }



    internal void Delete(int id)
    {
      string sql = "DELETE FROM keeps WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }

  }
}