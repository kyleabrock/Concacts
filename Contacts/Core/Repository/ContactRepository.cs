using System.Collections.Generic;
using Contacts.Core.Domain;
using NHibernate;
using NHibernate.Criterion;

namespace Contacts.Core.Repository
{
    public class ContactRepository : Repository<Contact>
    {
        public IList<Contact> GetAll(bool lazy = true)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var result = session.QueryOver<Contact>().List();

                if (!lazy)
                {
                    foreach (var contact in result)
                    {
                        NHibernateUtil.Initialize(contact.Phones);
                        foreach (var phone in contact.Phones)
                            NHibernateUtil.Initialize(phone.PhoneType);
                    }
                }

                return result;
            }
        }

        public IList<Contact> Find(string searchString)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ICriterion lastName = Restrictions.Like("LastName", searchString, MatchMode.Anywhere);
                ICriterion firstName = Restrictions.Like("FirstName", searchString, MatchMode.Anywhere);
                ICriterion patronymic = Restrictions.Like("Patronymic", searchString, MatchMode.Anywhere);
                ICriterion displayName = Restrictions.Like("DisplayName", searchString, MatchMode.Anywhere);
                ICriterion position = Restrictions.Like("Position", searchString, MatchMode.Anywhere);
                ICriterion department = Restrictions.Like("Department", searchString, MatchMode.Anywhere);

                ICriterion criterion = Restrictions.Or(lastName, firstName);
                criterion = Restrictions.Or(criterion, patronymic);
                criterion = Restrictions.Or(criterion, displayName);
                criterion = Restrictions.Or(criterion, position);
                criterion = Restrictions.Or(criterion, department);

                var result = session.CreateCriteria<Contact>().Add(criterion).List<Contact>();
                foreach (var contact in result)
                {
                    NHibernateUtil.Initialize(contact.Phones);
                    foreach (var phone in contact.Phones)
                        NHibernateUtil.Initialize(phone.PhoneType);
                }

                return result;
            }
        }
    }
}

