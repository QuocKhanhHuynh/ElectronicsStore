using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ElectronicsStore.BackendApi.Data.Entities
{
    [Table("VAITRO")]
    public class Role : IdentityRole
    {
    }
}
