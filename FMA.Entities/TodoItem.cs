using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMA.Entities
{
    [Table("TodoItems")]
    public class TodoItem
    {
        [Key] 
        public long Id { get; set; }
        public string Content { get; set; }

        public bool? IsDone { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public long AccountId { get; set; }
    }
}
