
///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
//[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
//public partial class MeterData
//{

//    private MeterDataMeter[] meterField;

//    /// <remarks/>
//    public MeterDataMeter[] Meter
//    {
//        get
//        {
//            return this.meterField;
//        }
//        set
//        {
//            this.meterField = value;
//        }
//    }
//}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
//public partial class MeterDataMeter
//{

//    private object aMRAlarmsField;

//    private string activeField;

//    private string addressField;

//    private byte barcodeField;

//    private ushort billingIDField;

//    private object combinedField;

//    private string coronisAddressField;

//    private byte coronisInputField;

//    private object coronisInput1Field;

//    private byte coronisTypeField;

//    private byte coronisUnitField;

//    private object coronisUnit1Field;

//    private object customerEmailField;

//    private object customerNumberField;

//    private byte dimensionField;

//    private object districtIDField;

//    private decimal gPSLatField;

//    private decimal gPSLatNewField;

//    private object gPSLatNewAMRField;

//    private decimal gPSLonField;

//    private decimal gPSLonNewField;

//    private object gPSLonNewAMRField;

//    private object imagesField;

//    private string isCheckedField;

//    private string lastMeterChangeDateField;

//    private object locationField;

//    private object mCAdditionalDataField;

//    private object mCBackflowProtectionIDField;

//    private object mCCoronisAddressField;

//    private object mCCoronisInput1Field;

//    private object mCCoronisInput2Field;

//    private object mCCoronisTypeField;

//    private object mCCoronisUnit1Field;

//    private object mCCoronisUnit2Field;

//    private object mCDateField;

//    private object mCDiameterField;

//    private object mCManufacturerIDField;

//    private string mCMeterChangedField;

//    private object mCMeterStampField;

//    private object mCMeterTypeIDField;

//    private object mCNewMeterReg1Field;

//    private object mCNewMeterReg2Field;

//    private object mCNoteField;

//    private object mCOldMeterReg1Field;

//    private object mCOldMeterReg2Field;

//    private object mCRFIDField;

//    private object mCRFIDTypeField;

//    private string mCRFUnitChangedField;

//    private object mCRemarkDescriptionField;

//    private object mCRemarkIDField;

//    private object mCSerialNoField;

//    private object mCSignatureField;

//    private object mCSignatureDateField;

//    private object mCUserIDField;

//    private ushort mpField;

//    private byte meterTypeField;

//    private object newBarcodeField;

//    private object noteField;

//    private string ownerField;

//    private byte periodField;

//    private ulong rFIDField;

//    private object rFIDKeyField;

//    private byte rFIDTypeField;

//    private byte readingTypeField;

//    private byte reg1AverageField;

//    private byte reg1LastValueField;

//    private byte reg1ValueField;

//    private byte reg2AverageField;

//    private object reg2LastValueField;

//    private object reg2ValueField;

//    private string regDateField;

//    private string regLastDateField;

//    private object remarkDescriptionField;

//    private object remarkIDField;

//    private uint serialNoField;

//    private ushort sortOrderField;

//    private byte terminalIDField;

//    private byte userIDField;

//    private byte workOrderTypeField;

//    /// <remarks/>
//    public object AMRAlarms
//    {
//        get
//        {
//            return this.aMRAlarmsField;
//        }
//        set
//        {
//            this.aMRAlarmsField = value;
//        }
//    }

//    /// <remarks/>
//    public string Active
//    {
//        get
//        {
//            return this.activeField;
//        }
//        set
//        {
//            this.activeField = value;
//        }
//    }

//    /// <remarks/>
//    public string Address
//    {
//        get
//        {
//            return this.addressField;
//        }
//        set
//        {
//            this.addressField = value;
//        }
//    }

//    /// <remarks/>
//    public byte Barcode
//    {
//        get
//        {
//            return this.barcodeField;
//        }
//        set
//        {
//            this.barcodeField = value;
//        }
//    }

