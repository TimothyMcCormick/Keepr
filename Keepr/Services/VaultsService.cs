using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _vaultrepo;

    public VaultsService(VaultsRepository vaultrepo)
    {
      _vaultrepo = vaultrepo;
    }

    internal Vault Create(Vault vaultData)
    {
      return _vaultrepo.Create(vaultData);
    }

    internal Vault Get(int id)
    {
      Vault found = _vaultrepo.Get(id);
      if (found == null)
      {
        throw new Exception("Invalid Id");
      }
      return found;
    }

    internal Vault Edit(Vault vaultData)
    {
      Vault original = Get(vaultData.Id);

      original.Name = vaultData.Name ?? original.Name;
      original.Description = vaultData.Description ?? original.Description;

      _vaultrepo.Edit(original);
      return original;
    }

    internal void Delete(int id)
    {
      Vault foundVault = Get(id);
      _vaultrepo.Delete(id);
    }
  }
}