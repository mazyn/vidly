using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public record Customer
    {
        public int Id { get; set; }
        [Required] [StringLength(255)] public string Name { get; set; }
        public DateTime? Birthdate { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }
        public MembershipType MembershipType { get; set; }
        public byte MembershipTypeId { get; set; }
    }
}