//    /// <remarks/>
//    public ushort BillingID
//    {
//        get
//        {
//            return this.billingIDField;
//        }
//        set
//        {
//            this.billingIDField = value;
//        }
//    }

//    /// <remarks/>
//    public object Combined
//    {
//        get
//        {
//            return this.combinedField;
//        }
//        set
//        {
//            this.combinedField = value;
//        }
//    }

//    /// <remarks/>
//    public string CoronisAddress
//    {
//        get
//        {
//            return this.coronisAddressField;
//        }
//        set
//        {
//            this.coronisAddressField = value;
//        }
//    }

//    /// <remarks/>
//    public byte CoronisInput
//    {
//        get
//        {
//            return this.coronisInputField;
//        }
//        set
//        {
//            this.coronisInputField = value;
//        }
//    }

//    /// <remarks/>
//    public object CoronisInput1
//    {
//        get
//        {
//            return this.coronisInput1Field;
//        }
//        set
//        {
//            this.coronisInput1Field = value;
//        }
//    }

//    /// <remarks/>
//    public byte CoronisType
//    {
//        get
//        {
//            return this.coronisTypeField;
//        }
//        set
//        {
//            this.coronisTypeField = value;
//        }
//    }

//    /// <remarks/>
//    public byte CoronisUnit
//    {
//        get
//        {
//            return this.coronisUnitField;
//        }
//        set
//        {
//            this.coronisUnitField = value;
//        }
//    }

//    /// <remarks/>
//    public object CoronisUnit1
//    {
//        get
//        {
//            return this.coronisUnit1Field;
//        }
//        set
//        {
//            this.coronisUnit1Field = value;
//        }
//    }

//    /// <remarks/>
//    public object CustomerEmail
//    {
//        get
//        {
//            return this.customerEmailField;
//        }
//        set
//        {
//            this.customerEmailField = value;
//        }
//    }

//    /// <remarks/>
//    public object CustomerNumber
//    {
//        get
//        {
//            return this.customerNumberField;
//        }
//        set
//        {
//            this.customerNumberField = value;
//        }
//    }

//    /// <remarks/>
//    public byte Dimension
//    {
//        get
//        {
//            return this.dimensionField;
//        }
//        set
//        {
//            this.dimensionField = value;
//        }
//    }

//    /// <remarks/>
//    public object DistrictID
//    {
//        get
//        {
//            return this.districtIDField;
//        }
//        set
//        {
//            this.districtIDField = value;
//        }
//    }

//    /// <remarks/>
//    public decimal GPSLat
//    {
//        get
//        {
//            return this.gPSLatField;
//        }
//        set
//        {
//            this.gPSLatField = value;
//        }
//    }

//    /// <remarks/>
//    public decimal GPSLatNew
//    {
//        get
//        {
//            return this.gPSLatNewField;
//        }
//        set
//        {
//            this.gPSLatNewField = value;
//        }
//    }

//    /// <remarks/>
//    public object GPSLatNewAMR
//    {
//        get
//        {
//            return this.gPSLatNewAMRField;
//        }
//        set
//        {
//            this.gPSLatNewAMRField = value;
//        }
//    }

//    /// <remarks/>
//    public decimal GPSLon
//    {
//        get
//        {
//            return this.gPSLonField;
//        }
//        set
//        {
//            this.gPSLonField = value;
//        }
//    }

//    /// <remarks/>
//    public decimal GPSLonNew
//    {
//        get
//        {
//            return this.gPSLonNewField;
//        }
//        set
//        {
//            this.gPSLonNewField = value;
//        }
//    }

//    /// <remarks/>
//    public object GPSLonNewAMR
//    {
//        get
//        {
//            return this.gPSLonNewAMRField;
//        }
//        set
//        {
//            this.gPSLonNewAMRField = value;
//        }
//    }

//    /// <remarks/>
//    public object Images
//    {
//        get
//        {
//            return this.imagesField;
//        }
//        set
//        {
//            this.imagesField = value;
//        }
//    }

//    /// <remarks/>
//    public string IsChecked
//    {
//        get
//        {
//            return this.isCheckedField;
//        }
//        set
//        {
//            this.isCheckedField = value;
//        }
//    }

