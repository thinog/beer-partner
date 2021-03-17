using System;
using Amazon.DynamoDBv2.DataModel;

namespace BeerPartner.Domain.Entities.Base
{
    public abstract class BaseEntity<TKey>
    {
        [DynamoDBHashKey] // Não teve jeito de deixar sem esse attr de infra :/
        public TKey Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }    
}