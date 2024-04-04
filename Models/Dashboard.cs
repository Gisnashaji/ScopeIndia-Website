using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace Scope.Models
{
    public class Dashboard
    {
       
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Fee { get; set; }
            public string Duration { get; set; }
            public string Languages { get; set; }


    }
}

