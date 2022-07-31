using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class ProfilesService
  {
    private readonly ProfilesRepository _profrepo;

    public ProfilesService(ProfilesRepository profrepo)
    {
      _profrepo = profrepo;
    }

    internal Profile GetProfileById(string id)
    {
      Profile found = _profrepo.GetProfileById(id);
      if (found == null)
      {
        throw new Exception("Invalid Id");
      }
      return found;
    }
  }
}