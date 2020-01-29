using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace Models
{
    public class ToDoList
    {
        public int Id { get; set;}

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public DateTime Duration { get; set; }


        public string UserId { get; set; }

        public ApplicationViewModel AppUser { get; set; }
    }
}
