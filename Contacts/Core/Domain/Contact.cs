using System.Collections.Generic;

namespace Contacts.Core.Domain
{
    public class Contact : EntityBase
    {
        public virtual string FirstLetter
        {
            get
            {
                if (!string.IsNullOrEmpty(LastName))
                    return LastName.Substring(0, 1);
                if (!string.IsNullOrEmpty(DisplayName))
                    return LastName.Substring(0, 1);
                return "#";
            }
        }

        public virtual string LastName { get; set; } = string.Empty;
        public virtual string FirstName { get; set; } = string.Empty;
        public virtual string Patronymic { get; set; } = string.Empty;
        public virtual string DisplayName { get; set; } = string.Empty;

        public virtual string Cabinet { get; set; } = string.Empty;
        public virtual string Position { get; set; } = string.Empty;
        public virtual string Department { get; set; } = string.Empty;

        public virtual IList<Phone> Phones { get; set; } = new List<Phone>();
    }
}
