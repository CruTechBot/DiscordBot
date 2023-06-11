using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Discord_Bot.DataModels
{
    [Table("Messages")]
    internal class MessageItem
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public ulong UserId { get; set; }
        public DateTime SentTime {get; set;}

        [ForeignKey("UserId")]
        public virtual UserItem User { get; set; }
    }
}
