using System;
using System.Collections.Generic;
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

    private Vault AllowAccess(int id, string userId)
    {
      Vault original = Get(id, userId);
      if (original.IsPrivate == true || original.CreatorId != userId)
      {
        throw new Exception("Access Denied");
      }
      return original;
    }



    internal Vault Create(Vault vaultData)
    {
      return _vaultrepo.Create(vaultData);
    }

    internal Vault Get(int id, string userId)
    {
      Vault found = _vaultrepo.Get(id);
      if (found == null)
      {
        throw new Exception("Invalid Id");
      }
      if (found.IsPrivate == true && found.CreatorId != userId)
      {
        throw new Exception("Access denied");
      }


      return found;
    }



    internal Vault Edit(Vault vaultData, string userId)
    {
      Vault original = AllowAccess(vaultData.Id, vaultData.CreatorId);


      original.Name = vaultData.Name ?? original.Name;
      original.Description = vaultData.Description ?? original.Description;
      original.Img = vaultData.Img ?? original.Img;
      original.IsPrivate = vaultData.IsPrivate ?? original.IsPrivate;

      _vaultrepo.Edit(original);
      original.UpdatedAt = new DateTime();
      return original;
    }

    internal void Delete(int id, string userId)
    {
      Vault foundVault = Get(id, userId);
      if (foundVault.CreatorId != userId)
      {
        throw new Exception("You cannot delete this");
      }
      else if (foundVault.IsPrivate == true && foundVault.CreatorId != userId)
      {
        throw new Exception("Access Denied");
      }
      _vaultrepo.Delete(id);
    }

    internal List<Vault> GetVaultsByCreatorId(string id)
    {
      return _vaultrepo.GetVaultsByCreatorId(id);
    }

    internal List<Vault> GetAccountVaults(string userId)
    {
      return _vaultrepo.GetAccountVaults(userId);
    }
  }
}