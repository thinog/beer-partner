using System;
using MongoDB.Bson.Serialization.Attributes;

namespace BeerPartner.Domain.Entities.Base
{
    public abstract class BaseEntity<TKey>
    {
        // https://stackoverflow.com/questions/31526879/mongodb-c-sharp-driver-change-id-serialization-for-inherited-class
        // [BsonIgnore]
        // public virtual TKey Id { get; set; }
        // public DateTime CreatedAt { get; set; }
    }    
}