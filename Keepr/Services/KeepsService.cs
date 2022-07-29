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
  }
}