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
      return found;
    }
  }
}