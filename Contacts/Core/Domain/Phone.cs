namespace Contacts.Core.Domain
{
    public class Phone : EntityBase
    {
        public virtual Contact Contact { get; set; }
        public virtual PhoneType PhoneType { get; set; } = new PhoneType();
        public virtual string PhoneNumber { get; set; } = string.Empty;
    }
}
