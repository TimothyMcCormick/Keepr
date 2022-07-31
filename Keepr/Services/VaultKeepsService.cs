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

    public VaultKeepsService(VaultKeepsRepository vaultkeeprepo, VaultsRepository vaultrepo, VaultsService vaultserv)
    {
      _vaultkeeprepo = vaultkeeprepo;
      _vaultrepo = vaultrepo;
      _vaultserv = vaultserv;
    }

    internal VaultKeep Create(VaultKeep vaultKeepData)
    {
      return _vaultkeeprepo.Create(vaultKeepData);
    }

    internal List<VaultKeepViewModel> Get(int id)
    {
      Vault vault = _vaultserv.Get(id);
      List<VaultKeepViewModel> vaultKeeps = _vaultkeeprepo.GetByVaultId(id);
      if (vault.IsPrivate == true)
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

    internal void Delete(int id)
    {
      GetById(id);
      _vaultkeeprepo.Delete(id);
    }
  }
}