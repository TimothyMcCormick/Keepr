using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _keeprepo;

    public KeepsService(KeepsRepository keeprepo)
    {
      _keeprepo = keeprepo;
    }

    internal Keep Create(Keep keepData)
    {
      return _keeprepo.Create(keepData);
    }

    internal List<Keep> Get()
    {
      return _keeprepo.Get();
    }

    internal Keep Get(int id)
    {
      Keep found = _keeprepo.Get(id);
      if (found == null)
      {
        throw new Exception("Invalid Id");
      }
      found.Views++;
      return found;
    }

    internal Keep Edit(Keep keepData)
    {
      Keep original = Get(keepData.Id);
      if (original.CreatorId != keepData.CreatorId)
      {
        throw new Exception("Invalid Access");
      }
      original.Name = keepData.Name ?? original.Name;
      original.Description = keepData.Description ?? original.Description;
      original.Img = keepData.Img ?? original.Img;

      _keeprepo.Edit(original);
      return original;
    }

    internal void Delete(int id, string userId)
    {
      Keep foundKeep = Get(id);
      if (foundKeep.CreatorId != userId)
      {
        throw new Exception("Invalid Access");
      }
      _keeprepo.Delete(id);
    }

    internal List<Keep> GetKeepsByCreatorId(string id)
    {
      return _keeprepo.GetKeepsByCreatorId(id);
    }

    internal object GetAccountKeeps(string userId)
    {
      return _keeprepo.GetAccountVaults(userId);
    }
  }
}