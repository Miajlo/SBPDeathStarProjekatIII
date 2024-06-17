using DeathStarLibrary;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;


namespace SPBDeathStarProjekatIII;

internal static class DataLayer
{
    private static ISessionFactory? _factory = null;
    private static readonly object objLock = new();

    //funkcija na zahtev otvara sesiju
    public static ISession? GetSession()
    {
        //ukoliko session factory nije kreiran
        if (_factory == null)
        {
            lock (objLock)
            {
                _factory ??= CreateSessionFactory();
            }
        }

        return _factory?.OpenSession();
    }

    //konfiguracija i kreiranje session factory
    private static ISessionFactory? CreateSessionFactory()
    {
        try
        {
            string? dataSource, id, password;

            SetEnvironmetVariables(out dataSource, out id, out password);

            var cfg = OracleManagedDataClientConfiguration.Oracle10
                        .ShowSql()
                        .ConnectionString(c =>
                            c.Is($"Data Source={dataSource};User Id={id};Password={password};"));

            return Fluently.Configure()
                .Database(cfg)
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                .BuildSessionFactory();
        }
        catch (Exception e)
        {
            string error = e.Message;
            /* Logovanje greške!
             * Console.ForegroundColor = ConsoleColor.Red;
             * Console.Error.WriteLine(error);
             * Nema potrebe, već se prikazuje identična greška.
             */
            return null;
        }
    }
    private static void SetEnvironmetVariables(out string? dataSource, out string? ID, out string? password)
    {
        var baseDir = DirExtension.ProjectBase();
        if (baseDir != null)
        {
            var path = Path.Combine(baseDir, ".env");
            DotEnv.Inject(path);
        }
        dataSource = Environment.GetEnvironmentVariable("DATA_SOURCE");
        ID = Environment.GetEnvironmentVariable("ID");
        password = Environment.GetEnvironmentVariable("PASSWORD");
    }
}