//    /// <remarks/>
//    public string LastMeterChangeDate
//    {
//        get
//        {
//            return this.lastMeterChangeDateField;
//        }
//        set
//        {
//            this.lastMeterChangeDateField = value;
//        }
//    }

//    /// <remarks/>
//    public object Location
//    {
//        get
//        {
//            return this.locationField;
//        }
//        set
//        {
//            this.locationField = value;
//        }
//    }

//    /// <remarks/>
//    public object MCAdditionalData
//    {
//        get
//        {
//            return this.mCAdditionalDataField;
//        }
//        set
//        {
//            this.mCAdditionalDataField = value;
//        }
//    }

//    /// <remarks/>
//    public object MCBackflowProtectionID
//    {
//        get
//        {
//            return this.mCBackflowProtectionIDField;
//        }
//        set
//        {
//            this.mCBackflowProtectionIDField = value;
//        }
//    }

//    /// <remarks/>
//    public object MCCoronisAddress
//    {
//        get
//        {
//            return this.mCCoronisAddressField;
//        }
//        set
//        {
//            this.mCCoronisAddressField = value;
//        }
//    }

//    /// <remarks/>
//    public object MCCoronisInput1
//    {
//        get
//        {
//            return this.mCCoronisInput1Field;
//        }
//        set
//        {
//            this.mCCoronisInput1Field = value;
//        }
//    }

//    /// <remarks/>
//    public object MCCoronisInput2
//    {
//        get
//        {
//            return this.mCCoronisInput2Field;
//        }
//        set
//        {
//            this.mCCoronisInput2Field = value;
//        }
//    }

//    /// <remarks/>
//    public object MCCoronisType
//    {
//        get
//        {
//            return this.mCCoronisTypeField;
//        }
//        set
//        {
//            this.mCCoronisTypeField = value;
//        }
//    }

//    /// <remarks/>
//    public object MCCoronisUnit1
//    {
//        get
//        {
//            return this.mCCoronisUnit1Field;
//        }
//        set
//        {
//            this.mCCoronisUnit1Field = value;
//        }
//    }

//    /// <remarks/>
//    public object MCCoronisUnit2
//    {
//        get
//        {
//            return this.mCCoronisUnit2Field;
//        }
//        set
//        {
//            this.mCCoronisUnit2Field = value;
//        }
//    }

//    /// <remarks/>
//    public object MCDate
//    {
//        get
//        {
//            return this.mCDateField;
//        }
//        set
//        {
//            this.mCDateField = value;
//        }
//    }

//    /// <remarks/>
//    public object MCDiameter
//    {
//        get
//        {
//            return this.mCDiameterField;
//        }
//        set
//        {
//            this.mCDiameterField = value;
//        }
//    }

//    /// <remarks/>
//    public object MCManufacturerID
//    {
//        get
//        {
//            return this.mCManufacturerIDField;
//        }
//        set
//        {
//            this.mCManufacturerIDField = value;
//        }
//    }

//    /// <remarks/>
//    public string MCMeterChanged
//    {
//        get
//        {
//            return this.mCMeterChangedField;
//        }
//        set
//        {
//            this.mCMeterChangedField = value;
//        }
//    }

//    /// <remarks/>
//    public object MCMeterStamp
//    {
//        get
//        {
//            return this.mCMeterStampField;
//        }
//        set
//        {
//            this.mCMeterStampField = value;
//        }
//    }

//    /// <remarks/>
//    public object MCMeterTypeID
//    {
//        get
//        {
//            return this.mCMeterTypeIDField;
//        }
//        set
//        {
//            this.mCMeterTypeIDField = value;
//        }
//    }

//    /// <remarks/>
//    public object MCNewMeterReg1
//    {
//        get
//        {
//            return this.mCNewMeterReg1Field;
//        }
//        set
//        {
//            this.mCNewMeterReg1Field = value;
//        }
//    }

