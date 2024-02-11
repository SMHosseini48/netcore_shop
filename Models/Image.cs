using System;

namespace ncorep.Models;

public class Image : BaseEntity
{
    public string FileName { get; set; }

    public string FilePath { get; set; }

    public Product Product { get; set; }

    public string ProductId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public string NameGenerator(int id, int productid)
    {
        return $"Image_{id}_{productid}_{DateTime.Now}";
    }

    public string PathGenerator(string filename)
    {
        return $"Images/{filename}";
    }
}