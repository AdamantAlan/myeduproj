﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAPI.Dtos
{
    public class CommandCreateDto
    {

        [Required]
        [MaxLength(250)]
        public string HowTo { get; set; }
        [Required]
        public string Line { get; set; }
        [Required]
        public string Plaform { get; set; }
    }

    public class CommandUpdateDto:CommandCreateDto
    {

    }
  
}