//    /// <remarks/>
//    public object MCNewMeterReg2
//    {
//        get
//        {
//            return this.mCNewMeterReg2Field;
//        }
//        set
//        {
//            this.mCNewMeterReg2Field = value;
//        }
//    }

//    /// <remarks/>
//    public object MCNote
//    {
//        get
//        {
//            return this.mCNoteField;
//        }
//        set
//        {
//            this.mCNoteField = value;
//        }
//    }

//    /// <remarks/>
//    public object MCOldMeterReg1
//    {
//        get
//        {
//            return this.mCOldMeterReg1Field;
//        }
//        set
//        {
//            this.mCOldMeterReg1Field = value;
//        }
//    }

//    /// <remarks/>
//    public object MCOldMeterReg2
//    {
//        get
//        {
//            return this.mCOldMeterReg2Field;
//        }
//        set
//        {
//            this.mCOldMeterReg2Field = value;
//        }
//    }

//    /// <remarks/>
//    public object MCRFID
//    {
//        get
//        {
//            return this.mCRFIDField;
//        }
//        set
//        {
//            this.mCRFIDField = value;
//        }
//    }

//    /// <remarks/>
//    public object MCRFIDType
//    {
//        get
//        {
//            return this.mCRFIDTypeField;
//        }
//        set
//        {
//            this.mCRFIDTypeField = value;
//        }
//    }

//    /// <remarks/>
//    public string MCRFUnitChanged
//    {
//        get
//        {
//            return this.mCRFUnitChangedField;
//        }
//        set
//        {
//            this.mCRFUnitChangedField = value;
//        }
//    }

//    /// <remarks/>
//    public object MCRemarkDescription
//    {
//        get
//        {
//            return this.mCRemarkDescriptionField;
//        }
//        set
//        {
//            this.mCRemarkDescriptionField = value;
//        }
//    }

//    /// <remarks/>
//    public object MCRemarkID
//    {
//        get
//        {
//            return this.mCRemarkIDField;
//        }
//        set
//        {
//            this.mCRemarkIDField = value;
//        }
//    }

//    /// <remarks/>
//    public object MCSerialNo
//    {
//        get
//        {
//            return this.mCSerialNoField;
//        }
//        set
//        {
//            this.mCSerialNoField = value;
//        }
//    }

//    /// <remarks/>
//    public object MCSignature
//    {
//        get
//        {
//            return this.mCSignatureField;
//        }
//        set
//        {
//            this.mCSignatureField = value;
//        }
//    }

//    /// <remarks/>
//    public object MCSignatureDate
//    {
//        get
//        {
//            return this.mCSignatureDateField;
//        }
//        set
//        {
//            this.mCSignatureDateField = value;
//        }
//    }

//    /// <remarks/>
//    public object MCUserID
//    {
//        get
//        {
//            return this.mCUserIDField;
//        }
//        set
//        {
//            this.mCUserIDField = value;
//        }
//    }

//    /// <remarks/>
//    public ushort MP
//    {
//        get
//        {
//            return this.mpField;
//        }
//        set
//        {
//            this.mpField = value;
//        }
//    }

//    /// <remarks/>
//    public byte MeterType
//    {
//        get
//        {
//            return this.meterTypeField;
//        }
//        set
//        {
//            this.meterTypeField = value;
//        }
//    }

//    /// <remarks/>
//    public object NewBarcode
//    {
//        get
//        {
//            return this.newBarcodeField;
//        }
//        set
//        {
//            this.newBarcodeField = value;
//        }
//    }

//    /// <remarks/>
//    public object Note
//    {
//        get
//        {
//            return this.noteField;
//        }
//        set
//        {
//            this.noteField = value;
//        }
//    }

//    /// <remarks/>
//    public string Owner
//    {
//        get
//        {
//            return this.ownerField;
//        }
//        set
//        {
//            this.ownerField = value;
//        }
//    }

//    /// <remarks/>
//    public byte Period
//    {
//        get
//        {
//            return this.periodField;
//        }
//        set
//        {
//            this.periodField = value;
//        }
//    }

