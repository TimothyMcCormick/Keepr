using Keepr.Models;

namespace Keepr.Interfaces
{
  public interface ICreated
  {
    string CreatorId { get; set; }
    Profile Creator { get; set; }
  }
}