using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrateDataCargoAIProject.DataModel
{
    public interface IEntity : IVersionable, IPartionable
    {
    }

    public interface IVersionable
    {
        string ETag { get; set; }
    }

    public interface IPartionable
    {
        string PartitionKey { get; }
    }
}
