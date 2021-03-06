﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Data.Linq.Mapping;

namespace UkrainianCurrency.Models
{
    [Table]
    public class DownloadData
    {
        public DownloadData() { }

        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column()]
        public int City { get; set; }

        [Column()]
        public int Bank { get; set; }

        [Column()]
        public long UpdateTime { get; set; }
    }
}
