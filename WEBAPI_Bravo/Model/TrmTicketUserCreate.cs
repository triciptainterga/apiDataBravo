﻿using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiBravo.Models
{
    public partial class TrmTicketUserCreate
    {
        public string Layer { get; set; }
        public int Id { get; set; }
        public string Nik { get; set; }
        public string TicketNumber { get; set; }
        public string GroupTicketNumber { get; set; }
        public string ChannelCode { get; set; }
        public string UnitId { get; set; }
        public string TicketSource { get; set; }
        public string TicketSourceName { get; set; }
        public string TicketGroup { get; set; }
        public string TicketGroupName { get; set; }
        public string ComplaintLevel { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string SubCategory1Id { get; set; }
        public string SubCategory1Name { get; set; }
        public string SubCategory2Id { get; set; }
        public string SubCategory2Name { get; set; }
        public string SubCategory21id { get; set; }
        public string SubCategory21name { get; set; }
        public string SubCategory3Id { get; set; }
        public string SubCategory3Name { get; set; }
        public string DetailComplaint { get; set; }
        public string ResponComplaint { get; set; }
        public DateTime? DateAgentResponse { get; set; }
        public int? SlaresponseAgent { get; set; }
        public int Sla { get; set; }
        public string Severity { get; set; }
        public string Status { get; set; }
        public string UserCreate { get; set; }
        public DateTime? DateCreate { get; set; }
        public string UserSolved { get; set; }
        public DateTime? DateSolved { get; set; }
        public string UserClose { get; set; }
        public DateTime? DateClose { get; set; }
        public string TicketPosition { get; set; }
        public string ClosedBy { get; set; }
        public string KirimEmail { get; set; }
        public string KirimEmailLayer { get; set; }
        public string Na { get; set; }
        public DateTime? DateCreateReal { get; set; }
        public string OverClockSystem { get; set; }
        public string DispatchUser { get; set; }
        public DateTime? DispatchTgl { get; set; }
        public string Divisi { get; set; }
        public DateTime? DispatchDivisiTgl { get; set; }
        public string Attch { get; set; }
        public string Posting { get; set; }
        public string IdTabel { get; set; }
        public string GroupId { get; set; }
        public string CompId { get; set; }
        public string OrganizationId { get; set; }
        public string Channelid { get; set; }
        public string NamaPelapor { get; set; }
        public string EmailPelapor { get; set; }
        public string PhonePelapor { get; set; }
        public string AlamatPelapor { get; set; }
        public string FlagGroupTicket { get; set; }
        public string AccountInbound { get; set; }
        public string NomorRekening { get; set; }
        public string SkalaPrioritas { get; set; }
        public string JenisNasabah { get; set; }
        public string Idlevel3 { get; set; }
        public string Phone { get; set; }
        public string SumberInformasi { get; set; }
        public int? ExtendId { get; set; }
        public int? ExtendSla { get; set; }
        public string ExtendName { get; set; }
        public string ThreadId { get; set; }
        public string GenesysId { get; set; }
        public string ReleaseUser { get; set; }
        public string UnitKerjaAgent { get; set; }
        public int? Slaori { get; set; }
        public string AccountId { get; set; }
        public string LayerCreateBy { get; set; }
        public string LastResponseBy { get; set; }
        public DateTime? LastResponseDate { get; set; }
        public string ParentNumberId { get; set; }
        public string ExtendStatusId { get; set; }
        public string ExtendStatusSla { get; set; }
        public string ParentChild { get; set; }
        public DateTime? ParentNumberDate { get; set; }
        public string ParentNumberCreated { get; set; }
        public string ParentReason { get; set; }
        public string FlagOutbound { get; set; }
        public string VendorId { get; set; }
        public string VendorName { get; set; }
        public int? VendorStatus { get; set; }
        public int? IndividuId { get; set; }
        public string ProductName { get; set; }
        public string AgentGroup { get; set; }
        public string AgentGroupName { get; set; }
        public string SiteId { get; set; }
        public string SiteName { get; set; }
        public string Name { get; set; }
        public string CustomerName { get; set; }
        public string RefId { get; set; }
    }
}
