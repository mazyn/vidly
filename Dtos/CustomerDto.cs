using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Dtos
{
    public record CustomerDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter customer's name")]
        [StringLength(255)]
        public string Name { get; set; }

        public byte MembershipTypeId { get; set; }

        // [Min18YearsIfAMember] 
        public DateTime? Birthdate { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }
    }
}