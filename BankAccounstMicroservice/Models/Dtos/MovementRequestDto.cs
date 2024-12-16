using BankAccountsMicroservice.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAccounstMicroservice.Models.Dtos
{
    public class MovementRequestDto
    {
        public DateTime Date { get; set; }

        public string? Type { get; set; }

        public float Ammount { get; set; }

        public string AccountNumber { get; set; }
    }
}
