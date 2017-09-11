using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ToDoApp.Models
{
    /// <summary>
    /// To do model    
    /// </summary>
    [Table("ToDo")]
    public class ToDoItem
    {
        /// <summary>
        /// To do Id    
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// To do Name    
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// DeadLine or due date    
        /// </summary>
        [Required]
        public DateTime DeadLine { get; set; }

        /// <summary>
        /// Notes about the to do item    
        /// </summary>
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }

        /// <summary>
        /// IsComplete   
        /// </summary>
        [Required]
        public Boolean IsComplete { get; set; }
    }
}
