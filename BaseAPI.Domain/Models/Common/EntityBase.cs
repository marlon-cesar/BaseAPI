using System;

namespace BaseAPI.Domain.Models.Common
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}
