using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;


namespace Scope.Models
{
    
        public class Review
        {
            public string Name { get; set; }
            public string YourReview { get; set; }
        }

    
}
