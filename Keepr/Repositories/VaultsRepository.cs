using System;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Vault Create(Vault vaultData)
    {
      string sql = @"
      INSERT INTO vaults
      (name, description, isPrivate, creatorId)
      VALUES
      (@Name, @Description, @IsPrivate, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, vaultData);
      vaultData.Id = id;
      vaultData.CreatedAt = new DateTime();
      vaultData.UpdatedAt = new DateTime();
      return vaultData;
    }

    internal Vault Get(int id)
    {
      string sql = @"
      SELECT
        a.*,
        v.*
      FROM vaults v
      JOIN accounts a ON a.id = v.creatorId
      WHERE v.id = @id;
      ";
      return _db.Query<Profile, Vault, Vault>(sql, (acct, vault) =>
      {
        vault.Creator = acct;
        return vault;
      }, new { id }).FirstOrDefault();
    }

    internal void Edit(Vault original)
    {
      string sql = @"
      UPDATE vaults
        SET
          name = @Name,
          description = @Description
      WHERE id = @Id;
      ";
      _db.Execute(sql, original);
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM vaults WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }
  }
}