using System.Collections.Generic;

namespace Bug.API.Services.DTO
{
    public class TagDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }

    }
}