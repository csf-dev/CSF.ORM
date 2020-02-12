using System;
using NHibernate;
using NHibernate.SqlCommand;

namespace Test.CSF.Data.NHibernate.Config
{
  public class LoggingInterceptor : EmptyInterceptor
  {
    private log4net.ILog _logger;

    public override SqlString OnPrepareStatement(SqlString sql)
    {
      _logger.Info(sql.ToString());
      return sql;
    }

    public LoggingInterceptor()
    {
      _logger = log4net.LogManager.GetLogger(this.GetType());
    }
  }
}

