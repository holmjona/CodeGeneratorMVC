using System;
using System.ComponentModel;

[Serializable()]
public class MasterPageContent : INotifyPropertyChanged {
    // Private _name As String
    // Private _ContainsLiteralText As Boolean
    // Private _ContainsLabelText As Boolean
    // Private _IsBody As Boolean


    // Public Property Name() As String
    // Get
    // Return _name
    // End Get
    // Set(ByVal value As String)
    // _name = value
    // NotifyPropertyChanged("Name")
    // End Set
    // End Property
    // Public Property ContainsLiteralText() As Boolean
    // Get
    // Return _ContainsLiteralText
    // End Get
    // Set(ByVal value As Boolean)
    // _ContainsLiteralText = value
    // NotifyPropertyChanged("ContainsLiteralText")
    // End Set
    // End Property
    // Public Property ContainsLabelText() As Boolean
    // Get
    // Return _ContainsLabelText
    // End Get
    // Set(ByVal value As Boolean)
    // _ContainsLabelText = value
    // NotifyPropertyChanged("ContainsLabelText")
    // End Set
    // End Property
    // Public Property IsBody() As Boolean
    // Get
    // Return _IsBody
    // End Get
    // Set(ByVal value As Boolean)
    // _IsBody = value
    // NotifyPropertyChanged("IsBody")
    // End Set
    // End Property

    public event PropertyChangedEventHandler PropertyChanged;

    public delegate void PropertyChangedEventHandler(object sender, System.ComponentModel.PropertyChangedEventArgs e);

    private void NotifyPropertyChanged(string name) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
