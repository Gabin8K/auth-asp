
using System.ComponentModel.DataAnnotations;

namespace Fullstack.Entities;

public class TaskModel
{
    [Required(ErrorMessage = "Tak most be have a title")]
    public string Title { get; set; }

    public DateTime date;
    public string Priority { get; set; }
    public string Description { get; set; }
}