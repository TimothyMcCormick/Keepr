using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal VaultKeep Create(VaultKeep vaultKeepData)
    {
      string sql = @"
      INSERT INTO vaultkeeps
      (keepId, vaultId, creatorId)
      VALUES
      (@KeepId, @VaultId, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      vaultKeepData.Id = _db.ExecuteScalar<int>(sql, vaultKeepData);
      return vaultKeepData;
    }

    // internal VaultKeep CheckForExists(VaultKeep vaultKeepData)
    // {
    //   string sql = "SELECT * FROM vaultkeeps WHERE vaultId = @VaultId AND keepId = @KeepId LIMIT 1";
    //   return _db.QueryFirstOrDefault<VaultKeep>(sql, vaultKeepData);
    // }

    internal VaultKeep GetById(int id)
    {
      string sql = "SELECT * FROM vaultkeeps WHERE id = @id";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
    }

    internal List<VaultKeepViewModel> GetByVaultId(int id)
    {
      string sql = @"
      SELECT
      a.*,
      k.*,
      vk.id AS VaultKeepId
      FROM vaultkeeps vk
      JOIN keeps k ON k.id = vk.keepId
      JOIN accounts a ON a.id = k.creatorId
      WHERE vk.vaultId = @id
      ";
      return _db.Query<Profile, VaultKeepViewModel, VaultKeepViewModel>(sql, (prof, vkvm) =>
      {
        vkvm.Creator = prof;
        return vkvm;
      }, new { id }).ToList();
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM vaultkeeps WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }
  }
}