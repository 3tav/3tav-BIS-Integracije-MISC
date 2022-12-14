﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

// 
// This source code was auto-generated by xsd, Version=4.0.30319.33440.
// 

/*
/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class MessageImprint {
    
    private string digestAlgorithmField;
    
    private byte[] digestValueField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="anyURI")]
    public string DigestAlgorithm {
        get {
            return this.digestAlgorithmField;
        }
        set {
            this.digestAlgorithmField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
    public byte[] DigestValue {
        get {
            return this.digestValueField;
        }
        set {
            this.digestValueField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class ArchiveData {
    
    private DataIdentification dataIdentificationField;
    
    private Data dataField;
    
    private MetaData metaDataField;
    
    /// <remarks/>
    public DataIdentification DataIdentification {
        get {
            return this.dataIdentificationField;
        }
        set {
            this.dataIdentificationField = value;
        }
    }
    
    /// <remarks/>
    public Data Data {
        get {
            return this.dataField;
        }
        set {
            this.dataField = value;
        }
    }
    
    /// <remarks/>
    public MetaData MetaData {
        get {
            return this.metaDataField;
        }
        set {
            this.metaDataField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class DataIdentification {
    
    private string externalIDField;
    
    /// <remarks/>
    public string ExternalID {
        get {
            return this.externalIDField;
        }
        set {
            this.externalIDField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class Data {
    
    private byte[] rawDataField;
    
    private MessageImprint[] messageImprintField;
    
    private byte[] dataReferenceField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
    public byte[] RawData {
        get {
            return this.rawDataField;
        }
        set {
            this.rawDataField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("MessageImprint")]
    public MessageImprint[] MessageImprint {
        get {
            return this.messageImprintField;
        }
        set {
            this.messageImprintField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
    public byte[] DataReference {
        get {
            return this.dataReferenceField;
        }
        set {
            this.dataReferenceField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class MetaData {
    
    private string titleField;
    
    private System.DateTime creationTimeField;
    
    private string creationLocationField;
    
    private string fileNameField;
    
    private string mIMETypeField;
    
    private string organizationField;
    
    private Param[] parametersField;
    
    private string classificationNameField;
    
    private string typeField;
    
    private string groupNameField;
    
    /// <remarks/>
    public string Title {
        get {
            return this.titleField;
        }
        set {
            this.titleField = value;
        }
    }
    
    /// <remarks/>
    public System.DateTime CreationTime {
        get {
            return this.creationTimeField;
        }
        set {
            this.creationTimeField = value;
        }
    }
    
    /// <remarks/>
    public string CreationLocation {
        get {
            return this.creationLocationField;
        }
        set {
            this.creationLocationField = value;
        }
    }
    
    /// <remarks/>
    public string FileName {
        get {
            return this.fileNameField;
        }
        set {
            this.fileNameField = value;
        }
    }
    
    /// <remarks/>
    public string MIMEType {
        get {
            return this.mIMETypeField;
        }
        set {
            this.mIMETypeField = value;
        }
    }
    
    /// <remarks/>
    public string Organization {
        get {
            return this.organizationField;
        }
        set {
            this.organizationField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Param", IsNullable=false)]
    public Param[] Parameters {
        get {
            return this.parametersField;
        }
        set {
            this.parametersField = value;
        }
    }
    
    /// <remarks/>
    public string ClassificationName {
        get {
            return this.classificationNameField;
        }
        set {
            this.classificationNameField = value;
        }
    }
    
    /// <remarks/>
    public string Type {
        get {
            return this.typeField;
        }
        set {
            this.typeField = value;
        }
    }
    
    /// <remarks/>
    public string GroupName {
        get {
            return this.groupNameField;
        }
        set {
            this.groupNameField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class Param {
    
    private string parameterNameField;
    
    private string parameterValueField;
    
    private string staticParamField;
    
    /// <remarks/>
    public string ParameterName {
        get {
            return this.parameterNameField;
        }
        set {
            this.parameterNameField = value;
        }
    }
    
    /// <remarks/>
    public string ParameterValue {
        get {
            return this.parameterValueField;
        }
        set {
            this.parameterValueField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string StaticParam {
        get {
            return this.staticParamField;
        }
        set {
            this.staticParamField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class ArchiveDataDraft {
    
    private int draftIDField;
    
    private bool draftIDFieldSpecified;
    
    private string userCreatedField;
    
    private string userCreatedDescField;
    
    private string userForField;
    
    private string userForDescField;
    
    private string groupForField;
    
    private string groupForDescField;
    
    private string folderNameField;
    
    private string folderDescField;
    
    private int docSizeField;
    
    private bool docSizeFieldSpecified;
    
    private ArchiveData archiveDataField;
    
    /// <remarks/>
    public int DraftID {
        get {
            return this.draftIDField;
        }
        set {
            this.draftIDField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool DraftIDSpecified {
        get {
            return this.draftIDFieldSpecified;
        }
        set {
            this.draftIDFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    public string UserCreated {
        get {
            return this.userCreatedField;
        }
        set {
            this.userCreatedField = value;
        }
    }
    
    /// <remarks/>
    public string UserCreatedDesc {
        get {
            return this.userCreatedDescField;
        }
        set {
            this.userCreatedDescField = value;
        }
    }
    
    /// <remarks/>
    public string UserFor {
        get {
            return this.userForField;
        }
        set {
            this.userForField = value;
        }
    }
    
    /// <remarks/>
    public string UserForDesc {
        get {
            return this.userForDescField;
        }
        set {
            this.userForDescField = value;
        }
    }
    
    /// <remarks/>
    public string GroupFor {
        get {
            return this.groupForField;
        }
        set {
            this.groupForField = value;
        }
    }
    
    /// <remarks/>
    public string GroupForDesc {
        get {
            return this.groupForDescField;
        }
        set {
            this.groupForDescField = value;
        }
    }
    
    /// <remarks/>
    public string FolderName {
        get {
            return this.folderNameField;
        }
        set {
            this.folderNameField = value;
        }
    }
    
    /// <remarks/>
    public string FolderDesc {
        get {
            return this.folderDescField;
        }
        set {
            this.folderDescField = value;
        }
    }
    
    /// <remarks/>
    public int DocSize {
        get {
            return this.docSizeField;
        }
        set {
            this.docSizeField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool DocSizeSpecified {
        get {
            return this.docSizeFieldSpecified;
        }
        set {
            this.docSizeFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    public ArchiveData ArchiveData {
        get {
            return this.archiveDataField;
        }
        set {
            this.archiveDataField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class ArchiveDataVer {
    
    private string internalIDField;
    
    private string verDescriptionField;
    
    private ArchiveData archiveDataField;
    
    /// <remarks/>
    public string InternalID {
        get {
            return this.internalIDField;
        }
        set {
            this.internalIDField = value;
        }
    }
    
    /// <remarks/>
    public string VerDescription {
        get {
            return this.verDescriptionField;
        }
        set {
            this.verDescriptionField = value;
        }
    }
    
    /// <remarks/>
    public ArchiveData ArchiveData {
        get {
            return this.archiveDataField;
        }
        set {
            this.archiveDataField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class VerifyData {
    
    private DataIdentification_1 dataIdentification_1Field;
    
    private Data dataField;
    
    /// <remarks/>
    public DataIdentification_1 DataIdentification_1 {
        get {
            return this.dataIdentification_1Field;
        }
        set {
            this.dataIdentification_1Field = value;
        }
    }
    
    /// <remarks/>
    public Data Data {
        get {
            return this.dataField;
        }
        set {
            this.dataField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class DataIdentification_1 {
    
    private string internalIDField;
    
    private string externalIDField;
    
    /// <remarks/>
    public string InternalID {
        get {
            return this.internalIDField;
        }
        set {
            this.internalIDField = value;
        }
    }
    
    /// <remarks/>
    public string ExternalID {
        get {
            return this.externalIDField;
        }
        set {
            this.externalIDField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class ServicePolicyInformation {
    
    private string policyIdentifierField;
    
    /// <remarks/>
    public string PolicyIdentifier {
        get {
            return this.policyIdentifierField;
        }
        set {
            this.policyIdentifierField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class ExportData {
    
    private DataIdentification_1 dataIdentification_1Field;
    
    private Data dataField;
    
    /// <remarks/>
    public DataIdentification_1 DataIdentification_1 {
        get {
            return this.dataIdentification_1Field;
        }
        set {
            this.dataIdentification_1Field = value;
        }
    }
    
    /// <remarks/>
    public Data Data {
        get {
            return this.dataField;
        }
        set {
            this.dataField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class LTAP_Request {
    
    private RequestInformation requestInformationField;
    
    private ArchiveData archiveDataField;
    
    private StatusData statusDataField;
    
    private VerifyData verifyDataField;
    
    private ExportData exportDataField;
    
    private DeleteData deleteDataField;
    
    /// <remarks/>
    public RequestInformation RequestInformation {
        get {
            return this.requestInformationField;
        }
        set {
            this.requestInformationField = value;
        }
    }
    
    /// <remarks/>
    public ArchiveData ArchiveData {
        get {
            return this.archiveDataField;
        }
        set {
            this.archiveDataField = value;
        }
    }
    
    /// <remarks/>
    public StatusData StatusData {
        get {
            return this.statusDataField;
        }
        set {
            this.statusDataField = value;
        }
    }
    
    /// <remarks/>
    public VerifyData VerifyData {
        get {
            return this.verifyDataField;
        }
        set {
            this.verifyDataField = value;
        }
    }
    
    /// <remarks/>
    public ExportData ExportData {
        get {
            return this.exportDataField;
        }
        set {
            this.exportDataField = value;
        }
    }
    
    /// <remarks/>
    public DeleteData DeleteData {
        get {
            return this.deleteDataField;
        }
        set {
            this.deleteDataField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class RequestInformation {
    
    private int versionField;
    
    private string serviceTypeField;
    
    private string nonceField;
    
    private long serialNumberField;
    
    private bool serialNumberFieldSpecified;
    
    private string requestTimeField;
    
    private string requestEntityIdentifierField;
    
    private string serverEntityIdentifierField;
    
    private ServicePolicyInformation servicePolicyInformationField;
    
    private ServiceParameter[] serviceConfigurationField;
    
    /// <remarks/>
    public int Version {
        get {
            return this.versionField;
        }
        set {
            this.versionField = value;
        }
    }
    
    /// <remarks/>
    public string ServiceType {
        get {
            return this.serviceTypeField;
        }
        set {
            this.serviceTypeField = value;
        }
    }
    
    /// <remarks/>
    public string Nonce {
        get {
            return this.nonceField;
        }
        set {
            this.nonceField = value;
        }
    }
    
    /// <remarks/>
    public long SerialNumber {
        get {
            return this.serialNumberField;
        }
        set {
            this.serialNumberField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool SerialNumberSpecified {
        get {
            return this.serialNumberFieldSpecified;
        }
        set {
            this.serialNumberFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    public string RequestTime {
        get {
            return this.requestTimeField;
        }
        set {
            this.requestTimeField = value;
        }
    }
    
    /// <remarks/>
    public string RequestEntityIdentifier {
        get {
            return this.requestEntityIdentifierField;
        }
        set {
            this.requestEntityIdentifierField = value;
        }
    }
    
    /// <remarks/>
    public string ServerEntityIdentifier {
        get {
            return this.serverEntityIdentifierField;
        }
        set {
            this.serverEntityIdentifierField = value;
        }
    }
    
    /// <remarks/>
    public ServicePolicyInformation ServicePolicyInformation {
        get {
            return this.servicePolicyInformationField;
        }
        set {
            this.servicePolicyInformationField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("ServiceParameter", IsNullable=false)]
    public ServiceParameter[] ServiceConfiguration {
        get {
            return this.serviceConfigurationField;
        }
        set {
            this.serviceConfigurationField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class ServiceParameter {
    
    private string parameterNameField;
    
    private string parameterValueField;
    
    /// <remarks/>
    public string ParameterName {
        get {
            return this.parameterNameField;
        }
        set {
            this.parameterNameField = value;
        }
    }
    
    /// <remarks/>
    public string ParameterValue {
        get {
            return this.parameterValueField;
        }
        set {
            this.parameterValueField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class StatusData {
    
    private DataIdentification_1 dataIdentification_1Field;
    
    private Data dataField;
    
    /// <remarks/>
    public DataIdentification_1 DataIdentification_1 {
        get {
            return this.dataIdentification_1Field;
        }
        set {
            this.dataIdentification_1Field = value;
        }
    }
    
    /// <remarks/>
    public Data Data {
        get {
            return this.dataField;
        }
        set {
            this.dataField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class DeleteData {
    
    private DataIdentification_1 dataIdentification_1Field;
    
    private Data dataField;
    
    /// <remarks/>
    public DataIdentification_1 DataIdentification_1 {
        get {
            return this.dataIdentification_1Field;
        }
        set {
            this.dataIdentification_1Field = value;
        }
    }
    
    /// <remarks/>
    public Data Data {
        get {
            return this.dataField;
        }
        set {
            this.dataField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class ServiceConfiguration {
    
    private ServiceParameter[] serviceParameterField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("ServiceParameter")]
    public ServiceParameter[] ServiceParameter {
        get {
            return this.serviceParameterField;
        }
        set {
            this.serviceParameterField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class Parameters {
    
    private Param[] paramField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Param")]
    public Param[] Param {
        get {
            return this.paramField;
        }
        set {
            this.paramField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class LTAPFile {
    
    private string internalIDField;
    
    private byte[] rawDataField;
    
    private MessageImprint messageImprintField;
    
    private string mIMETypeField;
    
    private string fileNameField;
    
    /// <remarks/>
    public string InternalID {
        get {
            return this.internalIDField;
        }
        set {
            this.internalIDField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
    public byte[] RawData {
        get {
            return this.rawDataField;
        }
        set {
            this.rawDataField = value;
        }
    }
    
    /// <remarks/>
    public MessageImprint MessageImprint {
        get {
            return this.messageImprintField;
        }
        set {
            this.messageImprintField = value;
        }
    }
    
    /// <remarks/>
    public string MIMEType {
        get {
            return this.mIMETypeField;
        }
        set {
            this.mIMETypeField = value;
        }
    }
    
    /// <remarks/>
    public string FileName {
        get {
            return this.fileNameField;
        }
        set {
            this.fileNameField = value;
        }
    }
}
*/