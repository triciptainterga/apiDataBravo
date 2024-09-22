﻿using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiBravo.Models
{
    public partial class DataMasterPriority
    {
        public string CategoryId { get; set; }
        public string SubCategory1Id { get; set; }
        public string SubCategory2Id { get; set; }
        public string SubCategory3Id { get; set; }
        public string NameMcategory { get; set; }
        public string MSubCategoryLv1Name { get; set; }
        public string MSubCategoryLv2Name { get; set; }
        public string MSubCategoryLv3Name { get; set; }
        public string Pic { get; set; }
        public double? Slalayer1 { get; set; }
        public string Solutionlayer2 { get; set; }
        public string Pic1 { get; set; }
        public string Slalayer2 { get; set; }
        public string Solutionlayer3 { get; set; }
        public string Pic2 { get; set; }
        public string Slalayer3 { get; set; }
        public string Tickettag { get; set; }
        public string Criticality { get; set; }
        public string Totalsla { get; set; }
        public string SelfService { get; set; }
        public string Solutionlayer1 { get; set; }
    }
}