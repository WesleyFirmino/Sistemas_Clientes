using CaseTempus.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace CaseTempus.Models
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            RegisterDate = DateTime.Now;
        }

        [Key]
        public long Id { get; set; }

        [Display(Name = "Nome do Cliente")]
        public string Name { get; set; }

        [CPFValidate(ErrorMessage = "CPF inválido")]
        [Display(Name = "Número do CPF")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DataType(DataType.Date)]
        [Display(Name = "Data do Nascimento")]
        public DateTime BirthDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Cadastro")]
        public DateTime RegisterDate { get; set; }

        [Display(Name = "Renda Familiar")]
        public decimal FamilyIncome { get; set; }
    }
}
