using NHibernate;
using NHibernate.Cfg;

namespace Contacts.Core
{
    public static class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;
        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    var configuration = new Configuration();
                    configuration.Configure();
                    configuration.AddAssembly(typeof(Domain.Contact).Assembly);
                    _sessionFactory = configuration.BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }

        public static void Generate()
        {
            var cfg = new NHibernate.Cfg.Configuration();
            cfg.Configure();
            cfg.AddAssembly(typeof(Domain.Contact).Assembly);
            new NHibernate.Tool.hbm2ddl.SchemaExport(cfg).Execute(true, true, false);
        }
    }
}
