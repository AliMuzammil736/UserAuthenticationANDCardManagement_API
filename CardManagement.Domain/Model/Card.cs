using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardManagement.Domain.Utils;

namespace CardManagement.Domain.Model
{
    public class Card : BaseModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(30)]
        [Column(TypeName = "nvarchar(30)")]
        public string CardNumber { get; set; }

        [Required]
        public DateOnly ExpiryDate { get; set; }

        [Required]
        [StringLength(3)]
        [Column(TypeName = "nvarchar(3)")]
        public string CVV { get; set; }

        [Required]
        public decimal Balance { get; set; }

        [Required]
        public Enums.CardStatus Status { get; set; } = Enums.CardStatus.Freeze;

        [StringLength(4)]
        [Column(TypeName = "nvarchar(4)")]
        public string? PIN { get; set; }

        [Required]
        [StringLength(255)]
        [Column(TypeName = "nvarchar(255)")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        [Column(TypeName = "nvarchar(255)")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string PhoneNumber { get; set; }

        [Required]
        public string IDPhoto { get; set; }

        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public SysUser SysUser { get; set; }
    }
}
