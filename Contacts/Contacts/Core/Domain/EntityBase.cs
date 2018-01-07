namespace Contacts.Core.Domain
{
    public class EntityBase
    {
        public virtual int Id { get; set; } = PlaceHolderId;

        public virtual bool IsNew => Id == PlaceHolderId;

        public virtual string Comments { get; set; } = string.Empty;

        private const int PlaceHolderId = -1;
    }
}
