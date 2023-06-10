using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Discord_Bot.DataModels
{
    [Table("Users")]
    internal class UserItem
    {

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        [Key]
        public ulong Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<MessageItem> Messages { get; set; }
    }
}
