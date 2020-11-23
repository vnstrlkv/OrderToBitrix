using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace OrderToBitrix.Bitrix
{
    [Serializable()]
    [XmlRoot(ElementName = "Dok")]
    public class Order
    {
        [XmlElement(ElementName = "Number")]
        public string Number { get; set; }
        [XmlElement(ElementName = "Date")]
        public string Date { get; set; }
        [XmlElement(ElementName = "Контрагент")]
        public string Contractor { get; set; }
        [XmlElement(ElementName = "Организация")]
        public string Organization { get; set; }
        [XmlElement(ElementName = "Комментарий")]
        public string Comment { get; set; }
        [XmlElement(ElementName = "ТипЗаказа")]
        public string TypeOrder { get; set; }
        [XmlElement(ElementName = "ДатаВыхода")]
        public string DateOut { get; set; }
        [XmlElement(ElementName = "Ответственный")]
        public string Responsible { get; set; }
        [XmlElement(ElementName = "СуммаРеализации")]
        public string SalesAmount { get; set; }
        [XmlElement(ElementName = "СуммаСебестоимости")]
        public string CostAmount { get; set; }
        [XmlElement(ElementName = "СуммаВП")]
        public string AmountVP { get; set; }
        [XmlElement(ElementName = "СуммаРеализацииПоПлану")]
        public string SalesAmountPlane { get; set; }
        [XmlElement(ElementName = "СуммаСебестоимостиПоПлану")]
        public string CostAmounPlane { get; set; }
        [XmlElement(ElementName = "СуммаВППоПлану")]
        public string AmountVPPlane { get; set; }
        [XmlElement(ElementName = "Менеджер")]
        public string Manager { get; set; }
        [XmlElement(ElementName = "ЗаказОплачен")]
        public string OrderStatusPaid { get; set; }
        [XmlElement(ElementName = "НетДолговПоДокументам")]
        public string NoDocumentsDepts { get; set; }
        [XmlElement(ElementName = "Отдел")]
        public string Department { get; set; }
        [XmlElement(ElementName = "СуммаВключаетНДС")]
        public string AmountIncludeVAT { get; set; }
        [XmlElement(ElementName = "УчитыватьНДС")]
        public string IncludeVAT { get; set; }
        [XmlElement(ElementName = "ТекстТЗ")]
        public string TextTZ { get; set; }
        [XmlElement(ElementName = "ОбщийОбъёмРабот")]
        public string TotalVolumeWork { get; set; }
        [XmlElement(ElementName = "НомерЗаказа")]
        public string NumberOrder { get; set; }
        [XmlElement(ElementName = "ПредставительЗаказчика")]
        public string Costumer { get; set; }
        [XmlElement(ElementName = "АдресПредставителя")]
        public string AdressCostumer { get; set; }
        [XmlElement(ElementName = "КонтактныйТелефонФакс")]
        public string ContactPhoneNumber { get; set; }
        [XmlElement(ElementName = "ДатаВвода")]
        public string EntryDate { get; set; }
        [XmlElement(ElementName = "ТипКонтрагента")]
        public string TypeContractor { get; set; }
        [XmlElement(ElementName = "МенеджерЗапрос")]
        public string ManagerRequest { get; set; }
        [XmlElement(ElementName = "СтатусЗаказа")]
        public string OrderStatus { get; set; }
        [XmlElement(ElementName = "Процент")]
        public string Percent { get; set; }
        [XmlElement(ElementName = "ТипПроводки")]
        public string PostType { get; set; }
        [XmlElement(ElementName = "СуммаОплаты")]
        public string PaymentAmount { get; set; }
        [XmlElement(ElementName = "СуммаМатериалов")]
        public string MaterialAmount { get; set; }
        [XmlElement(ElementName = "ОбрабатываетЗаказ")]
        public string HandlesOrder { get; set; }
        [XmlElement(ElementName = "ОтправитьСообщение")]
        public string SendMessage { get; set; }
        [XmlElement(ElementName = "СписаниеЗавершено")]
        public string WriteOffComplited { get; set; }
        [XmlElement(ElementName = "ЕстьРеализация")]
        public string Realization { get; set; }
        [XmlElement(ElementName = "ДатаДоставки")]
        public string DateDelivery { get; set; }
        [XmlElement(ElementName = "ДатаИзготовления")]
        public string DateProduction { get; set; }
        [XmlElement(ElementName = "ДатаНачалаМонтажа")]
        public string DateInstallStart { get; set; }
        [XmlElement(ElementName = "ДатаОкончанияМонтажа")]
        public string DateInstallComplete { get; set; }
        [XmlElement(ElementName = "ДатаСдачи")]
        public string DatePass { get; set; }
        [XmlElement(ElementName = "ДатаСогласования")]
        public string DateApproval { get; set; }
        [XmlElement(ElementName = "ДатаОтправкиНаПроизводство")]
        public string DateProductionDispance { get; set; }
        [XmlElement(ElementName = "ДатаПолученияЗаказа")]
        public string DateOrderRecived { get; set; }
        [XmlElement(ElementName = "Макет")]
        public string Model { get; set; }
        [XmlElement(ElementName = "ЗаказИзготовлен")]
        public string OrderMade { get; set; }
        [XmlElement(ElementName = "ЗаказВыполнен")]
        public string OrderCompete { get; set; }
        [XmlElement(ElementName = "ЗаказПринят")]
        public string OrderAccepted { get; set; }
        [XmlElement(ElementName = "СуммаСебестоимостиТМЦ")]
        public string AmountCostTMC { get; set; }
        [XmlElement(ElementName = "СуммаСебестоимостиУслуг")]
        public string AmountCostService { get; set; }
        [XmlElement(ElementName = "СуммаДолгаПоТМЦ")]
        public string AmountDeptTMC { get; set; }
        [XmlElement(ElementName = "СуммаДолгаПоУслугам")]
        public string AmountDeptService { get; set; }
        [XmlElement(ElementName = "СуммаСебестоимостиПрочиеРасходы")]
        public string AmountCostOther { get; set; }
        [XmlElement(ElementName = "ДолгПоЗаказу")]
        public string OrderDept { get; set; }
        [XmlElement(ElementName = "СуммаСебестоимостиПоПлануТМЦ")]
        public string AmountCostPlaneTMC { get; set; }
        [XmlElement(ElementName = "СуммаСебестоимостиПоПлануУслуги")]
        public string AmountCostPlaneService { get; set; }
        [XmlElement(ElementName = "ДатаПоступления")]
        public string ReceiptDate { get; set; }
        [XmlElement(ElementName = "ДатаСписаниеЗавершено")]
        public string DateWritingComplete { get; set; }
        [XmlElement(ElementName = "НеСоздаватьАвтоматическихДокументов")]
        public string DoNotCreateAutomaticDocument { get; set; }
        [XmlElement(ElementName = "ПроцентыВыплачены")]
        public string PercentPaid { get; set; }
        [XmlElement(ElementName = "НеУстанавливатьСтатусАвтоматически")]
        public string DoNotInstallStatusAutomatic { get; set; }
        [XmlElement(ElementName = "ДатаОплаты")]
        public string PaymentDate { get; set; }
        [XmlElement(ElementName = "СуммаДляНачисленияЗП")]
        public string AmountAccrueZP { get; set; }
        [XmlElement(ElementName = "КоличествоДокументовКлиента")]
        public string CountClientDocuments { get; set; }
        [XmlElement(ElementName = "КоличествоДокументовПодрядчиков")]
        public string CountConractorDocuments { get; set; }
        [XmlElement(ElementName = "ПлановаяДатаОплаты")]
        public string PlannedPayentDate { get; set; }
        [XmlElement(ElementName = "РучнаяПравка")]
        public string ManualEdit { get; set; }
        [XmlElement(ElementName = "ФормаОплаты")]
        public string PaymentForm { get; set; }
        [XmlElement(ElementName = "ПроверяющийЗаказ")]
        public string ChekingOrder { get; set; }
        [XmlElement(ElementName = "ФормаРасчетаСебестоимость")]
        public string CalculationOrderForm { get; set; }
        [XmlElement(ElementName = "ЗаказПолучатель")]
        public string OrderRecipient { get; set; }
        [XmlElement(ElementName = "ВторичныйЗаказ")]
        public string SecondaryOrder { get; set; }
        [XmlElement(ElementName = "СоздатьВторичныйЗаказ")]
        public string CreateSecondaryOrder { get; set; }
    }




}
