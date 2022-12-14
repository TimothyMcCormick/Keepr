using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _vaultkeeprepo;
    private readonly VaultsRepository _vaultrepo;
    private readonly VaultsService _vaultserv;
    private readonly KeepsService _keepserv;
    private readonly KeepsRepository _keeprepo;

    public VaultKeepsService(VaultKeepsRepository vaultkeeprepo, VaultsRepository vaultrepo, VaultsService vaultserv, KeepsService keepserv, KeepsRepository keeprepo)
    {
      _vaultkeeprepo = vaultkeeprepo;
      _vaultrepo = vaultrepo;
      _vaultserv = vaultserv;
      _keepserv = keepserv;
      _keeprepo = keeprepo;
    }

    internal VaultKeep Create(VaultKeep vaultKeepData, string userId)
    {
      Vault foundVault = _vaultserv.Get(vaultKeepData.VaultId, userId);
      Keep foundKeep = _keepserv.Get(vaultKeepData.KeepId);
      VaultKeep newVaultKeep = _vaultkeeprepo.Create(vaultKeepData);
      if (foundVault.CreatorId != userId)
      {
        throw new Exception("Access denied");
      }
      foundKeep.Kept++;
      _keeprepo.incKept(foundKeep);
      return newVaultKeep;
    }

    internal List<VaultKeepViewModel> Get(int id, string userId)
    {
      Vault vault = _vaultserv.Get(id, userId);
      List<VaultKeepViewModel> vaultKeeps = _vaultkeeprepo.GetByVaultId(id);
      if (vault.IsPrivate == true && vault.CreatorId != userId)
      {
        throw new Exception("You cannot access this vaults keeps");
      }
      return vaultKeeps;

    }

    // internal Vault GetVaultById(int id)
    // {
    //   return _vaultrepo.GetVaultById(id);
    // }

    internal VaultKeep GetById(int id)
    {

      VaultKeep found = _vaultkeeprepo.GetById(id);
      if (found == null)
      {
        throw new Exception("Invalid Id");
      }
      return found;
    }

    internal void Delete(int id, string userId)
    {
      VaultKeep found = GetById(id);
      Keep foundKeep = _keepserv.Get(found.KeepId);
      if (found.CreatorId != userId)
      {
        throw new Exception("You cannot delete this");
      }
      foundKeep.Kept--;
      _keeprepo.decKept(foundKeep);
      _vaultkeeprepo.Delete(id);
    }
  }
}