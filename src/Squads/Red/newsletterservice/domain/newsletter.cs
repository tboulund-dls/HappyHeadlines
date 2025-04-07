using System;
using System.Collections.Generic;

namespace newsletterservice.Models
{
    
public class Newsletter
{
    public string Title { get; set; } = string.Empty;
    public DateTime SentAt { get; set; }
    public List<Article> Articles { get; set; } = new();
}
}