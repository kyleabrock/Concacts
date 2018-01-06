using System.Collections.Generic;
using Contacts.Core.Domain;
using Contacts.Core.Repository;
using NUnit.Framework;

namespace Contacts.Core.Tests
{
    [TestFixture]
    public class DomainTests
    {
        [Test]
        public void InitializeTypes()
        {
            var types = new List<PhoneType>()
            {
                new PhoneType {PhoneTypeName = "ОС"},
                new PhoneType {PhoneTypeName = "АТС-2"},
                new PhoneType {PhoneTypeName = "М-500"},
                new PhoneType {PhoneTypeName = "М-633"},
                new PhoneType {PhoneTypeName = "ГТС"}
            };

            IRepository<PhoneType> repository = new Repository<PhoneType>();
            repository.Save(types);
        }

        [Test]
        public void InitializeList()
        {
            var contact = new Contact()
            {
                LastName = "Мальцев", FirstName = "Денис", Patronymic = "Сергеевич", DisplayName = "Мальцев Д.С.",
                Cabinet = "1033В", Department = "ООФСЦ ОРМ СО ПС", Position = "Начальник группы",

                Phones = new List<Phone>
                {
                    new Phone
                    { PhoneNumber = "895106", PhoneType = new PhoneType { Id = 1 } },
                    new Phone
                    { PhoneNumber = "895103", PhoneType = new PhoneType { Id = 1 } }
                }
            };

            foreach (var phone in contact.Phones)
                phone.Contact = contact;

            var repository = new Repository<EntityBase>();
            repository.Save(contact);

            foreach (var phone in contact.Phones)
                repository.Save(phone);
        }
    }
}
