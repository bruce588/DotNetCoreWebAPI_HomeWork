﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNetCoreWebAPIEFHomeWork.Models
{
    [Table("CourseInstructor")]
    public partial class CourseInstructor
    {
        [Key]
        [Column("CourseID")]
        public int CourseId { get; set; }
        [Key]
        [Column("InstructorID")]
        public int InstructorId { get; set; }

        [ForeignKey(nameof(CourseId))]
        [InverseProperty("CourseInstructors")]
        public virtual Course Course { get; set; }
        [ForeignKey(nameof(InstructorId))]
        [InverseProperty(nameof(Person.CourseInstructors))]
        public virtual Person Instructor { get; set; }
    }
}