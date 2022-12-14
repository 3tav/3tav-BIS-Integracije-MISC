
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class MeterData
{

    private MeterDataMeter[] meterField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Meter")]
    public MeterDataMeter[] Meter
    {
        get
        {
            return this.meterField;
        }
        set
        {
            this.meterField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class MeterDataMeter
{

    private string aMRAlarmsField;

    private string activeField;

    private string addressField;

    private uint barcodeField;

    private byte billingIDField;

    private string combinedField;

    private string coronisAddressField;

    private string coronisInputField;

    private string coronisInput1Field;

    private string coronisTypeField;

    private string coronisUnitField;

    private string coronisUnit1Field;

    private string customerEmailField;

    private string customerNumberField;

    private byte dimensionField;

    private string districtIDField;

    private decimal gPSLatField;

    private string gPSLatNewField;

    private decimal gPSLonField;

    private string gPSLonNewField;

    private string imagesField;

    private string isCheckedField;

    private string lastMeterChangeDateField;

    private string locationField;

    private string mCAdditionalDataField;

    private string mCBackflowProtectionIDField;

    private string mCCoronisAddressField;

    private string mCCoronisInput1Field;

    private string mCCoronisInput2Field;

    private string mCCoronisTypeField;

    private string mCCoronisUnit1Field;

    private string mCCoronisUnit2Field;

    private string mCDateField;

    private string mCDiameterField;

    private string mCManufacturerIDField;

    private string mCMeterChangedField;

    private string mCMeterStampField;

    private string mCMeterTypeIDField;

    private string mCNewMeterReg1Field;

    private string mCNewMeterReg2Field;

    private string mCNoteField;

    private string mCOldMeterReg1Field;

    private string mCOldMeterReg2Field;

    private string mCRFIDField;

    private string mCRFIDTypeField;

    private string mCRFUnitChangedField;

    private string mCRemarkDescriptionField;

    private string mCRemarkIDField;

    private string mCSerialNoField;

    private string mCSignatureField;

    private string mCSignatureDateField;

    private string mCUserIDField;

    private string mpField;

    private byte meterTypeField;

    private string newBarcodeField;

    private string noteField;

    private string ownerField;

    private byte periodField;

    private uint rFIDField;

    private string rFIDKeyField;

    private string rFIDTypeField;

    private byte readingTypeField;

    private byte reg1AverageField;

    private decimal reg1LastValueField;

    private byte reg1ValueField;

    private byte reg2AverageField;

    private byte reg2LastValueField;

    private string reg2ValueField;

    private string regDateField;

    private string regLastDateField;

    private string remarkDescriptionField;

    private string remarkIDField;

    private uint serialNoField;

    private ushort sortOrderField;

    private byte terminalIDField;

    private byte userIDField;

    private byte workOrderTypeField;

    /// <remarks/>
    public string AMRAlarms
    {
        get
        {
            return this.aMRAlarmsField;
        }
        set
        {
            this.aMRAlarmsField = value;
        }
    }

    /// <remarks/>
    public string Active
    {
        get
        {
            return this.activeField;
        }
        set
        {
            this.activeField = value;
        }
    }

    /// <remarks/>
    public string Address
    {
        get
        {
            return this.addressField;
        }
        set
        {
            this.addressField = value;
        }
    }

    /// <remarks/>
    public uint Barcode
    {
        get
        {
            return this.barcodeField;
        }
        set
        {
            this.barcodeField = value;
        }
    }

    /// <remarks/>
    public byte BillingID
    {
        get
        {
            return this.billingIDField;
        }
        set
        {
            this.billingIDField = value;
        }
    }

    /// <remarks/>
    public string Combined
    {
        get
        {
            return this.combinedField;
        }
        set
        {
            this.combinedField = value;
        }
    }

    /// <remarks/>
    public string CoronisAddress
    {
        get
        {
            return this.coronisAddressField;
        }
        set
        {
            this.coronisAddressField = value;
        }
    }

    /// <remarks/>
    public string CoronisInput
    {
        get
        {
            return this.coronisInputField;
        }
        set
        {
            this.coronisInputField = value;
        }
    }

    /// <remarks/>
    public string CoronisInput1
    {
        get
        {
            return this.coronisInput1Field;
        }
        set
        {
            this.coronisInput1Field = value;
        }
    }

    /// <remarks/>
    public string CoronisType
    {
        get
        {
            return this.coronisTypeField;
        }
        set
        {
            this.coronisTypeField = value;
        }
    }

    /// <remarks/>
    public string CoronisUnit
    {
        get
        {
            return this.coronisUnitField;
        }
        set
        {
            this.coronisUnitField = value;
        }
    }

    /// <remarks/>
    public string CoronisUnit1
    {
        get
        {
            return this.coronisUnit1Field;
        }
        set
        {
            this.coronisUnit1Field = value;
        }
    }

    /// <remarks/>
    public string CustomerEmail
    {
        get
        {
            return this.customerEmailField;
        }
        set
        {
            this.customerEmailField = value;
        }
    }

    /// <remarks/>
    public string CustomerNumber
    {
        get
        {
            return this.customerNumberField;
        }
        set
        {
            this.customerNumberField = value;
        }
    }

    /// <remarks/>
    public byte Dimension
    {
        get
        {
            return this.dimensionField;
        }
        set
        {
            this.dimensionField = value;
        }
    }

    /// <remarks/>
    public string DistrictID
    {
        get
        {
            return this.districtIDField;
        }
        set
        {
            this.districtIDField = value;
        }
    }

    /// <remarks/>
    public decimal GPSLat
    {
        get
        {
            return this.gPSLatField;
        }
        set
        {
            this.gPSLatField = value;
        }
    }

    /// <remarks/>
    public string GPSLatNew
    {
        get
        {
            return this.gPSLatNewField;
        }
        set
        {
            this.gPSLatNewField = value;
        }
    }

    /// <remarks/>
    public decimal GPSLon
    {
        get
        {
            return this.gPSLonField;
        }
        set
        {
            this.gPSLonField = value;
        }
    }

    /// <remarks/>
    public string GPSLonNew
    {
        get
        {
            return this.gPSLonNewField;
        }
        set
        {
            this.gPSLonNewField = value;
        }
    }

    /// <remarks/>
    public string Images
    {
        get
        {
            return this.imagesField;
        }
        set
        {
            this.imagesField = value;
        }
    }

    /// <remarks/>
    public string IsChecked
    {
        get
        {
            return this.isCheckedField;
        }
        set
        {
            this.isCheckedField = value;
        }
    }

    /// <remarks/>
    public string LastMeterChangeDate
    {
        get
        {
            return this.lastMeterChangeDateField;
        }
        set
        {
            this.lastMeterChangeDateField = value;
        }
    }

    /// <remarks/>
    public string Location
    {
        get
        {
            return this.locationField;
        }
        set
        {
            this.locationField = value;
        }
    }

    /// <remarks/>
    public string MCAdditionalData
    {
        get
        {
            return this.mCAdditionalDataField;
        }
        set
        {
            this.mCAdditionalDataField = value;
        }
    }

    /// <remarks/>
    public string MCBackflowProtectionID
    {
        get
        {
            return this.mCBackflowProtectionIDField;
        }
        set
        {
            this.mCBackflowProtectionIDField = value;
        }
    }

    /// <remarks/>
    public string MCCoronisAddress
    {
        get
        {
            return this.mCCoronisAddressField;
        }
        set
        {
            this.mCCoronisAddressField = value;
        }
    }

    /// <remarks/>
    public string MCCoronisInput1
    {
        get
        {
            return this.mCCoronisInput1Field;
        }
        set
        {
            this.mCCoronisInput1Field = value;
        }
    }

    /// <remarks/>
    public string MCCoronisInput2
    {
        get
        {
            return this.mCCoronisInput2Field;
        }
        set
        {
            this.mCCoronisInput2Field = value;
        }
    }

    /// <remarks/>
    public string MCCoronisType
    {
        get
        {
            return this.mCCoronisTypeField;
        }
        set
        {
            this.mCCoronisTypeField = value;
        }
    }

    /// <remarks/>
    public string MCCoronisUnit1
    {
        get
        {
            return this.mCCoronisUnit1Field;
        }
        set
        {
            this.mCCoronisUnit1Field = value;
        }
    }

    /// <remarks/>
    public string MCCoronisUnit2
    {
        get
        {
            return this.mCCoronisUnit2Field;
        }
        set
        {
            this.mCCoronisUnit2Field = value;
        }
    }

    /// <remarks/>
    public string MCDate
    {
        get
        {
            return this.mCDateField;
        }
        set
        {
            this.mCDateField = value;
        }
    }

    /// <remarks/>
    public string MCDiameter
    {
        get
        {
            return this.mCDiameterField;
        }
        set
        {
            this.mCDiameterField = value;
        }
    }

    /// <remarks/>
    public string MCManufacturerID
    {
        get
        {
            return this.mCManufacturerIDField;
        }
        set
        {
            this.mCManufacturerIDField = value;
        }
    }

    /// <remarks/>
    public string MCMeterChanged
    {
        get
        {
            return this.mCMeterChangedField;
        }
        set
        {
            this.mCMeterChangedField = value;
        }
    }

    /// <remarks/>
    public string MCMeterStamp
    {
        get
        {
            return this.mCMeterStampField;
        }
        set
        {
            this.mCMeterStampField = value;
        }
    }

    /// <remarks/>
    public string MCMeterTypeID
    {
        get
        {
            return this.mCMeterTypeIDField;
        }
        set
        {
            this.mCMeterTypeIDField = value;
        }
    }

    /// <remarks/>
    public string MCNewMeterReg1
    {
        get
        {
            return this.mCNewMeterReg1Field;
        }
        set
        {
            this.mCNewMeterReg1Field = value;
        }
    }

    /// <remarks/>
    public string MCNewMeterReg2
    {
        get
        {
            return this.mCNewMeterReg2Field;
        }
        set
        {
            this.mCNewMeterReg2Field = value;
        }
    }

    /// <remarks/>
    public string MCNote
    {
        get
        {
            return this.mCNoteField;
        }
        set
        {
            this.mCNoteField = value;
        }
    }

    /// <remarks/>
    public string MCOldMeterReg1
    {
        get
        {
            return this.mCOldMeterReg1Field;
        }
        set
        {
            this.mCOldMeterReg1Field = value;
        }
    }

    /// <remarks/>
    public string MCOldMeterReg2
    {
        get
        {
            return this.mCOldMeterReg2Field;
        }
        set
        {
            this.mCOldMeterReg2Field = value;
        }
    }

    /// <remarks/>
    public string MCRFID
    {
        get
        {
            return this.mCRFIDField;
        }
        set
        {
            this.mCRFIDField = value;
        }
    }

    /// <remarks/>
    public string MCRFIDType
    {
        get
        {
            return this.mCRFIDTypeField;
        }
        set
        {
            this.mCRFIDTypeField = value;
        }
    }

    /// <remarks/>
    public string MCRFUnitChanged
    {
        get
        {
            return this.mCRFUnitChangedField;
        }
        set
        {
            this.mCRFUnitChangedField = value;
        }
    }

    /// <remarks/>
    public string MCRemarkDescription
    {
        get
        {
            return this.mCRemarkDescriptionField;
        }
        set
        {
            this.mCRemarkDescriptionField = value;
        }
    }

    /// <remarks/>
    public string MCRemarkID
    {
        get
        {
            return this.mCRemarkIDField;
        }
        set
        {
            this.mCRemarkIDField = value;
        }
    }

    /// <remarks/>
    public string MCSerialNo
    {
        get
        {
            return this.mCSerialNoField;
        }
        set
        {
            this.mCSerialNoField = value;
        }
    }

    /// <remarks/>
    public string MCSignature
    {
        get
        {
            return this.mCSignatureField;
        }
        set
        {
            this.mCSignatureField = value;
        }
    }

    /// <remarks/>
    public string MCSignatureDate
    {
        get
        {
            return this.mCSignatureDateField;
        }
        set
        {
            this.mCSignatureDateField = value;
        }
    }

    /// <remarks/>
    public string MCUserID
    {
        get
        {
            return this.mCUserIDField;
        }
        set
        {
            this.mCUserIDField = value;
        }
    }

    /// <remarks/>
    public string MP
    {
        get
        {
            return this.mpField;
        }
        set
        {
            this.mpField = value;
        }
    }

    /// <remarks/>
    public byte MeterType
    {
        get
        {
            return this.meterTypeField;
        }
        set
        {
            this.meterTypeField = value;
        }
    }

    /// <remarks/>
    public string NewBarcode
    {
        get
        {
            return this.newBarcodeField;
        }
        set
        {
            this.newBarcodeField = value;
        }
    }

    /// <remarks/>
    public string Note
    {
        get
        {
            return this.noteField;
        }
        set
        {
            this.noteField = value;
        }
    }

    /// <remarks/>
    public string Owner
    {
        get
        {
            return this.ownerField;
        }
        set
        {
            this.ownerField = value;
        }
    }

    /// <remarks/>
    public byte Period
    {
        get
        {
            return this.periodField;
        }
        set
        {
            this.periodField = value;
        }
    }

    /// <remarks/>
    public uint RFID
    {
        get
        {
            return this.rFIDField;
        }
        set
        {
            this.rFIDField = value;
        }
    }

    /// <remarks/>
    public string RFIDKey
    {
        get
        {
            return this.rFIDKeyField;
        }
        set
        {
            this.rFIDKeyField = value;
        }
    }

    /// <remarks/>
    public string RFIDType
    {
        get
        {
            return this.rFIDTypeField;
        }
        set
        {
            this.rFIDTypeField = value;
        }
    }

    /// <remarks/>
    public byte ReadingType
    {
        get
        {
            return this.readingTypeField;
        }
        set
        {
            this.readingTypeField = value;
        }
    }

    /// <remarks/>
    public byte Reg1Average
    {
        get
        {
            return this.reg1AverageField;
        }
        set
        {
            this.reg1AverageField = value;
        }
    }

    /// <remarks/>
    public decimal Reg1LastValue
    {
        get
        {
            return this.reg1LastValueField;
        }
        set
        {
            this.reg1LastValueField = value;
        }
    }

    /// <remarks/>
    public byte Reg1Value
    {
        get
        {
            return this.reg1ValueField;
        }
        set
        {
            this.reg1ValueField = value;
        }
    }

    /// <remarks/>
    public byte Reg2Average
    {
        get
        {
            return this.reg2AverageField;
        }
        set
        {
            this.reg2AverageField = value;
        }
    }

    /// <remarks/>
    public byte Reg2LastValue
    {
        get
        {
            return this.reg2LastValueField;
        }
        set
        {
            this.reg2LastValueField = value;
        }
    }

    /// <remarks/>
    public string Reg2Value
    {
        get
        {
            return this.reg2ValueField;
        }
        set
        {
            this.reg2ValueField = value;
        }
    }

    /// <remarks/>
    public string RegDate
    {
        get
        {
            return this.regDateField;
        }
        set
        {
            this.regDateField = value;
        }
    }

    /// <remarks/>
    public string RegLastDate
    {
        get
        {
            return this.regLastDateField;
        }
        set
        {
            this.regLastDateField = value;
        }
    }

    /// <remarks/>
    public string RemarkDescription
    {
        get
        {
            return this.remarkDescriptionField;
        }
        set
        {
            this.remarkDescriptionField = value;
        }
    }

    /// <remarks/>
    public string RemarkID
    {
        get
        {
            return this.remarkIDField;
        }
        set
        {
            this.remarkIDField = value;
        }
    }

    /// <remarks/>
    public uint SerialNo
    {
        get
        {
            return this.serialNoField;
        }
        set
        {
            this.serialNoField = value;
        }
    }

    /// <remarks/>
    public ushort SortOrder
    {
        get
        {
            return this.sortOrderField;
        }
        set
        {
            this.sortOrderField = value;
        }
    }

    /// <remarks/>
    public byte TerminalID
    {
        get
        {
            return this.terminalIDField;
        }
        set
        {
            this.terminalIDField = value;
        }
    }

    /// <remarks/>
    public byte UserID
    {
        get
        {
            return this.userIDField;
        }
        set
        {
            this.userIDField = value;
        }
    }

    /// <remarks/>
    public byte WorkOrderType
    {
        get
        {
            return this.workOrderTypeField;
        }
        set
        {
            this.workOrderTypeField = value;
        }
    }
}

