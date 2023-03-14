using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.VisualBasic;

namespace ncorep.Models;

public class Image : IEntityBase
{
    public int Id { get; set; }


    public string FileName { get; set; }


    public string FilePath { get; set; }

    public Product Product { get; set; }

    public int ProductId { get; set; }

    public string NameGenerator(int id, int productid)
    {
        return $"Image_{id}_{productid}_{DateTime.Now}";
    }

    public string PathGenerator(string filename)
    {
        return $"Images/{filename}";
    }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}