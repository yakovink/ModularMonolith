using System;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.VisualBasic;
using Npgsql.Replication;
using Shared.Mechanism;
using Werhouse.Data.Configurations;
using Werhouse.Items.Dtos;
using Werhouse.Items.Models;


namespace Werhouse;

public class WerhouseModuleStructre : ModuleMechanism<WerhouseItem>
{

    public interface History : IMModelConfiguration.Property;
    public interface IWerhouseItemConfigurations : IMModelConfiguration;
    public interface IWerhouseItemHistoryConfigurations : IMModelConfiguration.IMPropertyConfiguration<WerhouseItemHistory>;

    public abstract record MWerhouseDto : IMModelConfiguration.MDto;
    public abstract class MWerhouseDbContext(DbContextOptions<WerhouseDbContext> options) : IMModelConfiguration.MContext<WerhouseDbContext>(options,new[] { typeof(WerhouseItem), typeof(WerhouseItemHistory) });
    public interface IMWerhouseRepository : IMModelConfiguration.IMRepository;

    public abstract class MWerhouseRepository(WerhouseDbContext dbContext) : IMModelConfiguration.MRepository<WerhouseDbContext>(dbContext);

    public abstract class MWerhouseCachedRepository(MWerhouseRepository repository, IDistributedCache cache) : IMModelConfiguration.MCachedRepository<WerhouseDbContext>(repository, cache);


    //commands
    public interface GetNewItem : MPost<Guid, Guid>;
    public interface PerformOperation : MPost<WerhouseItemHistoryDto, Guid>;
    public interface SendItem : MPut<Guid, bool>;

    //queries

    public interface GetItemById : MGet<Guid, HashSet<WerhouseItemHistoryDto>>;
    public interface GetItemsByCondition : MGet<WerhouseItemDto,HashSet<WerhouseItemDto>>;
  

    



    
}


