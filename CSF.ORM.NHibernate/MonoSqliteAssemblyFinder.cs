using System;
using System.Reflection;
using System.Globalization;

namespace CSF.ORM.NHibernate
{
  /// <summary>
  /// Service which locates the correct version of the Mono.Data.Sqlite assembly.
  /// </summary>
  public class MonoSqliteAssemblyFinder
  {
    #region constants

    private static readonly string[] AssemblyNames = new [] {
      "Mono.Data.Sqlite, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756",
      "Mono.Data.Sqlite, Version=2.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756",
    };

    #endregion

    #region methods

    /// <summary>
    /// Find the Mono Sqlite assembly.
    /// </summary>
    public Assembly Find()
    {
      foreach(var name in AssemblyNames)
      {
        var assembly = TryLoad(name);

        if(assembly != null)
        {
          return assembly;
        }
      }

      return null;
    }

    /// <summary>
    /// Assempts to load an assembly of the given name, returning it if it was loaded, or <c>null</c> if it was not.
    /// </summary>
    /// <returns>The assembly.</returns>
    /// <param name="assemblyName">Assembly name.</param>
    private Assembly TryLoad(string assemblyName)
    {
      try
      {
        return Assembly.Load(assemblyName);
      }
      catch(Exception) {}

      return null;
    }

    #endregion


  }
}

