using Autofac;
using TrackFinance.Core.Interfaces;
using TrackFinance.Core.Services;

namespace TrackFinance.Core;
public class DefaultCoreModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<ToDoItemSearchService>()
        .As<IToDoItemSearchService>().InstancePerLifetimeScope();
    builder.RegisterType<TransactionFinanceService>()
        .As<ITransactionFinanceService>().InstancePerLifetimeScope();
  }
}
