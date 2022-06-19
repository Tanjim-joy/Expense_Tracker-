using Expense01.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Expense01.Models
{
    public class Categories
    {
        public Categories()
        {
            this.DailyExpenses = new List<DailyExpense>();
        }
        [Key, Required]
        public int CategoriesId { get; set; }
        [Required, Display(Name = "Categories Name")]
        public String CName { get; set; }

        public virtual ICollection<DailyExpense> DailyExpenses { get; set; }
    }

    public class DailyExpense
    {
        [Key,Required]
        public int ExpenseId { get; set; }
        [Required,Display(Name ="Expense Name"), StringLength(20)]
        public string EName { get; set; }
        [Required,Column(TypeName="money")]
        public decimal Amount { get; set; }
        [Required, Display(Name = "Date"), FutureValidations(ErrorMessage="Please Enter Correct Date")]
        public DateTime EDate { get; set; }
        [Required, ForeignKey("Categories")]
        public int CategoriesId { get; set; }

        public virtual Categories Categories { get; set; }
    }
}
