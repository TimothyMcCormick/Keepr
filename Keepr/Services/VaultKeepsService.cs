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
  }
}