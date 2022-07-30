using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _vaultkeeprepo;

    public VaultKeepsService(VaultKeepsRepository vaultkeeprepo)
    {
      _vaultkeeprepo = vaultkeeprepo;
    }

    internal VaultKeep Create(VaultKeep vaultKeepData)
    {
      return _vaultkeeprepo.Create(vaultKeepData);
    }

    internal List<VaultKeepViewModel> Get(int id)
    {
      return _vaultkeeprepo.GetByKeepId(id);
    }

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