//    /// <remarks/>
//    public ulong RFID
//    {
//        get
//        {
//            return this.rFIDField;
//        }
//        set
//        {
//            this.rFIDField = value;
//        }
//    }

//    /// <remarks/>
//    public object RFIDKey
//    {
//        get
//        {
//            return this.rFIDKeyField;
//        }
//        set
//        {
//            this.rFIDKeyField = value;
//        }
//    }

//    /// <remarks/>
//    public byte RFIDType
//    {
//        get
//        {
//            return this.rFIDTypeField;
//        }
//        set
//        {
//            this.rFIDTypeField = value;
//        }
//    }

//    /// <remarks/>
//    public byte ReadingType
//    {
//        get
//        {
//            return this.readingTypeField;
//        }
//        set
//        {
//            this.readingTypeField = value;
//        }
//    }

//    /// <remarks/>
//    public byte Reg1Average
//    {
//        get
//        {
//            return this.reg1AverageField;
//        }
//        set
//        {
//            this.reg1AverageField = value;
//        }
//    }

//    /// <remarks/>
//    public byte Reg1LastValue
//    {
//        get
//        {
//            return this.reg1LastValueField;
//        }
//        set
//        {
//            this.reg1LastValueField = value;
//        }
//    }

//    /// <remarks/>
//    public byte Reg1Value
//    {
//        get
//        {
//            return this.reg1ValueField;
//        }
//        set
//        {
//            this.reg1ValueField = value;
//        }
//    }

//    /// <remarks/>
//    public byte Reg2Average
//    {
//        get
//        {
//            return this.reg2AverageField;
//        }
//        set
//        {
//            this.reg2AverageField = value;
//        }
//    }

//    /// <remarks/>
//    public object Reg2LastValue
//    {
//        get
//        {
//            return this.reg2LastValueField;
//        }
//        set
//        {
//            this.reg2LastValueField = value;
//        }
//    }

//    /// <remarks/>
//    public object Reg2Value
//    {
//        get
//        {
//            return this.reg2ValueField;
//        }
//        set
//        {
//            this.reg2ValueField = value;
//        }
//    }

//    /// <remarks/>
//    public string RegDate
//    {
//        get
//        {
//            return this.regDateField;
//        }
//        set
//        {
//            this.regDateField = value;
//        }
//    }

//    /// <remarks/>
//    public string RegLastDate
//    {
//        get
//        {
//            return this.regLastDateField;
//        }
//        set
//        {
//            this.regLastDateField = value;
//        }
//    }

//    /// <remarks/>
//    public object RemarkDescription
//    {
//        get
//        {
//            return this.remarkDescriptionField;
//        }
//        set
//        {
//            this.remarkDescriptionField = value;
//        }
//    }

//    /// <remarks/>
//    public object RemarkID
//    {
//        get
//        {
//            return this.remarkIDField;
//        }
//        set
//        {
//            this.remarkIDField = value;
//        }
//    }

//    /// <remarks/>
//    public uint SerialNo
//    {
//        get
//        {
//            return this.serialNoField;
//        }
//        set
//        {
//            this.serialNoField = value;
//        }
//    }

//    /// <remarks/>
//    public ushort SortOrder
//    {
//        get
//        {
//            return this.sortOrderField;
//        }
//        set
//        {
//            this.sortOrderField = value;
//        }
//    }

//    /// <remarks/>
//    public byte TerminalID
//    {
//        get
//        {
//            return this.terminalIDField;
//        }
//        set
//        {
//            this.terminalIDField = value;
//        }
//    }

//    /// <remarks/>
//    public byte UserID
//    {
//        get
//        {
//            return this.userIDField;
//        }
//        set
//        {
//            this.userIDField = value;
//        }
//    }

//    /// <remarks/>
//    public byte WorkOrderType
//    {
//        get
//        {
//            return this.workOrderTypeField;
//        }
//        set
//        {
//            this.workOrderTypeField = value;
//        }
//    }
//}

