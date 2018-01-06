using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Contacts.Core.Tests
{
    [TestFixture]
    public class NHibernateTests
    {
        [Test]
        public void GenerateSchema()
        {
            NHibernateHelper.Generate();
        }
    }
}
