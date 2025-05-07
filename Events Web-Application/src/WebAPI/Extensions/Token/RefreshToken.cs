using Events_Web_Application.src.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Events_Web_Application.src.WebAPI.Extensions.Token
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public DateTime Created { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
