using System;
namespace ComHunt.Models
{
    public class Chasse
    {
        public string name { get; set; }
        public string etat { get; set; }
        public string nombreChasseurs { get; set; }
        public string nombreChefs { get; set; }
        public string nombreChasseursActifs { get; set; }
        public string nombreChefsActifs { get; set; }
        public string ChevreuilVue { get; set; }
        public string RenardVue { get; set; }
        public string SanglierVue { get; set; }
        public string ChevreuilToucher { get; set; }
        public string RenardToucher { get; set; }
        public string SanglierToucher { get; set; }
        public string ChevreuilTuer { get; set; }
        public string RenardTuer { get; set; }
        public string SanglierTuer { get; set; }
    }
}
